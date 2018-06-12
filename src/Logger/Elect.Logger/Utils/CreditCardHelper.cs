using System.Text.RegularExpressions;

namespace Elect.Logger.Utils
{
    public static class CreditCardHelper
    {
        /// <summary>
        ///     Filters credit card numbers from the input.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/> with credit card numbers removed.
        /// </returns>
        public static string Filter(string input)
        {
            Regex cardRegex = new Regex(@"\b(?:\d[ -]*?){13,16}\b", RegexOptions.IgnoreCase);

            return cardRegex.Replace(input, m => IsValidCreditCardNumber(m.Value) ? "####-CC-TRUNCATED-####" : m.Value);
        }

        /// <summary>
        ///     Validates a credit card number using Luhn algorithm.
        ///     Extremely fast Luhn algorithm implementation, based on 
        ///     pseudo code from Cliff L. Biffle (http://microcoder.livejournal.com/17175.html)
        ///     Copyleft Thomas @ Orb of Knowledge:
        ///     http://orb-of-knowledge.blogspot.com/2009/08/extremely-fast-luhn-function-for-c.html
        /// </summary>
        /// <param name="number">
        ///     The credit card number.
        /// </param>
        /// <returns>
        ///     True if a valid credit card number; otherwise false.
        /// </returns>
        public static bool IsValidCreditCardNumber(string number)
        {
            number = number.Replace("-", string.Empty);
            number = number.Replace(" ", string.Empty);

            int[] deltas = {0, 1, 2, 3, 4, -4, -3, -2, -1, 0};

            int checksum = 0;

            char[] chars = number.ToCharArray();

            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = chars[i] - 48;

                checksum += j;

                if (((i - chars.Length) % 2) == 0)
                {
                    checksum += deltas[j];
                }
            }

            return checksum % 10 == 0;
        }
    }
}