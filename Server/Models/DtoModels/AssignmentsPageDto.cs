using System.Collections.Generic;

namespace Models.DtoModels
{
    public class AssignmentsPageDto
    {
        public AssignmentsPageDto(List<TestDto> tests, List<AssignmentsDto> assignments, List<UserDto> users)
        {
            Tests = tests;
            Assignments = assignments;
            Users = users;
        }
        public List<TestDto> Tests { get; set; }
        public List<AssignmentsDto> Assignments { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
