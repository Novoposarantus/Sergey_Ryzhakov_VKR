using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class QuestionsResultAnswersModel
    {
        public int Id { get; set; }
        public int AssignmentModelId { get; set; }
        public AssignmentModel Assignment { get; set; }
        public int AnswerModelId { get; set; }
        public AnswerModel Answer { get; set; }
    }
}
