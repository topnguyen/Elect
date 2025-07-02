namespace Elect.Core.SecurityUtils.Algorithms
{
    public sealed class Crc32 : HashAlgorithm
    {
        #region Properties
        public const uint DefaultPolynomial = 0xedb88320u;
        public const uint DefaultSeed = 0xffffffffu;
        public override int HashSize => 32;
        #endregion Properties
        #region Fields
        private static uint[] _defaultTable;
        private readonly uint _seed;
        private readonly uint[] _table;
        private uint _hash;
        #endregion Fields
        public Crc32() : this(DefaultPolynomial, DefaultSeed)
        {
        }
        public Crc32(uint polynomial, uint seed)
        {
            _table = InitializeTable(polynomial);
            _seed = _hash = seed;
        }
        #region Override
        public override void Initialize()
        {
            _hash = _seed;
        }
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            _hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
        }
        protected override byte[] HashFinal()
        {
            var hashBuffer = UintToBigEndianBytes(~_hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }
        #endregion Override
        #region Private Helper
        private static uint[] InitializeTable(uint polynomial)
        {
            if (polynomial == DefaultPolynomial && _defaultTable != null)
            {
                return _defaultTable;
            }
            var createTable = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (uint)i;
                for (var j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ polynomial;
                    }
                    else
                    {
                        entry = entry >> 1;
                    }
                createTable[i] = entry;
            }
            if (polynomial == DefaultPolynomial)
            {
                _defaultTable = createTable;
            }
            return createTable;
        }
        private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
        {
            var hash = seed;
            for (var i = start; i < start + size; i++)
                hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
            return hash;
        }
        private static byte[] UintToBigEndianBytes(uint @uint)
        {
            var result = BitConverter.GetBytes(@uint);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(result);
            }
            return result;
        }
        #endregion Private Helper
    }
}
