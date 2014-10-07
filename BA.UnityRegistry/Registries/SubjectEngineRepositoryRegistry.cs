using Framework.Unity;
using Microsoft.Practices.Unity;
using SubjectEngine.Repository.Contract;
using SubjectEngine.Repository;

namespace BA.UnityRegistry
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
