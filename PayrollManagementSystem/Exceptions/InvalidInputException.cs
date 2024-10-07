using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Exceptions
{
    internal class InvalidInputException:Exception
    {
        public InvalidInputException() : base() { }

        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
