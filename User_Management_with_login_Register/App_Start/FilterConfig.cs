﻿using System.Web;
using System.Web.Mvc;

namespace User_Management_with_login_Register
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
