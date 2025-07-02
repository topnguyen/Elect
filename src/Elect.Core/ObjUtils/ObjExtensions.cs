namespace Elect.Core.ObjUtils
{
    public static class ObjExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return ObjHelper.ToJsonString(obj);
        }
        public static T Clone<T>(this T obj)
        {
            return ObjHelper.Clone(obj);
        }
        public static T ConvertTo<T>(this object obj)
        {
            return ObjHelper.ConvertTo<T>(obj);
        }
        public static T WithoutRefLoop<T>(this T obj)
        {
            return ObjHelper.WithoutRefLoop(obj);
        }
        public static T WithoutVirtualProp<T>(this T obj)
        {
            return ObjHelper.WithoutVirtualProp(obj);
        }
    }
}
