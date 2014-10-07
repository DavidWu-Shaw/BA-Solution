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
    internal class ShipToSystem : BusinessComponent
    {
        public ShipToSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllShipTo<TDto>(IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IShipToService service = UnitOfWork.GetService<IShipToService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveShipTosByCustomer<TDto>(object instanceId, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IShipToService service = UnitOfWork.GetService<IShipToService>();

            var query = service.SearchByCustomer(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveShipTosByContactPhone<TDto>(string phone, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("phone", phone);
            ArgumentValidator.IsNotNull("converter", converter);
            IShipToService service = UnitOfWork.GetService<IShipToService>();

            var query = service.SearchByContactPhone(phone);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewShipTo<TDto>(object instanceId, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IShipToService service = UnitOfWork.GetService<IShipToService>();
            FacadeUpdateResult<ShipToData> result = new FacadeUpdateResult<ShipToData>();
            ShipTo instance = RetrieveOrNew<ShipToData, ShipTo, IShipToService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ShipToData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ShipToData> SaveShipTo(ShipToDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ShipToData> result = new FacadeUpdateResult<ShipToData>();
            IShipToService service = UnitOfWork.GetService<IShipToService>();
            ShipTo instance = RetrieveOrNew<ShipToData, ShipTo, IShipToService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Code = dto.Code;
                instance.CustomerId = dto.CustomerId;
                instance.ContactPerson = dto.ContactPerson;
                instance.ContactPhone = dto.ContactPhone;
                instance.AddressLine1 = dto.AddressLine1;
                instance.AddressLine2 = dto.AddressLine2;
                instance.Country = dto.Country;
                instance.CountryState = dto.CountryState;
                instance.City = dto.City;
                instance.ZipCode = dto.ZipCode;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ShipToData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ShipToData> DeleteShipTo(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ShipToData> result = new FacadeUpdateResult<ShipToData>();
            IShipToService service = UnitOfWork.GetService<IShipToService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                ShipTo instance = query.ToBo<ShipTo>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ShipToCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IShipToService service = UnitOfWork.GetService<IShipToService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ShipToData data in query.DataList)
                {
                    string shipToDisplay = string.Format("{0}, {1}, {2}", data.ContactPhone, data.ContactPerson, data.AddressLine1);
                    dataSource.Add(new BindingListItem(data.Id, shipToDisplay));
                }
            }

            return dataSource;
        }
    }
}
