using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class SetupMenuSystem : BusinessComponent
    {
        public SetupMenuSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllSetupMenu<TDto>(IDataConverter<SetupMenuData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISetupMenuService service = UnitOfWork.GetService<ISetupMenuService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewSetupMenu<TDto>(object instanceId, IDataConverter<SetupMenuData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISetupMenuService service = UnitOfWork.GetService<ISetupMenuService>();
            FacadeUpdateResult<SetupMenuData> result = new FacadeUpdateResult<SetupMenuData>();
            SetupMenu instance = RetrieveOrNew<SetupMenuData, SetupMenu, ISetupMenuService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<SetupMenuData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<SetupMenuData> SaveSetupMenu(SetupMenuDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<SetupMenuData> result = new FacadeUpdateResult<SetupMenuData>();
            ISetupMenuService service = UnitOfWork.GetService<ISetupMenuService>();
            SetupMenu instance = RetrieveOrNew<SetupMenuData, SetupMenu, ISetupMenuService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.ParentMenuId = dto.ParentMenuId;
                instance.MenuText = dto.MenuText;
                instance.Tooltip = dto.Tooltip;
                instance.NavigateUrl = dto.NavigateUrl;
                instance.Sort = dto.Sort;
                instance.MenuTypeId = dto.MenuTypeId;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<SetupMenuData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<SetupMenuData> DeleteSetupMenu(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<SetupMenuData> result = new FacadeUpdateResult<SetupMenuData>();
            ISetupMenuService service = UnitOfWork.GetService<ISetupMenuService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                SetupMenu instance = query.ToBo<SetupMenu>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "SetupMenuCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ISetupMenuService service = UnitOfWork.GetService<ISetupMenuService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (SetupMenuData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
