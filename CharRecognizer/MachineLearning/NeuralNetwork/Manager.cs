using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    public class Manager
    {
        private string pathToFileStorage;

        public Manager()
        {
            this.pathToFileStorage = Configs.GetInstance().GetPathToFileStorage();
        }

        public string[] GetAllNames()
        {
            var dir   = new DirectoryInfo(this.pathToFileStorage);
            var files = dir.GetFiles();

            string[] result = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                result[i] = Path.GetFileNameWithoutExtension(files[i].FullName);
            }

            return result;
        }

        public NeuralNetworkObj Get(string name)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{this.pathToFileStorage}{name}.dat", FileMode.Open))
            {
                return (NeuralNetworkObj)formatter.Deserialize(fs);
            }
        }

        public void Save(NeuralNetworkObj neuralNetworkObj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{this.pathToFileStorage}\\{neuralNetworkObj.Name}.dat", FileMode.Create))
            {
                formatter.Serialize(fs, neuralNetworkObj);
            }
        }
    }
}
