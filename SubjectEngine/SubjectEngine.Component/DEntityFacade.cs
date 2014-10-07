using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using SubjectEngine.Component.Converters;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component
{
    public class DEntityFacade : BusinessComponent
    {
        public DEntityFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DEntitySystem = new DEntitySystem(unitOfWork);
        }

        private DEntitySystem DEntitySystem { get; set; }

        public IEnumerable<DEntityDto> RetrieveAllDEntity()
        {
            UnitOfWork.BeginTransaction();
            IEnumerable<DEntityDto> result = DEntitySystem.RetrieveAllDEntity(new DEntityConverter());
            UnitOfWork.CommitTransaction();
            return result;
        }

        public DEntityDto RetrieveDEntity(object entityId)
        {
            UnitOfWork.BeginTransaction();
            DEntityDto result = DEntitySystem.RetrieveDEntity<DEntityDto>(entityId, new DEntityConverter());
            UnitOfWork.CommitTransaction();
            return result;
        }

        public DEntityDto RetrieveOrNewDEntity(object id)
        {
            UnitOfWork.BeginTransaction();
            DEntityDto result = DEntitySystem.RetrieveOrNewDEntity(id, new DEntityConverter());
            UnitOfWork.CommitTransaction();
            return result;
        }

        public IFacadeUpdateResult<DEntityData> SaveDEntity(DEntityDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DEntityData> result = DEntitySystem.SaveDEntity(dto);
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

        public IFacadeUpdateResult<DEntityData> SaveDEntityItem(object entityId, DEntityItemDto item)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DEntityData> result = DEntitySystem.SaveDEntityItem(entityId, item);
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

        public IFacadeUpdateResult<DEntityData> DeleteDEntityItem(object entityId, object itemId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<DEntityData> result = DEntitySystem.DeleteDEntityItem(entityId, itemId);
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
            return DEntitySystem.GetBindingList();
        }

        public IList<BindingListItem> GetEntityItemList(object entityId)
        {
            IList<BindingListItem> result = DEntitySystem.GetEntityItemList(entityId);

            return result;
        }
    }
}
