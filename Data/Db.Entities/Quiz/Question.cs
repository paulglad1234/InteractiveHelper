namespace InteractiveHelper.Db.Entities.Quiz;

public class Question : BaseEntity
{
    public string Text { get; set; }

    public int QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }
}
