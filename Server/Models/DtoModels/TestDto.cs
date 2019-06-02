using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DtoModels
{
    public class TestDto
    {
        public TestDto(TestModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Questions = model.QuestionToTests.Select(qt => new QuestionWithDifficultyDto(qt.Question, qt.QuestionDifficulty)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<QuestionWithDifficultyDto> Questions { get; set; }
    }
}
