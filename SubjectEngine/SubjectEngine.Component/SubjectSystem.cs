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
    internal class SubjectSystem : BusinessComponent
    {
        public SubjectSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllSubject<TDto>(IBusinessObjectConverter<Subject, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);

            ISubjectService service = UnitOfWork.GetService<ISubjectService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.BoToDtoList(converter);
            }

            return null;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ISubjectService service = UnitOfWork.GetService<ISubjectService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (SubjectData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.SubjectType));
                }
            }

            return dataSource;
        }

        internal TDto RetrieveSubject<TDto>(object subjectId, IBusinessObjectConverter<Subject, TDto> converter)
            where TDto : BaseDto
        {
            ArgumentValidator.IsNotNull("subjectId", subjectId);
            ArgumentValidator.IsNotNull("converter", converter);

            ISubjectService service = UnitOfWork.GetService<ISubjectService>();
            var query = service.Retrieve(subjectId);
            if (query.HasResult)
            {
                return query.BoToDto(converter);
            }

            return null;
        }

        internal TDto RetrieveSubjectByType<TDto>(string subjectType, IBusinessObjectConverter<Subject, TDto> converter)
            where TDto : BaseDto
        {
            ArgumentValidator.IsNotNull("subjectType", subjectType);
            ArgumentValidator.IsNotNull("converter", converter);

            ISubjectService service = UnitOfWork.GetService<ISubjectService>();

            var query = service.RetrieveBySubjectType(subjectType);

            if (query.HasResult)
            {
                return query.BoToDto(converter);
            }

            return null;
        }

        //internal string RetrieveSubjectManageUrlFormatString(object subjectId)
        //{
        //    ArgumentValidator.IsNotNull("subjectId", subjectId);
        //    ISubjectService service = UnitOfWork.GetService<ISubjectService>();
        //    var query = service.Retrieve(subjectId);
        //    if (query.HasResult)
        //    {
        //        return query.Data.ManageUrl;
        //    }
        //    return null;
        //}

        //internal string RetrieveSubjectType(object subjectId)
        //{
        //    ArgumentValidator.IsNotNull("subjectId", subjectId);
        //    ISubjectService service = UnitOfWork.GetService<ISubjectService>();
        //    var query = service.Retrieve(subjectId);
        //    if (query.HasResult)
        //    {
        //        return query.Data.SubjectType;
        //    }
        //    return null;
        //}

        internal IEnumerable<SubjectFieldInfoDto> RetrieveSubjectFieldInfos(object subjectId, IDataConverter<SubjectFieldInfoData, SubjectFieldInfoDto> converter)
        {
            ArgumentValidator.IsNotNull("subjectId", subjectId);
            ArgumentValidator.IsNotNull("converter", converter);

            ISubjectService service = UnitOfWork.GetService<ISubjectService>();

            IEnumerable<SubjectFieldInfoData> result = service.GetSubjectFieldInfosData(subjectId);
            if (result != null)
            {
                return converter.Convert(result);
            }

            return null;
        }

        internal IFacadeUpdateResult<SubjectData> SaveSubjectField(object subjectId, SubjectFieldInfoDto fieldInfoDto)
        {
            ArgumentValidator.IsNotNull("fieldInfoDto", fieldInfoDto);
            ArgumentValidator.IsNotNull("subjectId", subjectId);

            FacadeUpdateResult<SubjectData> result = new FacadeUpdateResult<SubjectData>();

            ISubjectService subjectService = UnitOfWork.GetService<ISubjectService>();
            var subjectQuery = subjectService.Retrieve(subjectId);
            if (!subjectQuery.HasResult)
            {
                AddError(result.ValidationResult, "SubjectCannotBeFound");
                return result;
            }

            IDataTypeService dataTypeService = UnitOfWork.GetService<IDataTypeService>();
            int dataTypeId = System.Convert.ToInt32(fieldInfoDto.FieldDataTypeId);
            var dataTypeQuery = dataTypeService.Retrieve(dataTypeId);
            if (!dataTypeQuery.HasResult)
            {
                AddError(result.ValidationResult, "DataTypeCannotBeFound");
                return result;
            }

            Subject subject = subjectQuery.ToBo<Subject>();
            SubjectField subjectField = RetrieveOrNewSubjectField(subject, fieldInfoDto.SubjectFieldId);
            if (subjectField == null)
            {
                AddWarning(result.ValidationResult, "SubjectFieldCannotBeFound");
                return result;
            }

            subjectField.FieldKey = fieldInfoDto.FieldKey;
            subjectField.FieldLabel = fieldInfoDto.FieldLabel;
            subjectField.HelpText = fieldInfoDto.HelpText;
            subjectField.FieldDataType.Value = dataTypeQuery.ToBo<DataType>();
            subjectField.PickupEntityId = fieldInfoDto.PickupEntityId;
            subjectField.LookupSubjectId = fieldInfoDto.LookupSubjectId;
            subjectField.Sort = fieldInfoDto.Sort;
            subjectField.RowIndex = fieldInfoDto.RowIndex;
            subjectField.ColIndex = fieldInfoDto.ColIndex;
            subjectField.IsRequired = fieldInfoDto.IsRequired;
            subjectField.IsReadonly = fieldInfoDto.IsReadonly;
            subjectField.IsLinkInGrid = fieldInfoDto.IsLinkInGrid;
            subjectField.IsShowInGrid = fieldInfoDto.IsShowInGrid;
            if (fieldInfoDto.MaxLength.HasValue)
            {
                subjectField.MaxLength = fieldInfoDto.MaxLength;
            }
            else
            {
                subjectField.MaxLength = fieldInfoDto.MaxLengthInTable;
            }
            subjectField.NavigateUrlFormatString = fieldInfoDto.NavigateUrlFormatString;

            var saveQuery = subjectService.Save(subject);
            result.Merge(saveQuery);
            result.AttachResult(subject.RetrieveData<SubjectData>());

            return result;
        }

        internal IFacadeUpdateResult<SubjectData> DeleteSubjectField(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("childId", childId);
            ArgumentValidator.IsNotNull("parentId", parentId);

            FacadeUpdateResult<SubjectData> result = new FacadeUpdateResult<SubjectData>();
            ISubjectService service = UnitOfWork.GetService<ISubjectService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Subject parent = query.ToBo<Subject>();
                SubjectField child = parent.SubjectFields.SingleOrDefault(o => o.Id.Equals(childId));
                if (child != null)
                {
                    parent.SubjectFields.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<SubjectData>());
                }
                else
                {
                    AddError(result.ValidationResult, "SubjectFieldCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "SubjectCannotBeFound");
            }

            return result;
        }

        internal SubjectField RetrieveOrNewSubjectField(Subject subject, object subjectFieldId)
        {
            SubjectField subjectField = null;

            if (subjectFieldId != null)
            {
                subjectField = subject.SubjectFields.SingleOrDefault(o => o.Id.Equals(subjectFieldId));
            }
            else
            {
                subjectField = subject.SubjectFields.AddNewBo();
            }

            return subjectField;
        }

        internal IFacadeUpdateResult<SubjectData> DeleteSubjectChildList(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<SubjectData> result = new FacadeUpdateResult<SubjectData>();
            ISubjectService service = UnitOfWork.GetService<ISubjectService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Subject parent = query.ToBo<Subject>();
                SubjectChildList child = parent.SubjectChildLists.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.SubjectChildLists.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<SubjectData>());
                }
                else
                {
                    AddError(result.ValidationResult, "SubjectChildListCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "SubjectCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<SubjectData> SaveSubjectChildList(object parentId, SubjectChildListDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<SubjectData> result = new FacadeUpdateResult<SubjectData>();
            ISubjectService projectService = UnitOfWork.GetService<ISubjectService>();
            var projectQuery = projectService.Retrieve(parentId);
            if (projectQuery.HasResult)
            {
                Subject parent = projectQuery.ToBo<Subject>();
                SubjectChildList child = RetrieveOrNewSubjectChildList(parent, childDto.Id);
                if (child != null)
                {
                    child.ChildListSubjectId = childDto.ChildListSubjectId;
                    child.Title = childDto.Title;
                    child.AllowAdd = childDto.AllowAdd;
                    child.AllowEdit = childDto.AllowEdit;
                    child.AllowDelete = childDto.AllowDelete;
                    child.AllowFiltering = childDto.AllowFiltering;
                    child.Sort = childDto.Sort;
                    child.AllowImport = childDto.AllowImport;
                    child.ImportUrl = childDto.ImportUrl;

                    var saveQuery = projectService.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<SubjectData>());
                }
                else
                {
                    AddError(result.ValidationResult, "SubjectChildListCannotBeFound");
                }
            }

            return result;
        }

        internal SubjectChildList RetrieveOrNewSubjectChildList(Subject parent, object childId)
        {
            SubjectChildList child = null;

            if (childId != null)
            {
                child = parent.SubjectChildLists.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.SubjectChildLists.AddNewBo();
            }

            return child;
        }
    }
}
