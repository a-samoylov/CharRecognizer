using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer
{
    class TestNeuralNetwork
    {
        const string NAME               = "TestNeuralNetwork";
        const int INPUT_VECTOR_LENGTH   = 3;
        const double MIN_CORRECT_ANSWER = 0.5;

        private int[] neuronsInLayer = new int[] { INPUT_VECTOR_LENGTH, 2, 1 };
        private NeuralNetworkObj neuralNetworkObj;

        public NeuralNetworkObj GetNeuralNetwork()
        {
            if (this.neuralNetworkObj != null)
            {
                return this.neuralNetworkObj;
            }

            Manager neuralNetworkManager = new Manager();
            this.neuralNetworkObj = neuralNetworkManager.Get(NAME);

            return this.neuralNetworkObj;
        }

        public void GenerateRandom()
        {
            Factory neuralNetworkFactory = new Factory();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkFactory.CreateWithRandomWeight(NAME, neuronsInLayer);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetworkObj);
        }

        public int GetAnswer(double[] inputData)
        {
            if (inputData.Length != INPUT_VECTOR_LENGTH)
            {
                throw new Exception("Invalid input data");
            }

            NeuralNetworkObj neuralNetworkObj = this.GetNeuralNetwork();

            neuralNetworkObj.SetInputVector(inputData);
            neuralNetworkObj.Process();

            if (neuralNetworkObj.GetLastLayer().GetNeuronById(1).GetOutputData() >= MIN_CORRECT_ANSWER)
            {
                return 1;
            }

            return 0;
        }
    }
}

