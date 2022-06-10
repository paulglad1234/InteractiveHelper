using InteractiveHelper.QuizConstructionServices.Answers;
using InteractiveHelper.QuizConstructionServices.Questions;
using InteractiveHelper.QuizConstructionServices.Quizes;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveHelper.QuizConstructionServices;

public static class Bootstrapper
{
    public static IServiceCollection AddQuizConstructorServices(this IServiceCollection services)
    {
        services.AddSingleton<IQuizConstructionService, QuizConstructionService>();
        services.AddSingleton<IQuestionConstructionService, QuestionConstructionService>();
        services.AddSingleton<IAnswerConstructionService, AnswerConstructionService>();
        services.AddSingleton<IResultConstructionService, ResultConstructionService>();

        return services;
    }
}
