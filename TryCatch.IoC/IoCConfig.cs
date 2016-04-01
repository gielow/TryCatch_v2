using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Http;
using TinyIoC;
using TryCatch.Core;
using TryCatch.Data;

namespace TryCatch.IoC
{
    public class IoCConfig
    {
        public static void Register()
        {
            // Container
            var container = TinyIoCContainer.Current;
            container.Register<IRepository, Repository>();

            // MVC dependency resolver
            DependencyResolver.SetResolver(new TinyIoCDependencyResolver(container));
            // Web Api dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
        
    }
}
