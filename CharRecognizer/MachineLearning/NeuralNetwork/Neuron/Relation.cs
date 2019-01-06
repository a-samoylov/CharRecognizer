using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.NeuralNetworkNS.NeuronNS.SynapseNS
{
    [Serializable]
    class Synapse
    {
        public Neuron Neuron { get; }

        public double Weight { get; set; }

        public Synapse(Neuron neuron, double weight)
        {
            Neuron = neuron;
            Weight = weight;
        }
    }
}
