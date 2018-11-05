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

        public string GenerateTable<T, TD>(T[] args, TD[] args2) where T: struct
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                if (this.ColumnNames.Length != args.Length) throw new ArgumentException();
                
                foreach (var i in Enumerable.Range(0, this.ColumnNames.Length))
                {
                    builder.AppendLine($"{this.ColumnNames[i]}: {args[i]}, {args2[i]}");
                }

                return builder.ToString();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(@"Argument with null value.");
                return string.Empty;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(@"Invalid argument.");
                return string.Empty;
            }
        }
    }
}
