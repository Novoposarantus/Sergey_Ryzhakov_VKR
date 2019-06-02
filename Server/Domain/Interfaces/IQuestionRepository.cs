using Models.DtoModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestionRepository
    {
        List<QuestionDto> Questions { get; }
        List<QuestionListItemDto> QuestionListItemDtos { get; }
        List<QuestionTypeModel> Types { get; }
        QuestionDto Get(int id);
        void Save(QuestionModel question);
        void Update(QuestionModel question);
        void Delete(int questionId);
        QuestionTypeModel SaveQuestionType(string name);
    }
}
