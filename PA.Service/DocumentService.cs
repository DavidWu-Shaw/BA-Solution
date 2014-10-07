using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;
using PA.Service.Contract;
using PA.Repository.Contract;
using Framework.Security;
using Framework.UoW;

namespace PA.Service
{
    public class DocumentService : UpdateEntityService<IDocumentRepository, DocumentData>, IDocumentService
    {
       

    }
}
