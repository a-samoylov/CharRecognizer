using System;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer
{
    class TestNeuralNetwork : AbstractNetwork
    {
        const string NAME               = "TestNeuralNetwork";
        const int INPUT_VECTOR_LENGTH   = 3;
        const double MIN_CORRECT_ANSWER = 0.9;

        private int[] neuronsInLayer = new int[] { 3, 2, 1 };

        public int GetAnswer(double[] inputVector)
        {
            if (inputVector.Length != INPUT_VECTOR_LENGTH)
            {
                throw new Exception("Invalid input vector length.");
            }

            NeuralNetworkObj neuralNetwork = this.GetNeuralNetwork();

            neuralNetwork.SetInputVector(inputVector);
            neuralNetwork.Process();

            if (neuralNetwork.GetLastLayer().GetNeuronById(1).GetOutputData() >= MIN_CORRECT_ANSWER)
            {
                return 1;
            }

            return 0;
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

