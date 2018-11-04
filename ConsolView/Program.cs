using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolView
{
    class Program
    {
        static void Main(string[] args)
        {
            var constants = new ConstantViews();
            Console.WriteLine(constants.ToString());
        }
    }
}
