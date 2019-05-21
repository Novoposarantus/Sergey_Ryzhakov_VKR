using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [ForeignKey("QuestinTypeModel")]
        public int QuestinTypeId { get; set; }
        public QuestionTypeModel Type { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
    }
}
 