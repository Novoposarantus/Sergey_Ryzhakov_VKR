using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        List<TestDto> Tests { get; }
        List<TestListItemDto> TestListItemDtos { get; }
        TestDto Get(int id);
    }
}
