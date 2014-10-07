using System.Collections.Generic;
using Framework.Component;
using Framework.UoW;
using SubjectEngine.Core;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;

namespace SubjectEngine.Component
{
    public class LanguageKeyFacade : BusinessComponent
    {
        public LanguageKeyFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Dictionary<string, string> GetSubjectLanguagePhrasesKeyValue()
        {
            // Create phrase keys with default values
            Dictionary<string, string> phrases = new Dictionary<string, string>();

            IServiceQueryResultList<SubjectData> query = UnitOfWork.GetService<ISubjectService>().GetAll();
            foreach (SubjectData item in query.DataList)
            {
                phrases.Add(string.Format(SubjectEngineConst.SubjectLabelKeyFormatString, item.Id), item.SubjectLabel);

                foreach (SubjectFieldData field in item.SubjectFieldsData)
                {
                    phrases.Add(string.Format(SubjectEngineConst.FieldLabelKeyFormatString, field.Id), field.FieldLabel);
                }

                foreach (SubjectChildListData childList in item.SubjectChildListsData)
                {
                    phrases.Add(string.Format(SubjectEngineConst.ChildListTitleKeyFormatString, childList.Id), childList.Title);
                }
            }

            return phrases;
        }
    }

}
