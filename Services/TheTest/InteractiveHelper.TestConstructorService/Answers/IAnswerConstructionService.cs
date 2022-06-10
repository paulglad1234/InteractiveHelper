using InteractiveHelper.QuizConstructionServices.Answers.Models;

namespace InteractiveHelper.QuizConstructionServices.Answers;

public interface IAnswerConstructionService
{
    Task<IEnumerable<OutputAnswerModel>> GetQuestionAnswers(int questionId);
    Task<OutputAnswerModel> AddNewAnswerToQuestion(int questionId, InputAnswerModel model);
    Task UpdateAnswer(int answerId, InputAnswerModel model);
    Task RemoveAnswer(int answerId);
}
