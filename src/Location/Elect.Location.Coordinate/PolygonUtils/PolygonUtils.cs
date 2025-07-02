namespace Elect.Location.Coordinate.PolygonUtils
{
    public static class PolygonUtils
    {
        public static bool IsInPolygon(CoordinateModel pointToCheck, List<CoordinateModel> polygon)
        {
            bool inside = false;
            if (polygon.Count < 3)
            {
                return false;
            }
            var oldPoint = polygon.Last();
            foreach (var polygonPoint in polygon)
            {
                if (polygonPoint.Latitude == pointToCheck.Latitude && polygonPoint.Longitude == pointToCheck.Longitude)
                {
                    return true;
                }
                if (polygonPoint.Longitude == oldPoint.Longitude && (pointToCheck.Longitude == oldPoint.Longitude) && oldPoint.Latitude <= pointToCheck.Latitude && pointToCheck.Latitude <= polygonPoint.Latitude)
                {
                    return true;
                }
                if ((polygonPoint.Longitude < pointToCheck.Longitude) && (oldPoint.Longitude >= pointToCheck.Longitude) || (oldPoint.Longitude < pointToCheck.Longitude) && (polygonPoint.Longitude >= pointToCheck.Longitude))
                {
                    if (polygonPoint.Latitude + (pointToCheck.Longitude - polygonPoint.Longitude) / (oldPoint.Longitude - polygonPoint.Longitude) * (oldPoint.Latitude - polygonPoint.Latitude) <= pointToCheck.Latitude)
                    {
                        inside = !inside;
                    }
                }
                oldPoint = polygonPoint;
            }
            return inside;
        }
    }
}
