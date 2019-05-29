using Models.DtoModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestionRepository
    {
        List<SimpleQuestionModel> Questions { get; }
        List<QuestionListItemDto> QuestionListItemDtos { get; }
        List<QuestionTypeModel> Types { get; }
        SimpleQuestionModel GetQuestion(int id);
        void SaveQuestion(QuestionModel question);
        void UpdateQuestion(QuestionModel question);
        QuestionTypeModel SaveQuestionType(string name);
    }
}
