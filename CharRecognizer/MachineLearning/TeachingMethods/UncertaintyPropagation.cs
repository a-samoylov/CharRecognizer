using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.TeachingMethods.ErrorMethods;

namespace CharRecognizer.MachineLearning.TeachingMethods
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
            neuralNetworkObj.Clear();

            neuralNetworkObj.SetInputVector(inputVector);
            neuralNetworkObj.Process();


            double[] resultVector   = new double[neuralNetworkObj.GetLastLayer().GetCountNeurons()];
            List<NeuronObj> neurons = neuralNetworkObj.GetLastLayer().GetListNeurons();
            for (int i = 0; i < neurons.Count; i++)
            {
                resultVector[i] = neurons[i].GetOutputData();
            }
            
            //double error = errorMethod.GetError(expectedResultVector, resultVector);
            //double weightDelta = 0;

            for (int currentLayerId = neuralNetworkObj.GetCountLayers() - 1; currentLayerId > 0; currentLayerId--)
            {
               
            }

            return neuralNetworkObj;
        }
    }
}
