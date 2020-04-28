using System;
using System.Collections.Generic;
using System.Text;

namespace StarApi.SendEmail
{
    public class BallotFlaggedFields
    {
        public string starId { get; set; }
        public string voterId { get; set; }
        public string reason { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string returnLink { get; set; }
    }
}
