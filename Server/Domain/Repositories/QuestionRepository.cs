using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Models.DtoModels;

namespace Domain.Repositories
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        public QuestionRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public IEnumerable<QuestionModel> Questions
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Questions
                        .Include(question => question.Answers)
                        .Include(question => question.QuestionTypeId);
                }
            }
        }

        public IEnumerable<QuestionListItemDto> QuestionListItemDtos
        {
            get => Questions.Select(question => new QuestionListItemDto(question));
        }

        public QuestionModel GetQuestion(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Questions
                    .Include(question => question.Answers)
                    .Include(question => question.QuestionTypeId)
                    .FirstOrDefault(question => question.Id == id);
            }
        }

        public QuestionModel SaveQuestion(QuestionModel question)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                if(question.QuestionTypeId < 1)
                {
                    var type = context.QuestionTypes.Add(question.Type);
                    context.SaveChanges();
                    question.QuestionTypeId = type.Entity.Id;
                    question.Type = type.Entity;
                }
                question.Id = 0;
                var saveQuestion = context.Questions.Add(question);
                context.SaveChanges();
                foreach(var answer in question.Answers)
                {
                    answer.Id = 0;
                    answer.QuestionId = saveQuestion.Entity.Id;
                    context.Add(answer);
                }
                context.SaveChanges();
                return GetQuestion(saveQuestion.Entity.Id);
            }
        }
    }
}
