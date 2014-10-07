using Framework.Unity;
using Microsoft.Practices.Unity;
using SubjectEngine.Repository;
using SubjectEngine.Repository.Contract;

namespace SubjectEngine.Configuration
{
    public class SubjectEngineRepositoryRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<IDEntityRepository, DEntityRepository>();
            container.RegisterType<IDataTypeRepository, DataTypeRepository>();
        }
    }
}
