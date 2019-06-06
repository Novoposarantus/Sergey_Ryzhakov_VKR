using Models.Models;
using System;

namespace Models.DtoModels
{
    public class AssignmentsDto
    {
        public AssignmentsDto(AssignmentModel model)
        {
            Id = model.Id;
            UserName = model.User.UserName;
            TestName = model.Test.Name;
            TestId = model.TestModelId;
            Time = model.Duration.HasValue
                ? TimeSpan.FromMilliseconds(model.Duration.Value).ToString()
                : "";
            Result = model.Result;
            DateCreate = model.DateCreate.ToString("dd.MM.yyyy HH:mm");
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string Time { get; set; }
        public double? Result { get; set; }
        public string DateCreate { get; set; }
    }
}
