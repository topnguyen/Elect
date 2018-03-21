#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ClusterHelper.cs </Name>
//         <Created> 20/03/2018 11:17:48 PM </Created>
//         <Key> 6b587328-5557-46ae-b349-a1143766488c </Key>
//     </File>
//     <Summary>
//         ClusterHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Elect.Location.Coordinate.DistanceUtils;
using Elect.Location.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Location.Coordinate.ClusterUtils
{
    public class ClusterHelper
    {
        public List<ClusterModel> GetCluster(List<CoordinateModel> coordinates, int totalGroup)
        {
            if (totalGroup <= 1 || totalGroup >= coordinates.Count)
            {
                throw new NotSupportedException($"{nameof(totalGroup)} <= 1 || {nameof(totalGroup)} >= total {nameof(coordinates)}");
            }

            // Step 1: random first center Coordinate

            var centerCoordinates = InitialCenterCoordinates(coordinates, totalGroup);

            bool modified;

            do
            {
                modified = false;

                // Step 2: put Coordinates to group based on closest distance

                foreach (var coordinate in coordinates)
                {
                    var newGroup = GetClosestCoordinateIndex(centerCoordinates, coordinate);

                    if (newGroup == coordinate.GroupNo)
                    {
                        continue;
                    }

                    coordinate.GroupNo = newGroup;

                    modified = true;
                }

                // Step 3: Re-calculate center Coordinate for each group

                if (modified)
                {
                    RecalculateCenterCoordinates(centerCoordinates, coordinates);
                }
            } while (modified);

            var clusters = new List<ClusterModel>(totalGroup);

            for (int i = 0; i < totalGroup; i++)
            {
                var groupNo = i;

                clusters.Add(new ClusterModel
                {
                    CenterCoordinate = centerCoordinates[groupNo],

                    Coordinates = coordinates.Where(p => p.GroupNo == groupNo).ToList()
                });
            }

            return clusters;
        }

        private List<CoordinateModel> InitialCenterCoordinates(IList<CoordinateModel> coordinates, int totalGroup)
        {
            if (totalGroup <= 1 || totalGroup >= coordinates.Count)
            {
                throw new NotSupportedException($"{nameof(totalGroup)} <= 1 || {nameof(totalGroup)} >= total ${nameof(coordinates)}");
            }

            Random random = new Random();

            for (var i = 0; i < totalGroup; i++)
            {
                var j = random.Next(coordinates.Count);

                Swap(coordinates, i, j);
            }

            List<CoordinateModel> centerCoordinates = new List<CoordinateModel>();

            var selectedCoordinates = coordinates.Take(totalGroup).ToList();

            foreach (var selectedCoordinate in selectedCoordinates)
            {
                var centerCoordinate = selectedCoordinate.Clone();

                centerCoordinates.Add(centerCoordinate);
            }

            return centerCoordinates;
        }

        private static void RecalculateCenterCoordinates(IReadOnlyList<CoordinateModel> centerCoordinates, IEnumerable<CoordinateModel> coordinates)
        {
            var totalGroup = centerCoordinates.Count;

            var sumLat = new double[totalGroup];

            var sumLng = new double[totalGroup];

            var count = new int[totalGroup];

            foreach (var coordinate in coordinates)
            {
                sumLng[coordinate.GroupNo] += coordinate.Longitude;

                sumLat[coordinate.GroupNo] += coordinate.Latitude;

                count[coordinate.GroupNo]++;
            }
            for (var i = 0; i < totalGroup; i++)
            {
                centerCoordinates[i].Longitude = sumLng[i] / count[i];

                centerCoordinates[i].Latitude = sumLat[i] / count[i];
            }
        }

        private static int GetClosestCoordinateIndex(IReadOnlyList<CoordinateModel> coordinates, CoordinateModel coordinate)
        {
            var result = 0;

            var minDistance = coordinate.FlatDistanceTo(coordinates[result]);

            for (int i = 1; i < coordinates.Count; i++)
            {
                var distance = coordinate.FlatDistanceTo(coordinates[i]);

                if (distance < minDistance)
                {
                    result = i;
                }
            }

            return result;
        }

        private static void Swap<T>(IList<T> list, int firstIndex, int secondIndex)
        {
            var temp = list[firstIndex];

            list[firstIndex] = list[secondIndex];

            list[secondIndex] = temp;
        }
    }
}