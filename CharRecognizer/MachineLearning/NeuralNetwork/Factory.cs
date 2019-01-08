using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    class Factory
    {
        public NeuralNetworkObj CreateWithRandomWeight(string name, int[] countNeuronsInLayer)
        {
            NeuralNetworkObj neuralNetworkObj = new NeuralNetworkObj(name);

            for (int layerId = 0; layerId < countNeuronsInLayer.Length; layerId++)
            {
                Layer layer = new Layer(layerId);
                if (layerId == 0)
                {
                    layer.SetPositionFirst();
                }
                else if (layerId == countNeuronsInLayer.Length - 1)
                {
                    layer.SetPositionLast();
                }
                
                for (int neuronId = 0; neuronId < countNeuronsInLayer[layerId]; neuronId++)
                {
                    NeuronObj neuronObj = new NeuronObj(neuronId);
                    layer.AddNeuron(neuronObj);
                }

                neuralNetworkObj.AddLayer(layer);
            }

            Random rand        = new Random();
            List<Layer> layers = neuralNetworkObj.GetListLayers();
            for (int layerId = 0; layerId < layers.Count - 1; layerId++)
            {
                Layer layer     = layers[layerId];
                Layer nextLayer = layers[layerId + 1];

                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    foreach (NeuronObj nextLayerNeuron in nextLayer.GetListNeurons())
                    {
                        double weight = Convert.ToDouble(rand.Next(-100, 100)) / 100;
                        neuron.AddSynapse(new Synapse(nextLayerNeuron, weight));
                    }
                }
            }

            return neuralNetworkObj;
        }
    }
}
