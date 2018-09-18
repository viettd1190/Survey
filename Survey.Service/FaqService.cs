using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Model.Abstract;
using Survey.Model.Models;

namespace Survey.Service
{
    public interface IFaqService : IBaseService
    {
        UpdateResult Add(Faq faq);

        UpdateResult Update(Faq faq);

        UpdateResult Delete(int id);

        UpdateResult Delete(Faq faq);

        IEnumerable<Faq> GetAll();

        IEnumerable<Faq> GetMulti(Expression<Func<Faq, bool>> predicate);

        IEnumerable<Faq> GetMultiPaging(Expression<Func<Faq, bool>> predicate,
                                        Func<IQueryable<Faq>, IOrderedQueryable<Faq>> orderBy,
                                        out int total,
                                        int index = 0,
                                        int size = 20);

        Faq GetById(int id);
    }

    public class FaqService : BaseService,
                              IFaqService
    {
        private readonly IFaqRepository _faqRepository;

        public FaqService(ILogRepository logRepository,
                          IUnitOfWork unitOfWork,
                          IFaqRepository faqRepository) : base(logRepository,
                                                               unitOfWork)
        {
            _faqRepository = faqRepository;
        }

        #region IFaqService Members

        public UpdateResult Add(Faq faq)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _faqRepository.Add(faq);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when add faq");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Update(Faq faq)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _faqRepository.Update(faq);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when update faq");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Delete(int id)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _faqRepository.Delete(id);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when delete faq {id}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public UpdateResult Delete(Faq faq)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _faqRepository.Delete(faq);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when delete faq");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public IEnumerable<Faq> GetAll()
        {
            try
            {
                return _faqRepository.GetAll();
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get all faqs");
            }
            return null;
        }

        public IEnumerable<Faq> GetMulti(Expression<Func<Faq, bool>> predicate)
        {
            try
            {
                return _faqRepository.GetMulti(predicate);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get multi faqs");
            }
            return null;
        }

        public IEnumerable<Faq> GetMultiPaging(Expression<Func<Faq, bool>> predicate,
                                               Func<IQueryable<Faq>, IOrderedQueryable<Faq>> orderBy,
                                               out int total,
                                               int index = 0,
                                               int size = 20)
        {
            try
            {
                return _faqRepository.GetMultiPaging(predicate,
                                                     orderBy,
                                                     out total,
                                                     index,
                                                     size);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get multi paging faqs");
            }
            total = 0;
            return null;
        }

        public Faq GetById(int id)
        {
            try
            {
                return _faqRepository.GetSingleById(id);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get faq by id");
            }
            return null;
        }

        #endregion
    }
}
