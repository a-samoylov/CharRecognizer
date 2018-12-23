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
using MachineLearning.NeuralNetwork;
using MachineLearning.TeachingMethods;

namespace CharRecognizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /*GenerateTestNetworkWithErrorWeight();

            UncertaintyPropagationMethod uncertaintyPropagationMethod = new UncertaintyPropagationMethod();

            Manager neuralNetworkManager = new Manager();
            NeuralNetwork neuralNetwork = neuralNetworkManager.Get("TestErrorNeuralNetwork");

            double[] v = new double[] { 1, 1, 0};
            uncertaintyPropagationMethod.GetTeachedNeuralNetwork(neuralNetwork, v, 1, 0);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateTestNetworkWithWeight();

            TestNeuralNetwork network = new TestNeuralNetwork();
            double[] data = new double[] { 1, 0, 0 };

            MessageBox.Show(network.GetAnswer(data).ToString());
        }

        private void GenerateTestNetworkWithWeight()
        {
            string name = "TestNeuralNetwork";
            NeuralNetwork neuralNetwork = new NeuralNetwork(name);

            Layer layer1 = new Layer(1);
            layer1.AddNeuron(new Neuron(1));
            layer1.AddNeuron(new Neuron(2));
            layer1.AddNeuron(new Neuron(3));

            Layer layer2 = new Layer(2);
            layer2.AddNeuron(new Neuron(1));
            layer2.AddNeuron(new Neuron(2));

            Layer layer3 = new Layer(3);
            layer3.AddNeuron(new Neuron(1));
            
            layer1.GetNeuronById(1).AddRelation(new Relation(layer2.GetNeuronById(1), 0.25));
            layer1.GetNeuronById(1).AddRelation(new Relation(layer2.GetNeuronById(2), 0.5));

            layer1.GetNeuronById(2).AddRelation(new Relation(layer2.GetNeuronById(1), 0.25));
            layer1.GetNeuronById(2).AddRelation(new Relation(layer2.GetNeuronById(2), -0.4));

            layer1.GetNeuronById(3).AddRelation(new Relation(layer2.GetNeuronById(1), 0));
            layer1.GetNeuronById(3).AddRelation(new Relation(layer2.GetNeuronById(2), 0.9));

            layer2.GetNeuronById(1).AddRelation(new Relation(layer3.GetNeuronById(1), -1));
            layer2.GetNeuronById(2).AddRelation(new Relation(layer3.GetNeuronById(1), 1));

            neuralNetwork.AddLayer(layer1);
            neuralNetwork.AddLayer(layer2);
            neuralNetwork.AddLayer(layer3);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetwork);
        }

        private void GenerateTestNetworkWithErrorWeight()
        {
            string name = "TestErrorNeuralNetwork";
            NeuralNetwork neuralNetwork = new NeuralNetwork(name);

            Layer layer1 = new Layer(1);
            layer1.AddNeuron(new Neuron(1));
            layer1.AddNeuron(new Neuron(2));
            layer1.AddNeuron(new Neuron(3));

            Layer layer2 = new Layer(2);
            layer2.AddNeuron(new Neuron(1));
            layer2.AddNeuron(new Neuron(2));

            Layer layer3 = new Layer(3);
            layer3.AddNeuron(new Neuron(1));


            layer1.GetNeuronById(1).AddRelation(new Relation(layer2.GetNeuronById(1), 0.79));
            layer1.GetNeuronById(1).AddRelation(new Relation(layer2.GetNeuronById(2), 0.85));

            layer1.GetNeuronById(2).AddRelation(new Relation(layer2.GetNeuronById(1), 0.44));
            layer1.GetNeuronById(2).AddRelation(new Relation(layer2.GetNeuronById(2), 0.43));

            layer1.GetNeuronById(3).AddRelation(new Relation(layer2.GetNeuronById(1), 0.43));
            layer1.GetNeuronById(3).AddRelation(new Relation(layer2.GetNeuronById(2), 0.29));

            layer2.GetNeuronById(1).AddRelation(new Relation(layer3.GetNeuronById(1), 0.5));
            layer2.GetNeuronById(2).AddRelation(new Relation(layer3.GetNeuronById(1), 0.52));

            neuralNetwork.AddLayer(layer1);
            neuralNetwork.AddLayer(layer2);
            neuralNetwork.AddLayer(layer3);

            Manager neuralNetworkManager = new Manager();
            neuralNetworkManager.Save(neuralNetwork);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
