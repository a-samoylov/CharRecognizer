using System;
using System.Collections.Generic;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    [Serializable]
    public class Layer
    {
        public int Id { get; }

        private bool            isFirst = false;
        private bool            isLast  = false;
        private List<NeuronObj> neurons = new List<NeuronObj>();
               
        public Layer(int id)
        {
            Id = id;
        }

        public void SetPositionFirst()
        {
            isFirst = true;
        }

        public void SetPositionLast()
        {
            isLast = true;
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

            if (isFirst)
            {
                neuronObj.SetPositionInFirstLayer();
            } 
            else if (isLast)
            {
                neuronObj.SetPositionInLastLayer();
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
