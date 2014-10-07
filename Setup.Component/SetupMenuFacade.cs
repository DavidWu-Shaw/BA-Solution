using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Data;

namespace Setup.Component
{
    public class SetupMenuFacade : BusinessComponent
    {
        public SetupMenuFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SetupMenuSystem = new SetupMenuSystem(unitOfWork);
        }

        private SetupMenuSystem SetupMenuSystem { get; set; }

        public IEnumerable<SetupMenuDto> RetrieveAllSetupMenu()
        {
            IEnumerable<SetupMenuDto> instances = SetupMenuSystem.RetrieveAllSetupMenu(new SetupMenuConverter());
            if (instances == null)
            {
                instances = new List<SetupMenuDto>();
            }
            return instances.OrderBy(o => o.Sort);
        }

        public SetupMenuDto RetrieveOrNewSetupMenu(object id)
        {
            return SetupMenuSystem.RetrieveOrNewSetupMenu(id, new SetupMenuConverter());
        }

        public IFacadeUpdateResult<SetupMenuData> SaveSetupMenu(SetupMenuDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SetupMenuData> result = SetupMenuSystem.SaveSetupMenu(dto);
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

        public IFacadeUpdateResult<SetupMenuData> DeleteSetupMenu(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SetupMenuData> result = SetupMenuSystem.DeleteSetupMenu(id);
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
            return SetupMenuSystem.GetBindingList();
        }
    }
}
