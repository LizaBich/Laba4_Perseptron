using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class HiddenNeuron
    {
        /// <summary>
        /// Border value.
        /// </summary>
        private float _borderValue;

        /// <summary>
        /// Count of neurons in previous layer.
        /// </summary>
        private byte _inputLength;

        /// <summary>
        /// Values from hidden layer.
        /// </summary>
        private sbyte[] _inputs;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HiddenNeuron(byte countOfInputs)
        {
            this._inputLength = countOfInputs;
            this.InputWeights = new sbyte[this._inputLength];
            this.ActiveInputs = new List<byte>();
            this.Initialize();
        }

        /// <summary>
        /// Weights of input connections.
        /// </summary>
        public sbyte[] InputWeights { get; private set; }

        /// <summary>
        /// Numbers of active inputs (used on network learning).
        /// </summary>
        public IList<byte> ActiveInputs { get; private set; }

        /// <summary>
        /// Output of neuron.
        /// </summary>
        public sbyte NeuronOutput { get; private set; }

        /// <summary>
        /// Initialize start values.
        /// </summary>
        private void Initialize()
        {
            var random = new Random();

            this._borderValue = random.Next(-1, 1);

            for (var i = 0; i < this._inputLength; ++i)
            {
                this.InputWeights[i] = Convert.ToSByte(random.Next(-1, 1));
            }
        }

        public void PrepareOutput(sbyte[] inputs)
        {
            // f = { 1, x > 0 ; 0, x <= 0}
            this._inputs = inputs;
            var sum = 0f;

            for (var i = 0; i < inputs.Length && i < this.InputWeights.Length; ++i)
            {
                sum += inputs[i] * this.InputWeights[i];

                if (inputs[i] == 1)
                {
                    this.ActiveInputs.Add(Convert.ToByte(i));
                }
            }

            this.NeuronOutput = sum + this._borderValue > 0 ? (sbyte)1 : (sbyte)0;
        }

        public void AdjustTheWeights(int b, float error)
        {
            var coef = b * error * (this.NeuronOutput == 0 ? float.MaxValue : 0);

            this._borderValue = this._borderValue + coef;

            for (var i = 0; i < this.InputWeights.Length; ++i)
            {
                var temp = this.InputWeights[i] + coef * this._inputs[i];
                this.InputWeights[i] = temp > 0 ? (sbyte)1 : temp < 0 ? (sbyte)-1 : (sbyte)0;
            }
        }
    }
}
