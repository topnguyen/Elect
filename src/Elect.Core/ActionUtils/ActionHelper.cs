namespace Elect.Core.ActionUtils
{
    public class ActionHelper
    {
        /// <summary>
        ///     Get instance of T from Action 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T GetValue<T>(Action<T> action) where T : class, new()
        {
            T obj = (T) Activator.CreateInstance(typeof(T));
            action.DynamicInvoke(obj);
            return obj;
        }
        /// <summary>
        ///     Invoke/Run an operation and ignores any exceptions.
        /// </summary>
        /// <param name="operation">lambda that performs an operation that may throw exception</param>
        /// <param name="onError">Do your logic on error occur</param>
        /// <returns>True if don't have any exception, false if occur an error.</returns>
        public static bool IgnoreError(Action operation, Action<Exception> onError = null)
        {
            try
            {
                operation?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
                return false;
            }
        }
        /// <summary>
        ///     Invoke/Run an operation and ignores any exceptions.
        /// </summary>
        /// <param name="operation">lambda that performs an operation that may throw exception</param>
        /// <param name="onError">Do your logic on error occur</param>
        /// <param name="defaultValue">Default value returned if operation fail</param>
        /// <returns>Actual result if don't have any exception, default value if occur an error.</returns>
        public static T IgnoreError<T>(Func<T> operation, Action<Exception> onError = null, T defaultValue = default)
        {
            if (operation == null)
            {
                return defaultValue;
            }
            T result;
            try
            {
                result = operation.Invoke();
            }
            catch (Exception e)
            {
                result = defaultValue;
                onError?.Invoke(e);
            }
            return result;
        }
    }
}
