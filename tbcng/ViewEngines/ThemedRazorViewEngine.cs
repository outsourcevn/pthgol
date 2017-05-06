using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tbcng
{
    public class ThemedRazorViewEngine : RazorViewEngine
    {
        public ThemedRazorViewEngine(string themeName)
        {
            //and same for all of below
            FileExtensions = new string[] { "cshtml" };
            MasterLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
            PartialViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
            ViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
          
        }
    }
}