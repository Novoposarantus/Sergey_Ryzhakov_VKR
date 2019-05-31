using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Models
{
    public class SimpleQuestionModel
    {
        public SimpleQuestionModel(QuestionModel model)
        {
            Id = model.Id;
            Text = model.Text;
            QuestionTypeId = model.QuestionTypeId;
            QuestionType = model.QuestionType;
            Answers = model.Answers.Select(a=>new SimpleAnswerModel(a)).ToList();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionTypeModel QuestionType { get; set; }
        public List<SimpleAnswerModel> Answers { get; set; }
    }
}
