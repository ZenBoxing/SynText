using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using SynTextDataManager.Library.Internal.DataAccess;
using SynTextDataManager.Library.DataAccess;

namespace SynTextDataManager.App_Start
{
    public class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //Add register types here
            //Example: builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();
            builder.RegisterType<SqlDataAccess>().As<ISqlDataAccess>();
            builder.RegisterType<TextData>().As<ITextData>();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}