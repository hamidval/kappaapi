using CommonServiceLocator;
using SimpleInjector;

namespace KappaApi
{
    public class SimpleInjectorServiceLocator : ServiceLocatorImplBase
    {
        private readonly Container _container;
        public SimpleInjectorServiceLocator(Container container)
        {
            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType);
        }
    }
}
