using Serilog;
using System;
using System.Threading.Tasks;

namespace ERSA.Mobile.Shared
{
    /// <summary>
    /// Exception helpers
    /// </summary>
    public static class Exceptions
    {
        private static bool LogException(Exception ex, string comment)
        {
            var exMessageAndTrace = ex.Message + "\n" + ex.StackTrace;

            if (string.IsNullOrWhiteSpace(comment))
                Log.Error(exMessageAndTrace);
            else
                Log.Error(comment + "\n" + exMessageAndTrace);

            return false;
        }

        /// <summary>
        /// Wywołuje podaną akcję w wewnętrznym bloku try-catch, w przypadku wystąpienia wyjątku loguje wyjątek
        /// </summary>
        /// <param name="func">Akcja która ma zostać wykonana</param>
        /// <param name="comment">Komentarz dopisywany do logu po wystąpieniu wyjątku</param>
        /// <returns>true gdy akcja została wykonana pomyślnie, false gdy wywołano wyjątek</returns>
        public static T LogAndCatch<T>(Func<T> func, string comment = "", T defaultReturn = default(T))
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                LogException(ex, comment);
                return defaultReturn;
            }
        }

        /// <summary>
        /// Wywołuje podaną akcję w wewnętrznym bloku try-catch, w przypadku wystąpienia wyjątku loguje wyjątek
        /// </summary>
        /// <param name="func">Akcja która ma zostać wykonana</param>
        /// <param name="comment">Komentarz dopisywany do logu po wystąpieniu wyjątku</param>
        /// <returns>true gdy akcja została wykonana pomyślnie, false gdy wywołano wyjątek</returns>
        public static T LogAndCatch<T>(Func<T> func, out Exception exception, string comment = "", T defaultReturn = default(T))
        {
            exception = null;
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                LogException(ex, comment);
                exception = ex;
                return defaultReturn;
            }
        }


        /// <summary>
        /// Wywołuje podaną akcję w wewnętrznym bloku try-catch, w przypadku wystąpienia wyjątku loguje wyjątek
        /// </summary>
        /// <param name="func">Akcja która ma zostać wykonana</param>
        /// <param name="comment">Komentarz dopisywany do logu po wystąpieniu wyjątku</param>
        /// <returns>true gdy akcja została wykonana pomyślnie, false gdy wywołano wyjątek</returns>
        public static async Task<T> LogAndCatchAsync<T>(Func<Task<T>> func, string comment = "", T defaultReturn = default(T))
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                LogException(ex, comment);
                return defaultReturn;
            }
        }
    }
}
