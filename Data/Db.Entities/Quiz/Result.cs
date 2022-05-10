using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.Db.Entities.Quiz;

public class Result : BaseEntity
{
    public string Conclusion { get; set; }

    public int QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}
