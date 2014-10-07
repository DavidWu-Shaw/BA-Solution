using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PA.Data;
using PA.Repository.Contract;
using Framework.Data.NHibernate;

namespace PA.Repository
{
    public class DocumentRepository : NHUpdateEntityRepository<DocumentData>, IDocumentRepository
    {
    }
}
