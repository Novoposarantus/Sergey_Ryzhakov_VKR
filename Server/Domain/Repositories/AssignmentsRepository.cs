using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using Models.DtoModels;

namespace Domain.Repositories
{
    public class AssignmentsRepository : BaseRepository, IAssignmentsRepository
    {
        public AssignmentsRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public List<AssignmentsDto> Assignments
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Assignments
                        .Include(assignment => assignment.User)
                        .Include(assignment => assignment.Test)
                        .Select(assignment => new AssignmentsDto(assignment))
                        .ToList();
                }
            }
        }

        public AssignmentsDto Save(SaveAssignmnentsDto dto)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var assignment = new AssignmentModel()
                {
                    UserModelId = dto.UserId,
                    TestModelId = dto.TestId
                };
                var assignmentEntity = context.Assignments.Add(assignment);
                context.SaveChanges();
                return new AssignmentsDto(assignmentEntity.Entity);
            }
        }
    }
}
