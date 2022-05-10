using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.QuizConstructionServices.Models;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.QuizConstructionServices;

public class AnswerConstructionService : IAnswerConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AnswerConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<AnswerModel> AddNewAnswerToQuestion(int questionId, AddAnswerModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var answer = mapper.Map<Answer>(model);
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);
        answer.Question = question;
        await context.Answers.AddAsync(answer);
        await context.SaveChangesAsync();

        return mapper.Map<AnswerModel>(answer);
    }

    public async Task<IEnumerable<AnswerModel>> GetQuestionAnswers(int questionId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);

        return mapper.Map<IEnumerable<AnswerModel>>(question.Answers.ToList());
    }

    public async Task RemoveAnswer(int answerId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var answer = await context.Answers.FindAsync(answerId);
        CommonException.ThrowIfNull(answer, $"Answer with id {answerId} not found", 404);
        context.Answers.Remove(answer);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAnswer(int answerId, UpdateAnswerModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var answer = await context.Answers.FindAsync(answerId);
        CommonException.ThrowIfNull(answer, $"Answer with id {answerId} not found", 404);

        answer = mapper.Map(model, answer);
        context.Answers.Update(answer);

        await context.SaveChangesAsync();
    }
}
