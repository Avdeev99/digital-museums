using System;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Exceptions
{
    /// <summary>
    /// The base solution exception.
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        protected BaseException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
        
        protected BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public virtual int StatusCode { get; } = StatusCodes.Status500InternalServerError;

    }
}