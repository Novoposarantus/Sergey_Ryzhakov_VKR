using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        List<SimpleTestModel> Tests { get; }
        List<TestListItemDto> TestListItemDtos { get; }
        SimpleTestModel GetTest(int id);
    }
}
