using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsova
{
     internal class SortExceptions: Exception
    {
        public SortExceptions() { }

        public SortExceptions(string message):base(message) { }
    }
}
