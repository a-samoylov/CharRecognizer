using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.NeuralNetworkNS;
using MachineLearning.NeuralNetworkNS.RelationNS;

namespace MachineLearning.TeachingMethods
{
    class UncertaintyPropagationMethod
    {
        const double LEARNING_RATE = 0.1;

        public NeuralNetwork GetTeachedNeuralNetwork(NeuralNetwork neuralNetwork, double[] v, int sucessNeuronId, double expectedResult)
        {
            neuralNetwork.Clear();

            neuralNetwork.SetInputVector(v);
            neuralNetwork.Process();

            double error       = neuralNetwork.GetLastLayer().GetNeuronById(sucessNeuronId).GetOutputData() - expectedResult;
            double weightDelta = error * SigmoidDx(neuralNetwork.GetLastLayer().GetNeuronById(sucessNeuronId).GetInputData());

            for (int currentLayerId = neuralNetwork.GetListLayers().Count - 1; currentLayerId > 0; currentLayerId--)
            {
                foreach (Neuron neuron in neuralNetwork.GetLayerById(currentLayerId - 1).GetListNeurons())
                {
                    foreach (Relation relation in neuron.GetRelations())
                    {
                        relation.Weight = relation.Weight - (neuron.GetOutputData() * weightDelta * LEARNING_RATE);
                    }
                }

                //todo
                //error = weightDelta * 
            }

            return neuralNetwork;
        }

        //todo agregation obj
        private double SigmoidDx(double x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        }
    }
}
