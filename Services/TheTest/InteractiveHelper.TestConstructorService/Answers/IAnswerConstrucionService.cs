using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.QuizConstructionServices;

public interface IAnswerConstrucionService
{
    Task<IEnumerable<AnswerModel>> GetQuestionAnswers(int questionId);
    Task<AnswerModel> AddNewAnswerToQuestion(int questionId, AddAnswerModel model);
    Task UpdateAnswer(int answerId, UpdateAnswerModel model);
    Task RemoveAnswer(int answerId);
}
