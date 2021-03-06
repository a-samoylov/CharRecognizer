﻿using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;

namespace CharRecognizer.MachineLearning.EducationMethods
{
    class UncertaintyPropagationMethod
    {
        private NeuralNetwork.Neuron.ActivationFunc.IDifferentiable activationFunc;
        private List<EducationLayer> _educationNetwork;
        private double learningRate;
        
        public UncertaintyPropagationMethod(double learningRate)
        {
            activationFunc = new NeuralNetwork.Neuron.ActivationFunc.Sigmoid();//todo make choice
            this.learningRate = learningRate;
        }
        
        public NeuralNetworkObj GetTaughtNeuralNetwork(NeuralNetworkObj neuralNetworkObj, double[] inputVector, double[] expectedResultVector)
        {
            double[] resultVector = this.GetResultVector(neuralNetworkObj, inputVector);

            List<EducationLayer> educationNetwork = this.GetEducationNetwork(neuralNetworkObj);
            for (int educationLayerId = educationNetwork.Count - 1; educationLayerId >= 0; educationLayerId--)
            {
                EducationLayer educationLayer = educationNetwork[educationLayerId];
                foreach (EducationNeuron educationNeuron in educationLayer.GetListNeurons())
                {
                    NeuronObj neuron = educationNeuron.NeuronObj;
                    if (neuron.IsInLastLayer())
                    {
                        //deltaWeight0 = (OUT(ideal) - OUT(actual)) * f'(IN)
                        educationNeuron.WeightDelta = (expectedResultVector[neuron.Id] - resultVector[neuron.Id]) * activationFunc.GetDerivativeValue(neuron.GetInputData());
                    }
                    else
                    {
                        //deltaWeightH = f'(IN) * sum(Wi * delataWeighti)
                        double sumWeightDeltaInNextLayer = 0;
                        foreach (EducationSynapse educationSynapse in educationNeuron.GetEducationSynapses())
                        {
                            sumWeightDeltaInNextLayer += educationSynapse.Synapse.Weight * educationSynapse.EducationNeuron.WeightDelta;
                        }
                
                        educationNeuron.WeightDelta = this.activationFunc.GetDerivativeValue(neuron.GetInputData()) * sumWeightDeltaInNextLayer;
                        
                        foreach (EducationSynapse educationSynapse in educationNeuron.GetEducationSynapses())
                        {
                            //grad(A, B) = deltaWeight(B) * OUT(A)
                            double grad = educationSynapse.EducationNeuron.WeightDelta * educationNeuron.NeuronObj.GetOutputData();
                            educationSynapse.Synapse.Weight += this.learningRate * grad;//todo moment a*deltaWi
                        }
                    }
                }
            }

            return neuralNetworkObj;
        }

        private double[] GetResultVector(NeuralNetworkObj neuralNetworkObj, double[] inputVector)
        {
            double[] result = new double[neuralNetworkObj.GetLastLayer().GetCountNeurons()];

            neuralNetworkObj.Clear();

            neuralNetworkObj.SetInputVector(inputVector);
            neuralNetworkObj.Process();

            List<NeuralNetwork.NeuronObj> neurons = neuralNetworkObj.GetLastLayer().GetListNeurons();
            for (int i = 0; i < neurons.Count; i++)
            {
                result[i] = neurons[i].GetOutputData();
            }

            return result;
        }

        private List<EducationLayer> GetEducationNetwork(NeuralNetworkObj neuralNetworkObj)
        {
            if (this._educationNetwork != null)
            {
                return this._educationNetwork;
            }

            List<EducationLayer> result = new List<EducationLayer>();
            foreach (Layer layer in neuralNetworkObj.GetListLayers())
            {
                EducationLayer educationLayer = new EducationLayer(layer.Id);
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    educationLayer.AddNeuron(new EducationNeuron(neuron));
                }
                
                result.Add(educationLayer);
            }

            for (int educationLayerId = 0; educationLayerId < result.Count - 1; educationLayerId++)
            {
                EducationLayer educationLayer     = result[educationLayerId];
                EducationLayer nextEducationLayer = result[educationLayerId + 1];

                foreach (EducationNeuron educationNeuron in educationLayer.GetListNeurons())
                {
                    foreach (EducationNeuron nextLayerEducationNeuron in nextEducationLayer.GetListNeurons())
                    {
                        Synapse synapse = this.GetSynapse(
                            neuralNetworkObj,
                            educationLayer.Id, 
                            educationNeuron.NeuronObj.Id, 
                            nextLayerEducationNeuron.NeuronObj.Id
                        );

                        if (synapse == null)
                        {
                            throw new Exception("Something goes wrong with id layers.");
                        }
                        
                        educationNeuron.AddEducationSynapse(new EducationSynapse(nextLayerEducationNeuron, synapse));
                    }
                }
            }

            this._educationNetwork = result;

            return this._educationNetwork;
        }

        private Synapse GetSynapse(NeuralNetworkObj neuralNetworkObj, int currentLayerId, int neuronId, int nextNeuronId)
        {
            foreach (Synapse synapse in neuralNetworkObj.GetLayerById(currentLayerId).GetNeuronById(neuronId).GetSynapses())
            {
                if (synapse.NeuronObj.Id == nextNeuronId)
                {
                    return synapse;
                }
            }

            return null;
        }
    }
}
