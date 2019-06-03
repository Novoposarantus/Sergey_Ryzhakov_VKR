using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class AssignmentsDto
    {
        public AssignmentsDto(AssignmentModel model)
        {
            Id = model.Id;
            UserName = model.User.UserName;
            TestName = model.Test.Name;
            Time = model.Duration.HasValue
                ? TimeSpan.FromSeconds(model.Duration.Value).ToString(@"hh\:mm\:ss\")
                : null;
            Result = model.Result;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
        public string Time { get; set; }
        public double? Result { get; set; }
    }
}
