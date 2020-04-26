using System;
using System.Collections.Generic;
using System.Text;

namespace StarApi.SendEmail
{
    public class EmailRequest
    {
        public bool isEmpty => apiVersion != "v1" || string.IsNullOrEmpty(requestId) || requestId == "false";
        public string apiVersion { get; set; }
        public string requestId { get; set; }
        public string token { get; set; }
        public string template { get; set; }
        public dynamic fields { get; set; }
    }
}
