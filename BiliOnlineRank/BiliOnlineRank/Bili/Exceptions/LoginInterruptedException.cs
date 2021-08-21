﻿using System;

namespace Bili.Exceptions
{
    /// <summary>
    /// Exception when the login was not succeed
    /// </summary>
    public class LoginStatusException : Exception
    {
        /// <summary>
        /// Login status from remote
        /// </summary>
        public int Status { get; private set; }

        public LoginStatusException(int status, string message) : base(message)
        {
            Status = status;
        }
    }
}
