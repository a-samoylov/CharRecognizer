using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.EducationMethods;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CharRecognizer
{
    public partial class CharRecognizer : Form
    {
        const double LEARNING_RATE = 0.0001;
        const int COUNT_ITERATION  = 100;

        const int IMG_HEIGHT = 100;
        const int IMG_WIDHT  = 100;

        private Point lastPoint  = Point.Empty;
        private bool isMouseDown = false;

        private NeuralNetworkObj neuralNetwork;
        
        public CharRecognizer()
        {
            InitializeComponent();
        }

        private void CharRecognizer_Load(object sender, EventArgs e)
        {
            this.FillPictureBox();
            this.LoadNetworks();
        }

        private void networkComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string networkName = networkComboBox.SelectedItem.ToString();

            MachineLearning.NeuralNetwork.Manager manager = new Manager();
            this.neuralNetwork = manager.Get(networkName);

            epochPassedLabel.Text = $"Epoch passed: {this.neuralNetwork.GetCountEpochPassed()}";

            charRecognizerGroupBox.Enabled = true;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            //todo validation

            string name          = newNetworkNameTextBox.Text;
            int countLayers      = Convert.ToInt16(layersCountTextBox.Text);
            int[] neuronsInLayer = new int[countLayers];

            for (int layerId = 0; layerId < countLayers; layerId++)
            {
                neuronsInLayer[layerId] = Convert.ToInt16(
                    Interaction.InputBox($"Input count neuron in layer {layerId}?", "Configuration layers", "")
                );
            }

            NumberRecognizerNeuralNetwork numberRecognizerNeuralNetwork = new NumberRecognizerNeuralNetwork(name, neuronsInLayer, IMG_HEIGHT, IMG_WIDHT);
            numberRecognizerNeuralNetwork.GenerateRandom();

            this.LoadNetworks();
        }

        private void LoadNetworks()
        {
            MachineLearning.NeuralNetwork.Manager manager = new Manager();

            networkComboBox.Items.Clear();
            networkComboBox.Items.AddRange(manager.GetAllNames());
        }

        private void educateNetworkButton_Click(object sender, EventArgs e)
        {
            UncertaintyPropagationMethod uncertaintyPropagationMethod   = new UncertaintyPropagationMethod(LEARNING_RATE);
            NumberRecognizerNeuralNetwork numberRecognizerNeuralNetwork = new NumberRecognizerNeuralNetwork(this.neuralNetwork);
            MachineLearning.NeuralNetwork.Report.Manager reportManager  = new MachineLearning.NeuralNetwork.Report.Manager();

            NeuralNetworkObj neuralNetworkObj = numberRecognizerNeuralNetwork.GetNeuralNetwork();

            var prepareData    = this.GetPrepareData(neuralNetworkObj.GetLastLayer().GetCountNeurons());
            int countEpoch =  Convert.ToInt16(educateNetworkNumericUpDown.Value);//todo

            reportManager.AddDataBeforeEducate(neuralNetworkObj, prepareData);

            for (int epoch = 0; epoch < countEpoch; epoch++)
            {
                for (int iteration = 0; iteration < COUNT_ITERATION; iteration++)
                {
                    foreach (var entity in prepareData)
                    {
                        double[] inputVector          = entity.Key;
                        double[] expectedResultVector = entity.Value;

                        neuralNetworkObj = uncertaintyPropagationMethod.GetTaughtNeuralNetwork(
                            numberRecognizerNeuralNetwork.GetNeuralNetwork(),
                            inputVector, 
                            expectedResultVector
                        );
                    }

                    educateNetworkProgressBar.Value = ((iteration + 1) * 100 / COUNT_ITERATION) / countEpoch;
                }
            }

            reportManager.AddDataAfterEducate(neuralNetworkObj, prepareData);

            reportManager.SaveReport(neuralNetworkObj);

            neuralNetworkObj.EpochPassed();
            numberRecognizerNeuralNetwork.UpdateNeuralNetwork(neuralNetworkObj);
        }

        private Dictionary<double[], double[]> GetPrepareData(int outputVectorLength)
        {
            Dictionary<double[], double[]> result = new Dictionary<double[], double[]>();

            string pathToData = Configs.GetInstance().GetPathToData();

            foreach (var directory in Directory.GetDirectories(pathToData))
            {
                string dataFolderName = Path.GetFileName(directory);
                int reightAnswer = Convert.ToInt16(dataFolderName);

                double[] outputVector = new double[outputVectorLength];
                outputVector[reightAnswer] = 1;

                foreach (var file in Directory.GetFiles(directory))
                {
                    var bitmap = (Bitmap)Image.FromFile(file);
                    var inputVector = new double[bitmap.Width * bitmap.Height];
                    for (var x = 0; x < bitmap.Width; x++)
                    {
                        for (var y = 0; y < bitmap.Height; y++)
                        {
                            var pixel = bitmap.GetPixel(x, y);
                            inputVector[y + x * IMG_HEIGHT] = Convert.ToDouble(pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
                        }
                    }

                    result.Add(inputVector, outputVector);
                }
            }

            return result;
        }

        private void charPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            isMouseDown = true;
        }

        private void charPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                using (Graphics g = Graphics.FromImage(charPictureBox.Image))
                {
                    g.DrawLine(new Pen(Color.Black, 3), lastPoint, e.Location);
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                }

                charPictureBox.Invalidate();

                lastPoint = e.Location;
            }
        }

        private void charPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Dialog";
                saveFileDialog.Filter = "Bitmap Images (*.bmp)|*.bmp|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    charPictureBox.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Saved Successfully");
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }

        private void FillPictureBox()
        {
            Bitmap bitmap = new Bitmap(IMG_WIDHT, IMG_WIDHT);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), 0, 0, IMG_WIDHT, IMG_WIDHT);
            }

            charPictureBox.Image = bitmap;
            charPictureBox.Invalidate();
        }
    }
}
