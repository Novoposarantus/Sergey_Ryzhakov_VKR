using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public int UserModelId { get; set; }
        public UserModel User { get; set; }
        public int TestModelId { get; set; }
        public TestModel Test { get; set; }
        public int? Duration { get; set; }
        public double? Result { get; set; }
    }
}
