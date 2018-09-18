using System;
using Survey.Common;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Model.Models;

namespace Survey.Service
{
    public interface IBaseService
    {
        void Save();
    }

    public class BaseService : IBaseService
    {
        protected readonly ILogRepository _logRepository;

        private readonly IUnitOfWork _unitOfWork;

        public BaseService(ILogRepository logRepository,
                           IUnitOfWork unitOfWork)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
        }

        #region IBaseService Members

        public void Save()
        {
            _unitOfWork.Commit();
        }

        #endregion

        protected void AddLogError(Exception exception,
                                   string title)
        {
            _logRepository.Add(new Log
                               {
                                   Title = title,
                                   Message = exception.Message,
                                   Status = LogStatus.ERROR
                               });
        }

        protected void AddLogInfo(Exception exception,
                                  string title)
        {
            _logRepository.Add(new Log
                               {
                                   Title = title,
                                   Message = exception.Message,
                                   Status = LogStatus.INFO
                               });
        }
    }
}
