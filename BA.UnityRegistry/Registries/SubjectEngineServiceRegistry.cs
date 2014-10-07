using Microsoft.Practices.Unity;
using Framework.Unity;
using SubjectEngine.Service.Contract;
using SubjectEngine.Service;

namespace BA.UnityRegistry
{
    public class SubjectEngineServiceRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<ISubjectService, SubjectService>();
            container.RegisterType<IDEntityService, DEntityService>();
            container.RegisterType<IDataTypeService, DataTypeService>();
        }
    }
}
