using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.QuizConstructionServices.Models;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.QuizConstructionServices;

public class QuizConstructionService : IQuizConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public QuizConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<QuizModel> CreateNewQuiz()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.AddAsync(new Quiz());
        await context.SaveChangesAsync();

        return mapper.Map<QuizModel>(quiz.Entity);
    }

    public async Task<IEnumerable<QuizModel>> GetAllQuizes()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        return mapper.Map<IEnumerable<QuizModel>>(await context.Quizes.ToListAsync());
    }

    public async Task RemoveQuiz(int quizId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        context.Quizes.Remove(quiz);
        await context.SaveChangesAsync();
    }

    public async Task SetActiveQuiz(int quizId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);
        quiz.IsActive = true;

        var activeQuiz = await context.Quizes.FirstOrDefaultAsync(quiz => quiz.IsActive);
        if (activeQuiz is not null)
        {
            activeQuiz.IsActive = false;
            context.Quizes.UpdateRange(quiz, activeQuiz);
        }
        else
        {
            context.Quizes.Update(quiz);
        }

        await context.SaveChangesAsync();
    }
}
