using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using SubjectEngine.Data;
using Framework.UoW;

namespace SubjectEngine.Service.Contract
{
    public interface ISubjectService : IUpdateEntityService<SubjectData>
    {
        IServiceQueryResult<SubjectData> RetrieveBySubjectType(string subjectType);
        IEnumerable<SubjectFieldInfoData> GetSubjectFieldInfosData(object subjectId);
    }
}
