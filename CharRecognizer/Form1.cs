using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.EducationMethods;
using CharRecognizer.MachineLearning.EducationMethods.ErrorMethods;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CharRecognizer
{
    public partial class Form1 : Form
    {
        const double LEARNING_RATE = 0.001;

        Point lastPoint = Point.Empty;
        bool isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
            //GenerateTestNetworkWithErrorWeight();
            //GenerateTestNetworkWithWeight();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap btm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            /*var bitmap = (Bitmap)Image.FromFile(@"F:\c#\CharRecognizer\CharRecognizer\data\test.bmp");
            var array = new int[bitmap.Width, bitmap.Height];
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);
                    array[i, j] = Convert.ToInt16(pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
                }
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager neuralNetworkManager = new Manager();
            NeuralNetworkObj neuralNetworkObj = neuralNetworkManager.Get("TestNeuralNetwork");

            Dictionary<double[], double[]> data = new Dictionary<double[], double[]>();
            data.Add(new double[] { 1, 1, 0 }, new double[] { 0 });
            data.Add(new double[] { 1, 1, 1 }, new double[] { 0 });
            data.Add(new double[] { 1, 0, 0 }, new double[] { 1 });
            data.Add(new double[] { 1, 0, 1 }, new double[] { 1 });
            data.Add(new double[] { 0, 1, 0 }, new double[] { 0 });
            data.Add(new double[] { 0, 1, 1 }, new double[] { 1 });
            data.Add(new double[] { 0, 0, 0 }, new double[] { 0 });
            data.Add(new double[] { 0, 0, 1 }, new double[] { 1 });
            
            foreach (var iterationData in data)
            {
                double[] inputVector = iterationData.Key;
                double[] expectedVector = iterationData.Value;

                neuralNetworkObj.Clear();
                neuralNetworkObj.SetInputVector(inputVector);
                neuralNetworkObj.Process();
                
                double[] resultVector = this.GetResultVector(neuralNetworkObj);

                listBox1.Items.Add($"Input ({inputVector[0]}, {inputVector[1]}, {inputVector[2]}) Expect: {expectedVector[0]} Result: {resultVector[0]}");
            }
            
            RootMse errorMethod                                       = new RootMse();
            UncertaintyPropagationMethod uncertaintyPropagationMethod = new UncertaintyPropagationMethod(LEARNING_RATE);

            int countIteration = 900000;
            for (int i = 0; i < countIteration; i++)
            {
                foreach (var iterationData in data)
                {
                    double[] inputVector = iterationData.Key;
                    double[] expectedVector = iterationData.Value;
                    
                    neuralNetworkObj = uncertaintyPropagationMethod.GetTaughtNeuralNetwork(neuralNetworkObj, inputVector, expectedVector);

                    double error = errorMethod.GetError(expectedVector, this.GetResultVector(neuralNetworkObj));
                    
                    //listBox1.Items.Add($"Iteration: {i} Error: {error}");
                }

                progressBar1.Value = i * 100 / countIteration ;
            }

            listBox1.Items.Add("#########################");

            foreach (var iterationData in data)
            {
                double[] inputVector = iterationData.Key;
                double[] expectedVector = iterationData.Value;

                neuralNetworkObj.Clear();
                neuralNetworkObj.SetInputVector(inputVector);
                neuralNetworkObj.Process();
                
                double[] resultVector = this.GetResultVector(neuralNetworkObj);

                listBox1.Items.Add($"Input ({inputVector[0]}, {inputVector[1]}, {inputVector[2]}) Expect: {expectedVector[0]} Result: {resultVector[0]}");
            }

            neuralNetworkManager.Save(neuralNetworkObj);
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

            Layer layer1 = new Layer(0);
            layer1.SetPositionFirst();
            
            layer1.AddNeuron(new NeuronObj(0));
            layer1.AddNeuron(new NeuronObj(1));
            layer1.AddNeuron(new NeuronObj(2));

            Layer layer2 = new Layer(1);
            layer2.AddNeuron(new NeuronObj(0));
            layer2.AddNeuron(new NeuronObj(1));

            Layer layer3 = new Layer(2);
            layer3.SetPositionLast();
            
            layer3.AddNeuron(new NeuronObj(0));
            
            layer1.GetNeuronById(0).AddSynapse(new Synapse(layer2.GetNeuronById(0), 0.25));
            layer1.GetNeuronById(0).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.5));

            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(0), 0.25));
            layer1.GetNeuronById(1).AddSynapse(new Synapse(layer2.GetNeuronById(1), -0.4));

            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(0), 0));
            layer1.GetNeuronById(2).AddSynapse(new Synapse(layer2.GetNeuronById(1), 0.9));

            layer2.GetNeuronById(0).AddSynapse(new Synapse(layer3.GetNeuronById(0), -1));
            layer2.GetNeuronById(1).AddSynapse(new Synapse(layer3.GetNeuronById(0), 1));

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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            isMouseDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(new Pen(Color.Black, 3), lastPoint, e.Location);
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                }

                pictureBox1.Invalidate();

                lastPoint = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Dialog";
                saveFileDialog.Filter = "Bitmap Images (*.bmp)|*.bmp|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Saved Successfully");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
