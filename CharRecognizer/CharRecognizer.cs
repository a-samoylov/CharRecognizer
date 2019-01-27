using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CharRecognizer.MachineLearning.NeuralNetwork;

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
            MachineLearning.NeuralNetwork.Manager manager = new Manager();
            networkComboBox.Items.AddRange(manager.GetAllNames());
        }

        private void networkComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //var a = networkComboBox.SelectedText;
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
        }
    }
}
