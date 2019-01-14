using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.EducationMethods;
using CharRecognizer.MachineLearning.EducationMethods.ErrorMethods;

namespace CharRecognizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GenerateTestNetworkWithErrorWeight();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager neuralNetworkManager = new Manager();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkManager.Get("TestErrorNeuralNetwork");

            const int countTest = 8;
            //const int countInputNeurons = 3;
            //const int countOutputNeurons = 1;

            List<double[]> inputVectors = new List<double[]>();
            inputVectors.Add(new double[] { 1, 1, 0});
            inputVectors.Add(new double[] { 1, 1, 1 });
            inputVectors.Add(new double[] { 1, 0, 0 });
            inputVectors.Add(new double[] { 1, 0, 1 });
            inputVectors.Add(new double[] { 0, 1, 0 });
            inputVectors.Add(new double[] { 0, 1, 1 });
            inputVectors.Add(new double[] { 0, 0, 0 });
            inputVectors.Add(new double[] { 0, 0, 1 });

            List<double[]> expectedVectors = new List<double[]>();
            expectedVectors.Add(new double[] { 0 });
            expectedVectors.Add(new double[] { 0 });
            expectedVectors.Add(new double[] { 1 });
            expectedVectors.Add(new double[] { 1 });
            expectedVectors.Add(new double[] { 0 });
            expectedVectors.Add(new double[] { 1 });
            expectedVectors.Add(new double[] { 0 });
            expectedVectors.Add(new double[] { 1 });

            RootMse errorMethod = new RootMse();

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < countTest; j++)
                {
                    neuralNetworkObj = EducateNetwork(neuralNetworkObj, inputVectors[j], expectedVectors[j]);
                    double error = errorMethod.GetError(expectedVectors[j], this.GetResultVector(neuralNetworkObj));

                    listBox1.Items.Add($"Iteration: {i} Error: {error}");
                }
            }

            listBox1.Items.Add("#########################");

            for (int j = 0; j < countTest; j++)
            {
                double[] inputVector = inputVectors[j];

                neuralNetworkObj = EducateNetwork(neuralNetworkObj, inputVectors[j], expectedVectors[j]);
                double[] resultVector = this.GetResultVector(neuralNetworkObj);

                listBox1.Items.Add($"Input ({inputVector[0]}, {inputVector[1]}, {inputVector[2]}) Result: {resultVector[0]}");
            }
        }

        private NeuralNetworkObj EducateNetwork(NeuralNetworkObj neuralNetworkObj, double[] inputVector, double[] expectedVector)
        {
            UncertaintyPropagationMethod uncertaintyPropagationMethod = new UncertaintyPropagationMethod();
            return uncertaintyPropagationMethod.GetTaughtNeuralNetwork(neuralNetworkObj, inputVector, expectedVector);
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
        }
    }
}
