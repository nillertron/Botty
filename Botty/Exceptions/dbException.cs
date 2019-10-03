using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botty.Exceptions
{
    public class dbException : Exception
    {
        public override string ToString()
        {
            return "Database connection failed";
        }
    }
}
