using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lib.ServiceResults
{

    /// <summary>
    /// ITSBaseServiceResult
    /// </summary>將enum轉為List
    public class ServiceResult : IServiceResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResult"/> class.
        /// </summary>
        public ServiceResult()
        {
            State = ResultState.Success;
            ValidationResults = new List<string>();
            IsContinueFlow = true;
        }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public ResultState State { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int HttpStatusCode { get; set; } = 200;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message => getValidationResultErrorFormat();
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        [JsonIgnore]
        public Exception Exception { get; set; }

        public bool IsContinueFlow { get; set; }

        protected List<string> ValidationResults { get; set; }

        /// <summary>
        /// Sets the error.
        /// </summary>
        /// <param name="errorMsg">The error MSG.</param>
        public virtual void AddError(string errorMsg)
        {
            State = ResultState.Failure;
            ValidationResults.Add(errorMsg);
            IsContinueFlow = false;
        }

        public virtual void AddError(string errorMsg, bool isContinueFlow)
        {
            State = ResultState.Failure;
            ValidationResults.Add(errorMsg);
            IsContinueFlow = isContinueFlow;
        }

        /// <summary>
        /// Gets the validation result error format.
        /// </summary>
        protected virtual string getValidationResultErrorFormat()
        {
            return string.Join("\r\n", ValidationResults);
        }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public virtual void SetException(Exception ex)
        {
            State = ResultState.Exception;
            var logMessage = ex.ToString();
            HttpStatusCode = 500;
#if DEBUG
            ValidationResults.Add(logMessage);
#else
            ValidationResults.Add("伺服器發生異常");
#endif
            Exception = ex;
            IsContinueFlow = false;
        }
        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public virtual void SetException(Exception ex, bool isContinueFlow)
        {
            State = ResultState.Exception;
            var logMessage = ex.ToString();
#if DEBUG
            ValidationResults.Add(logMessage);
#else
            ValidationResults.Add("伺服器發生異常");
#endif
            Exception = ex;
            IsContinueFlow = isContinueFlow;
        }
        /// <summary>
        /// Sets the information.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public virtual void SetInfo(string message)
        {
            ValidationResults.Add(message);
        }
    }

    public class ServiceResult<T> : ServiceResult, IServiceResult<T>
    {
        public T Data { get; set; }
    }
}
