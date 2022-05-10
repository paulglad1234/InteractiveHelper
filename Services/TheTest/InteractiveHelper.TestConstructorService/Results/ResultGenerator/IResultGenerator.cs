using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Results;

public interface IResultGenerator
{
    public Task UpdateQuizWithGeneratedResults(Quiz test);
}
