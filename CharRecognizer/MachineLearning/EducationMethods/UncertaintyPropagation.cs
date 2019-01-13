using System.Collections.Generic;
using CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation;
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

            List<EducationLayer> educationNetwork = this.GetEducationNetwork(neuralNetworkObj);
            foreach (EducationLayer educationLayer in educationNetwork)
            {
                foreach (EducationNeuron educationNeuron in educationLayer.GetListNeurons())
                {
                    NeuronObj neuron = educationNeuron.NeuronObj;
                    if (neuron.IsInLastLayer())
                    {
                        educationNeuron.WeightDelta = expectedResultVector[neuron.Id] - inputVector[neuron.Id] * activationFunc.GetDerivativeValue(neuron.GetInputData());
                    }
                    else
                    {
                        double sumWeightDeltaInNextLayer = 0;
                        foreach (EducationNeuron nextLayerEducationNeuron in educationNeuron.NextEducationLayer.GetListNeurons())
                        {
                            sumWeightDeltaInNextLayer += nextLayerEducationNeuron.WeightDelta * 1;
                        }
                
                        educationNeuron.WeightDelta = this.activationFunc.GetDerivativeValue(neuron.GetInputData()) ;
                    }
                }
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
            List<EducationLayer> result = new List<EducationLayer>();
            foreach (Layer layer in neuralNetworkObj.GetListLayers())
            {
                EducationLayer educationLayer = new EducationLayer();
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
                    educationNeuron.NextEducationLayer = nextEducationLayer;
                }
            }

            return result;
        }
    }
}
