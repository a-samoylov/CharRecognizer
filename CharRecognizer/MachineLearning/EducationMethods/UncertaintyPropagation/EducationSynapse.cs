using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationSynapse
    {
        public EducationNeuron EducationNeuron { get; }

        public Synapse Synapse { get; }

        public EducationSynapse(EducationNeuron educationNeuron, Synapse synapse)
        {
            EducationNeuron = educationNeuron;
            Synapse = synapse;
        }
    }
}