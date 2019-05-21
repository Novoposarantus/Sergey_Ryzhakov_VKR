using Models.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<TestModel> Tests { get; }
        TestModel GetTest(int id);
    }
}
