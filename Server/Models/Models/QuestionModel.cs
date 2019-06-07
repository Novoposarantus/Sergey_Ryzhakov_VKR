using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionTypeModel QuestionType { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
        public IEnumerable<QuestionToTestModel> QuestionToTests { get; set; }
    }
}
 