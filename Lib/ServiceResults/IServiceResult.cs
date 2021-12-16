namespace Lib.ServiceResults
{
    /// <summary>
    /// IServiceResult
    /// </summary>
    public interface IServiceResult
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        ResultState State { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        Exception Exception { get; set; }
        /// <summary>
        /// Sets the error.
        /// </summary>
        /// <param name="errorMsg">The error MSG.</param>
        void AddError(string errorMsg);
        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void SetException(Exception ex);

        void SetInfo(string message);
    }
    public interface IServiceResult<T> : IServiceResult
    {
        T Data { get; set; }
    }
}
