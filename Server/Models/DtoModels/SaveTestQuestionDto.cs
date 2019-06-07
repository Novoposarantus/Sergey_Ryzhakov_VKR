using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class SaveTestQuestionDto
    {
        public int Id { get; set; }
        public int Difficulty { get; set; }
        public int ReferenceResponseSeconds { get; set; }
    }
}
