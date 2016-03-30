using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TinyIoC;

namespace TryCatch.IoC
{
    public class IoCConfig
    {
        public static void Register()
        {
            var container = TinyIoCContainer.Current;

            DependencyResolver.SetResolver(new TinyIoCDependencyResolver(container));
        }
    }
}
