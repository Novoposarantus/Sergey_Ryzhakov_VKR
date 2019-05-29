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

        public List<SimpleTestModel> Tests
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
                        .Select(test => new SimpleTestModel(test))
                        .ToList();
                }
            }
        }

        public List<TestListItemDto> TestListItemDtos
        {
            get => Tests.Select(test => new TestListItemDto(test)).ToList();
        }

        public SimpleTestModel GetTest(int id)
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
                return new SimpleTestModel(test);
            }
        }
    }
}
