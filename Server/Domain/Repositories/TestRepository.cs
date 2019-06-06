using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Models.DtoModels;

namespace Domain.Repositories
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        public TestRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

        public List<TestDto> Tests
        {
            get
            {
                using (var context = ContextFactory.CreateDbContext(ConnectionString))
                {
                    return context.Tests
                        .Include(test => test.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.Answers)
                        .Include(test => test.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.QuestionType)
                        .Select(test => new TestDto(test))
                        .ToList();
                }
            }
        }

        public List<TestListItemDto> TestListItemDtos
        {
            get => Tests.Select(test => new TestListItemDto(test)).ToList();
        }

        public TestEditDto Get(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var test =  context.Tests
                        .Include(t => t.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.Answers)
                        .Include(t => t.QuestionToTests)
                        .ThenInclude(questionToTest => questionToTest.Question)
                        .ThenInclude(question => question.QuestionType)
                        .FirstOrDefault(t => t.Id == id);
                var allQuestions = context.Questions
                    .Include(question => question.QuestionType)
                    .Include(question => question.Answers)
                    .ToList();
                return new TestEditDto(test, allQuestions);
            }
        }

        public TestWorkDto GetTestToWork(int assignmentId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var test = context.Assignments
                    .Include(a => a.Test)
                    .ThenInclude(t => t.QuestionToTests)
                    .ThenInclude(qt => qt.Question)
                    .ThenInclude(q => q.Answers)
                    .Include(a => a.Test)
                    .ThenInclude(t => t.QuestionToTests)
                    .ThenInclude(qt => qt.Question)
                    .ThenInclude(q => q.QuestionType)
                    .FirstOrDefault(x => x.Id == assignmentId)
                    .Test;
                return new TestWorkDto(test, assignmentId);
            }
        }

        public void Save(SaveTestDto testDto)
        {
            int testId = 0;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                TestModel test = new TestModel()
                {
                    Name = testDto.Name,
                    Description = testDto.Description
                };
                var testEntity = context.Tests.Add(test);
                context.SaveChanges();
                testId = testEntity.Entity.Id;
            }
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var questionToTests = testDto.Questions.Select(question => new QuestionToTestModel()
                {
                    QuestionId = question.Id,
                    Difficulty = question.Difficulty,
                    TestId = testId
                });
                context.QuestionToTests.AddRange(questionToTests);
                context.SaveChanges();
            }
        }

        public void Update(SaveTestDto testDto)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                TestModel test = new TestModel()
                {
                    Id = testDto.Id,
                    Name = testDto.Name,
                    Description = testDto.Description
                };
                var testEntity = context.Tests.Update(test);
                context.SaveChanges();
            }
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var testQuestions = context.QuestionToTests.Where(qt => qt.TestId == testDto.Id);
                foreach (var testQuestion in testQuestions)
                {
                    var questionDto = testDto.Questions.FirstOrDefault(question => question.Id == testQuestion.Id);
                    if (questionDto == null)
                    {
                        context.QuestionToTests.Remove(testQuestion);
                    }
                    else
                    {
                        testQuestion.Difficulty = questionDto.Difficulty;
                        context.Update(testQuestion);
                    }
                }
                foreach (var questionDto in testDto.Questions)
                {
                    if(!testQuestions.Any(qt => qt.QuestionId == questionDto.Id))
                    {
                        context.QuestionToTests.Add(new QuestionToTestModel()
                        {
                            QuestionId = questionDto.Id,
                            Difficulty = questionDto.Difficulty,
                            TestId = testDto.Id
                        });
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var test = context.Tests.FirstOrDefault(t => t.Id == id);
                if(test == null)
                {
                    return;
                }
                var questionsToTests = context.QuestionToTests.Where(qt => qt.TestId == id);
                if (questionsToTests.Any())
                {
                    context.QuestionToTests.RemoveRange(questionsToTests);
                }
                context.Tests.Remove(test);
                context.SaveChanges();
            }
        }

        public void SaveTestResult(TestAnswersDto result)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {

                List<QuestionWithDifficultyDto> allTestQuestions = context.Assignments
                    .Where(a => a.Id == result.AssignmentId)
                    .Include(a => a.Test)
                    .ThenInclude(t => t.QuestionToTests)
                    .SelectMany(a => a.Test.QuestionToTests)
                    .Include(t => t.Question)
                    .ThenInclude(q => q.Answers)
                    .Select(qt=> new QuestionWithDifficultyDto(qt))
                    .ToList();

                var answers = context.Answers.Where(a => result.CheckedAnswers.Contains(a.Id));
                Dictionary<int, QuestionTime> timeByQuestion = result.Times.ToDictionary(x => x.Id);
                Dictionary<int, List<AnswerModel>> answersByQuestions = new Dictionary<int, List<AnswerModel>>();
                foreach (var answer in answers)
                {
                    if (!answersByQuestions.ContainsKey(answer.QuestionId))
                    {
                        answersByQuestions[answer.QuestionId] = new List<AnswerModel>();
                    }
                    answersByQuestions[answer.QuestionId].Add(answer);
                }

                var assignment = context.Assignments.FirstOrDefault(a => a.Id == result.AssignmentId);
                context.TestResultQuestions.AddRange(result.Times.Select(questionTime => new TestResultQuestionsModel()
                {
                    AssignmentModelId = assignment.Id,
                    Milliseconds = questionTime.Milliseconds,
                    QuestionModelId = questionTime.Id
                }));
                context.QuestionsResultAnswers.AddRange(result.CheckedAnswers.Select(answerId => new QuestionsResultAnswersModel()
                {
                    AnswerModelId = answerId,
                    AssignmentModelId = assignment.Id
                }));
                assignment.Duration = result.Times.Sum(x => x.Milliseconds);
                assignment.Result = GetTestResult(allTestQuestions, answersByQuestions, timeByQuestion);
                context.Update(assignment);
                context.SaveChanges();
            }
        }

        private double GetTestResult(
            IEnumerable<QuestionWithDifficultyDto> allTestQuestion,
            Dictionary<int, List<AnswerModel>> checkedAnswersByWork,
            Dictionary<int, QuestionTime> timeByQuestion )
        {
            double result = 0;
            foreach (var question in allTestQuestion)
            {
                var checkedAnswers = checkedAnswersByWork.ContainsKey(question.Id)
                    ? checkedAnswersByWork[question.Id]
                    : new List<AnswerModel>();
                var currentQuestionTime = timeByQuestion.ContainsKey(question.Id)
                    ? timeByQuestion[question.Id].Milliseconds
                    : 0;
                result += question.Difficulty
                    * GetResponseWeight(question.Answers, checkedAnswers)
                    * GetTimeCoefficient(question.ReferenceResponseSeconds, currentQuestionTime);

            }
            return result;
        }

        private double GetResponseWeight(List<AnswerDto> answers, List<AnswerModel> checkedAnswers)
        {
            int rightAnswersCount = answers.Where(a => a.IsRight).Count();
            int answersCount = answers.Count;
            int wrongAnswersCount = answers.Where(a => !a.IsRight).Count();
            if (checkedAnswers.Count == 0)
            {
                return 0;
            }
            if(checkedAnswers.All(x=>x.IsRight) && checkedAnswers.Count == rightAnswersCount)
            {
                return rightAnswersCount / (double)answersCount;
            }
            return - (wrongAnswersCount / (double)answersCount);
        }

        private double GetTimeCoefficient(int defaultQuestionSeconds, int currentQuestionMiliseconds)
        {
            if(defaultQuestionSeconds == 0) return 1;

            var defaultQuestionMiliseconds = defaultQuestionSeconds * 1000;
            if (currentQuestionMiliseconds < defaultQuestionMiliseconds) return 1;
            if (currentQuestionMiliseconds > 3 * defaultQuestionMiliseconds) return 0.5;
            return 1 / (currentQuestionMiliseconds / (double)defaultQuestionMiliseconds);
        }
    }
}
