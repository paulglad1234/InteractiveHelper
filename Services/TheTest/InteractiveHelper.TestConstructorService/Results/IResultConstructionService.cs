using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.QuizConstructionServices;

public interface IResultConstructionService
{
    Task<IEnumerable<ResultModel>> GetQuizResults(int quizId);
    Task<QuizModel> GenerateQuizResults(int quizId);
    Task UpdateResult(int resultId, UpdateResultModel model);
    Task RemoveResult(int resultId);

    Task<ResultModel> AddItemToResult(int itemId, int resultId);
    Task<ResultModel> RemoveItemFromResult(int itemId, int resultId);

    Task<ResultModel> AddResultToQuiz(int quizId, AddResultModel model);
}
