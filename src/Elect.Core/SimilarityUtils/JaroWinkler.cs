using System;
using System.Linq;

namespace Elect.Core.SimilarityUtils
{
    /// The Jaro–Winkler distance metric is designed and best suited for short
    /// strings such as person names, and to detect typos; it is (roughly) a
    /// variation of Damerau-Levenshtein, where the substitution of 2 close
    /// characters is considered less important then the substitution of 2 characters
    /// that a far from each other.
    /// Jaro-Winkler was developed in the area of record linkage (duplicate
    /// detection) (Winkler, 1990). It returns a value in the interval [0.0, 1.0].
    /// The distance is computed as 1 - Jaro-Winkler similarity.
    public class JaroWinkler
    {
        private const double DefaultThreshold = 0.7;
        private const int Three = 3;
        private const double JwCoef = 0.1;

        /// <summary>
        ///     The current value of the threshold used for adding the Winkler bonus. The default value is 0.7.
        /// </summary>
        private double Threshold { get; }

        /// <summary>
        ///     Creates a new instance with default threshold (0.7)
        /// </summary>
        public JaroWinkler()
        {
            Threshold = DefaultThreshold;
        }

        /// <summary>
        ///     Creates a new instance with given threshold to determine when Winkler bonus should
        ///     be used. Set threshold to a negative value to get the Jaro distance.
        /// </summary>
        /// <param name="threshold"></param>
        public JaroWinkler(double threshold)
        {
            Threshold = threshold;
        }

        /// <summary>
        ///     Compute Jaro-Winkler similarity.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>The Jaro-Winkler similarity in the range [0, 1]</returns>
        /// <exception cref="ArgumentNullException">If s1 or s2 is null.</exception>
        public double Similarity(string s1, string s2)
        {
            if (s1 == null)
            {
                throw new ArgumentNullException(nameof(s1));
            }

            if (s2 == null)
            {
                throw new ArgumentNullException(nameof(s2));
            }

            if (s1.Equals(s2))
            {
                return 1f;
            }

            int[] mtp = Matches(s1, s2);
            float m = mtp[0];
            if (m == 0)
            {
                return 0f;
            }

            double j = ((m / s1.Length + m / s2.Length + (m - mtp[1]) / m))
                       / Three;
            double jw = j;

            if (j > Threshold)
            {
                jw = j + Math.Min(JwCoef, 1.0 / mtp[Three]) * mtp[2] * (1 - j);
            }

            return jw;
        }

        /// <summary>
        ///     Return 1 - similarity.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>1 - similarity</returns>
        /// <exception cref="ArgumentNullException">If s1 or s2 is null.</exception>
        public double Distance(string s1, string s2) => 1.0 - Similarity(s1, s2);

        private static int[] Matches(string s1, string s2)
        {
            string max, min;
            if (s1.Length > s2.Length)
            {
                max = s1;
                min = s2;
            }
            else
            {
                max = s2;
                min = s1;
            }

            int range = Math.Max(max.Length / 2 - 1, 0);

            int[] matchIndexes = Enumerable.Repeat(-1, min.Length).ToArray();

            bool[] matchFlags = new bool[max.Length];
            int matches = 0;
            for (int mi = 0; mi < min.Length; mi++)
            {
                char c1 = min[mi];
                for (int xi = Math.Max(mi - range, 0),
                    xn = Math.Min(mi + range + 1, max.Length);
                    xi < xn;
                    xi++)
                {
                    if (!matchFlags[xi] && c1 == max[xi])
                    {
                        matchIndexes[mi] = xi;
                        matchFlags[xi] = true;
                        matches++;
                        break;
                    }
                }
            }

            char[] ms1 = new char[matches];
            char[] ms2 = new char[matches];
            for (int i = 0, si = 0; i < min.Length; i++)
            {
                if (matchIndexes[i] != -1)
                {
                    ms1[si] = min[i];
                    si++;
                }
            }

            for (int i = 0, si = 0; i < max.Length; i++)
            {
                if (matchFlags[i])
                {
                    ms2[si] = max[i];
                    si++;
                }
            }

            int transpositions = 0;
            for (int mi = 0; mi < ms1.Length; mi++)
            {
                if (ms1[mi] != ms2[mi])
                {
                    transpositions++;
                }
            }

            int prefix = 0;
            for (int mi = 0; mi < min.Length; mi++)
            {
                if (s1[mi] == s2[mi])
                {
                    prefix++;
                }
                else
                {
                    break;
                }
            }

            return new[] {matches, transpositions / 2, prefix, max.Length};
        }
    }
}