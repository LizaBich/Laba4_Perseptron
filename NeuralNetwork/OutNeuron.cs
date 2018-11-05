using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class OutNeuron
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
        public OutNeuron(byte countOfInputs)
        {
            this._inputLength = countOfInputs;
            this.InputWeights = new float[this._inputLength];
            this.ActiveInputs = new List<byte>();
        }

        /// <summary>
        /// Weights of input connections.
        /// </summary>
        public float[] InputWeights { get; private set; }

        /// <summary>
        /// Numbers of active inputs (used on network learning).
        /// </summary>
        public IList<byte> ActiveInputs { get; private set; }

        /// <summary>
        /// Output of neuron.
        /// </summary>
        public sbyte NeuronOutput { get; private set; }

        /// <summary>
        /// Error for hidden layer.
        /// </summary>
        public float E { get; private set; }

        /// <summary>
        /// Initialize start values.
        /// </summary>
        private void Initialize()
        {
            var random = new Random();

            this._borderValue = random.Next(0, 1) == 0 ? -1 : 1;

            for (var i = 0; i < this._inputLength; ++i)
            {
                this.InputWeights[i] = Convert.ToSByte(random.Next(0, 1) == 0 ? -1 : 1);
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

        public void AdjustTheWeights(sbyte idealOutput, int a)
        {
            var coef = a * (idealOutput - this.NeuronOutput) * (this.NeuronOutput == 0 ? 255 : 0);

            this._borderValue = this._borderValue + coef;

            var e = 0f;

            for (var i = 0; i < this.InputWeights.Length; ++i)
            {
                e += coef * this.InputWeights[i] / a;

                this.InputWeights[i] = this.InputWeights[i] + coef * this._inputs[i];
            }

            this.E = e;
        }
    }
}
