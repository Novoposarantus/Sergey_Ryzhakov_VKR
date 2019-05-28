using Models.Models;
using System.Linq;

namespace Models.DtoModels
{
    public class QuestionListItemDto
    {
        public QuestionListItemDto(QuestionModel model)
        {
            Id = model.Id;
            Text = model.Text;
            TypeId = model.QuestionTypeId;
            TypeName = model.Type.Name;
            AnswersCount = model.Answers.Count();
            RightAnswersCount = model.Answers.Where(answer => answer.IsRight).Count();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int AnswersCount { get; set; }
        public int RightAnswersCount { get; set; }
    }
}
