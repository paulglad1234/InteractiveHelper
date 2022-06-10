using InteractiveHelper.QuizConstructionServices.Questions.Models;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;

namespace InteractiveHelper.QuizConstructionServices.Quizes;

public interface IQuizConstructionService
{
    Task<IEnumerable<OutputQuizModel>> GetAllQuizes();
    Task<OutputQuizModel> CreateNewQuiz(InputQuizModel model);
    Task RemoveQuiz(int quizId);
    Task AddHeadQuestionToQuiz(int quizId, InputQuestionModel model);
}
