using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class AnswerToWorkDto
    {
        public AnswerToWorkDto(AnswerModel model)
        {
            Id = model.Id;
            Text = model.Text;
        }
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
