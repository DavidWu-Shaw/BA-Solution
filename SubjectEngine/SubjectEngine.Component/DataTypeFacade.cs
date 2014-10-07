using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace SubjectEngine.Component
{
    public class DataTypeFacade : BusinessComponent
    {
        public DataTypeFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DataTypeSystem = new DataTypeSystem(unitOfWork);
        }

        private DataTypeSystem DataTypeSystem { get; set; }

        public IEnumerable<BindingListItem> GetBindingList()
        {
            return DataTypeSystem.GetBindingList();
        }
    }
}
