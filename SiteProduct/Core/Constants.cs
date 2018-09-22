using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SiteProduct.Core
{
    public static class Constants
    {
        public static string ProductDescriptionPath
        {
            get { return ConfigurationManager.AppSettings["ProductDescriptionPath"]; }
        }
    }
}