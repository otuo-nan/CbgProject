﻿namespace CbgTaxi24.API.Infrastructure.Exceptions
{
    public class PlatformException : Exception
    {
        public int? CustomStatusCode { get; set; }
        public IEnumerable<string> Errors { get; private set; } = [];

        public PlatformException() { }

        public PlatformException(string message) : base(message)
        {
            Errors = [message];
        }
        public PlatformException(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public PlatformException(string message, Exception innerException) : base(message, innerException) { }
    }
}
