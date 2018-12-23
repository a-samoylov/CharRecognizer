using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MachineLearning.NeuralNetworkNS;

namespace CharRecognizer
{
    class TestNeuralNetwork
    {
        const string NAME               = "TestNeuralNetwork";
        const int INPUT_VECTOR_LENGTH   = 3;
        const double MIN_CORRECT_ANSWER = 0.6;

        private int[] neuronsInLayer = new int[] { INPUT_VECTOR_LENGTH, 2, 1 };
        private NeuralNetwork neuralNetwork;

        public NeuralNetwork GetNeuralNetwork()
        {
            if (this.neuralNetwork == null)
            {
                return this.neuralNetwork;
            }

            Manager neuralNetworkManager = new Manager();
            this.neuralNetwork = neuralNetworkManager.Get(NAME);

            return this.neuralNetwork;
        }

        public void GenerateRandom()
        {
            Factory neuralNetworkFactory = new Factory();
            NeuralNetwork neuralNetwork = neuralNetworkFactory.CreateWithRandomWeight(NAME, neuronsInLayer);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetwork);
        }

        public int GetAnswer(double[] inputData)
        {
            if (inputData.Length != INPUT_VECTOR_LENGTH)
            {
                throw new Exception("Invalid input data");
            }

            NeuralNetwork neuralNetwork = this.GetNeuralNetwork();

            neuralNetwork.SetInputVector(inputData);
            neuralNetwork.Process();

            if (neuralNetwork.GetLastLayer().GetNeuronById(1).GetData() >= MIN_CORRECT_ANSWER)
            {
                return 1;
            }

            return 0;
        }
    }
}

