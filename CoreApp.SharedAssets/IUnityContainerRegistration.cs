using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets
{
    public interface IUnityContainerRegistration
    {
        /// <summary>
        ///     Installs the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Install(IUnityContainer container);
    }
}
