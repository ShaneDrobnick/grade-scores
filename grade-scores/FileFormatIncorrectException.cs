using System;
using System.Runtime.Serialization;

namespace grade_scores
{
    [Serializable]
    public class FileFormatIncorrectException : Exception
    {
        public FileFormatIncorrectException()
        {
        }

        public FileFormatIncorrectException(string message) : base(message)
        {
        }

        public FileFormatIncorrectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileFormatIncorrectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ScoreValueIncorrectFormat : Exception
    {
        public ScoreValueIncorrectFormat()
        {
        }

        public ScoreValueIncorrectFormat(string message) : base(message)
        {
        }

        public ScoreValueIncorrectFormat(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ScoreValueIncorrectFormat(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}