using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApp.Models
{
    public class RequestInfo
    {
        public string Type { get; set; }
        public string IP { get; set; }
        public string URL { get; set; }

        public override string ToString()
        {
            string info = "Type: " + Type + "\n" + "IP: "+ IP + "\n" + "URL: " + URL + "\n";

            return info;
        }
    }
}