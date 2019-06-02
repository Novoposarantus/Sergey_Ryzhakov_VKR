using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.DtoModels
{
    public class QuestionDto
    {
        public QuestionDto(QuestionModel model)
        {
            Id = model.Id;
            Text = model.Text;
            QuestionTypeId = model.QuestionTypeId;
            QuestionType = model.QuestionType;
            Answers = model.Answers.Select(a=>new AnswerDto(a)).ToList();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionTypeModel QuestionType { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
