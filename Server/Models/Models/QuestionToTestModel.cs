namespace Models.Models
{
    public class QuestionToTestModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
        public int Difficulty { get; set; }
        public int ReferenceResponseSeconds { get; set; }
        public int TestId { get; set; }
        public TestModel Test { get; set; }
    }
}
