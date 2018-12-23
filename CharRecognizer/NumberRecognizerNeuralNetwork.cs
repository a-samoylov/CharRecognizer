using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.NeuralNetworkNS;

namespace CharRecognizer
{
    class NumberRecognizerNeuralNetwork
    {
        const string NAME    = "CharRecognizerNeuralNetwork";
        const int IMG_SIZE   = 32 * 32;
        const double MINIMUM = 0.8;

        private int[] neuronsInLayer = new int[] { IMG_SIZE, 100, 30, 9 };

        public void Generate()
        {
            Factory neuralNetworkFactory = new Factory();
            NeuralNetwork neuralNetwork = neuralNetworkFactory.CreateWithRandomWeight(NAME, neuronsInLayer);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetwork);
        }

        public int GetNumberFromImgVector(double[] inputData)
        {
            if (inputData.Length != IMG_SIZE)
            {
                throw new Exception("Invalid input data");
            }

            //todo load network LazyLoad
            Manager neuralNetworkManager = new Manager();
            NeuralNetwork neuralNetwork  = neuralNetworkManager.Get(NAME);

            neuralNetwork.SetInputVector(inputData);
            neuralNetwork.Process();

            //for neuralNetwork.GetLastLayer();

            return 0;
        }
    }
}
