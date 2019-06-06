using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DtoModels
{
    public class TestWorkDto
    {
        public TestWorkDto(TestModel model, int assignmentId)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            AssignmentId = assignmentId;
            Questions = model.QuestionToTests.Select(qt => new QuestionToWorkDto(qt.Question)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignmentId { get; set; }

        public List<QuestionToWorkDto> Questions { get; set; }
    }
}
