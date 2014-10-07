using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;

namespace Setup.Component
{
    public class DomainFacade : BusinessComponent
    {
        public DomainFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DomainSystem = new DomainSystem(unitOfWork);
            MainMenuSystem = new MainMenuSystem(unitOfWork);
            SetupMenuSystem = new SetupMenuSystem(unitOfWork);
        }

        private DomainSystem DomainSystem { get; set; }
        private MainMenuSystem MainMenuSystem { get; set; }
        private SetupMenuSystem SetupMenuSystem { get; set; }

        public IEnumerable<DomainDto> RetrieveAllDomain()
        {
            IEnumerable<DomainDto> instances = DomainSystem.RetrieveAllDomain(new DomainConverter());
            if (instances == null)
            {
                instances = new List<DomainDto>();
            }
            return instances;
        }

        public DomainDto RetrieveOrNewDomain(object id)
        {
            return DomainSystem.RetrieveOrNewDomain(id, new DomainConverter());
        }

        public IFacadeUpdateResult<DomainData> SaveDomain(DomainDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.SaveDomain(dto);
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

        public IFacadeUpdateResult<DomainData> DeleteDomain(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.DeleteDomain(id);
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

        public IFacadeUpdateResult<DomainData> SaveDomainMainMenu(object parentId, DomainMainMenuDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.SaveDomainMainMenu(parentId, childDto);
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

        public IFacadeUpdateResult<DomainData> DeleteDomainMainMenu(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.DeleteDomainMainMenu(parentId, childId);
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

        public IFacadeUpdateResult<DomainData> SaveDomainSetupMenu(object parentId, DomainSetupMenuDto childDto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.SaveDomainSetupMenu(parentId, childDto);
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

        public IFacadeUpdateResult<DomainData> DeleteDomainSetupMenu(object parentId, object childId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DomainData> result = DomainSystem.DeleteDomainSetupMenu(parentId, childId);
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
            return DomainSystem.GetBindingList();
        }

        public Dictionary<object, DomainSetting> GetDomainSettingList()
        {
            Dictionary<object, DomainSetting> domainList = new Dictionary<object, DomainSetting>();

            IEnumerable<DomainDto> domains = DomainSystem.RetrieveAllDomain(new DomainConverter());

            // Prepare Id ordered menu list for looking up
            Dictionary<object, MainMenuDto> mainMenuDic = new Dictionary<object, MainMenuDto>();
            IEnumerable<MainMenuDto> allMainMenus = MainMenuSystem.RetrieveAllMainMenu(new MainMenuConverter());
            foreach (MainMenuDto item in allMainMenus)
            {
                mainMenuDic.Add(item.Id, item);
            }

            Dictionary<object, SetupMenuDto> setupMenuDic = new Dictionary<object, SetupMenuDto>();
            IEnumerable<SetupMenuDto> allSetupMenus = SetupMenuSystem.RetrieveAllSetupMenu(new SetupMenuConverter());
            foreach (SetupMenuDto item in allSetupMenus)
            {
                setupMenuDic.Add(item.Id, item);
            }

            foreach (DomainDto domain in domains)
            {
                DomainSetting domainSetting = new DomainSetting();
                domainList.Add(domain.Id, domainSetting);
                domainSetting.DomainId = domain.Id;
                domainSetting.Name = domain.Name;
                domainSetting.DefaultUrl = domain.DefaultUrl;
                domainSetting.RelatedSubjectIdField = domain.RelatedSubjectIdField;
                IList<MainMenuDto> mainMenus = new List<MainMenuDto>();
                foreach (DomainMainMenuDto item in domain.DomainMainMenus)
                {
                    mainMenus.Add(mainMenuDic[item.MainMenuId]);
                }
                if (domainSetting.DefaultUrl.Trim().Length == 0 && mainMenus.Count > 0)
                {
                    domainSetting.DefaultUrl = mainMenus[0].NavigateUrl;
                }
                domainSetting.MainMenus = mainMenus;

                IList<SetupMenuDto> setupMenus = new List<SetupMenuDto>();
                foreach (DomainSetupMenuDto item in domain.DomainSetupMenus)
                {
                    setupMenus.Add(setupMenuDic[item.SetupMenuId]);
                }
                domainSetting.SetupMenus = setupMenus;
            }

            // Add SuperAdmin items
            DomainSetting superSetting = new DomainSetting();
            superSetting.DomainId = (int)UserDomains.SuperAdmin;
            superSetting.MainMenus = allMainMenus;
            superSetting.SetupMenus = allSetupMenus;
            if (allMainMenus.Count <MainMenuDto>() > 0)
            {
                superSetting.DefaultUrl = allMainMenus.First().NavigateUrl;
            }
            domainList.Add(superSetting.DomainId, superSetting);

            return domainList;
        }
    }
}
