using AOP.Services;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using DynamicProxyLog;
using System.Web.Mvc;

namespace AOP.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.Register(c => new LogInterceptor());
            builder.Register(c => new ProxyGenerator());
            builder.RegisterType<GreetingService>()
                   .As<IGreetingService>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(LogInterceptor));

            //PostSharp
            //builder.RegisterType<GreetingService>()
            //       .As<IGreetingService>();

            var container = builder.Build();
            var willBeIntercepted = container.Resolve<IGreetingService>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}