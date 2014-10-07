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
    public class MainMenuFacade : BusinessComponent
    {
        public MainMenuFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            MainMenuSystem = new MainMenuSystem(unitOfWork);
        }

        private MainMenuSystem MainMenuSystem { get; set; }

        public IEnumerable<MainMenuDto> RetrieveAllMainMenu()
        {
            IEnumerable<MainMenuDto> instances = MainMenuSystem.RetrieveAllMainMenu(new MainMenuConverter());
            if (instances == null)
            {
                instances = new List<MainMenuDto>();
            }
            return instances.OrderBy(o => o.Sort);
        }

        public MainMenuDto RetrieveOrNewMainMenu(object id)
        {
            return MainMenuSystem.RetrieveOrNewMainMenu(id, new MainMenuConverter());
        }

        public IFacadeUpdateResult<MainMenuData> SaveMainMenu(MainMenuDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<MainMenuData> result = MainMenuSystem.SaveMainMenu(dto);
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

        public IFacadeUpdateResult<MainMenuData> DeleteMainMenu(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<MainMenuData> result = MainMenuSystem.DeleteMainMenu(id);
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
            return MainMenuSystem.GetBindingList();
        }
    }
}
