using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        List<TestDto> Tests { get; }
        List<TestListItemDto> TestListItemDtos { get; }
        TestEditDto Get(int id);
        TestWorkDto GetTestToWork(int id);
        void Save(SaveTestDto testDto);
        void Update(SaveTestDto testDto);
        void Delete(int id);
        void SaveTestResult(TestAnswersDto result);
    }
}
