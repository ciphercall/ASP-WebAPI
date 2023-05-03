using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.LogIn.Interface
{
    public class LogInInterface
    {
        // Default Model Start //
        public Int64 UserIdInsert { get; set; }
        public string UserPcInsert { get; set; }
        public DateTime InTimeInsert { get; set; }
        public string IpAddressInsert { get; set; }
        public string LotiLengTudeInsert { get; set; }
        public Int64 UserIdUpdate { get; set; }
        public string UserPcUpdate { get; set; }
        public DateTime InTimeUpdate { get; set; }
        public string IpAddressUpdate { get; set; }
        public string LotiLengTudeUpdate { get; set; }

        // Default Model End //


        public Int64 UserID { get; set; }
        public string Password { get; set; }


        public object Token { get; set; }
    }
}