using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.QuizConstructionServices.Models;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.QuizConstructionServices;

public class QuestionConstructionService : IQuestionConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public QuestionConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<QuestionModel> AddQuestionToQuiz(int quizId, AddQuestionModel model)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);
        var question = mapper.Map<Question>(model);
        question.Quiz = quiz;
        await context.Questions.AddAsync(question);
        await context.SaveChangesAsync();

        return mapper.Map<QuestionModel>(question);
    }

    public async Task<IEnumerable<QuestionModel>> GetQuizQuestions(int quizId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        return mapper.Map<IEnumerable<QuestionModel>>(quiz.Questions.ToList());
    }

    public async Task RemoveQuestion(int questionId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);

        context.Questions.Remove(question);
        await context.SaveChangesAsync();
    }

    public async Task UpdateQuestion(int questionId, UpdateQuestionModel model)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);

        question = mapper.Map(model, question);
        context.Questions.Update(question);

        await context.SaveChangesAsync();
    }
}
