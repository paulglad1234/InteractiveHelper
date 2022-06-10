using AutoMapper;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Quiz;
using Microsoft.EntityFrameworkCore;
using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.QuizConstructionServices.Questions;

public class QuestionConstructionService : IQuestionConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public QuestionConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<OutputQuestionModel> AddQuestionToQuiz(int quizId, InputQuestionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        var question = mapper.Map<Question>(model);
        if (quiz.Head is null)
        {
            quiz.Head = question;
            await context.Questions.AddAsync(question);
            context.Quizes.Update(quiz);
        }
        else
        {
            var last = quiz.Head;
            while (last.Next is not null)
                last = last.Next;
            question.Previous = last;
            last.Next = question;
            await context.Questions.AddAsync(question);
            context.Questions.Update(last);
        }
        await context.SaveChangesAsync();

        return mapper.Map<OutputQuestionModel>(question);
    }

    public async Task<IEnumerable<OutputQuestionModel>> GetQuizQuestions(int quizId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var quiz = await context.Quizes.FindAsync(quizId);
        CommonException.ThrowIfNull(quiz, $"Quiz with id {quizId} not found", 404);

        var data = new List<OutputQuestionModel>();
        if (quiz.Head is not null)
        {
            var current = quiz.Head;
            data.Add(mapper.Map<OutputQuestionModel>(current));
            while (current.Next is not null)
            {
                current = current.Next;
                data.Add(mapper.Map<OutputQuestionModel>(current));
            }
        }

        return data;
    }

    public async Task RemoveQuestion(int questionId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);

        var (previous, next) = (question.Previous, question.Next);

        if (previous is not null)
            if (next is not null)
            {
                previous.Next = next;
                next.Previous = previous;
                context.Questions.UpdateRange(previous, next);
            } 
            else
            {
                previous.Next = null;
                context.Questions.Update(previous);
            }
        else
        {
            var quiz = question.Quiz;
            quiz.Head = next;
            context.Quizes.Update(quiz);
        }

        context.Questions.Remove(question);
        await context.SaveChangesAsync();
    }

    public async Task UpdateQuestion(int questionId, InputQuestionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var question = await context.Questions.FindAsync(questionId);
        CommonException.ThrowIfNull(question, $"Question with id {questionId} not found", 404);

        question = mapper.Map(model, question);
        context.Questions.Update(question);

        await context.SaveChangesAsync();
    }
}
