using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class DocumentFacade : BusinessComponent
    {
        public DocumentFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DocumentSystem = new DocumentSystem(unitOfWork);
        }

        private DocumentSystem DocumentSystem { get; set; }

        public List<TDto> RetrieveAllDocument<TDto>(IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = DocumentSystem.RetrieveAllDocument(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveDocumentsByUser<TDto>(object userId, IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = DocumentSystem.RetrieveDocumentsByUser(userId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewDocument<TDto>(object id, IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto result = DocumentSystem.RetrieveOrNewDocument(id, converter);
            UnitOfWork.CommitTransaction();
            return result;
        }

        public IFacadeUpdateResult<DocumentData> SaveDocument(DocumentDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DocumentData> result = DocumentSystem.SaveDocument(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<DocumentData> DeleteDocument(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DocumentData> result = DocumentSystem.DeleteDocument(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }
    }
}
