using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationNeuron
    {
        public NeuronObj NeuronObj { get; }
        
        public double WeightDelta { get; set; }

        public EducationLayer NextEducationLayer { get; set; }
        
        public EducationNeuron(NeuronObj neuronObj)
        {
            NeuronObj = neuronObj;
        }
    }
}