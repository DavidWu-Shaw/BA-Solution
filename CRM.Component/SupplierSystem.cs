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
    internal class SupplierSystem : BusinessComponent
    {
        public SupplierSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllSupplier<TDto>(IDataConverter<SupplierData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISupplierService service = UnitOfWork.GetService<ISupplierService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewSupplier<TDto>(object instanceId, IDataConverter<SupplierData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            FacadeUpdateResult<SupplierData> result = new FacadeUpdateResult<SupplierData>();
            Supplier instance = RetrieveOrNew<SupplierData, Supplier, ISupplierService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<SupplierData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<SupplierData> SaveSupplier(SupplierDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<SupplierData> result = new FacadeUpdateResult<SupplierData>();
            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            Supplier instance = RetrieveOrNew<SupplierData, Supplier, ISupplierService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                TransferData(dto, instance);
                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<SupplierData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<SupplierData> DeleteSupplier(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<SupplierData> result = new FacadeUpdateResult<SupplierData>();
            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Supplier instance = query.ToBo<Supplier>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "SupplierCannotBeFound");
            }

            return result;
        }

        private void TransferData(SupplierDto dto, Supplier instance)
        {
            instance.Name = dto.Name;
            instance.AddressLine1 = dto.AddressLine1;
            instance.AddressLine2 = dto.AddressLine2;
            instance.Country = dto.Country;
            instance.CountryState = dto.CountryState;
            instance.City = dto.City;
            instance.ZipCode = dto.ZipCode;
            instance.ContactPerson = dto.ContactPerson;
            instance.ContactPhone = dto.ContactPhone;
            instance.ContactEmail = dto.ContactEmail;
            instance.ContactFax = dto.ContactFax;
            instance.CategoryId = dto.CategoryId;
            instance.NationId = dto.NationId;
            instance.AllowTakeOut = dto.AllowTakeOut;
            instance.AllowReserve = dto.AllowReserve;
            instance.AllowBringWine = dto.AllowBringWine;
            instance.DayStartTime = dto.DayStartTime;
            instance.DayEndTime = dto.DayEndTime;
            instance.Grade = dto.Grade;
            instance.Logo = dto.Logo;
            instance.Website = dto.Website;
            instance.ProductCatalogId = dto.ProductCatalogId;
        }

        internal FacadeUpdateResult<SupplierData> SaveSuppliers(List<SupplierDto> dtoList)
        {
            ArgumentValidator.IsNotNull("dtoList", dtoList);

            FacadeUpdateResult<SupplierData> result = new FacadeUpdateResult<SupplierData>();

            List<Supplier> instances = new List<Supplier>();
            foreach (SupplierDto dto in dtoList)
            {
                Supplier instance = RetrieveOrNew<SupplierData, Supplier, ISupplierService>(result.ValidationResult, dto.Id);
                if (!result.IsSuccessful)
                {
                    break;
                }
                TransferData(dto, instance);
                instances.Add(instance);
            }

            if (result.IsSuccessful)
            {
                ISupplierService service = UnitOfWork.GetService<ISupplierService>();

                var saveQuery = service.SaveAll(instances);
                result.Merge(saveQuery);

                List<SupplierData> dataList = new List<SupplierData>();
                foreach (Supplier instance in instances)
                {
                    dataList.Add(instance.RetrieveData<SupplierData>());
                }
                result.AttachResult(dataList);
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (SupplierData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
