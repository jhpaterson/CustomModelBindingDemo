﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomModelBindingDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(
                typeof(CustomModelBindingDemo.Models.User),
                new CustomModelBindingDemo.Infrastructure.UserModelBinder(ModelBinders.Binders.DefaultBinder));
            ModelBinders.Binders.Add(
                typeof(CustomModelBindingDemo.Models.Member),
                new CustomModelBindingDemo.Infrastructure.MemberModelBinder()
                );
            ModelBinders.Binders.Add(
                typeof(CustomModelBindingDemo.Models.Card),
                new CustomModelBindingDemo.Infrastructure.CardModelBinder()
                );
            ModelBinders.Binders.Add(
                typeof(CustomModelBindingDemo.Models.ShoppingCart),
                new CustomModelBindingDemo.Infrastructure.ShoppingCartModelBinder()
                );
        }
    }
}