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

        public IEnumerable<TestModel> Tests
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Tests
                        .Include(test => test.Questions)
                        .ThenInclude(question => question.Answers)
                        .Include(test => test.Questions)
                        .ThenInclude(question => question.QuestionTypeId);
                }
            }
        }

        public IEnumerable<TestListItemDto> TestListItemDtos
        {
            get => Tests.Select(test => new TestListItemDto(test));
        }

        public TestModel GetTest(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Tests
                    .Include(test => test.Questions)
                    .ThenInclude(question => question.Answers)
                    .FirstOrDefault(test => test.Id == id);
            }
        }
    }
}
