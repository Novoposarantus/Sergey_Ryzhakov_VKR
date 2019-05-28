using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Exceptions
{
    public class QuestionRepositoryException : Exception
    {
        public QuestionRepositoryException() : base() { }
        public QuestionRepositoryException(string text) : base(text) { }
    }
}
