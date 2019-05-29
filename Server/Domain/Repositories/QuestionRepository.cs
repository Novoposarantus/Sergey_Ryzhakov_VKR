using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Models.DtoModels;
using Models.Exceptions;

namespace Domain.Repositories
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        public QuestionRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public List<SimpleQuestionModel> Questions
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Questions
                        .Include(question => question.Answers)
                        .Include(question => question.QuestionType)
                        .Select(question => new SimpleQuestionModel(question))
                        .ToList();
                }
            }
        }

        public List<QuestionListItemDto> QuestionListItemDtos
        {
            get => Questions.Select(question => new QuestionListItemDto(question)).ToList();
        }

        public List<QuestionTypeModel> Types
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.QuestionTypes.ToList();
                }
            }
        }

        public SimpleQuestionModel GetQuestion(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var question = context.Questions
                    .Include(q => q.Answers)
                    .Include(q => q.QuestionType)
                    .FirstOrDefault(q => q.Id == id);
                if(question == null)
                {
                    throw new QuestionRepositoryException("Вопроса не существует.");
                }
                return new SimpleQuestionModel(question);
            }
        }


        public void SaveQuestion(QuestionModel question)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                question.Id = 0;
                var saveQuestion = context.Questions.Add(question);
                context.SaveChanges();
            }
        }

        public void UpdateQuestion(QuestionModel question)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Questions.Update(question);
                context.SaveChanges();
            }
        }

        public QuestionTypeModel SaveQuestionType(string name)
        {
            var type = Types.FirstOrDefault(t => t.Name == name);
            if (type != null)
            {
                return type;
            }

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var newType = context.QuestionTypes.Add(new QuestionTypeModel() { Name = name });
                context.SaveChanges();
                return newType.Entity;
            }
        }
    }
}
