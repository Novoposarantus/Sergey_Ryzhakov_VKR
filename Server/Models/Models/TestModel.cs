﻿using System.Collections.Generic;

namespace Models.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<QuestionModel> Questions { get; set; }
    }
}