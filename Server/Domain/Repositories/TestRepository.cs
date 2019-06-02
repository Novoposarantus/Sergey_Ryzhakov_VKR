using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Models.DtoModels;

namespace Domain.Repositories
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        public TestRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public List<TestDto> Tests
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Tests
                        .Include(test => test.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.Answers)
                        .Include(test => test.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.QuestionType)
                        .Select(test => new TestDto(test))
                        .ToList();
                }
            }
        }

        public List<TestListItemDto> TestListItemDtos
        {
            get => Tests.Select(test => new TestListItemDto(test)).ToList();
        }

        public TestEditDto Get(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var test =  context.Tests
                        .Include(t => t.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.Answers)
                        .Include(t => t.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.QuestionType)
                        .FirstOrDefault(t => t.Id == id);
                var allQuestions = context.Questions
                    .Include(question => question.QuestionType)
                    .Include(question => question.Answers)
                    .ToList();
                return new TestEditDto(test, allQuestions);
            }
        }

        public void Save(SaveTestDto testDto)
        {
            int testId = 0;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                TestModel test = new TestModel()
                {
                    Name = testDto.Name,
                    Description = testDto.Description
                };
                var testEntity = context.Tests.Add(test);
                context.SaveChanges();
                testId = testEntity.Entity.Id;
            }
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var questionToTests = testDto.Questions.Select(question => new QuestionToTestModel()
                {
                    QuestionId = question.Id,
                    Difficulty = question.Difficulty,
                    TestId = testId
                });
                context.QuestionToTests.AddRange(questionToTests);
                context.SaveChanges();
            }
        }

        public void Update(SaveTestDto testDto)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var testQuestions = context.QuestionToTests.Where(qt => qt.TestId == testDto.Id);
                foreach (var testQuestion in testQuestions)
                {
                    if (!testDto.Questions.Any(question => question.Id == testQuestion.Id))
                    {
                        context.QuestionToTests.Remove(testQuestion);
                    }
                }
                context.SaveChanges();
            }
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {   
                TestModel test = new TestModel()
                {
                    Id = testDto.Id,
                    Name = testDto.Name,
                    Description = testDto.Description,
                    QuestionToTests = testDto.Questions.Select(question => new QuestionToTestModel()
                    {
                        QuestionId = question.Id,
                        Difficulty = question.Difficulty
                    })
                };
                context.Tests.Add(test);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var test = context.Tests.FirstOrDefault(t => t.Id == id);
                if(test == null)
                {
                    return;
                }
                var questionsToTests = context.QuestionToTests.Where(qt => qt.TestId == id);
                if (questionsToTests.Any())
                {
                    context.QuestionToTests.RemoveRange(questionsToTests);
                }
                context.Tests.Remove(test);
                context.SaveChanges();
            }
        }
    }
}
