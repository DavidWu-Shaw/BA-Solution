using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class ContactSystem : BusinessComponent
    {
        public ContactSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllContact<TDto>(IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IContactService service = UnitOfWork.GetService<IContactService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveContactsByCustomer<TDto>(object customerId, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("customerId", customerId);
            ArgumentValidator.IsNotNull("converter", converter);

            IContactService service = UnitOfWork.GetService<IContactService>();
            var query = service.SearchByCustomer(customerId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveContactsByEmployee<TDto>(object employeeId, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("EmployeeId", employeeId);
            ArgumentValidator.IsNotNull("converter", converter);

            IContactService service = UnitOfWork.GetService<IContactService>();
            var query = service.SearchByEmployee(employeeId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewContact<TDto>(object instanceId, IDataConverter<ContactData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IContactService service = UnitOfWork.GetService<IContactService>();
            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();
            Contact instance = RetrieveOrNew<ContactData, Contact, IContactService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ContactData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ContactData> SaveContact(ContactDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();
            IContactService service = UnitOfWork.GetService<IContactService>();
            Contact instance = RetrieveOrNew<ContactData, Contact, IContactService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                TransferData(dto, instance);

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ContactData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        private void TransferData(ContactDto dto, Contact instance)
        {
            instance.EmployeeId = dto.EmployeeId;
            instance.CustomerId = dto.CustomerId;
            instance.CategoryId = dto.CategoryId;
            instance.FullName = dto.FullName;
            instance.FamilyName = dto.FamilyName;
            instance.Gender = dto.Gender;
            instance.AddressLine1 = dto.AddressLine1;
            instance.AddressLine2 = dto.AddressLine2;
            instance.Country = dto.Country;
            instance.CountryState = dto.CountryState;
            instance.City = dto.City;
            instance.ZipCode = dto.ZipCode;
            instance.Phone = dto.Phone;
            instance.Fax = dto.Fax;
            instance.Email = dto.Email;
        }

        internal FacadeUpdateResult<ContactData> SaveContacts(List<ContactDto> dtoList)
        {
            ArgumentValidator.IsNotNull("dtoList", dtoList);

            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();

            List<Contact> instances = new List<Contact>();
            foreach (ContactDto dto in dtoList)
            {
                Contact instance = RetrieveOrNew<ContactData, Contact, IContactService>(result.ValidationResult, dto.Id);
                if (!result.IsSuccessful)
                {
                    break;
                }
                TransferData(dto, instance);
                instances.Add(instance);
            }

            if (result.IsSuccessful)
            {
                IContactService service = UnitOfWork.GetService<IContactService>();

                var saveQuery = service.SaveAll(instances);
                result.Merge(saveQuery);

                List<ContactData> dataList = new List<ContactData>();
                foreach (Contact instance in instances)
                {
                    dataList.Add(instance.RetrieveData<ContactData>());
                }
                result.AttachResult(dataList);
            }

            return result;
        }

        internal IFacadeUpdateResult<ContactData> DeleteContact(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();
            IContactService service = UnitOfWork.GetService<IContactService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Contact instance = query.ToBo<Contact>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ContactCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<ContactData> DeleteContactContactMethod(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();
            IContactService service = UnitOfWork.GetService<IContactService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Contact parent = query.ToBo<Contact>();
                ContactContactMethod child = parent.ContactContactMethods.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.ContactContactMethods.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<ContactData>());
                }
                else
                {
                    AddError(result.ValidationResult, "ContactContactMethodCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "ContactCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<ContactData> SaveContactContactMethod(object parentId, ContactContactMethodDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<ContactData> result = new FacadeUpdateResult<ContactData>();
            IContactService projectService = UnitOfWork.GetService<IContactService>();
            var projectQuery = projectService.Retrieve(parentId);
            if (projectQuery.HasResult)
            {
                Contact parent = projectQuery.ToBo<Contact>();
                ContactContactMethod child = RetrieveOrNewContactContactMethod(parent, childDto.Id);
                if (child != null)
                {
                    child.ContactMethodTypeId = childDto.ContactMethodTypeId;
                    child.ContactMethodValue = childDto.ContactMethodValue;

                    var saveQuery = projectService.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<ContactData>());
                }
                else
                {
                    AddError(result.ValidationResult, "ContactContactMethodCannotBeFound");
                }
            }

            return result;
        }

        internal ContactContactMethod RetrieveOrNewContactContactMethod(Contact parent, object childId)
        {
            ContactContactMethod child = null;

            if (childId != null)
            {
                child = parent.ContactContactMethods.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.ContactContactMethods.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IContactService service = UnitOfWork.GetService<IContactService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ContactData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.FullName));
                }
            }

            return dataSource;
        }
    }
}
