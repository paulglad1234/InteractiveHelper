using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.QuizConstructionServices.Models;
using InteractiveHelper.QuizConstructionServices.Results;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.QuizConstructionServices;

// TODO: use builder pattern

public class ResultConstructionService : IResultConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IResultGenerator resultGenerator;

    public ResultConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper,
        IResultGenerator resultGenerator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.resultGenerator = resultGenerator;
    }

    public async Task<ResultModel> AddItemToResult(int itemId, int resultId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var result = await context.Results.FindAsync(resultId);
        CommonException.ThrowIfNull(result, $"Result with id {resultId} not found", 404);
        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, $"Item with id {itemId} not found", 404);
        result.Items.Add(item);
        //item.Results.Add(result);
        await context.SaveChangesAsync();

        return mapper.Map<ResultModel>(result);
    }

    public async Task<QuizModel> GenerateQuizResults(int quizId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        await resultGenerator.UpdateQuizWithGeneratedResults(quiz);
        context.Quizes.Update(quiz);

        await context.SaveChangesAsync();
        return mapper.Map<QuizModel>(quiz);
    }

    public async Task<IEnumerable<ResultModel>> GetQuizResults(int quizId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        return mapper.Map<IEnumerable<ResultModel>>(quiz.Results.ToList());
    }

    public async Task<ResultModel> RemoveItemFromResult(int itemId, int resultId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var result = await context.Results.FindAsync(resultId);
        CommonException.ThrowIfNull(result, $"Result with id {resultId} not found", 404);
        var item = result.Items.AsQueryable().FirstOrDefault(item => item.Id.Equals(itemId));
        CommonException.ThrowIfNull(item, $"Item with id {itemId} not found in result {resultId}", 404);
        result.Items.Remove(item);
        //item.Results.Remove(result);
        await context.SaveChangesAsync();

        return mapper.Map<ResultModel>(result);
    }

    public async Task RemoveResult(int resultId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var result = await context.Results.FindAsync(resultId);
        CommonException.ThrowIfNull(result, $"Result with id {resultId} not found", 404);

        context.Results.Remove(result);
        await context.SaveChangesAsync();
    }

    public async Task UpdateResult(int resultId, UpdateResultModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var result = await context.Results.FindAsync(resultId);
        CommonException.ThrowIfNull(result, $"Result with id {resultId} not found", 404);

        result = mapper.Map(model, result);
        context.Results.Update(result);

        await context.SaveChangesAsync();
    }

    public async Task<ResultModel> AddResultToQuiz(int quizId, AddResultModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);
        var result = mapper.Map<Result>(model);
        result.Quiz = quiz;
        await context.Results.AddAsync(result);
        await context.SaveChangesAsync();

        return mapper.Map<ResultModel>(result);
    }
}
