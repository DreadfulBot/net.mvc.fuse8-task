﻿using System.Web;
using System.Web.Mvc;

namespace net.mvc.fuse8_task
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
