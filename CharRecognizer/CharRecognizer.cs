using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CharRecognizer.MachineLearning;
using CharRecognizer.MachineLearning.NeuralNetwork;
using CharRecognizer.MachineLearning.EducationMethods;

namespace CharRecognizer
{
    public partial class CharRecognizer : Form
    {
        const int IMG_HEIGHT = 100;
        const int IMG_WIDHT  = 100;

        public CharRecognizer()
        {
            InitializeComponent();
        }

        private void CharRecognizer_Load(object sender, EventArgs e)
        {
            this.LoadNetworks();
        }

        private void networkComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string networkName = networkComboBox.SelectedItem.ToString();

            MachineLearning.NeuralNetwork.Manager manager = new Manager();
            NeuralNetworkObj neuralNetwork = manager.Get(networkName);

            epochPassedLabel.Text = $"Epoch passed: {neuralNetwork.GetCountEpochPassed()}";

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
            string networkName = networkComboBox.SelectedItem.ToString();

            UncertaintyPropagationMethod uncertaintyPropagationMethod = new UncertaintyPropagationMethod();
            NumberRecognizerNeuralNetwork numberRecognizerNeuralNetwork = new NumberRecognizerNeuralNetwork(networkName);

            NeuralNetworkObj neuralNetworkObj = numberRecognizerNeuralNetwork.GetNeuralNetwork();

            for (int iteration = 0; iteration < educateNetworkNumericUpDown.Value; iteration++)
            {
                //neuralNetworkObj = uncertaintyPropagationMethod.GetTaughtNeuralNetwork(numberRecognizerNeuralNetwork.GetNeuralNetwork());
            }

            numberRecognizerNeuralNetwork.UpdateNeuralNetwork(neuralNetworkObj);

            //генерация отчета
            //загрузка данных 
            //прогрес бар
        }
    }
}
