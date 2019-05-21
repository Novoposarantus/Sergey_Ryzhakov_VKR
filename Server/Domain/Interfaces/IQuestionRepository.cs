using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<QuestionModel> Questions { get; }
    }
}
