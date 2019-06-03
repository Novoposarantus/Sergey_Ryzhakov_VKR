using Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAssignmentsRepository
    {
        List<AssignmentsDto> Assignments { get; }
        AssignmentsDto Save(SaveAssignmnentsDto dto);
    }
}
