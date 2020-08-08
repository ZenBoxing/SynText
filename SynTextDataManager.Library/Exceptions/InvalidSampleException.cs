using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynTextDataManager.Library.Exceptions
{
    public class InvalidSampleException : Exception
    {
        public InvalidSampleException()
        { 
        }

        public InvalidSampleException(string message) : base(message)
        { 
        }

        public InvalidSampleException(string message, Exception inner)
            : base(message, inner)
        { 
        }
    }
}
