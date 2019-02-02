using System;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer
{
    class NumberRecognizerNeuralNetwork : AbstractNetwork
    {
        const double MINIMUM_OUTPUT_DATA_FOR_SUCCESS_RESULT = 0.8;

        private string name;
        private int countNeuronsInFirstLayer;
        private int[] neuronsInLayer;

        public NumberRecognizerNeuralNetwork(NeuralNetworkObj neuralNetwork)
        {
            this.name = neuralNetwork.Name; 
            this.SetNeuralNetworkObj(neuralNetwork);
            
            neuronsInLayer = new int[neuralNetwork.GetCountLayers()];

            int index = 0;
            foreach (Layer layer in neuralNetwork.GetListLayers())
            {
                this.neuronsInLayer[index] = layer.GetCountNeurons();
                index++;
            }

            this.countNeuronsInFirstLayer = neuralNetwork.GetFirstLayer().GetCountNeurons();
        }
        
        public NumberRecognizerNeuralNetwork(string name)
        {
            this.name = name;

            NeuralNetworkObj neuralNetwork = this.GetNeuralNetwork();

            neuronsInLayer = new int[neuralNetwork.GetCountLayers()];

            int index = 0;
            foreach (Layer layer in neuralNetwork.GetListLayers())
            {
                this.neuronsInLayer[index] = layer.GetCountNeurons();
                index++;
            }

            this.countNeuronsInFirstLayer = neuralNetwork.GetFirstLayer().GetCountNeurons();
        }

        public NumberRecognizerNeuralNetwork(string name, int[] neuronsInLayer, int imgHeight, int imgWidth)
        {
            if (neuronsInLayer.Length < 2)
            {
                throw new Exception("The count of layers must be equal or greater than 2.");
            }

            if (neuronsInLayer[0] != imgHeight * imgWidth)
            {
                throw new Exception("Invalid count neurons in first layer.");
            }

            this.name                     = name;
            this.neuronsInLayer           = neuronsInLayer;
            this.countNeuronsInFirstLayer = imgWidth * imgHeight;
        }

        public int GetNumberFromImgVector(double[] inputVector)
        {
            if (inputVector.Length != this.countNeuronsInFirstLayer)
            {
                throw new Exception("Invalid input vector length.");
            }

            NeuralNetworkObj neuralNetworkObj = this.GetNeuralNetwork();

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
            return this.name;
        }

        public override int[] GetCountNeuronsInLayer()
        {
            return neuronsInLayer;
        }
    }
}
