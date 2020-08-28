using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class MosquittoBrokerViewModel
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Timeout { get; set; }

        public IEnumerable<MosquittoTopicViewModel> Topics { get; set; }
    }
}
