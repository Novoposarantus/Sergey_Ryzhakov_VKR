using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class SimpleAnswerModel
    {
        public SimpleAnswerModel(AnswerModel model)
        {
            Id = model.Id;
            Text = model.Text;
            IsRight = model.IsRight;
        }
        public int Id { get; set; }
        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}
