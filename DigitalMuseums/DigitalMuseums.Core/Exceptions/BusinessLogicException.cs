using System.Collections.Generic;

namespace DigitalMuseums.Core.Exceptions
{
    /// <summary>
    /// Custom business logic exception.
    /// </summary>
    public class BusinessLogicException : BaseException
    {
        /// <inheritdoc />
        public BusinessLogicException(string message) : base(message)
        {
        }
        
        public BusinessLogicException(string message, Dictionary<string, List<string>> errors) : base(message)
        {
            Errors = errors ?? new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Errors { get; protected set; }
    }
}