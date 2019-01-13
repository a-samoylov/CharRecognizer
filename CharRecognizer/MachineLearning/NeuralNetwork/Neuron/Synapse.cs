using System;

namespace CharRecognizer.MachineLearning.NeuralNetwork.Neuron
{
    [Serializable]
    public class Synapse
    {
        public NeuronObj NeuronObj { get; }

        public double Weight { get; set; }

        public Synapse(NeuronObj neuronObj, double weight)
        {
            NeuronObj = neuronObj;
            Weight = weight;
        }
    }
}
