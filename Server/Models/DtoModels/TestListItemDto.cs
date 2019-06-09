using System;
using System.Linq;
using Models.Models;

namespace Models.DtoModels
{
    public class TestListItemDto
    {
        public TestListItemDto(TestDto model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            QuestionsCount = model.Questions.Count();
            MaxResult = Math.Round(model.Questions
                .Select(question => question.Difficulty * (question.Answers.Where(a => a.IsRight).Count() / (double)question.Answers.Count()))
                .Sum(), 3);
            MinResult = Math.Round(model.Questions
                .Select(question => question.Difficulty
                    * (-question.Answers.Where(a => !a.IsRight).Count() / (double)question.Answers.Count())
                    * 0.5)
                .Sum(), 3);
 
            ReferenceTime = TimeSpan.FromSeconds(model.Questions.Sum(q => q.ReferenceResponseSeconds)).ToString();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuestionsCount { get; set; }
        public double MaxResult { get; set; }
        public double MinResult { get; set; }
        public string ReferenceTime { get; set; }
    }
}
