using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc;
using CharRecognizer.MachineLearning.EducationMethods;

namespace CharRecognizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //GenerateTestNetworkWithWeight();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateTestNetworkWithWeight();

            TestNeuralNetwork network = new TestNeuralNetwork();
            double[] data = new double[] { 1, 0, 0 };

            MessageBox.Show(network.GetAnswer(data).ToString());
        }

        private void Test()
        {
            Manager neuralNetworkManager = new Manager();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkManager.Get("TestNeuralNetwork");

            double[] inputVector = new double[] { 1, 1, 0};
            double[] expectedVector = new double[] { 0 };
            
            UncertaintyPropagationMethod uncertaintyPropagationMethod = new UncertaintyPropagationMethod();
            uncertaintyPropagationMethod.GetTaughtNeuralNetwork(neuralNetworkObj, inputVector, expectedVector);
        }

        private void GenerateTestNetworkWithWeight()
        {
            string name = "TestNeuralNetwork";
            NeuralNetworkObj neuralNetworkObj = new NeuralNetworkObj(name);

            Layer layer1 = new Layer(1);
            layer1.SetPositionFirst();
            
            layer1.AddNeuron(new NeuronObj(1));
            layer1.AddNeuron(new NeuronObj(2));
            layer1.AddNeuron(new NeuronObj(3));

            Layer layer2 = new Layer(2);
            layer2.AddNeuron(new NeuronObj(1));
            layer2.AddNeuron(new NeuronObj(2));

            Layer layer3 = new Layer(3);
            layer3.SetPositionLast();
            
            layer3.AddNeuron(new NeuronObj(1));
            
            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.25));
            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(2), 0.5));

            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.25));
            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(2), -0.4));

            layer1.GetNeuronById(3).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0));
            layer1.GetNeuronById(3).AddSynapse(new Synapse(layer2.GetNeuronById(2), 0.9));

            layer2.GetNeuronById(1).AddSynapse(new Synapse(layer3.GetNeuronById(1), -1));
            layer2.GetNeuronById(2).AddSynapse(new Synapse(layer3.GetNeuronById(1), 1));

            neuralNetworkObj.AddLayer(layer1);
            neuralNetworkObj.AddLayer(layer2);
            neuralNetworkObj.AddLayer(layer3);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetworkObj);
        }

        private void GenerateTestNetworkWithErrorWeight()
        {
            string name = "TestErrorNeuralNetwork";
            NeuralNetworkObj neuralNetworkObj = new NeuralNetworkObj(name);

            Layer layer1 = new Layer(1);
            layer1.AddNeuron(new NeuronObj(1));
            layer1.AddNeuron(new NeuronObj(2));
            layer1.AddNeuron(new NeuronObj(3));

            Layer layer2 = new Layer(2);
            layer2.AddNeuron(new NeuronObj(1));
            layer2.AddNeuron(new NeuronObj(2));

            Layer layer3 = new Layer(3);
            layer3.AddNeuron(new NeuronObj(1));


            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.79));
            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(2), 0.85));

            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.44));
            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(2), 0.43));

            layer1.GetNeuronById(3).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.43));
            layer1.GetNeuronById(3).AddSynapse(new Synapse(layer2.GetNeuronById(2), 0.29));

            layer2.GetNeuronById(1).AddSynapse(new Synapse(layer3.GetNeuronById(1), 0.5));
            layer2.GetNeuronById(2).AddSynapse(new Synapse(layer3.GetNeuronById(1), 0.52));

            neuralNetworkObj.AddLayer(layer1);
            neuralNetworkObj.AddLayer(layer2);
            neuralNetworkObj.AddLayer(layer3);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetworkObj);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Test();
        }
    }
}
