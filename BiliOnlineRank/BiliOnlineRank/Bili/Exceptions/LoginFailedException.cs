using System;

namespace Bili.Exceptions
{
    public class LoginFailedException : Exception
    {
        public int Code { get; private set; }

        public LoginFailedException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
