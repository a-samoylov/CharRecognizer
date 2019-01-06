using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning
{
    [Serializable]
    class NeuralNetworkObj
    {
        public string Name { get; }

        private List<Layer> layers    = new List<Layer>();
        private bool isSetInputVector = false;

        public NeuralNetworkObj(string name)
        {
            Name = name;
        }

        public Layer GetFirstLayer()
        {
            if (layers.Count == 0)
            {
                throw new Exception("Layers is empty.");
            }

            return layers[0];
        }

        public Layer GetLastLayer()
        {
            if (layers.Count == 0)
            {
                throw new Exception("Layers is empty.");
            }

            return layers[layers.Count - 1];
        }

        public List<Layer> GetListLayers()
        {
            return layers;
        }

        public Layer GetLayerById(int id)
        {
            foreach (Layer layer in layers)
            {
                if (layer.Id == id)
                {
                    return layer;
                }
            }

            return null;
        }

        public void AddLayer(Layer layer)
        {
            if (GetLayerById(layer.Id) != null)
            {
                throw new Exception("Layer with this id has already added.");
            }

            layers.Add(layer);
        }

        public void SetInputVector(double[] v)
        {
            List<NeuronObj> neurons = this.GetFirstLayer().GetListNeurons();
            if (v.Length != neurons.Count)
            {
                throw new Exception("Invalid input vector.");
            }

            isSetInputVector = true;

            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].AddSignal(v[i]);
                //neurons[i].IsActive = true;
            }
        }

        public void Process()
        {
            if (!isSetInputVector)
            {
                throw new Exception("The input vector is not set.");
            }

            foreach (Layer layer in layers)
            {
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    neuron.SendSignals();
                }
            }
        }

        public void Clear()
        {
            isSetInputVector = false;

            foreach (Layer layer in layers)
            {
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    neuron.ClearData();
                }
            }
        }
    }
}
