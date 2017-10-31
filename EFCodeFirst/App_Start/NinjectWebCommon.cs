using System.Configuration;
using ECommerce.Common;
using ECommerce.Storage.Contexts;
using ECommerce.Storage.Repository;
using ECommerce.Web.Infrastructure;
using log4net;
using log4net.Repository.Hierarchy;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;
using NServiceBus;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ECommerce.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ECommerce.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ECommerce.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
          var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            IocContainer.Instance.Initialize(kernel);
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<IUnitOfWork>().To<EmpContext>().InRequestScope();
            kernel.Bind<IRepository>().To<Repository>();
            CreateBus(kernel);
        }


        static ISendOnlyBus CreateBus(IKernel kernel)
        {
            //var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var config = new BusConfiguration();

            config.UseContainer<NinjectBuilder>(c => c.ExistingKernel(kernel));
            config.UseSerialization<XmlSerializer>();
            config.Transactions().Disable().DoNotWrapHandlersExecutionInATransactionScope();
            config.UseTransport<MsmqTransport>();
            config.RijndaelEncryptionService();
            config.UseDataBus<FileShareDataBus>().BasePath(Constants.NServiceBus_DataBusBasePath);
            config.Conventions().ModuleConventions();
            return Bus.CreateSendOnly(config);
        }
    }
}