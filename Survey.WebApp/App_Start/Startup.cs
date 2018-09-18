using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using Survey.Data;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Service;
using Survey.WebApp.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace Survey.WebApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                   .As<IDbFactory>()
                   .InstancePerRequest();

            builder.RegisterType<SurveyDbContext>()
                   .AsSelf()
                   .InstancePerRequest();

            //repositories
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                   .Where(c => c.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerRequest();

            //services
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                   .Where(c => c.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
