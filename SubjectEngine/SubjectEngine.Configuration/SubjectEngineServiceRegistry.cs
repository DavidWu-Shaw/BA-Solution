using Framework.Unity;
using Microsoft.Practices.Unity;
using SubjectEngine.Service;
using SubjectEngine.Service.Contract;

namespace SubjectEngine.Configuration
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
