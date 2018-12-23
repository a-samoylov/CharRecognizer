using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.NeuralNetworkNS
{
    class Manager
    {
        public NeuralNetwork Get(string name)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(name + ".dat", FileMode.Open))
            {
                return (NeuralNetwork)formatter.Deserialize(fs);
            }
        }

        public void Save(NeuralNetwork neuralNetwork)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(neuralNetwork.Name + ".dat", FileMode.Create))
            {
                formatter.Serialize(fs, neuralNetwork);
            }
        }
    }
}
