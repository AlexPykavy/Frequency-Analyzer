using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAnalyzer.Exceptions
{
    class NotVocabularyException : Exception
    {
        public NotVocabularyException(string message) : base(message) { }

        public NotVocabularyException(string message, Exception ex) : base(message, ex) { }
    }
}
