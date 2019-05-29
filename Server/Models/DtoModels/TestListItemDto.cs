using System.Linq;
using Models.Models;

namespace Models.DtoModels
{
    public class TestListItemDto
    {
        public TestListItemDto(SimpleTestModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            QuestionsCount = model.Questions.Count();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuestionsCount { get; set; }
    }
}
