using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class TestAnswersDto
    {
        public int AssignmentId { get; set; }
        public List<int> CheckedAnswers { get; set; }
        public List<QuestionTime> Times { get; set; }
    }
}
