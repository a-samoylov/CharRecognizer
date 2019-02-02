using System.Collections.Generic;
using System.IO;
using CharRecognizer.MachineLearning.EducationMethods.ErrorMethods;

namespace CharRecognizer.MachineLearning.NeuralNetwork.Report
{
    class Manager
    {
        private List<string> data = new List<string>();
        private RootMse errorMethod = new RootMse();
        private string pathToFileStorage;

        public Manager()
        {
            this.pathToFileStorage = Configs.GetInstance().GetPathToFileStorage();
        }

        public void SaveReport(NeuralNetworkObj neuralNetworkObj)
        {
            var dirPath = $"{this.pathToFileStorage}\\REPORT_{neuralNetworkObj.Name}";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            using (StreamWriter sw = new StreamWriter($"{dirPath}\\report_{neuralNetworkObj.GetCountEpochPassed()}.txt", false, System.Text.Encoding.Unicode))
            {
                foreach (var text in data)
                {
                    sw.WriteLine(text);
                }
            }
        }

        public void AddDataBeforeEducate(NeuralNetworkObj neuralNetworkObj, Dictionary<double[], double[]> prepareData)
        {
            foreach (var entity in prepareData)
            {
                double[] inputVector          = entity.Key;
                double[] expectedResultVector = entity.Value;

                neuralNetworkObj.Clear();
                neuralNetworkObj.SetInputVector(inputVector);
                neuralNetworkObj.Process();

                double[] resultVector = GetResultVector(neuralNetworkObj);

                data.Add("#########################################");
                data.Add($"Epoch:  {neuralNetworkObj.GetCountEpochPassed()} (Before educate)");
                data.Add($"Error:  {this.errorMethod.GetError(expectedResultVector, resultVector).ToString()}");
                data.Add("Expected Result:");
                foreach (var item in expectedResultVector)
                {
                    data.Add(item.ToString());
                }
                data.Add("Result:");
                foreach (var item in resultVector)
                {
                    data.Add(item.ToString());
                }
                data.Add("#########################################");
            }
        }

        public void AddDataAfterEducate(NeuralNetworkObj neuralNetworkObj, Dictionary<double[], double[]> prepareData)
        {
            foreach (var entity in prepareData)
            {
                double[] inputVector = entity.Key;
                double[] expectedResultVector = entity.Value;

                neuralNetworkObj.Clear();
                neuralNetworkObj.SetInputVector(inputVector);
                neuralNetworkObj.Process();

                double[] resultVector = GetResultVector(neuralNetworkObj);

                data.Add("#########################################");
                data.Add($"Epoch:  {neuralNetworkObj.GetCountEpochPassed()} (After educate)");
                data.Add($"Error:  {this.errorMethod.GetError(expectedResultVector, resultVector).ToString()}");
                data.Add("Expected Result:");
                foreach (var item in expectedResultVector)
                {
                    data.Add(item.ToString());
                }
                data.Add("Result:");
                foreach (var item in resultVector)
                {
                    data.Add(item.ToString());
                }
                data.Add("#########################################");
            }
        }

        private double[] GetResultVector(NeuralNetworkObj neuralNetworkObj)
        {
            double[] result = new double[neuralNetworkObj.GetLastLayer().GetCountNeurons()];

            List<NeuronObj> neurons = neuralNetworkObj.GetLastLayer().GetListNeurons();
            for (int i = 0; i < neurons.Count; i++)
            {
                result[i] = neurons[i].GetOutputData();
            }

            return result;
        }
    }
}
