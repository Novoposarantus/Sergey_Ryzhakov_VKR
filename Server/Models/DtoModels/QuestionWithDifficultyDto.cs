using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class QuestionWithDifficultyDto : QuestionDto
    {
        public QuestionWithDifficultyDto(QuestionToTestModel model) :base(model.Question)
        {
            Difficulty = model.Difficulty;
            ReferenceResponseSeconds = model.ReferenceResponseSeconds;
        }
        public QuestionWithDifficultyDto(QuestionModel model, int difficulty, int referenceResponseSeconds) : base(model)
        {
            Difficulty = difficulty;
            ReferenceResponseSeconds = referenceResponseSeconds;
        }
        public int Difficulty { get; set; }
        public int ReferenceResponseSeconds { get; set; }
    }
}
