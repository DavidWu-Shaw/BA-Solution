using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using General.Utility.Excel;
using Setup.Business;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    public class LanguageFacade : BusinessComponent
    {
        public LanguageFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            LanguageSystem = new LanguageSystem(unitOfWork);
        }

        private LanguageSystem LanguageSystem { get; set; }

        public IEnumerable<LanguageDto> RetrieveAllLanguage()
        {
            IEnumerable<LanguageDto> instances = LanguageSystem.RetrieveAllLanguage(new LanguageConverter());
            return instances;
        }

        // For multi-language key/value setup
        public Dictionary<object, LanguageDto> RetrieveLanguagesAndDataPhrases()
        {
            IEnumerable<LanguageDto> instances = LanguageSystem.RetrieveAllLanguage(new LanguageConverter());

            Dictionary<object, LanguageDto> languageDic = new Dictionary<object, LanguageDto>();
            // Attach LanguagePhrases
            DataPhraseFacade facade = new DataPhraseFacade(UnitOfWork);
            foreach (LanguageDto language in instances)
            {
                languageDic.Add(language.Id, language);
                // Data Phrases
                SortedDictionary<string, string> dataPhraseDic = new SortedDictionary<string, string>();
                foreach (DataPhraseDto phrase in facade.RetrieveDataPhrasesByLanguage(language.Id))
                {
                    dataPhraseDic.Add(phrase.PhraseKey, phrase.PhraseValue);
                }
                language.DataPhrases = dataPhraseDic;
            }

            return languageDic;
        }

        // For multi-language key/value setup
        public Dictionary<object, LanguageDto> RetrieveLanguagesAndSysPhrases()
        {
            IEnumerable<LanguageDto> instances = LanguageSystem.RetrieveAllLanguage(new LanguageConverter());

            Dictionary<object, LanguageDto> languageDic = new Dictionary<object, LanguageDto>();
            // Attach LanguagePhrases
            LanguagePhraseFacade facade = new LanguagePhraseFacade(UnitOfWork);
            foreach (LanguageDto language in instances)
            {
                languageDic.Add(language.Id, language);
                // UI level system Phrases
                SortedDictionary<string, string> sysPhraseDic = new SortedDictionary<string, string>();
                foreach (LanguagePhraseDto phrase in facade.RetrieveLanguagePhrasesByLanguage(language.Id))
                {
                    sysPhraseDic.Add(phrase.PhraseKey, phrase.PhraseValue);
                }
                language.SysPhrases = sysPhraseDic;
            }

            return languageDic;
        }

        // For key/value look up purpose
        public Dictionary<object, LanguageDto> RetrievePublishedLanguagesAndPhrases(bool isMultiLanguageSupported)
        {
            IEnumerable<LanguageDto> instances = LanguageSystem.RetrieveAllLanguage(new LanguageConverter());

            Dictionary<object, LanguageDto> languageDic = new Dictionary<object, LanguageDto>();
            // Attach LanguagePhrases
            LanguagePhraseFacade sysPhraseFacade = new LanguagePhraseFacade(UnitOfWork);
            DataPhraseFacade dataPhraseFacade = new DataPhraseFacade(UnitOfWork);
            foreach (LanguageDto language in instances.Where(o => o.IsPublished))
            {
                languageDic.Add(language.Id, language);

                if (isMultiLanguageSupported)
                {
                    // UI level system Phrases
                    SortedDictionary<string, string> sysPhraseDic = new SortedDictionary<string, string>();
                    foreach (LanguagePhraseDto phrase in sysPhraseFacade.RetrieveLanguagePhrasesByLanguage(language.Id))
                    {
                        sysPhraseDic.Add(phrase.PhraseKey, phrase.PhraseValue);
                    }
                    language.SysPhrases = sysPhraseDic;
                    // Data Phrases
                    SortedDictionary<string, string> dataPhraseDic = new SortedDictionary<string, string>();
                    foreach (DataPhraseDto phrase in dataPhraseFacade.RetrieveDataPhrasesByLanguage(language.Id))
                    {
                        dataPhraseDic.Add(phrase.PhraseKey, phrase.PhraseValue);
                    }
                    language.DataPhrases = dataPhraseDic;
                }
            }

            return languageDic;
        }

        public LanguageDto RetrieveOrNewLanguage(object id)
        {
            return LanguageSystem.RetrieveOrNewLanguage(id, new LanguageConverter());
        }

        public IFacadeUpdateResult<LanguageData> SaveLanguage(LanguageDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<LanguageData> result = LanguageSystem.SaveLanguage(dto);
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

        public IFacadeUpdateResult<LanguageData> DeleteLanguage(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<LanguageData> result = LanguageSystem.DeleteLanguage(id);
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
            return LanguageSystem.GetBindingList();
        }

        public IFacadeUpdateResult<DataPhraseData> SaveDataPhrases(object languageId, List<DataPhraseDto> dtoList)
        {
            FacadeUpdateResult<DataPhraseData> result = new FacadeUpdateResult<DataPhraseData>();
            // Retrieve phrases by languageId
            Dictionary<string, DataPhrase> phrases = new Dictionary<string, DataPhrase>();
            IDataPhraseService service = UnitOfWork.GetService<IDataPhraseService>();
            var query = service.SearchByLanguage(languageId);
            if (query.HasResult)
            {
                foreach (DataPhrase phrase in query.ToBoList<DataPhrase>())
                {
                    phrases.Add(phrase.PhraseKey, phrase);
                }
            }
            // Retrieve or Create instance
            List<DataPhrase> instances = new List<DataPhrase>();
            foreach (DataPhraseDto dto in dtoList)
            {
                DataPhrase instance = null;
                if (phrases.ContainsKey(dto.PhraseKey))
                {
                    instance = phrases[dto.PhraseKey];
                    instance.PhraseValue = dto.PhraseValue;
                }
                else
                {
                    instance = service.CreateNew<DataPhrase>();
                    instance.LanguageId = languageId;
                    instance.PhraseKey = dto.PhraseKey;
                    instance.PhraseValue = dto.PhraseValue;
                }
                instances.Add(instance);
            }
            // Save batch
            UnitOfWork.BeginTransaction();
            var saveQuery = service.SaveAll(instances);
            result.Merge(saveQuery);

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

        public IFacadeUpdateResult<LanguagePhraseData> SaveSystemPhrases(object languageId, List<LanguagePhraseDto> dtoList)
        {
            // Retrieve phrases by languageId
            Dictionary<string, LanguagePhrase> phrases = new Dictionary<string, LanguagePhrase>();
            ILanguagePhraseService service = UnitOfWork.GetService<ILanguagePhraseService>();
            var query = service.SearchByLanguage(languageId);
            if (query.HasResult)
            {
                foreach (LanguagePhrase phrase in query.ToBoList<LanguagePhrase>())
                {
                    phrases.Add(phrase.PhraseKey, phrase);
                }
            }

            UnitOfWork.BeginTransaction();
            FacadeUpdateResult<LanguagePhraseData> result = new FacadeUpdateResult<LanguagePhraseData>();
            List<LanguagePhrase> instances = new List<LanguagePhrase>();

            foreach (LanguagePhraseDto dto in dtoList)
            {
                LanguagePhrase instance = null;
                if (phrases.ContainsKey(dto.PhraseKey))
                {
                    instance = phrases[dto.PhraseKey];
                    instance.PhraseValue = dto.PhraseValue;
                }
                else
                {
                    instance = service.CreateNew<LanguagePhrase>();
                    instance.LanguageId = languageId;
                    instance.PhraseKey = dto.PhraseKey;
                    instance.PhraseValue = dto.PhraseValue;
                }
                instance.DateModified = DateTime.Now;
                instances.Add(instance);
            }

            var saveQuery = service.SaveAll(instances);
            result.Merge(saveQuery);

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

        public ExcelSheet CreateSystemPhraseSheet(object languageId)
        {
            Dictionary<object, LanguageDto> languages = RetrieveLanguagesAndSysPhrases();
            LanguageDto defaultLanguage = languages[languageId];

            ExcelSheet sheet = new ExcelSheet();
            // Create table schema
            sheet.Columns.Add("PhraseKey");
            foreach (LanguageDto language in languages.Values)
            {
                sheet.Columns.Add(language.Name);
            }
            // Fill in row data
            foreach (string key in defaultLanguage.SysPhrases.Keys)
            {
                ExcelSheetRow row = sheet.NewRow();
                sheet.Rows.Add(row);
                row[0].Value = key;
                int index = 1;
                foreach (LanguageDto language in languages.Values)
                {
                    string value = string.Empty;
                    if (language.SysPhrases.ContainsKey(key))
                    {
                        value = language.SysPhrases[key];
                    }
                    row[index].Value = value;
                    index++;
                }
            }

            return sheet;
        }

        public ExcelSheet GetMenuPhraseSheet()
        {
            Dictionary<string, string> menuPhrases = GetMenuPhraseKeyValue();
            return CreateDataPhraseSheet(menuPhrases);
        }

        public ExcelSheet CreateDataPhraseSheet(Dictionary<string, string> phrases)
        {
            Dictionary<object, LanguageDto> languages = RetrieveLanguagesAndDataPhrases();

            ExcelSheet sheet = new ExcelSheet();
            // Create table schema
            sheet.Columns.Add("PhraseKey");
            sheet.Columns.Add("DefaultValue");
            foreach (LanguageDto language in languages.Values)
            {
                sheet.Columns.Add(language.Name);
            }
            // Fill in row data
            foreach (string key in phrases.Keys)
            {
                ExcelSheetRow row = sheet.NewRow();
                sheet.Rows.Add(row);
                row[0].Value = key;
                row[1].Value = phrases[key];
                int index = 2;
                foreach (LanguageDto language in languages.Values)
                {
                    string value = string.Empty;
                    if (language.DataPhrases.ContainsKey(key))
                    {
                        value = language.DataPhrases[key];
                    }
                    row[index].Value = value;
                    index++;
                }
            }

            return sheet;
        }

        private Dictionary<string, string> GetMenuPhraseKeyValue()
        {
            // Create phrase keys with default values
            Dictionary<string, string> menuPhrases = new Dictionary<string, string>();

            IServiceQueryResultList<MainMenuData> mainMenus = UnitOfWork.GetService<IMainMenuService>().GetAll();
            IServiceQueryResultList<SetupMenuData> setupMenus = UnitOfWork.GetService<ISetupMenuService>().GetAll();
            foreach (MainMenuData item in mainMenus.DataList)
            {
                menuPhrases.Add(string.Format(SetupConst.MainMenuTextKeyFormatString, item.Id), item.MenuText);
            }
            foreach (SetupMenuData item in setupMenus.DataList)
            {
                menuPhrases.Add(string.Format(SetupConst.SetupMenuTextKeyFormatString, item.Id), item.MenuText);
            }

            return menuPhrases;
        }

        //// <language, <phraseKey, phraseValue>>
        //public Dictionary<string, Dictionary<string, string>> GetMenuPhrasesOfLanguages()
        //{
        //    Dictionary<string, Dictionary<string, string>> phrasesOfLanguages = new Dictionary<string, Dictionary<string, string>>();
        //    // Create phrase keys with default values
        //    Dictionary<string, string> menuPhrases = GetMenuPhrasesKeyValue();
        //    phrasesOfLanguages.Add("DefaultValue", menuPhrases);
        //    // Loop for all supported languages
        //    foreach (LanguageDto language in RetrieveLanguagesAndDataPhrases().Values)
        //    {
        //        Dictionary<string, string> languagePhrases = new Dictionary<string, string>();
        //        phrasesOfLanguages.Add(language.Name, languagePhrases);
        //        foreach (string key in menuPhrases.Keys)
        //        {
        //            string value = string.Empty;
        //            if (language.DataPhrases.ContainsKey(key))
        //            {
        //                value = language.DataPhrases[key];
        //            }
        //            languagePhrases.Add(key, value);
        //        }
        //    }
        //    return phrasesOfLanguages;
        //}
    }
}
