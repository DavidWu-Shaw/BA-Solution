using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class NewsSystem : BusinessComponent
    {
        public NewsSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllNews<TDto>(IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            INewsService service = UnitOfWork.GetService<INewsService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveNewssByUser<TDto>(object instanceId, IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            INewsService service = UnitOfWork.GetService<INewsService>();

            var query = service.SearchByUser(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewNews<TDto>(object instanceId, IDataConverter<NewsData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            INewsService service = UnitOfWork.GetService<INewsService>();
            FacadeUpdateResult<NewsData> result = new FacadeUpdateResult<NewsData>();
            News instance = RetrieveOrNew<NewsData, News, INewsService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<NewsData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<NewsData> SaveNews(NewsDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<NewsData> result = new FacadeUpdateResult<NewsData>();
            INewsService service = UnitOfWork.GetService<INewsService>();
            News instance = RetrieveOrNew<NewsData, News, INewsService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Title = dto.Title;
                instance.Content = dto.Content;
                instance.IssuedById = dto.IssuedById;
                instance.IssuedTime = dto.IssuedTime;
                instance.NewsGroupId = dto.NewsGroupId;
                instance.CategoryId = dto.CategoryId;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<NewsData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<NewsData> DeleteNews(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<NewsData> result = new FacadeUpdateResult<NewsData>();
            INewsService service = UnitOfWork.GetService<INewsService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                News instance = query.ToBo<News>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "NewsCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            INewsService service = UnitOfWork.GetService<INewsService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (NewsData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Title));
                }
            }

            return dataSource;
        }
    }
}
