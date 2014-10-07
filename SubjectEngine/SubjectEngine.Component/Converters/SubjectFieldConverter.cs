using System;
using System.Collections.Generic;
using Framework.UoW;
using SubjectEngine.Business;
using SubjectEngine.Component.Dto;
using SubjectEngine.Core;
using SubjectEngine.Data;

namespace SubjectEngine.Component.Converters
{
    public sealed class SubjectFieldConverter : IBusinessObjectConverter<SubjectField, SubjectFieldDto>
    {
        public IEnumerable<SubjectFieldDto> Convert(IEnumerable<SubjectField> entitys)
        {
            List<SubjectFieldDto> dtoList = new List<SubjectFieldDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public SubjectFieldDto Convert(SubjectField entity)
        {
            SubjectFieldDto dto = new SubjectFieldDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.FieldKey = entity.FieldKey;
            dto.FieldLabel = entity.FieldLabel;
            dto.HelpText = entity.HelpText;
            dto.FieldDataTypetId = entity.FieldDataType.Value.Id;
            dto.DucType = (DucTypes)Enum.Parse(typeof(DucTypes), entity.FieldDataType.Value.DucType);
            dto.PickupEntityId = entity.PickupEntityId;
            dto.LookupSubjectId = entity.LookupSubjectId;
            dto.IsRequired = entity.IsRequired;
            dto.IsLinkInGrid = entity.IsLinkInGrid;
            dto.IsReadonly = entity.IsReadonly;
            dto.IsShowInGrid = entity.IsShowInGrid;
            dto.Sort = entity.Sort;
            dto.RowIndex = entity.RowIndex;
            dto.ColIndex = entity.ColIndex;
            dto.MaxLength = entity.MaxLength;
            dto.NavigateUrlFormatString = entity.NavigateUrlFormatString;

            dto.SubjectFieldLanguages = new SubjectFieldLanguageConverter().Convert(entity.RetrieveData<SubjectFieldData>().SubjectFieldLanguagesData);
            // Prepare dictionary
            dto.SubjectFieldLanguagesDic = new SortedDictionary<object, SubjectFieldLanguageDto>();
            foreach (SubjectFieldLanguageDto item in dto.SubjectFieldLanguages)
            {
                dto.SubjectFieldLanguagesDic.Add(item.LanguageId, item);
            }

            return dto;
        }
    }
}
