using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DtoModels
{
    public class TestEditDto
    {
        public TestEditDto(TestModel test, List<QuestionModel> allQuestions) 
            : this(new TestDto(test), allQuestions.Select(question => new QuestionDto(question)).ToList()) { }
        public TestEditDto(TestDto test, List<QuestionModel> allQuestions)
            : this(test, allQuestions.Select(question => new QuestionDto(question)).ToList()) { }
        public TestEditDto(TestModel test, List<QuestionDto> allQuestions) 
            : this(new TestDto(test), allQuestions) { }
        public TestEditDto(TestDto test, List<QuestionDto> allQuestions)
        {
            Test = test;
            AllQuestions = allQuestions;
        }
        public TestDto Test { get; set; }
        public List<QuestionDto> AllQuestions { get; set; }
    }
}
