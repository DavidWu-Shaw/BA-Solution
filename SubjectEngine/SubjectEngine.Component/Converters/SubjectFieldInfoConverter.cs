using System.Collections.Generic;
using Framework.Core;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectFieldInfoConverter : IDataConverter<SubjectFieldInfoData, SubjectFieldInfoDto>
    {
        public IEnumerable<SubjectFieldInfoDto> Convert(IEnumerable<SubjectFieldInfoData> entitys)
        {
            List<SubjectFieldInfoDto> dtoList = new List<SubjectFieldInfoDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectFieldInfoDto Convert(SubjectFieldInfoData entity)
        {
            SubjectFieldInfoDto dto = new SubjectFieldInfoDto();

            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.TableColumn = entity.TableColumn;
            dto.SubjectFieldId = entity.SubjectFieldId;
            dto.FieldKey = entity.FieldKey;
            dto.FieldLabel = entity.FieldLabel;
            dto.HelpText = entity.HelpText;
            dto.FieldDataTypeId = entity.FieldDataTypeId;
            dto.PickupEntityId = entity.PickupEntityId;
            dto.LookupSubjectId = entity.LookupSubjectId;
            dto.IsRequired = entity.IsRequired;
            dto.IsLinkInGrid = entity.IsLinkInGrid;
            dto.IsReadonly = entity.IsReadonly;
            dto.IsShowInGrid = entity.IsShowInGrid;
            dto.Sort = entity.Sort;
            dto.RowIndex = entity.RowIndex;
            dto.ColIndex = entity.ColIndex;
            dto.MaxLengthInTable = entity.MaxLengthInTable;
            dto.MaxLength = entity.MaxLength;
            dto.NavigateUrlFormatString = entity.NavigateUrlFormatString;

            return dto;
        }
    }
}
