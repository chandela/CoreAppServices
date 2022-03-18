using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using CoreApp.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Hangfire;

namespace CoreApp.Business.Configuration
{
    public static class ApplicationInitializer
    {
        public static IUnityContainer Initialize(Action<IMapperConfigurationExpression> intializeUxMapper)
        {
            var unityContainer = new UnityContainer();
            AutoMapperConfigurator.CreateMaps(intializeUxMapper);
            new Infrastructure.UnityContainerRegistration().Install(unityContainer);
            new UnityContainerRegistration().Install(unityContainer);
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(unityContainer));
            return unityContainer;
        }
    }

    public class ContainerJobActivator : JobActivator
    {
        private IUnityContainer _container;

        public ContainerJobActivator(IUnityContainer container)
        {
            _container = container;
        }

        public override object ActivateJob(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
