namespace Elect.Core.StringUtils
{
    public static class IdHelper
    {
        #region Short Id Generator
        public static string ToShortString(uint id)
        {
            var idBytes = BitConverter.GetBytes(id);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
            }
            var idString = Safe32Encoding.EncodeBytes(idBytes);
            return idString;
        }
        public static uint FromShortString(string idString)
        {
            var idBytes = Safe32Encoding.DecodeBytes(idString);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
            }
            var id = BitConverter.ToUInt32(idBytes, 0);
            return id;
        }
        public static bool TryFromShortString(string idString, out uint id)
        {
            try
            {
                id = FromShortString(idString);
                return true;
            }
            catch
            {
                id = 0;
                return false;
            }
        }
        #endregion Short Id Generator
        #region Long Id Generator
        public static string ToString(ulong id)
        {
            var idBytes = BitConverter.GetBytes(id);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
            }
            var idString = Safe64Encoding.EncodeBytes(idBytes);
            return idString;
        }
        public static ulong FromString(string idString)
        {
            var idBytes = Safe64Encoding.DecodeBytes(idString);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
            }
            var id = BitConverter.ToUInt64(idBytes, 0);
            return id;
        }
        public static bool TryFromString(string idString, out ulong id)
        {
            try
            {
                id = FromString(idString);
                return true;
            }
            catch
            {
                id = 0;
                return false;
            }
        }
        #endregion Long Id Generator
    }
}
