using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using Messim.UI.Controllers;
using Messim.UI.Installers;
using NUnit.Framework;

namespace Messum.UI.Test
{
    [TestFixture]
    class ControllersIoCTests
    {
        private IWindsorContainer controllersContainer;

        [SetUp]
        public void SetUpContainer()
        {
            controllersContainer = new WindsorContainer()
                .Install(new ControllerInstaller());
        }

        [TearDown]
        public void TearDownContainer()
        {
            controllersContainer.Dispose();
            controllersContainer = null;
        }

        //Now, let's start by verifying the first rule - 
        //that all types implementing IController are registered
        //and that only types implementing IController are registered. 
        //The second part is equally important - we want to make sure 
        //we don't have any false positives.
        [Test]
        public void all_controllers_implement_IController()
        {
            var allHandlers = GetAllHandlers(controllersContainer);
            var controllerHandlers = GetHandlersFor(typeof(IController), controllersContainer);
            Assert.IsNotEmpty(allHandlers);
            Assert.IsNotEmpty(controllerHandlers);
            Assert.AreEqual(allHandlers, controllerHandlers);
        }

        private IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        [Test]
        public void all_controllers_are_registered()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = GetImplementationTypesFor(typeof(IController), controllersContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        //Consistency tests
        [Test]
        public void all_controllers_have_Controller_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), controllersContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void all_controllers_in_same_namespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace != null && c.Namespace.Contains("Controllers"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), controllersContainer);
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void all_controllers_are_transient()
        {
            var nonTransientControllers = GetHandlersFor(typeof(IController), controllersContainer)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();
            Assert.IsEmpty(nonTransientControllers);

        }

        [Test]
        public void all_controllers_expose_themselves_as_service()
        {
            var controllersWithWrongName = GetHandlersFor(typeof(IController), controllersContainer)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            Assert.IsEmpty(controllersWithWrongName);
        }
    }
}
