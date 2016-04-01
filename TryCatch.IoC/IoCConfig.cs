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
        public static void Register(HttpConfiguration config)
        {
            // Container
            var container = Container();

            // MVC dependency resolver
            DependencyResolver.SetResolver(new TinyIoCDependencyResolver(container));
            // Web Api dependency resolver
            //GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
            config.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
        

        public static TinyIoCContainer Container()
        {
            var container = TinyIoCContainer.Current;
            container.Register<IRepository, Repository>();

            return container;
        }
    }
}
