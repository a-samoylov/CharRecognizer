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
        
        public NeuralNetworkObj GetTaughtNeuralNetwork(NeuralNetworkObj neuralNetworkObj, double[] v, int successNeuronId, double expectedResult)
        {
            neuralNetworkObj.Clear();

            neuralNetworkObj.SetInputVector(v);
            neuralNetworkObj.Process();

            double error       = neuralNetworkObj.GetLastLayer().GetNeuronById(successNeuronId).GetOutputData() - expectedResult;
            double weightDelta = error * activationFunc.GetDerivativeValue(neuralNetworkObj.GetLastLayer().GetNeuronById(successNeuronId).GetInputData());

            for (int currentLayerId = neuralNetworkObj.GetListLayers().Count - 1; currentLayerId > 0; currentLayerId--)
            {
                foreach (NeuronObj neuron in neuralNetworkObj.GetLayerById(currentLayerId - 1).GetListNeurons())
                {
                    foreach (Synapse relation in neuron.GetSynapses())
                    {
                        relation.Weight = relation.Weight - (neuron.GetOutputData() * weightDelta * LEARNING_RATE);
                    }
                }

                //todo
                //error = weightDelta * 
            }

            return neuralNetworkObj;
        }
    }
}
