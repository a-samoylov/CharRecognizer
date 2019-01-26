using System;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer
{
    class NumberRecognizerNeuralNetworkObj : AbstractNetwork
    {
        const string NAME    = "CharRecognizerNeuralNetwork";
        const int IMG_SIZE   = 100 * 100;
        
        const double MINIMUM_OUTPUT_DATA_FOR_SUCCESS_RESULT = 0.8;

        private int[] neuronsInLayer = new int[] { IMG_SIZE, 100, 30, 9 };

        public int GetNumberFromImgVector(double[] inputVector)
        {
            if (inputVector.Length != IMG_SIZE)
            {
                throw new Exception("Invalid input vector length.");
            }

            //todo load network LazyLoad
            Manager neuralNetworkManager = new Manager();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkManager.Get(NAME);

            neuralNetworkObj.SetInputVector(inputVector);
            neuralNetworkObj.Process();

            foreach (NeuronObj neuron in neuralNetworkObj.GetLastLayer().GetListNeurons())
            {
                if (neuron.GetOutputData() > MINIMUM_OUTPUT_DATA_FOR_SUCCESS_RESULT)
                {
                    return neuron.Id;
                }
            }

            return -1;
        }
        
        public override string GetNetworkName()
        {
            return NAME;
        }

        public override int[] GetCountNeuronsInLayer()
        {
            return neuronsInLayer;
        }
    }
}
