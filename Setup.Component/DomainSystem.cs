using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class DomainSystem : BusinessComponent
    {
        public DomainSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllDomain<TDto>(IDataConverter<DomainData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IDomainService service = UnitOfWork.GetService<IDomainService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewDomain<TDto>(object instanceId, IDataConverter<DomainData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            Domain instance = RetrieveOrNew<DomainData, Domain, IDomainService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<DomainData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<DomainData> SaveDomain(DomainDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            Domain instance = RetrieveOrNew<DomainData, Domain, IDomainService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.RelatedSubjectIdField = dto.RelatedSubjectIdField;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<DomainData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<DomainData> DeleteDomain(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Domain instance = query.ToBo<Domain>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "DomainCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<DomainData> SaveDomainMainMenu(object parentId, DomainMainMenuDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                Domain parent = parentQuery.ToBo<Domain>();
                DomainMainMenu child = RetrieveOrNewDomainMainMenu(parent, childDto.Id);
                if (child != null)
                {
                    child.MainMenuId = childDto.MainMenuId;
                    child.Sort = childDto.Sort;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DomainData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DomainMainMenuCannotBeFound");
                }
            }

            return result;
        }

        internal IFacadeUpdateResult<DomainData> DeleteDomainMainMenu(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Domain parent = query.ToBo<Domain>();
                DomainMainMenu child = parent.DomainMainMenus.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.DomainMainMenus.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DomainData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DomainMainMenuCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "DomainCannotBeFound");
            }

            return result;
        }

        internal DomainMainMenu RetrieveOrNewDomainMainMenu(Domain parent, object childId)
        {
            DomainMainMenu child = null;

            if (childId != null)
            {
                child = parent.DomainMainMenus.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.DomainMainMenus.AddNewBo();
            }

            return child;
        }

        internal IFacadeUpdateResult<DomainData> SaveDomainSetupMenu(object parentId, DomainSetupMenuDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                Domain parent = parentQuery.ToBo<Domain>();
                DomainSetupMenu child = RetrieveOrNewDomainSetupMenu(parent, childDto.Id);
                if (child != null)
                {
                    child.SetupMenuId = childDto.SetupMenuId;
                    child.Sort = childDto.Sort;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DomainData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DomainSetupMenuCannotBeFound");
                }
            }

            return result;
        }

        internal IFacadeUpdateResult<DomainData> DeleteDomainSetupMenu(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<DomainData> result = new FacadeUpdateResult<DomainData>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Domain parent = query.ToBo<Domain>();
                DomainSetupMenu child = parent.DomainSetupMenus.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.DomainSetupMenus.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<DomainData>());
                }
                else
                {
                    AddError(result.ValidationResult, "DomainSetupMenuCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "DomainCannotBeFound");
            }

            return result;
        }

        internal DomainSetupMenu RetrieveOrNewDomainSetupMenu(Domain parent, object childId)
        {
            DomainSetupMenu child = null;

            if (childId != null)
            {
                child = parent.DomainSetupMenus.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.DomainSetupMenus.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDomainService service = UnitOfWork.GetService<IDomainService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (DomainData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
