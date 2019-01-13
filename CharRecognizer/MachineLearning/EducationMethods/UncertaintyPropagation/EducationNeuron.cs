using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationNeuron
    {
        public NeuronObj NeuronObj { get; set; }
        
        public double WeightDelta { get; set; }
    }
}