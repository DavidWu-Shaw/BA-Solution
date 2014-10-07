using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubjectEngine.Data;
using SubjectEngine.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate.Criterion;
using NHibernate;

namespace SubjectEngine.Repository
{
    public class SubjectRepository : NHUpdateEntityRepository<SubjectData>, ISubjectRepository
    {
        public SubjectData RetrieveBySubjectType(string subjectType)
        {
            SubjectData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
                {
                    result = CurrentSession.CreateCriteria<SubjectData>()
                        .AddExpressionInsensitiveLike<SubjectData, string>(o => o.SubjectType, subjectType, MatchMode.Exact)
                        .UniqueResult<SubjectData>();
                });

            return result;
        }

        public IEnumerable<SubjectFieldInfoData> GetSubjectFieldInfosData(object subjectId)
        {
            IEnumerable<SubjectFieldInfoData> results = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetSubjectFieldInfosData");
                query.SetParameter("subjectId", subjectId);
                results = query.List<SubjectFieldInfoData>();
            });

            return results;
        }
    }
}
