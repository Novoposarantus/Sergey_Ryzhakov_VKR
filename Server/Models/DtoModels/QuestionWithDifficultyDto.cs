using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class QuestionWithDifficultyDto : QuestionDto
    {
        public QuestionWithDifficultyDto(QuestionModel model, int difficulty) : base(model)
        {
            Difficulty = difficulty;
        }
        public int Difficulty { get; set; } 
    }
}
