﻿using MoM.Module.Interfaces;
using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MoM.Module.Managers;
using Microsoft.AspNet.Hosting;

namespace MoM.CMS
{
    public class Module : IModule
    {
        private IConfigurationRoot ConfigurationRoot;
        private DataStorageManager StorageManager;
        public string Name
        {
            get
            {
                return "CMS";
            }
        }

        public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }


        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            Type type = GetIStorageImplementationType();

            if (type != null)
            {
                PropertyInfo connectionStringPropertyInfo = type.GetProperty("ConnectionString");

                if (connectionStringPropertyInfo != null)
                    connectionStringPropertyInfo.SetValue(null, this.ConfigurationRoot["Data:DefaultConnection:ConnectionString"]);

                PropertyInfo assembliesPropertyInfo = type.GetProperty("Assemblies");

                if (assembliesPropertyInfo != null)
                    assembliesPropertyInfo.SetValue(null, ModuleManager.GetAssemblies);

                services.AddScoped(typeof(IDataStorage), type);
            }

        }

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("error", "{controller=Error}/{action=Index}");
            //routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
            routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");
            routeBuilder.MapRoute("CMSComponents", "{controller=Components}/{action}");
            //routeBuilder.MapRoute(name: "CMSComponents", template: "cms", defaults: new { controller = "Home", action = "Index" });
        }

        private Type GetIStorageImplementationType()
        {
            AssemblyName a = new AssemblyName { Name = "MoM.Module" };
            var t = Assembly.Load(a).GetTypes();
            foreach (Type type in t)
                if (typeof(IDataStorage).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                    return type;

            return null;
        }
    }
}