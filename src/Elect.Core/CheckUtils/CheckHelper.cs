namespace Elect.Core.CheckUtils
{
    public static class CheckHelper
    {
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckNullOrWhiteSpace(string propertyValue, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new ArgumentNullException($"{propertyName} cannot be null or empty or whitespace.", propertyName);
            }
        }
    }
}
