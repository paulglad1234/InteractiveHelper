using InteractiveHelper.Db.Entities.Quiz;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;
using InteractiveHelper.QuizConstructionServices.Results.Models;

namespace InteractiveHelper.QuizPassionService;

public interface IQuizPassionService
{
    ResultNode CurrentStage { get; }
    Task<OutputQuizModel> RootStage(string url);
    Task<OutputNodeModel> NextStage(int answerId);
}
