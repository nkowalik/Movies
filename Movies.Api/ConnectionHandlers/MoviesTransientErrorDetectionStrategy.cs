using Microsoft.Rest.TransientFaultHandling;
using System.Net;

namespace Movies.Api.ConnectionHandlers
{
    /// <summary>
    /// Transient error detection strategy (for fetching movies data)
    /// </summary>
    public class MoviesTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        /// <summary>
        /// Checks if received exception is transient
        /// </summary>
        /// <param name="ex">Received exception</param>
        /// <returns></returns>
        public bool IsTransient(Exception ex)
        {
            return CheckIsTransient(ex);
        }

        private static bool CheckIsTransient(Exception ex)
        {
            if (ex is TimeoutException)
            {
                return true;
            }
            else if (ex is WebException)
            {
                return true;
            }
            else if (ex is UnauthorizedAccessException)
            {
                return false;
            }

            return false;
        }
    }
}
