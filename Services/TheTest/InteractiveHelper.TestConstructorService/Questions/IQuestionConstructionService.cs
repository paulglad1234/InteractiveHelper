using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.QuizConstructionServices.Questions;

public interface IQuestionConstructionService
{
    Task<IEnumerable<OutputQuestionModel>> GetQuizQuestions(int quizId);
    Task<OutputQuestionModel> AddQuestionToQuiz(int quizId, InputQuestionModel model);
    Task UpdateQuestion(int questionId, InputQuestionModel model);
    Task RemoveQuestion(int questionId);
}
