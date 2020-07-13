using System.Collections.Generic;

namespace Deviot.Hermes.Common.BaseController
{
    public class DeviotActionResult
    {
        public bool Status { get; private set; }

        public string Message { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public object Data { get; private set; }

        public DeviotActionResult(object data, string message = "A requisição foi executada com sucesso.")
        {
            Status = true;
            Data = data;
            Errors = null;
            Message = message;
        }

        public DeviotActionResult(IEnumerable<string> errors, string message = "Houve um problema ao executar a requisição.")
        {
            Status = false;
            Data = null;
            Errors = errors;
            Message = message;
        }
    }
}
