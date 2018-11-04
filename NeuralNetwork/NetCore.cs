using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NetCore
    {
        private byte _inputLength;

        private byte _outputLength;

        private int _a;

        private sbyte _d;

        private IList<HiddenNeuron> _hiddenLayer;

        private IList<OutNeuron> _outLayer;

        public NetCore(byte inputLength, byte outputLength, int a, sbyte d)
        {
            this._inputLength = inputLength;
            this._outputLength = outputLength;
            this._a = a;
            this._d = d;
            this.Initialize();
        }

        public int CountOfIterations { get; private set; }

        public void TrainTheNetwork(IList<bool[,]> trainImages)
        {
            var dks = new sbyte[this._outputLength];
            for (var i = 0; i < this._outputLength; ++i)
            {
                dks[i] = 1;
            }

            foreach (var image in trainImages)
            {
                var ideal = new sbyte[this._outputLength];
                ideal[trainImages.IndexOf(image)] = 1;
                this.TrainNetworkWithSpecialImage(this.ConvertBoolMatrixToMassive(image), dks, ideal);
            }
        }

        public int ProceedData(bool[,] image)
        {
            var inputs = this.ConvertBoolArrayToSbyteArray(this.ConvertBoolMatrixToMassive(image));
            Parallel.ForEach(this._hiddenLayer, item => item.PrepareOutput(inputs));
            var sbyteHInnputs = this._hiddenLayer.Select(item => item.NeuronOutput).ToArray();
            Parallel.ForEach(this._outLayer, item => item.PrepareOutput(sbyteHInnputs));
            return this._outLayer.Select(item => item.NeuronOutput).ToList().IndexOf(1) + 1;
        }

        private void Initialize()
        {
            this._hiddenLayer = new List<HiddenNeuron>();
            this._outLayer = new List<OutNeuron>();

            var countOfHiddenNeurons = (this._inputLength + this._outputLength) / 2;

            foreach (var i in Enumerable.Range(0, countOfHiddenNeurons))
            {
                this._hiddenLayer.Add(new HiddenNeuron(this._inputLength));
            }

            foreach (var i in Enumerable.Range(0, this._outputLength))
            {
                this._outLayer.Add(new OutNeuron((byte)countOfHiddenNeurons));
            }
        }

        private bool[] ConvertBoolMatrixToMassive(bool[,] source)
        {
            var result = new bool[source.Length];
            var maxI = source.GetLength(1);

            for (var j = 0; j < source.GetLength(0); ++j)
            {
                for (var i = 0; i < source.GetLength(1); ++i)
                {
                    result[j * maxI + i] = source[j, i];
                }
            }

            return result;
        }

        private void TrainNetworkWithSpecialImage(bool[] image, sbyte[] dks, sbyte[] idealOutputs)
        {
            var maxError = 0;
            var sbyteInnputs = this.ConvertBoolArrayToSbyteArray(image);
            do
            {
                Parallel.ForEach(this._hiddenLayer, item => item.PrepareOutput(sbyteInnputs));
                var sbyteHInnputs = this._hiddenLayer.Select(item => item.NeuronOutput).ToArray();
                Parallel.ForEach(this._outLayer, item => item.PrepareOutput(sbyteHInnputs));
                this.AdjustValues(idealOutputs);
                var totalError = this._outLayer.Select(item => item.E).Sum();
                Parallel.ForEach(this._hiddenLayer, item => item.AdjustTheWeights(this._a, totalError));
                this.CalculateErrors(ref dks, sbyteInnputs, this._outLayer.Select(item => item.NeuronOutput).ToArray());
                maxError = dks.Select(item => Math.Abs(item)).Max();
                ++this.CountOfIterations;
            }
            while (maxError > this._d);
        }

        private sbyte[] ConvertBoolArrayToSbyteArray(bool[] input) => input.Select(item => item ? (sbyte)1 : (sbyte)0).ToArray();

        private void CalculateErrors(ref sbyte[] dks, sbyte[] ideal, sbyte[] real)
        {
            var maxCount = (new int[] { dks.Length, ideal.Length, real.Length }).Min();
            foreach (var i in Enumerable.Range(0, maxCount))
            {
                dks[i] = (sbyte)((int)ideal[i] - (int)real[i]);
            }
        }

        private void AdjustValues(sbyte[] ideal)
        {
            var maxCount = (new int[] { ideal.Length, this._outLayer.Count }).Min();
            foreach (var i in Enumerable.Range(0, maxCount))
            {
                this._outLayer[i].AdjustTheWeights(ideal[i], this._a);
            }
        }
    }
}
