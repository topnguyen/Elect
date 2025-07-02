namespace Elect.Location.Google.Services
{
    /// <summary>
    ///     Combine both A -&gt; Z and Round Trip with optimize by many algorithm and distance,
    ///     duration by Google Matrix
    /// </summary>
    internal class FastestTrip
    {
        #region Property
        public List<CoordinateModel> Coordinates { get; }
        public DistanceDurationMatrixResultModel DistanceDurationMatrix { get; }
        private List<int> BestPath { get; set; }
        private List<int> NextSet { get; set; }
        private double BestTrip { get; set; }
        private const double MaxTripSentry = 2000000000;
        private int[] _currentPath;
        private bool[] _visitTracks;
        private double[] _min;
        private double _minCost;
        private double _currentCost;
        private double _bestCost;
        private readonly TripType _tripType;
        private double[] _costForward;
        private double[] _costBackward;
        private bool _isImproved;
        #endregion Property
        /// <summary>
        ///     Combine both A -&gt; Z and Round Trip with optimize by many algorithm and distance,
        ///     duration by Google Matrix
        /// </summary>
        /// <param name="type">        </param>
        /// <param name="googleApiKey">
        ///     Use for TripType.RoundTrip - Optional, method still work without key but have
        ///     limitation by Google Policy.
        /// </param>
        /// <param name="coordinates"> </param>
        /// <remarks>
        ///     Concorde TSP Solver algorithm combine with Ant colony optimization algorithms to find
        ///     wayCoordinate and best path
        /// </remarks>
        public FastestTrip(TripType type = TripType.AZ, string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates)
        {
            if (coordinates.Any() != true)
            {
                throw new NotSupportedException();
            }
            _tripType = type;
            Coordinates = new List<CoordinateModel>();
            Coordinates.AddRange(coordinates);
            // Get Distance Duration Matrix
            var coordinateModels = Coordinates.Select(x => new CoordinateModel(x.Longitude, x.Latitude)).ToList();
            IElectGoogleClient googleClient = new ElectGoogleClient(_ =>
            {
                _.GoogleApiKey = googleApiKey;
            });
            var getDistanceDurationMatrixTask = googleClient.GetDistanceDurationMatrixAsync(_ =>
            {
                _.OriginalCoordinates = coordinateModels;
                _.DestinationCoordinates = coordinateModels;
            });
            getDistanceDurationMatrixTask.Wait();
            DistanceDurationMatrix = getDistanceDurationMatrixTask.Result;
            // Start Calculate Trip
            CalculateTrip(type);
        }
        public List<CoordinateModel> GetTrip()
        {
            return BestPath.Select((t, i) => new CoordinateModel
            {
                Latitude = Coordinates[t].Latitude,
                Longitude = Coordinates[t].Longitude,
                GroupNo = Coordinates[t].GroupNo,
                SequenceNo = i + 1
            }).ToList();
        }
        public double GetTotalDurationInSecond()
        {
            double totalDuration = 0;
            for (int i = 0; i < BestPath.Count - 1; i++)
            {
                totalDuration += GetDuration(BestPath[i], BestPath[i + 1]);
            }
            return totalDuration;
        }
        public double GetTotalDistanceInMeter()
        {
            double totalDistance = 0;
            for (int i = 0; i < BestPath.Count - 1; i++)
            {
                totalDistance += GetDistance(BestPath[i], BestPath[i + 1]);
            }
            return totalDistance;
        }
        #region Helper
        private double GetDuration(int index1, int index2)
        {
            var duration = DistanceDurationMatrix.DurationMatrix[index1, index2];
            return duration;
        }
        private double GetDistance(int index1, int index2)
        {
            var distance = DistanceDurationMatrix.DistanceMatrix[index1, index2];
            return distance;
        }
        private double GetMinDistance(int index)
        {
            List<double> distances = new List<double>();
            for (int i = 0; i < Coordinates.Count; i++)
            {
                var distance = DistanceDurationMatrix.DistanceMatrix[index, i];
                distances.Add(distance);
            }
            var min = distances.Min();
            return min;
        }
        private void CalculateTrip(TripType type)
        {
            if (Coordinates.Count <= 13)
            {
                CalculateTripBackTrackingImplementation(0, type);
            }
            else if (Coordinates.Count <= 15)
            {
                TspDynamic(type);
            }
            else
            {
                TspAntColonyK2(type);
                TspK3();
            }
        }
        #endregion
        #region TspAntColonyK2
        /// <summary>
        ///     Computes a near-optimal solution to the TSP problem, using Ant Colony Optimization
        ///     and local optimization in the form of k2-opting each candidate route. Run time is
        ///     O(numWaves * numAnts * numActive ^ 2) for ACO and O(numWaves * numAnts * numActive ^
        ///     3) for rewiring? if type is 1, we start at node 0 and end at node numActive-1.
        /// </summary>
        /// <param name="type"></param>
        private void TspAntColonyK2(TripType type)
        {
            BestTrip = MaxTripSentry;
            int numActive = Coordinates.Count;
            _currentPath = new int[numActive];
            _visitTracks = new bool[numActive];
            var currentPath = new int[numActive];
            if (type == TripType.RoundTrip)
            {
                currentPath = new int[numActive + 1];
            }
            var alpha = 0.1; // The importance of the previous trails
            var beta = 2.0; // The importance of the durations
            var rho = 0.1;  // The decay rate of the pheromone trails
            var asymptoteFactor = 0.9; // The sharpness of the reward as the solutions approach the best solution
            double[,] pher = new double[numActive, numActive];
            double[,] nextPher = new double[numActive, numActive];
            double[] prob = new double[numActive];
            var numAnts = 20;
            var numWaves = 20;
            for (var i = 0; i < numActive; ++i)
            {
                for (var j = 0; j < numActive; ++j)
                {
                    pher[i, j] = 1;
                    nextPher[i, j] = 0.0;
                }
            }
            var lastNode = 0;
            const int startNode = 0;
            var numSteps = numActive - 1;
            var numValidDest = numActive;
            if (type == TripType.AZ)
            {
                lastNode = numActive - 1;
                numSteps = numActive - 2;
                numValidDest = numActive - 1;
            }
            for (var wave = 0; wave < numWaves; ++wave)
            {
                for (var ant = 0; ant < numAnts; ++ant)
                {
                    var currentNode = startNode;
                    var currentDistance = 0;
                    for (var i = 0; i < numActive; ++i)
                    {
                        _visitTracks[i] = false;
                    }
                    currentPath[0] = currentNode;
                    for (var step = 0; step < numSteps; ++step)
                    {
                        _visitTracks[currentNode] = true;
                        var cumProb = 0.0;
                        for (var next = 1; next < numValidDest; ++next)
                        {
                            if (!_visitTracks[next])
                            {
                                prob[next] = Math.Pow(pher[currentNode, next], alpha) *
                              Math.Pow(GetDuration(currentNode, next), 0.0 - beta);
                                cumProb += prob[next];
                            }
                        }
                        var guess = new Random().NextDouble() * cumProb;
                        var nextI = -1;
                        for (var next = 1; next < numValidDest; ++next)
                        {
                            if (!_visitTracks[next])
                            {
                                nextI = next;
                                guess -= prob[next];
                                if (guess < 0)
                                {
                                    nextI = next;
                                    break;
                                }
                            }
                        }
                        currentDistance += (int)GetDuration(currentNode, nextI);
                        currentPath[step + 1] = nextI;
                        currentNode = nextI;
                    }
                    currentPath[numSteps + 1] = lastNode;
                    currentDistance += (int)GetDuration(currentNode, lastNode);
                    // k2-rewire:
                    var lastStep = numActive;
                    if (type == TripType.AZ)
                    {
                        lastStep = numActive - 1;
                    }
                    var changed = true;
                    var m = 0;
                    while (changed)
                    {
                        changed = false;
                        for (; m < lastStep - 2 && !changed; ++m)
                        {
                            var cost = GetDuration(currentPath[m + 1], currentPath[m + 2]);
                            var revCost = GetDuration(currentPath[m + 2], currentPath[m + 1]);
                            var iCost = GetDuration(currentPath[m], currentPath[m + 1]);
                            for (var j = m + 2; j < lastStep && !changed; ++j)
                            {
                                var nowCost = cost + iCost + GetDuration(currentPath[j], currentPath[j + 1]);
                                var newCost = revCost + GetDuration(currentPath[m], currentPath[j]) + GetDuration(currentPath[m + 1], currentPath[j + 1]);
                                if (nowCost > newCost)
                                {
                                    currentDistance += (int)(newCost - nowCost);
                                    // Reverse the detached road segment.
                                    for (var k = 0; k < Math.Floor((double)(j - m) / 2); ++k)
                                    {
                                        double tmp = currentPath[m + 1 + k];
                                        currentPath[m + 1 + k] = currentPath[j - k];
                                        currentPath[j - k] = (int)tmp;
                                    }
                                    changed = true;
                                    --m;
                                }
                                cost += GetDuration(currentPath[j], currentPath[j + 1]);
                                revCost += GetDuration(currentPath[j + 1], currentPath[j]);
                            }
                        }
                    }
                    if (currentDistance < BestTrip)
                    {
                        BestPath = currentPath.ToList();
                        BestTrip = currentDistance;
                    }
                    for (var i = 0; i <= numSteps; ++i)
                    {
                        nextPher[currentPath[i], currentPath[i + 1]] += (BestTrip - asymptoteFactor * BestTrip) / (numAnts * (currentDistance - asymptoteFactor * BestTrip));
                    }
                }
                for (var i = 0; i < numActive; ++i)
                {
                    for (var j = 0; j < numActive; ++j)
                    {
                        pher[i, j] = pher[i, j] * (1.0 - rho) + rho * nextPher[i, j];
                        nextPher[i, j] = 0.0;
                    }
                }
            }
        }
        #endregion
        #region TspK3
        /// <summary>
        ///     Uses the 3-opt algorithm to find a good solution to the TSP. 
        /// </summary>
        private void TspK3()
        {
            _isImproved = false;
            _currentPath = new int[BestPath.Count];
            for (var i = 0; i < BestPath.Count; ++i)
            {
                _currentPath[i] = BestPath[i];
            }
            UpdateCosts();
            _isImproved = true;
            while (_isImproved)
            {
                _isImproved = false;
                for (var i = 0; i < _currentPath.Length - 3; ++i)
                {
                    for (var j = i + 1; j < _currentPath.Length - 2; ++j)
                    {
                        for (var k = j + 1; k < _currentPath.Length - 1; ++k)
                        {
                            if (CostPerm(i, i + 1, j, k, j + 1, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, i + 1, j, k, j + 1, k + 1);
                            }
                            if (CostPerm(i, j, i + 1, j + 1, k, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, j, i + 1, j + 1, k, k + 1);
                            }
                            if (CostPerm(i, j, i + 1, k, j + 1, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, j, i + 1, k, j + 1, k + 1);
                            }
                            if (CostPerm(i, j + 1, k, i + 1, j, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, j + 1, k, i + 1, j, k + 1);
                            }
                            if (CostPerm(i, j + 1, k, j, i + 1, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, j + 1, k, j, i + 1, k + 1);
                            }
                            if (CostPerm(i, k, j + 1, i + 1, j, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, k, j + 1, i + 1, j, k + 1);
                            }
                            if (CostPerm(i, k, j + 1, j, i + 1, k + 1) < BestTrip)
                            {
                                UpdatePerm(i, k, j + 1, j, i + 1, k + 1);
                            }
                        }
                    }
                }
            }
            for (var i = 0; i < BestPath.Count; ++i) BestPath[i] = _currentPath[i];
        }
        private void UpdatePerm(int a, int b, int c, int d, int e, int f)
        {
            _isImproved = true;
            var nextPath = new int[_currentPath.Length];
            for (var i = 0; i < _currentPath.Length; ++i)
            {
                nextPath[i] = _currentPath[i];
            }
            var offset = a + 1;
            nextPath[offset++] = _currentPath[b];
            if (b < c)
            {
                for (var i = b + 1; i <= c; ++i)
                {
                    nextPath[offset++] = _currentPath[i];
                }
            }
            else
            {
                for (var i = b - 1; i >= c; --i)
                {
                    nextPath[offset++] = _currentPath[i];
                }
            }
            nextPath[offset++] = _currentPath[d];
            if (d < e)
            {
                for (var i = d + 1; i <= e; ++i)
                {
                    nextPath[offset++] = _currentPath[i];
                }
            }
            else
            {
                for (var i = d - 1; i >= e; --i)
                {
                    nextPath[offset++] = _currentPath[i];
                }
            }
            nextPath[offset] = _currentPath[f];
            _currentPath = nextPath;
            UpdateCosts();
        }
        /// <summary>
        ///     Returns the cost of the given 3-opt variation of the current solution. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private double CostPerm(int a, int b, int c, int d, int e, int f)
        {
            var iA = _currentPath[a];
            var iB = _currentPath[b];
            var iC = _currentPath[c];
            var iD = _currentPath[d];
            var iE = _currentPath[e];
            var iF = _currentPath[f];
            var iG = _currentPath.Length - 1;
            var ret = Cost(0, a) + GetDuration(iA, iB) + Cost(b, c) + GetDuration(iC, iD) + Cost(d, e) + GetDuration(iE, iF) + Cost(f, iG);
            return ret;
        }
        /// <summary>
        ///     Returns the cost of moving along the current solution path offset given by a to
        ///     b.Handles moving both forward and backward.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double Cost(int a, int b)
        {
            if (a <= b)
            {
                return _costForward[b] - _costForward[a];
            }
            else
            {
                return _costBackward[b] - _costBackward[a];
            }
        }
        /// <summary>
        ///     Update the data structure necessary for cost(a,b) and costPerm to work efficiently. 
        /// </summary>
        private void UpdateCosts()
        {
            _costForward = new double[_currentPath.Length];
            _costBackward = new double[_currentPath.Length];
            _costForward[0] = 0.0;
            for (var i = 1; i < _currentPath.Length; ++i)
            {
                _costForward[i] = _costForward[i - 1] + GetDuration(_currentPath[i - 1], _currentPath[i]);
            }
            BestTrip = _costForward[_currentPath.Length - 1];
            _costBackward[BestPath.Count - 1] = 0.0;
            for (var i = BestPath.Count - 2; i >= 0; --i)
            {
                _costBackward[i] = _costBackward[i + 1] + GetDuration(_currentPath[i + 1], _currentPath[i]);
            }
        }
        #endregion
        #region TripBackTracking
        private void CalculateTripBackTrackingImplementation(int start, TripType type)
        {
            _currentCost = 0;
            _bestCost = MaxTripSentry;
            _currentPath = new int[Coordinates.Count];
            _visitTracks = new bool[Coordinates.Count];
            _min = new double[Coordinates.Count];
            for (int i = 0; i < Coordinates.Count; i++)
            {
                _min[i] = GetMinDistance(i);
            }
            _minCost = _min.Sum();
            if (type == TripType.AZ)
            {
                _minCost -= _min[start];
            }
            _currentPath[0] = start;
            _visitTracks[start] = true;
            BackTrackingVisit(1);
        }
        private void BackTrackingVisit(int step)
        {
            if (step == Coordinates.Count)
            {
                if (_tripType == TripType.RoundTrip)
                {
                    if (_currentCost + GetDistance(_currentPath[step - 1], _currentPath[0]) < _bestCost)
                    {
                        _bestCost = _currentCost;
                        BestPath = new List<int>(_currentPath)
                        {
                            _currentPath[0]
                        };
                    }
                }
                else
                {
                    if (_currentCost < _bestCost)
                    {
                        _bestCost = _currentCost;
                        BestPath = new List<int>(_currentPath);
                    }
                }
            }
            for (int i = 0; i < Coordinates.Count; i++)
            {
                if (!_visitTracks[i])
                {
                    var stepCost = GetDistance(_currentPath[step - 1], i);
                    _visitTracks[i] = true;
                    _currentPath[step] = i;
                    _currentCost += stepCost;
                    _minCost -= _min[_currentPath[step - 1]];
                    if (_currentCost + _minCost < _bestCost)
                    {
                        BackTrackingVisit(step + 1);
                    }
                    _minCost += _min[_currentPath[step - 1]];
                    _currentCost -= stepCost;
                    _visitTracks[i] = false;
                }
            }
        }
        #endregion
        #region TspDynamic
        /// <summary>
        ///     Ant colony optimization algorithms and Solves the TSP problem to optimality. Memory
        ///     requirement is O(numActive * 2^numActive)
        /// </summary>
        private void TspDynamic(TripType type)
        {
            BestPath = new List<int>();
            NextSet = new List<int>();
            BestTrip = MaxTripSentry;
            int numActive = Coordinates.Count;
            var numCombos = 1 << Coordinates.Count;
            var c = new List<List<double>>();
            var parent = new List<List<int>>();
            for (var i = 0; i < numCombos; i++)
            {
                c.Add(new List<double>());
                parent.Add(new List<int>());
                for (var j = 0; j < numActive; ++j)
                {
                    c[i].Add(0.0);
                    parent[i].Add(0);
                }
            }
            int index;
            for (var k = 1; k < numActive; ++k)
            {
                index = 1 + (1 << k);
                c[index][k] = GetDistance(0, k);
            }
            for (var s = 3; s <= numActive; ++s)
            {
                for (var i = 0; i < numActive; ++i)
                {
                    NextSet.Add(0);
                }
                index = NextSetOf(s);
                while (index >= 0)
                {
                    for (var k = 1; k < numActive; ++k)
                    {
                        if (NextSet[k] != 0)
                        {
                            var previousIndex = index - (1 << k);
                            c[index][k] = MaxTripSentry;
                            for (var m = 1; m < numActive; ++m)
                            {
                                if (NextSet[m] != 0 && m != k)
                                {
                                    if (c[previousIndex][m] + GetDistance(m, k) < c[index][k])
                                    {
                                        c[index][k] = c[previousIndex][m] + GetDistance(m, k);
                                        parent[index][k] = m;
                                    }
                                }
                            }
                        }
                    }
                    index = NextSetOf(s);
                }
            }
            for (var i = 0; i < numActive; ++i)
            {
                BestPath.Add(0);
            }
            index = (1 << numActive) - 1;
            int currentNode;
            // Case RoundTrip (A -> A), A -> Z START
            if (type == TripType.RoundTrip)
            {
                currentNode = -1;
                BestPath.Add(0);
                for (var i = 1; i < numActive; ++i)
                {
                    if (c[index][i] + GetDistance(i, 0) < BestTrip)
                    {
                        BestTrip = c[index][i] + GetDistance(i, 0);
                        currentNode = i;
                    }
                }
                BestPath[numActive - 1] = currentNode;
            }
            else
            {
                currentNode = numActive - 1;
                BestPath[numActive - 1] = numActive - 1;
                BestTrip = c[index][numActive - 1];
            }
            // Case A->A, A->Z END
            for (var i = numActive - 1; i > 0; --i)
            {
                currentNode = parent[index][currentNode];
                index -= (1 << BestPath[i]);
                BestPath[i - 1] = currentNode;
            }
        }
        private int NextSetOf(int num)
        {
            int numActive = Coordinates.Count;
            var count = 0;
            var ret = 0;
            for (var i = 0; i < numActive; ++i)
            {
                count += NextSet[i];
            }
            if (count < num)
            {
                for (var i = 0; i < num; ++i)
                {
                    NextSet[i] = 1;
                }
                for (var i = num; i < numActive; ++i)
                {
                    NextSet[i] = 0;
                }
            }
            else
            {
                // Find first 1
                var firstOne = -1;
                for (var i = 0; i < numActive; ++i)
                {
                    if (NextSet[i] != 0)
                    {
                        firstOne = i;
                        break;
                    }
                }
                // Find first 0 greater than firstOne
                var firstZero = -1;
                for (var i = firstOne + 1; i < numActive; ++i)
                {
                    if (NextSet[i] == 0)
                    {
                        firstZero = i;
                        break;
                    }
                }
                if (firstZero < 0)
                {
                    return -1;
                }
                // Increment the first zero with ones behind it
                NextSet[firstZero] = 1;
                // Set the part behind that one to its lowest possible value
                for (var i = 0; i < firstZero - firstOne - 1; ++i)
                {
                    NextSet[i] = 1;
                }
                for (var i = firstZero - firstOne - 1; i < firstZero; ++i)
                {
                    NextSet[i] = 0;
                }
            }
            // Return the index for this set
            for (var i = 0; i < numActive; ++i)
            {
                ret += (NextSet[i] << i);
            }
            return ret;
        }
        #endregion
    }
}
