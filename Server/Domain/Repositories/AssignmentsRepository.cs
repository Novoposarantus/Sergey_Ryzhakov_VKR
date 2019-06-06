using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using Models.DtoModels;
using System;

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
                        .OrderByDescending(assignment => assignment.DateCreate)
                        .Select(assignment => new AssignmentsDto(assignment))
                        .ToList();
                }
            }
        }

        public List<AssignmentsDto> Get(int userid)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Assignments
                    .Include(assignment => assignment.User)
                    .Include(assignment => assignment.Test)
                    .Where(assignment => assignment.UserModelId == userid)
                    .OrderByDescending(assignment => assignment.DateCreate)
                    .Select(assignment => new AssignmentsDto(assignment))
                    .ToList();
            }
        }

        public AssignmentsDto Save(SaveAssignmentsDto dto)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var assignment = new AssignmentModel()
                {
                    UserModelId = dto.UserId,
                    TestModelId = dto.TestId,
                    DateCreate = DateTime.Now
                };
                var assignmentEntity = context.Assignments.Add(assignment);
                context.SaveChanges();
                assignment = context.Assignments
                    .Include(a => a.User)
                    .Include(a => a.Test)
                    .FirstOrDefault(a => a.Id == assignmentEntity.Entity.Id);
                return new AssignmentsDto(assignment);
            }
        }
    }
}
