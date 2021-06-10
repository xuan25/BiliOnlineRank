using System;

namespace Bili.Exceptions
{
    public class LoginStatusException : Exception
    {
        public int Status { get; private set; }

        public LoginStatusException(int status, string message) : base(message)
        {
            Status = status;
        }
    }
}
