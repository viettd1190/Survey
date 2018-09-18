using System;
using System.Linq;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Model.Abstract;
using Survey.Model.Models;

namespace Survey.Service
{
    public interface ISuppportInformationService : IBaseService
    {
        UpdateResult Update(SupportInformation supportInformation);

        SupportInformation Get();
    }

    public class SuppportInformationService : BaseService,
                                              ISuppportInformationService
    {
        private readonly ISupportInformationRepository _supportInformationRepository;

        public SuppportInformationService(ILogRepository logRepository,
                                          IUnitOfWork unitOfWork,
                                          ISupportInformationRepository supportInformationRepository) : base(logRepository,
                                                                                                             unitOfWork)
        {
            _supportInformationRepository = supportInformationRepository;
        }

        #region ISuppportInformationService Members

        public UpdateResult Update(SupportInformation supportInformation)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                if(supportInformation.Id == 0)
                {
                    _supportInformationRepository.Add(supportInformation);
                }
                else
                {
                    _supportInformationRepository.Update(supportInformation);
                }
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when update support information");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        public SupportInformation Get()
        {
            try
            {
                return _supportInformationRepository.GetAll()
                                                    .FirstOrDefault();
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            "Error when get support information");
            }
            return null;
        }

        #endregion
    }
}
