using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        List<TestModel> Tests { get; }
        List<TestListItemDto> TestListItemDtos { get; }
        TestModel GetTest(int id);
    }
}
