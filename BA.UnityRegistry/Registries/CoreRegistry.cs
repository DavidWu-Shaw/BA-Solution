using Framework.Unity;
using Microsoft.Practices.Unity;
using Framework.UoW;
using Framework.Service;
using Framework.Data;

namespace BA.UnityRegistry
{
    public class CoreRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IDataStoreResolver, DataStoreResolver>();
            container.RegisterType<IDataStoreManager, DataStoreManager>();
            container.RegisterType<IBusinessObjectFactory, BusinessObjectFactory>();
        }
    }
}
