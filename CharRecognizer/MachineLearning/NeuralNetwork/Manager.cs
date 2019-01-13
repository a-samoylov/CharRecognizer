using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    public class Manager
    {
        public NeuralNetworkObj Get(string name)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(name + ".dat", FileMode.Open))
            {
                return (NeuralNetworkObj)formatter.Deserialize(fs);
            }
        }

        public void Save(NeuralNetworkObj neuralNetworkObj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(neuralNetworkObj.Name + ".dat", FileMode.Create))
            {
                formatter.Serialize(fs, neuralNetworkObj);
            }
        }
    }
}
