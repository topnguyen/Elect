namespace Elect.Data.EF.Utils.DataReaderUtils
{
    public static class DataReaderExtensions
    {
        public static List<T> QueryTo<T>(this DbDataReader dr) where T : class, new()
        {
            if (dr != null && dr.HasRows)
            {
                var entities = new List<T>();
                var props = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var propDict = props.ToDictionary(p => p.Name.ToUpperInvariant(), p => p);
                while (dr.Read())
                {
                    T newObj = new T();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (propDict.ContainsKey(dr.GetName(i).ToUpperInvariant()))
                        {
                            var info = propDict[dr.GetName(i).ToUpperInvariant()];
                            if (info != null && info.CanWrite)
                            {
                                var val = dr.GetValue(i);
                                info.SetValue(newObj, val == DBNull.Value ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObj);
                }
                return entities;
            }
            return null;
        }
    }
}
