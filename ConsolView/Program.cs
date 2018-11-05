using NeuralNetwork;
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
            var changedImages = new ChangedViews();
            var table = new Table();
            changedImages.PrepareViewsWithNoise(constants.EtalonValues, new int[] { 0, 60, 80 });
            Console.WriteLine($"Fill in speed of learning.");
            var a = Convert.ToInt32(Console.ReadLine());

            var network = new NetCore(15, 5, a, 1);

            Console.WriteLine($"Training started.");
            network.TrainTheNetwork(constants.GetMatrix());
            Console.WriteLine($"Training finished.\n\rIterations: {network.CountOfIterations}");

            foreach (var i in Enumerable.Range(0, changedImages.Views.Count))
            {
                Console.WriteLine($"Image {i + 1}\n\r\n\r{changedImages.ToString(i)}\n\r\n\r{constants.ToString()}\n\r");

                var klaster = network.ProceedData(changedImages.GetMatrix(i));

                var results = new bool[5];
                if (klaster > 0)
                {
                    results[klaster - 1] = true;
                }
                
                table.ColumnNames = new string[] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5" };
                var output = table.GenerateTable<bool, string>(results, new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });

                Console.WriteLine($"{output}\n\r\n\r");
            }

            Console.Read();
        }
    }
}
