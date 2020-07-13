using System.Collections.Generic;

namespace Deviot.Hermes.Common.BaseClientHttp
{
    public class ResponseError
    {
        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public ResponseError()
        {
            Errors = new List<string>();
        }
    }
}
