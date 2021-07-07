﻿using Calendar_Apriorit.Initialazer.MapperInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Calendar_Apriorit.Jobs;

namespace Calendar_Apriorit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //RouteTable.Routes.MapHubs();

            ModelMapper.Init();
            //NearlyEventsSheulder.Start();
            NearlyEventsChecker.StartTimer();

        }
    }
}
