namespace Models.Models
{
    public class UserAnswerModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public QuestionModel Question { get; set; }
        public AnswerModel Answer { get; set; }
    }
}
