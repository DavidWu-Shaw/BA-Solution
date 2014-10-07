using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class NewsFacade : BusinessComponent
    {
        public NewsFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            NewsSystem = new NewsSystem(unitOfWork);
        }

        private NewsSystem NewsSystem { get; set; }

        public List<TDto> RetrieveAllNews<TDto>(IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = NewsSystem.RetrieveAllNews(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveNewssByUser<TDto>(object userId, IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = NewsSystem.RetrieveNewssByUser(userId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewNews<TDto>(object id, IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto result = NewsSystem.RetrieveOrNewNews(id, converter);
            UnitOfWork.CommitTransaction();
            return result;
        }

        public IFacadeUpdateResult<NewsData> SaveNews(NewsDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<NewsData> result = NewsSystem.SaveNews(dto);
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

        public IFacadeUpdateResult<NewsData> DeleteNews(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<NewsData> result = NewsSystem.DeleteNews(id);
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
    }
}
