using MachineLearning.NeuralNetworkNS;
using MachineLearning.NeuralNetworkNS.NeuronNS.SynapseNS;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc;

namespace MachineLearning.TeachingMethods
{
    class UncertaintyPropagationMethod
    {
        const double LEARNING_RATE = 0.1;

        private IDifferentiable activationFunc;
        
        public UncertaintyPropagationMethod()
        {
            activationFunc = new Sigmoid();//todo make choice
        }
        
        public NeuralNetwork GetTaughtNeuralNetwork(NeuralNetwork neuralNetwork, double[] v, int successNeuronId, double expectedResult)
        {
            neuralNetwork.Clear();

            neuralNetwork.SetInputVector(v);
            neuralNetwork.Process();

            double error       = neuralNetwork.GetLastLayer().GetNeuronById(successNeuronId).GetOutputData() - expectedResult;
            double weightDelta = error * activationFunc.GetDerivativeValue(neuralNetwork.GetLastLayer().GetNeuronById(successNeuronId).GetInputData());

            for (int currentLayerId = neuralNetwork.GetListLayers().Count - 1; currentLayerId > 0; currentLayerId--)
            {
                foreach (Neuron neuron in neuralNetwork.GetLayerById(currentLayerId - 1).GetListNeurons())
                {
                    foreach (Synapse relation in neuron.GetRelations())
                    {
                        relation.Weight = relation.Weight - (neuron.GetOutputData() * weightDelta * LEARNING_RATE);
                    }
                }

                //todo
                //error = weightDelta * 
            }

            return neuralNetwork;
        }
    }
}
