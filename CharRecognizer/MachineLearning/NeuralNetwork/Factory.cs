using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.NeuralNetworkNS.RelationNS;

namespace MachineLearning.NeuralNetworkNS
{
    class Factory
    {
        public NeuralNetwork CreateWithRandomWeight(string name, int[] counrNeuronsInLayer)
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork(name);

            for (int layerId = 0; layerId < counrNeuronsInLayer.Length; layerId++)
            {
                Layer layer = new Layer(layerId);
                for (int neuronId = 0; neuronId < counrNeuronsInLayer[layerId]; neuronId++)
                {
                    Neuron neuron = new Neuron(neuronId, true);
                    layer.AddNeuron(neuron);
                }

                neuralNetwork.AddLayer(layer);
            }

            Random rand        = new Random();
            List<Layer> layers = neuralNetwork.GetListLayers();
            for (int layerId = 0; layerId < layers.Count - 1; layerId++)
            {
                Layer layer     = layers[layerId];
                Layer nextLayer = layers[layerId + 1];

                foreach (Neuron neuron in layer.GetListNeurons())
                {
                    foreach (Neuron nextLayerNeuron in nextLayer.GetListNeurons())
                    {
                        double weight = Convert.ToDouble(rand.Next(-100, 100)) / 100;
                        neuron.AddRelation(new Relation(nextLayerNeuron, weight));
                    }
                }
            }

            return neuralNetwork;
        }
    }
}
