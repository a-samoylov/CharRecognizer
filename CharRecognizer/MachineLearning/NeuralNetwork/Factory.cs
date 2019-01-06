using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    class Factory
    {
        public NeuralNetworkObj CreateWithRandomWeight(string name, int[] counrNeuronsInLayer)
        {
            NeuralNetworkObj neuralNetworkObj = new NeuralNetworkObj(name);

            for (int layerId = 0; layerId < counrNeuronsInLayer.Length; layerId++)
            {
                Layer layer = new Layer(layerId);
                for (int neuronId = 0; neuronId < counrNeuronsInLayer[layerId]; neuronId++)
                {
                    NeuronObj neuronObj = new NeuronObj(neuronId, true);
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
                        neuron.AddRelation(new Synapse(nextLayerNeuron, weight));
                    }
                }
            }

            return neuralNetworkObj;
        }
    }
}
