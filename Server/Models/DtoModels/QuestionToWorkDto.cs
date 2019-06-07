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
            Code = model.Code;
            Answers = model.Answers.Select(a => new AnswerToWorkDto(a)).ToList();
            IsOneAnswer = model.Answers.Where(a => a.IsRight).Count() == 1;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public bool IsOneAnswer { get; set; }
        public List<AnswerToWorkDto> Answers { get; set; }
    }
}
