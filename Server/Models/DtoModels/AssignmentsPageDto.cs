using System.Collections.Generic;

namespace Models.DtoModels
{
    public class AssignmentsPageDto
    {
        public AssignmentsPageDto(List<TestDto> tests, List<AssignmentsDto> assignments)
        {
            Tests = tests;
            Assignments = assignments;
        }
        public List<TestDto> Tests { get; set; }
        public List<AssignmentsDto> Assignments { get; set; }
    }
}
