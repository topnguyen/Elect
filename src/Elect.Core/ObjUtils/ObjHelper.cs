#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ObjHelper.cs </Name>
//         <Created> 15/03/2018 4:58:17 PM </Created>
//         <Key> a0e84b91-473d-4482-afc2-7c3b639d61eb </Key>
//     </File>
//     <Summary>
//         ObjHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.JsonContractResolver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Elect.Core.ObjUtils
{
    public class ObjHelper
    {
        public static string ToJsonString(object obj)
        {
            if (obj is JObject jObject)
            {
                return jObject.ToString(Constants.Formatting.JsonSerializerSettings.Formatting);
            }
            return JsonConvert.SerializeObject(obj, Constants.Formatting.JsonSerializerSettings);
        }

        public static T Clone<T>(T obj)
        {
            if (obj == null)
            {
                return default;
            }

            var json = JsonConvert.SerializeObject(obj);

            var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }

        public static T ConvertTo<T>(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return default;
                }

                if (obj is T variable)
                {
                    return variable;
                }

                Type t = typeof(T);

                Type u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (u == typeof(string))
                    {
                        return (T)(object)obj.ToString();
                    }

                    return (T)Convert.ChangeType(obj, u);
                }

                if (t == typeof(string))
                {
                    return (T)((object)obj.ToString());
                }

                if (t.IsPrimitive)
                {
                    return (T)Convert.ChangeType(obj.ToString(), t);
                }

                return (T)Convert.ChangeType(obj, t);
            }
            catch
            {
                return default;
            }
        }

        public static T WithoutRefLoop<T>(T obj)
        {
            if (obj == null)
            {
                return default;
            }

            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }

        public static T WithoutVirtualProp<T>(T obj)
        {
            if (obj == null)
            {
                return default;
            }

            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new WithoutVirtualContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None
            });

            var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }
    }
}