using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Exceptions
{
    public sealed class StudentNotFoundException:Exception
    {                     
        public override string Message => "The Student is not Found";
    }
}
