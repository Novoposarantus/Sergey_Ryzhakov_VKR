using Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class TestResultQuestionsModel
    {
        public int Id { get; set; }
        public int AssignmentModelId { get; set; }
        public AssignmentModel Assignment { get; set; }
        public int QuestionModelId { get; set; }
        public QuestionModel Question { get; set; }
        public int Milliseconds { get; set; }
    }
}
