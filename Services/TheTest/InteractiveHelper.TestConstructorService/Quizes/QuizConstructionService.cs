using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;
using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.QuizConstructionServices.Quizes;

public class QuizConstructionService : IQuizConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public QuizConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task AddHeadQuestionToQuiz(int quizId, InputQuestionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, "Quiz not found", 404);

        var question = mapper.Map<Question>(model);
        quiz.Head = question;
        await context.Questions.AddAsync(question);
        context.Quizes.Update(quiz);

        await context.SaveChangesAsync();
    }

    public async Task<OutputQuizModel> CreateNewQuiz(InputQuizModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        return mapper.Map<OutputQuizModel>(await context.Quizes.AddAsync(
            mapper.Map<Quiz>(model)));
    }

    public async Task<IEnumerable<OutputQuizModel>> GetAllQuizes()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        return mapper.Map<IEnumerable<OutputQuizModel>>(await context.Quizes.ToListAsync());
    }

    public async Task RemoveQuiz(int quizId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, "Quiz not found", 404);

        context.Quizes.Remove(quiz);
        await context.SaveChangesAsync();
    }
}
