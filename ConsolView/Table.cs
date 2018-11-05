using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolView
{
    internal class Table
    {
        public string[] ColumnNames;
        public string[] RowNames;

        public string GenerateTable<T, TD>(T[] args, TD[] args2) where T: struct
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                if (this.ColumnNames.Length != args.Length) throw new ArgumentException();
                
                
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"Argument with null value.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine(@"Invalid argument.");
            }
        }
    }
}
