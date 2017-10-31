
using ECommerce.Common;
using ECommerce.Storage.Repository;
using ECommerce.Web.Infrastructure;
using Ninject;
using Ninject.Modules;
using NServiceBus;
using NServiceBus.Logging;

namespace ECommerce.NserviceBusHost
{
	using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint,INeedInitialization,AsA_Server,IWantToRunBeforeConfigurationIsFinalized
	{

		IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
			//kernel.Load(new INinjectModule[]
			//{
			//    new HandlersNinjectModule(), new ServiceNinjectModule(), new CommonNinjectModule(), new DataNinjectModule(),
			//});
			IocContainer.Instance.Initialize(kernel);
			return kernel;
		}

		public void Customize(BusConfiguration configuration)
		{
			configuration.RegisterComponents(c => c.ConfigureComponent<NServiceBusUnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork));
			configuration.RegisterComponents(c => c.ConfigureComponent<Repository>(DependencyLifecycle.InstancePerUnitOfWork));
			configuration.UseContainer<NinjectBuilder>(k => k.ExistingKernel(CreateKernel()));
			configuration.UsePersistence<NHibernatePersistence>();
			configuration.UseTransport<MsmqTransport>();
			configuration.RijndaelEncryptionService();
			configuration.FileShareDataBus("NServiceBus.DataBus.BasePath");
			configuration.Transactions();
			configuration.EnableOutbox();
			configuration.Conventions().ModuleConventions();
		}

		public void Run(Configure config)
		{
			//throw new System.NotImplementedException();
		}
	}
}
