using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.NeuralNetworkNS
{
    [Serializable]
    class Layer
    {
        public int Id { get; }

        private List<Neuron> neurons = new List<Neuron>();
               
        public Layer(int id)
        {
            Id = id;
        }

        public List<Neuron> GetListNeurons()
        {
            return neurons;
        }

        public Neuron GetNeuronById(int id)
        {
            foreach (Neuron neuron in neurons)
            {
                if (neuron.Id == id)
                {
                    return neuron;
                }
            }

            return null;
        }

        public void AddNeuron(Neuron neuron)
        {
            if (GetNeuronById(neuron.Id) != null)
            {
                throw new Exception("Neuron with this id has already added.");
            }

            neurons.Add(neuron);
        }

        public void SendNextLayerSignal()
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.SendSignals();
            }
        }               
    }
}
