using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.Service.Contract;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    internal class SupplierSystem : BusinessComponent
    {
        public SupplierSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveSuppliersBySearch<TDto>(SupplierCriteria criteria, IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("criteria", criteria);
            ArgumentValidator.IsNotNull("converter", converter);

            ISupplierService service = UnitOfWork.GetService<ISupplierService>();

            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveSupplierInfo<TDto>(object SupplierId, IDataConverter<SupplierInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("SupplierId", SupplierId);
            ArgumentValidator.IsNotNull("converter", converter);

            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            var query = service.RetrieveSupplierInfo(SupplierId);

            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }

            return null;
        }
    }
}
