using CoreApp.Business.BusinessProcess;
using CoreApp.Models.CoreModels;
using CoreApp.SharedAssets;
using CoreApp.SharedAssets.Configuration;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Business.Configuration
{
    public class UnityContainerRegistration : IUnityContainerRegistration
    {
        public void Install(IUnityContainer container)
        {
            var asm = GetType().Assembly;
            var interfaces = asm.GetInterfaces();
            foreach (var interfaceType in interfaces)
            {
                var currentInterfaceType = interfaceType;
                var implementations = asm.GetImplementationsOfInterface(interfaceType);
                if (implementations.Count > 1)
                    implementations.ToList().ForEach(i => container.RegisterType(currentInterfaceType, i, i.Name));
                else
                    implementations.ToList().ForEach(i => container.RegisterType(currentInterfaceType, i));
            }

            container.RegisterType<IConfigurationReader, ConfigurationReader>(new ContainerControlledLifetimeManager());
        }
    }
}
