using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }
}
