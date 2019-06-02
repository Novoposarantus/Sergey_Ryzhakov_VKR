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

        public List<QuestionDto> Questions
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Questions
                        .Include(question => question.Answers)
                        .Include(question => question.QuestionType)
                        .Select(question => new QuestionDto(question))
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

        public QuestionDto Get(int id)
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
                return new QuestionDto(question);
            }
        }


        public void Save(QuestionModel question)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                question.Id = 0;
                var saveQuestion = context.Questions.Add(question);
                context.SaveChanges();
            }
        }

        public void Update(QuestionModel question)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var questionAnsers = context.Answers.Where(a => a.QuestionId == question.Id);
                foreach(var answer in questionAnsers)
                {
                    if(!question.Answers.Any(a=>a.Id == answer.Id))
                    {
                        context.Answers.Remove(answer);
                    }
                }
                context.SaveChanges();
            }
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                question.QuestionType = null;
                context.Questions.Update(question);
                context.SaveChanges();
            }
        }

        public void Delete(int questionId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var question = context.Questions.FirstOrDefault(q => q.Id == questionId);
                if (question == null)
                {
                    return;
                }
                var QuestionToTestsWithQuestion = context
                    .QuestionToTests
                    .Where(qt => qt.QuestionId == questionId);
                if (QuestionToTestsWithQuestion.Any())
                {
                    context.QuestionToTests.RemoveRange(QuestionToTestsWithQuestion);
                }
                context.Questions.Remove(question);
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
