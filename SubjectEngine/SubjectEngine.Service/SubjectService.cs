using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Framework.Service;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;
using SubjectEngine.Repository.Contract;
using Framework.Security;
using Framework.UoW;

namespace SubjectEngine.Service
{
    public class SubjectService : UpdateEntityService<ISubjectRepository, SubjectData>, ISubjectService
    {
        public IServiceQueryResult<SubjectData> RetrieveBySubjectType(string subjectName)
        {
            SubjectData data = Repository.RetrieveBySubjectType(subjectName);
            return ServiceResultFactory.BuildServiceQueryResult(data);
        }

        public IEnumerable<SubjectFieldInfoData> GetSubjectFieldInfosData(object subjectId)
        {
            return Repository.GetSubjectFieldInfosData(subjectId);

        }

    }
}
