using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Models
{
    class OpenAPIModel
    {
        public string id { get; set; }
        public string userid { get; set; }
        public string APIEndPoint { get; set; }
        public string ConnectorName { get; set; }
        public string HostName { get; set; }
        public List<ActionModel> actions {get; set;}
    }    
}
