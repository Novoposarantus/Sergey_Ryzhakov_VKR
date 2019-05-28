using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<TestModel> Tests { get; }
        IEnumerable<TestListItemDto> TestListItemDtos { get; }
        TestModel GetTest(int id);
    }
}
