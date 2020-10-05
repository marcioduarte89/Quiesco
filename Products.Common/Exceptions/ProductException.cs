namespace Products.Common.Exceptions
{
    using System;

    public class ProductException : Exception
    {
        public const int INVALID_DATA = 1;
        public const int NOT_FOUND = 2;
        public const int SERVER_ERROR = 3;

        public int Detail { get; private set; }

        public ProductException(int detail, string message) : base(message)
        {
            Detail = detail;
        }
    }
}
