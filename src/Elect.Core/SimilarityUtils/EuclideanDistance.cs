namespace Elect.Core.SimilarityUtils
{
    public class Euclidean
    {
        /// <summary>
        ///     Euclidean the distance
        /// </summary>
        /// <param name="firstArray"></param>
        /// <param name="secondArray"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double Distance(IEnumerable<double> firstArray, IEnumerable<double> secondArray)
        {
            var firstValue = firstArray as double[] ?? new double[]{};
            var secondValue = secondArray as double[] ?? new double[]{};
            // Make sure the dimensions are the same.
            if (firstValue.Count() != secondValue.Count())
            {
                throw new ArgumentOutOfRangeException($"{nameof(firstArray)} do not match length with {nameof(secondArray)}");
            }
            // Iterate through each point and create the vector.
            var vectors = new List<double>();
            for (var i = 0; i < firstValue.Count(); i++)
            {
                vectors.Add(Math.Pow(Math.Abs(firstValue.ElementAt(i) - secondValue.ElementAt(i)), 2));
            }
            // Return the sqrt root of the sum of vectors.
            return Math.Sqrt(vectors.Sum());
        }
    }
}
