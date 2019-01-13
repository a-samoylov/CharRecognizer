using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationNeuron
    {
        public NeuronObj NeuronObj { get; }
        
        public double WeightDelta { get; set; }

        private List<EducationNeuron> nextEducationNeurons = new List<EducationNeuron>();
        
        public EducationNeuron(NeuronObj neuronObj)
        {
            NeuronObj = neuronObj;
        }

        public void AddNextEducationNeuron(EducationNeuron nextEducationNeuron)
        {
            this.nextEducationNeurons.Add(nextEducationNeuron);
        }

        public List<EducationNeuron> GetNextEducationNeurons()
        {
            return this.nextEducationNeurons;
        }
    }
}