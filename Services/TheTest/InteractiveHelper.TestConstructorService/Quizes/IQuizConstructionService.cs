using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.QuizConstructionServices;

public interface IQuizConstructionService
{
    Task<IEnumerable<QuizModel>> GetAllQuizes();
    Task<QuizModel> CreateNewQuiz();
    Task SetActiveQuiz(int quizId);
    Task RemoveQuiz(int quizId);
}
