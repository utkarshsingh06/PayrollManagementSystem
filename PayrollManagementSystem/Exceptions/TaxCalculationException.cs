using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagementSystem.Exceptions
{
    internal class TaxCalculationException:Exception
    {
        public TaxCalculationException() : base("Tax year must be in the range of 2000 to the current year.")
        {
        }

        public TaxCalculationException(string message) : base(message)
        {
        }

        public TaxCalculationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
