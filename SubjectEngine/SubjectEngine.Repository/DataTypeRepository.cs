using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubjectEngine.Data;
using SubjectEngine.Repository.Contract;
using Framework.Data.NHibernate;

namespace SubjectEngine.Repository
{
    public class DataTypeRepository : NHUpdateEntityRepository<DataTypeData>, IDataTypeRepository
    {
    }
}
