using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolView
{
    public class ConstantViews
    {
        public IList<bool[][]> EtalonValues
        {
            get => new List<bool[][]>
            {
                new bool[][] 
                {
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, true,  false, false, false },
                    new bool[] { false, false, true,  false, true,  false, false },
                    new bool[] { false, true,  false, false, false, true,  false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false }
                },
                new bool[][]
                {
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, true,  false, false, false, true,  false },
                    new bool[] { false, false, true,  false, true,  false, false },
                    new bool[] { false, false, false, true,  false, false, false },
                    new bool[] { false, false, false, false, false, false, false }
                },
                new bool[][]
                {
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, true,  false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, true,  false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, false, false, false }
                },
                new bool[][]
                {
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, true,  false, false, false, false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false }
                },
                new bool[][]
                {
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, false, true,  false },
                    new bool[] { false, false, true,  true,  true,  false, false },
                    new bool[] { false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false }
                }
            };
        }

        private string SeparateMassiveToString(int value, int row)
        {
            bool Get(int index) => this.EtalonValues[value][row][index];

            return $"{ (Get(0)? '#' : ' ' ) }{ (Get(1) ? '#' : ' ') }{ (Get(2) ? '#' : ' ') }{ (Get(3) ? '#' : ' ') }{ (Get(4) ? '#' : ' ') }{ (Get(5) ? '#' : ' ') }{ (Get(6) ? '#' : ' ') }";
        }

        private string MassiveToString(int row)
        {
            return $"{ this.SeparateMassiveToString(0, row) }|{ this.SeparateMassiveToString(1, row) }|{ this.SeparateMassiveToString(2, row) }|{ this.SeparateMassiveToString(3, row) }|{ this.SeparateMassiveToString(4, row) }";
        }

        public override string ToString()
        {
            return $"{ this.MassiveToString(0) }\n\r{ this.MassiveToString(1) }\n\r{ this.MassiveToString(2) }\n\r{ this.MassiveToString(3) }\n\r{ this.MassiveToString(4) }\n\r{ this.MassiveToString(5) }\n\r{ this.MassiveToString(6) }";
        }
    }
}
