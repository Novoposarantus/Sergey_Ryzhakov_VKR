using Models.DtoModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestionRepository
    {
        List<QuestionModel> Questions { get; }
        List<QuestionListItemDto> QuestionListItemDtos { get; }
        List<QuestionTypeModel> Types { get; }
        QuestionModel GetQuestion(int id);
        void SaveQuestion(QuestionModel question);
        QuestionTypeModel SaveQuestionType(string name);
    }
}
