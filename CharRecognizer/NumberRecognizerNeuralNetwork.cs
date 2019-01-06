using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

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
            NeuralNetworkObj neuralNetworkObj = neuralNetworkFactory.CreateWithRandomWeight(NAME, neuronsInLayer);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetworkObj);
        }

        public int GetNumberFromImgVector(double[] inputData)
        {
            if (inputData.Length != IMG_SIZE)
            {
                throw new Exception("Invalid input data");
            }

            //todo load network LazyLoad
            Manager neuralNetworkManager = new Manager();
            NeuralNetworkObj neuralNetworkObj  = neuralNetworkManager.Get(NAME);

            neuralNetworkObj.SetInputVector(inputData);
            neuralNetworkObj.Process();

            //for neuralNetwork.GetLastLayer();

            return 0;
        }
    }
}
