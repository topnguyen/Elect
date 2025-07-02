namespace Elect.Core.StringUtils
{
    /// <summary>
    ///     This encoding produces a 'url' safe string from bytes, similar to base64 encoding yet it
    ///     replaces '+' with '-', '/' with '_' and omits padding.
    /// </summary>
    public static class Safe64Encoding
    {
        private const int Min = '-';
        private const int Max = 'z' + 1;
        internal static readonly byte[] ChTable64;
        internal static readonly byte[] ChValue64;
        static Safe64Encoding()
        {
            ChTable64 = Encoding.ASCII.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_");
            ChValue64 = new byte[Max - Min];
            for (byte i = 0; i < 64; i++)
                ChValue64[ChTable64[i] - Min] = i;
        }
        /// <summary>
        ///     Returns a encoded string of ascii characters that are URI safe 
        /// </summary>
        public static string EncodeBytes(byte[] input)
        {
            return EncodeBytes(input, 0, input?.Length ?? 0);
        }
        /// <summary>
        ///     Returns a encoded string of ascii characters that are URI safe 
        /// </summary>
        public static string EncodeBytes(byte[] input, int start, int length)
        {
            var output = new byte[(int)Math.Ceiling((length << 3) / 6d)];
            var len = EncodeBytes(input, start, length, output, 0);
            return Encoding.ASCII.GetString(output, 0, len);
        }
        /// <summary>
        ///     Returns a encoded string of ascii characters that are URI safe 
        /// </summary>
        public static int EncodeBytes(byte[] input, int start, int length, byte[] output, int offset)
        {
            if (output.Length < offset + (int)Math.Ceiling((length << 3) / 6d))
                throw new ArgumentOutOfRangeException();
            var leftover = length % 3;
            var stop = start + (length - leftover);
            var index = offset;
            int pos;
            for (pos = start; pos < stop; pos += 3)
            {
                output[index] = ChTable64[(input[pos] & 0xfc) >> 2];
                output[index + 1] = ChTable64[((input[pos] & 3) << 4) | ((input[pos + 1] & 240) >> 4)];
                output[index + 2] = ChTable64[((input[pos + 1] & 15) << 2) | ((input[pos + 2] & 0xc0) >> 6)];
                output[index + 3] = ChTable64[input[pos + 2] & 0x3f];
                index += 4;
            }
            switch (leftover)
            {
                case 1:
                    output[index] = ChTable64[(input[pos] & 0xfc) >> 2];
                    output[index + 1] = ChTable64[(input[pos] & 3) << 4];
                    index += 2;
                    break;
                case 2:
                    output[index] = ChTable64[(input[pos] & 0xfc) >> 2];
                    output[index + 1] = ChTable64[((input[pos] & 3) << 4) | ((input[pos + 1] & 240) >> 4)];
                    output[index + 2] = ChTable64[(input[pos + 1] & 15) << 2];
                    index += 3;
                    break;
            }
            return index - offset;
        }
        /// <summary>
        ///     Decodes the ascii text from the bytes provided into the original byte array 
        /// </summary>
        public static byte[] DecodeBytes(string input)
        {
            return DecodeBytes(input, 0, input?.Length ?? 0);
        }
        /// <summary>
        ///     Decodes the ascii text from the bytes provided into the original byte array 
        /// </summary>
        public static byte[] DecodeBytes(string input, int start, int length)
        {
            return DecodeBytes(Encoding.ASCII.GetBytes(input), start, length);
        }
        /// <summary>
        ///     Decodes the ascii text from the bytes provided into the original byte array 
        /// </summary>
        public static byte[] DecodeBytes(byte[] input)
        {
            return DecodeBytes(input, 0, input?.Length ?? 0);
        }
        /// <summary>
        ///     Decodes the ascii text from the bytes provided into the original byte array 
        /// </summary>
        public static byte[] DecodeBytes(byte[] input, int start, int length)
        {
            var results = new byte[(length * 6) >> 3];
            var used = DecodeBytes(input, start, length, results, 0);
            if (used != results.Length)
                Array.Resize(ref results, used);
            return results;
        }
        /// <summary>
        ///     Decodes the ascii text from the bytes provided into the original byte array 
        /// </summary>
        public static int DecodeBytes(byte[] input, int start, int length, byte[] output, int offset)
        {
            if (output.Length < offset + ((length * 6) >> 3))
                throw new ArgumentOutOfRangeException();
            var leftover = length % 4;
            var stop = start + (length - leftover);
            var index = offset;
            int pos;
            for (pos = start; pos < stop; pos += 4)
            {
                output[index] = (byte)((ChValue64[input[pos] - Min] << 2) | (ChValue64[input[pos + 1] - Min] >> 4));
                output[index + 1] =
                    (byte)((ChValue64[input[pos + 1] - Min] << 4) | (ChValue64[input[pos + 2] - Min] >> 2));
                output[index + 2] = (byte)((ChValue64[input[pos + 2] - Min] << 6) | ChValue64[input[pos + 3] - Min]);
                index += 3;
            }
            if (leftover == 2)
            {
                output[index] = (byte)((ChValue64[input[pos] - Min] << 2) | (ChValue64[input[pos + 1] - Min] >> 4));
                index += 1;
            }
            else if (leftover == 3)
            {
                output[index] = (byte)((ChValue64[input[pos] - Min] << 2) | (ChValue64[input[pos + 1] - Min] >> 4));
                output[index + 1] =
                    (byte)((ChValue64[input[pos + 1] - Min] << 4) | (ChValue64[input[pos + 2] - Min] >> 2));
                index += 2;
            }
            return index - offset;
        }
    }
}
