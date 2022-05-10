using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.QuizConstructionServices;

public interface IQuestionConstructionService
{
    Task<IEnumerable<QuestionModel>> GetQuizQuestions(int quizId);
    Task<QuestionModel> AddQuestionToQuiz(int quizId, AddQuestionModel model);
    Task UpdateQuestion(int questionId, UpdateQuestionModel model);
    Task RemoveQuestion(int questionId);
}
