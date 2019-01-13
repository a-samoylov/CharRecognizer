using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.EducationMethods.ErrorMethods;

namespace CharRecognizer.MachineLearning.EducationMethods
{
    class UncertaintyPropagationMethod
    {
        const double LEARNING_RATE = 0.1;

        private NeuralNetwork.Neuron.ActivationFunc.IDifferentiable activationFunc;
        private ErrorMethods.IBase errorMethod;
        
        public UncertaintyPropagationMethod()
        {
            errorMethod    = new RootMse();
            activationFunc = new NeuralNetwork.Neuron.ActivationFunc.Sigmoid();//todo make choice
        }
        
        public NeuralNetworkObj GetTaughtNeuralNetwork(NeuralNetworkObj neuralNetworkObj, double[] inputVector, double[] expectedResultVector)
        {
            double[] resultVector = this.GetResultVector(neuralNetworkObj, inputVector);

            List<UncertaintyPropagation.EducationLayer> layers = new List<UncertaintyPropagation.EducationLayer>();
            foreach (Layer layer in neuralNetworkObj.GetListLayers())
            {
                UncertaintyPropagation.EducationLayer methodLayer = new UncertaintyPropagation.EducationLayer();
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    UncertaintyPropagation.EducationNeuron methodNeuron = new UncertaintyPropagation.EducationNeuron();
                    methodNeuron.NeuronObj = neuron;

                    if (neuron.IsInLastLayer())
                    {
                        methodNeuron.WeightDelta = expectedResultVector[neuron.Id] - inputVector[neuron.Id] * activationFunc.GetDerivativeValue(neuron.GetInputData());
                    }
                    else
                    {
                        double sumWeightDeltaInNextLayer = 0;
                        foreach (NeuralNetwork.Neuron.Synapse synapse in neuron.GetSynapses())
                        {
                            
                        }
                        
                        methodNeuron.WeightDelta = this.activationFunc.GetDerivativeValue(neuron.GetInputData()) ;
                    }
                    
                    methodLayer.AddNeuron(methodNeuron);
                }
                
                layers.Add(methodLayer);
            }

            
            double error = errorMethod.GetError(expectedResultVector, resultVector);
            double weightDelta = 0;

            for (int currentLayerId = neuralNetworkObj.GetCountLayers() - 2; currentLayerId > 0; currentLayerId--)
            {
                foreach (NeuralNetwork.NeuronObj neuron in neuralNetworkObj.GetLayerById(currentLayerId - 1).GetListNeurons())
                {
                    double sumWeightDeltaNextLayer = 0;
                    foreach (NeuralNetwork.Neuron.Synapse synapse in neuron.GetSynapses())
                    {
                        NeuralNetwork.NeuronObj nextNeuron = synapse.NeuronObj;
                    }
                        
                    
                    //foreach (Synapse synapse in neuron.GetSynapses())
                    //{
                    //    synapse.Weight = synapse.Weight + (neuron.GetOutputData() * weightDelta * LEARNING_RATE);//todo add moment
                    //}
                }
            }

            return neuralNetworkObj;
        }

        private double[] GetResultVector(NeuralNetworkObj neuralNetworkObj, double[] inputVector)
        {
            neuralNetworkObj.Clear();

            neuralNetworkObj.SetInputVector(inputVector);
            neuralNetworkObj.Process();

            double[] resultVector = new double[neuralNetworkObj.GetLastLayer().GetCountNeurons()];
            List<NeuralNetwork.NeuronObj> neurons = neuralNetworkObj.GetLastLayer().GetListNeurons();
            for (int i = 0; i < neurons.Count; i++)
            {
                resultVector[i] = neurons[i].GetOutputData();
            }

            return resultVector;
        }
    }
}
