using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;

namespace SubjectEngine.Component
{
    internal class DataTypeSystem : BusinessComponent
    {
        public DataTypeSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDataTypeService service = UnitOfWork.GetService<IDataTypeService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (DataTypeData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
