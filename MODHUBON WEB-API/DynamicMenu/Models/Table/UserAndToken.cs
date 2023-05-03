using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicMenu.Functions;

namespace DynamicMenu.Models.Table
{
    public class UserAndToken
    {
        public class DataWithToken
        {
            public string Token { get; set; }
            public List<AllUserInformation> Data { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

    }
}