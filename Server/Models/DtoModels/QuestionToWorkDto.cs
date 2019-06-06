using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.DtoModels
{
    public class QuestionToWorkDto
    {
        public QuestionToWorkDto(QuestionModel model)
        {
            Id = model.Id;
            Text = model.Text;
            Answers = model.Answers.Select(a => new AnswerToWorkDto(a)).ToList();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerToWorkDto> Answers { get; set; }
    }
}
