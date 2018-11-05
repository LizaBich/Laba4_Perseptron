using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolView
{
    /// <summary>
    /// Зашумлённые изображения.
    /// </summary>
    internal class ChangedViews
    {
        public ChangedViews()
        {
            this.Views = new List<bool[][]>();
        }

        public IList<bool[][]> Views { get; private set; }

        public void PrepareViewsWithNoise(IList<bool[][]> sources, int[] persentage)
        {
            foreach (var source in sources)
            {
                foreach (var i in Enumerable.Range(0, 3))
                {
                    this.Views.Add(this.AddNoise(source, persentage[i]));
                }
            }
        }

        public bool[,] GetMatrix(int index)
        {
            var temp = new bool[this.Views[index].Length, this.Views[index][0].Length];

            for (var j = 0; j < this.Views[index].Length; ++j)
            {
                for (var i = 0; i < this.Views[index][0].Length; ++i)
                {
                    temp[j, i] = this.Views[index][j][i];
                }
            }

            return temp;
        }

        public string ToString(int number)
        {
            return $"|{ this.SeparateMassiveToString(number, 0) }|\n\r|{ this.SeparateMassiveToString(number, 1) }|\n\r|{ this.SeparateMassiveToString(number, 2) }|\n\r|{ this.SeparateMassiveToString(number, 3) }|\n\r|{ this.SeparateMassiveToString(number, 4) }|\n\r|{ this.SeparateMassiveToString(number, 5) }|\n\r|{ this.SeparateMassiveToString(number, 6) }|";
        }

        private string SeparateMassiveToString(int value, int row)
        {
            bool Get(int index) => this.Views[value][row][index];

            return $"{ (Get(0) ? '#' : ' ') }{ (Get(1) ? '#' : ' ') }{ (Get(2) ? '#' : ' ') }{ (Get(3) ? '#' : ' ') }{ (Get(4) ? '#' : ' ') }{ (Get(5) ? '#' : ' ') }{ (Get(6) ? '#' : ' ') }";
        }

        private bool[][] AddNoise(bool[][] origin, float percent)
        {
            if (percent == 0) return origin;
            var random = new Random();
            percent /= 100;
            var width = origin[0].Length;
            var height = origin.Length;

            bool[][] result;
            this.InitializeBoolMatrix(out result, width, height);

            int countOfNoisePixels = Convert.ToInt32(width * height * percent);

            for (var j = 0; j < height; ++j)
            {
                for (var i = 0; i < width; ++i)
                {
                    result[j][i] = origin[j][i];
                }
            }

            var usedIndexI = new List<int[]>();
            var usedIndexJ = new List<int[]>();

            for (var x = 0; x < countOfNoisePixels; ++x)
            {
                int i, j;

                do
                {
                    i = random.Next(0, width);
                    j = random.Next(0, height);
                } while (usedIndexJ.Any(item => this.ArrayEquals(item, new[] { i, j })));
                usedIndexJ.Add(new[] { i, j });

                result[j][i] = !result[j][i];
            }

            return result;
        }

        private bool ArrayEquals(int[] first, int[] second) => first[0] == second[0] && first[1] == second[1];

        private void InitializeBoolMatrix(out bool[][] result, int width, int height)
        {
            result = new bool[height][];
            for (var i = 0; i < result.Length; ++i)
            {
                result[i] = new bool[width];
            }
        }
    }
}
