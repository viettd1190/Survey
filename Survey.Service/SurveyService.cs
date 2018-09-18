using System;
using Survey.Data.Infrastructure;
using Survey.Data.Repositories;
using Survey.Model.Abstract;
using Survey.Model.Models;

namespace Survey.Service
{
    public interface ISurveyService : IBaseService
    {
        UpdateResult Create(Model.Models.Survey survey);

        UpdateResult Delete(int id);
    }

    public class SurveyService : BaseService,
                                 ISurveyService
    {
        private readonly IAnswerRepository _answerRepository;

        private readonly IQuestionRepository _questionRepository;

        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ILogRepository logRepository,
                             IUnitOfWork unitOfWork,
                             ISurveyRepository surveyRepository,
                             IQuestionRepository questionRepository,
                             IAnswerRepository answerRepository) : base(logRepository,
                                                                        unitOfWork)
        {
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        #region ISurveyService Members

        public UpdateResult Create(Model.Models.Survey survey)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                _surveyRepository.Add(survey);
                Save();

                foreach (Question question in survey.Questions)
                {
                    question.SurveyId = survey.Id;
                    _questionRepository.Add(question);
                }

                Save();

                foreach (Question question in survey.Questions)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        answer.QuestionId = question.Id;
                        _answerRepository.Add(answer);
                    }
                }

                Save();
            }
            catch (Exception exception)
            {
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            return result;
        }

        public UpdateResult Delete(int id)
        {
            UpdateResult result = new UpdateResult();
            try
            {
                Model.Models.Survey survey = _surveyRepository.GetSingleById(id);
                foreach (Question question in survey.Questions)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        _answerRepository.Delete(answer);
                    }
                    _questionRepository.Delete(question);
                }
                _surveyRepository.Delete(survey);
            }
            catch (Exception exception)
            {
                AddLogError(exception,
                            $"Error when delete survey {id}");
                result.State = 4;
                result.KeyLanguage = UpdateResult.ERROR_WHEN_UPDATED;
            }
            if(result.State == 1)
            {
                Save();
            }
            return result;
        }

        #endregion
    }
}
