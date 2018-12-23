using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.NeuralNetworkNS
{
    [Serializable]
    class Relation
    {
        public Neuron Neuron { get; }

        public double Weight { get; set; }

        public Relation(Neuron neuron, double weight)
        {
            Neuron = neuron;
            Weight = weight;
        }
    }
}
