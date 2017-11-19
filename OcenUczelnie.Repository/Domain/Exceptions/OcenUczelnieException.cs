using System;

namespace OcenUczelnie.Core.Domain.Exceptions
{
    public class OcenUczelnieException : Exception
    {
        protected OcenUczelnieException()
        {
        }

        public OcenUczelnieException(string code)
        {
            Code = code;
        }

        public OcenUczelnieException(string code, string message) : base(message)
        {
            Code = code;
        }

        public OcenUczelnieException(string code, string message, Exception innerException) : base(message,
            innerException)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}