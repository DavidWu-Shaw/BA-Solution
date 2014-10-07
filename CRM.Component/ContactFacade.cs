using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ContactFacade : BusinessComponent
    {
        public ContactFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ContactSystem = new ContactSystem(unitOfWork);
        }

        private ContactSystem ContactSystem { get; set; }

        public List<TDto> RetrieveAllContact<TDto>(IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ContactSystem.RetrieveAllContact(converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveContactsByCustomer<TDto>(object id, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ContactSystem.RetrieveContactsByCustomer(id, converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveContactsByEmployee<TDto>(object id, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ContactSystem.RetrieveContactsByEmployee(id, converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewContact<TDto>(object id, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto contact = ContactSystem.RetrieveOrNewContact(id, converter);
            UnitOfWork.CommitTransaction();
            return contact;
        }

        public IFacadeUpdateResult<ContactData> SaveContact(ContactDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ContactData> result = ContactSystem.SaveContact(dto);
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

        public IFacadeUpdateResult<ContactData> SaveContacts(List<ContactDto> dtoList)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ContactData> result = ContactSystem.SaveContacts(dtoList);
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

        public IFacadeUpdateResult<ContactData> DeleteContact(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ContactData> result = ContactSystem.DeleteContact(id);
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

        public IFacadeUpdateResult<ContactData> DeleteContactContactMethod(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ContactData> result = ContactSystem.DeleteContactContactMethod(parentId, childId);
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

        public IFacadeUpdateResult<ContactData> SaveContactContactMethod(object parentId, ContactContactMethodDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ContactData> result = ContactSystem.SaveContactContactMethod(parentId, childDto);
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

        public IList<BindingListItem> GetBindingList()
        {
            UnitOfWork.BeginTransaction();
            IList<BindingListItem> result = ContactSystem.GetBindingList();
            UnitOfWork.CommitTransaction();

            return result;
        }
    }
}
