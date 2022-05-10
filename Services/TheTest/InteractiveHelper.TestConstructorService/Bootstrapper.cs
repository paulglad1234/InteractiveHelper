using InteractiveHelper.QuizConstructionServices.Results;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveHelper.QuizConstructionServices;

public static class Bootstrapper
{
    public static IServiceCollection AddQuizConstructorServices(this IServiceCollection services)
    {
        services.AddSingleton<IQuizConstructionService, QuizConstructionService>();
        services.AddSingleton<IQuestionConstructionService, QuestionConstructionService>();
        services.AddSingleton<IAnswerConstructionService, AnswerConstructionService>();

        services.AddSingleton<IResultGenerator, ResultGenerator>();
        services.AddSingleton<IResultConstructionService, ResultConstructionService>();

        return services;
    }
}
