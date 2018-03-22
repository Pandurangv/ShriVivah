using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;


namespace ShriVivah.App_Start
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    


    public class AutherizationPages
    {
        public AutherizationPages()
        {
            ActionName = new List<string>();
        }
        public int PageIndex { get; set; }

        public string PageName { get; set; }

        public List<string> ActionName { get; set; }

    }

    
}