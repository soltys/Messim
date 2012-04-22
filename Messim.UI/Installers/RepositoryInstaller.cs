using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Messim.UI.Models;
using Messim.UI.Repository;

namespace Messim.UI.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepository<Message>>()
                .ImplementedBy<MessageRepository>()
                .LifestyleTransient(),
                Component.For<IRepository<User>>()
                .ImplementedBy<UserRepository>()
                .LifestyleTransient());
        }
    }
}