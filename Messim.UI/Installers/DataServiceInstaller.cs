using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Messim.UI.Service;

namespace Messim.UI.Installers
{
    public class DataServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMessimDataService>()
                .ImplementedBy<MessimDataService>()
                .LifestyleTransient());
        }
    }
}