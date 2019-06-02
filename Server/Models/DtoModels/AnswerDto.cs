using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class AnswerDto
    {
        public AnswerDto(AnswerModel model)
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
