using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Models
{
    public class SimpleTestModel
    {
        public SimpleTestModel(TestModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Questions = model.QuestionToTests.Select(x => new SimpleQuestionModel(x.Question)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<SimpleQuestionModel> Questions { get; set; }
    }
}
