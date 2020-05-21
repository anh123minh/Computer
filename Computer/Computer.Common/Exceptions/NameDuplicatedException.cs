using System;

namespace Computer.Common.Exceptions
{
    public class NameDuplicatedException : Exception
    {
        public NameDuplicatedException()
        {
        }

        public NameDuplicatedException(string message)
            : base(message)
        {
        }

        public NameDuplicatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}