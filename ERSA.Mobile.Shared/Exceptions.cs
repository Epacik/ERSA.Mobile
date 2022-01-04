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
        public static T LogAndCatch<T>(Func<T> func, string comment = "", T defaultReturn = default)
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
        public static T LogAndCatch<T>(Func<T> func, out Exception exception, string comment = "", T defaultReturn = default)
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
        public static async Task<T> LogAndCatchAsync<T, E>(Func<Task<T>> func, string comment = "", T defaultReturn = default) where E : Exception
        {
            try
            {
                return await func();
            }
            catch (E ex)
            {
                LogException(ex, comment);
                return defaultReturn;
            }
        }


        /// <summary>
        /// Wykonuje przekazany Func, jeśli func wyrzuci wyjątek, zostanie on zapisany ale nie wyłapany
        /// </summary>
        /// <typeparam name="T">Typ jaki ma zostać zwrócony</typeparam>
        /// <param name="func">Zadanie które ma zostać wykonane</param>
        /// <param name="comment">Dodatkowy komentarz który zostanie dopisany do logu w przypadku wystąpienia wyjątku</param>
        /// <returns></returns>
        public static async Task<T> LogAndThrowAsync<T>(Func<Task<T>> func, string comment = "")
        {
            try
            {
                return await func();
            }
            catch (Exception ex) when (LogException(ex, comment)) //LogException zawsze zwraca false więc warunek nigdy nie zostanie spełniony
            {/*to się NIGDY nie wykona ;)*/}
            
            return default; // bezużyteczne
                            // dodane żeby kompilator nie krzyczał
                            //
                            // jeśli func będzie wykonany poprawnie, to tu nie dotrzemy,
                            // jeśli func wyrzuci wyjątek, to nie zostanie on wyłapany i też tu nie dotrzemy
        }
    }
}
