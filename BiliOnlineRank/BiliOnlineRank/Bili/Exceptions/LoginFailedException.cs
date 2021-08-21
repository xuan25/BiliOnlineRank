using System;

namespace Bili.Exceptions
{
    /// <summary>
    /// Exception when the login has failed
    /// </summary>
    public class LoginFailedException : Exception
    {
        /// <summary>
        /// Error code from remote
        /// </summary>
        public int Code { get; private set; }

        public LoginFailedException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
