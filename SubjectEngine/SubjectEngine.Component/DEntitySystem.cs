using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using SubjectEngine.Business;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;

namespace SubjectEngine.Component
{
    internal class DEntitySystem : BusinessComponent
    {
        public DEntitySystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllDEntity<TDto>(IDataConverter<DEntityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);

            IDEntityService service = UnitOfWork.GetService<IDEntityService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal IFacadeUpdateResult<DEntityData> SaveDEntity(DEntityDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<DEntityData> result = new FacadeUpdateResult<DEntityData>();
            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            DEntity instance = RetrieveOrNew<DEntityData, DEntity, IDEntityService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Code = dto.Code;
                instance.Description = dto.Description;
                instance.EntityTypeId = dto.EntityTypeId;
                instance.AllowAddItem = dto.AllowAddItem;
                instance.AllowDeleteItem = dto.AllowDeleteItem;
                instance.AllowEditItem = dto.AllowEditItem;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<DEntityData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (DEntityData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Code));
                }
            }

            return dataSource;
        }

        internal TDto RetrieveOrNewDEntity<TDto>(object instanceId, IDataConverter<DEntityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);

            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            FacadeUpdateResult<DEntityData> result = new FacadeUpdateResult<DEntityData>();
            DEntity instance = RetrieveOrNew<DEntityData, DEntity, IDEntityService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<DEntityData>());
            }
            else
            {
                return null;
            }
        }

        internal TDto RetrieveDEntity<TDto>(object entityId, IDataConverter<DEntityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("entityId", entityId);
            ArgumentValidator.IsNotNull("converter", converter);

            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            var query = service.Retrieve(entityId);
            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<DEntityData> DeleteDEntityItem(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<DEntityData> result = new FacadeUpdateResult<DEntityData>();
            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                DEntity parent = query.ToBo<DEntity>();
                DEntityItem child = parent.DEntityItems.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.DEntityItems.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DEntityData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DEntityItemCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "DEntityCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<DEntityData> SaveDEntityItem(object parentId, DEntityItemDto childDto)
        {            
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<DEntityData> result = new FacadeUpdateResult<DEntityData>();
            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                DEntity parent = parentQuery.ToBo<DEntity>();
                DEntityItem child = RetrieveOrNewDEntityItem(parent, childDto.Id);
                if (child != null)
                {
                    child.Text = childDto.Text;
                    child.Value = childDto.Value;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DEntityData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DEntityItemCannotBeFound");
                }
            }

            return result;
        }

        internal DEntityItem RetrieveOrNewDEntityItem(DEntity parent, object childId)
        {
            DEntityItem child = null;

            if (childId != null)
            {
                child = parent.DEntityItems.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.DEntityItems.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetEntityItemList(object entityId)
        {
            ArgumentValidator.IsNotNull("entityId", entityId);

            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDEntityService service = UnitOfWork.GetService<IDEntityService>();
            var query = service.Retrieve(entityId);
            if (query.HasResult)
            {
                foreach (DEntityItemData item in query.Data.DEntityItemsData)
                {
                    dataSource.Add(new BindingListItem(item.Value, item.Text));
                }
            }

            return dataSource;
        }
    }
}
