using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationSynapse
    {
        public EducationNeuron EducationNeuron { get; }

        public double Weight { get; }

        public EducationSynapse(EducationNeuron educationNeuron, double weight)
        {
            EducationNeuron = educationNeuron;
            Weight = weight;
        }
    }
}