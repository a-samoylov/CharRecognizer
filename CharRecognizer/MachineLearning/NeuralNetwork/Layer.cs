using System;
using System.Collections.Generic;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    [Serializable]
    class Layer
    {
        public int Id { get; }

        private List<NeuronObj> neurons = new List<NeuronObj>();
               
        public Layer(int id)
        {
            Id = id;
        }

        public List<NeuronObj> GetListNeurons()
        {
            return neurons;
        }

        public int GetCountNeurons()
        {
            return neurons.Count;
        }

        public NeuronObj GetNeuronById(int id)
        {
            foreach (NeuronObj neuron in neurons)
            {
                if (neuron.Id == id)
                {
                    return neuron;
                }
            }

            return null;
        }

        public void AddNeuron(NeuronObj neuronObj)
        {
            if (GetNeuronById(neuronObj.Id) != null)
            {
                throw new Exception("Neuron with this id has already added.");
            }

            neurons.Add(neuronObj);
        }

        public void SendNextLayerSignal()
        {
            foreach (NeuronObj neuron in neurons)
            {
                neuron.SendSignals();
            }
        }               
    }
}
