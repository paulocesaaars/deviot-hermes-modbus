using System;
using System.Net;

namespace Deviot.Hermes.Common.BaseClientHttp
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;

        public CustomHttpRequestException() { }

        public CustomHttpRequestException(HttpStatusCode httpStatusCode) 
            : base ($"StatusCode: {httpStatusCode.ToString()}")
        {
            StatusCode = httpStatusCode;
        }
    }
}
