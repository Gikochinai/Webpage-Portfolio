using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateCustomExceptionAssembly
{
    public class CustomExceptionClass
    {
        public class InvalidException : Exception
        {
            public InvalidException(string str) : base(str)
            {

            }
        }
    }
}
