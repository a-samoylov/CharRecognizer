using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer
{
    public abstract class AbstractNetwork
    {
        private NeuralNetworkObj neuralNetworkObj;

        public NeuralNetworkObj GetNeuralNetwork()
        {
            if (this.neuralNetworkObj != null)
            {
                return this.neuralNetworkObj;
            }

            Manager neuralNetworkManager = new Manager();
            this.neuralNetworkObj = neuralNetworkManager.Get(this.GetNetworkName());

            return this.neuralNetworkObj;
        }
        
        public void GenerateRandom()
        {
            Factory neuralNetworkFactory = new Factory();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkFactory.CreateWithRandomWeight(this.GetNetworkName(), this.GetCountNeuronsInLayer());

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetworkObj);
        }

        public abstract string GetNetworkName();
        
        public abstract int[] GetCountNeuronsInLayer();
    }
}