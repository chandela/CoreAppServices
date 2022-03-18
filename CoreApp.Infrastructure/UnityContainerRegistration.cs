using CoreApp.Infrastructure.DBContext;
using CoreApp.Models.CoreModels;
using CoreApp.Models.CoreModels.IRepositories;
using CoreApp.SharedAssets;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Infrastructure
{
    public class UnityContainerRegistration : IUnityContainerRegistration
    {
        public void Install(IUnityContainer container)
        {
            var asm = Assembly.GetExecutingAssembly();
            var interfaces = typeof(IRepository<>).Assembly.GetInterfaces();
            foreach (var interfaceType in interfaces)
            {
                var currentInterfaceType = interfaceType;
                var implementations = asm.GetImplementationsOfInterface(interfaceType);
                if (implementations.Count > 1)
                    implementations.ToList().ForEach(i => container.RegisterType(currentInterfaceType, i, i.Name));
                else
                    implementations.ToList().ForEach(i => container.RegisterType(currentInterfaceType, i));
            }
            container.RegisterType<IDbContext, CoreAppDbContext>();
            container.RegisterType<DefaultDbContext, DefaultDbContext>();
        }
    }
}
