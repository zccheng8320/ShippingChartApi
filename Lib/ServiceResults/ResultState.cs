using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.ServiceResults
{
    /// <summary>
    /// ServiceCode
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// The success
        /// </summary>
        Success = 0,
        /// <summary>
        /// The failure
        /// </summary>
        Failure = 1,
        /// <summary>
        /// The exception
        /// </summary>
        Exception = 2,
        /// <summary>
        /// The other
        /// </summary>
        Other = 3
    }

    public static class ResultStataEx
    {
        public static string ToStringFaster(this ResultState resultState)
        {
            return resultState switch
            {
                ResultState.Success => nameof(ResultState.Success),
                ResultState.Failure => nameof(ResultState.Failure),
                ResultState.Exception => nameof(ResultState.Exception),
                ResultState.Other => nameof(ResultState.Other),
                _ => throw new ArgumentOutOfRangeException(nameof(resultState), resultState, null)
            };
        }
    }
}
