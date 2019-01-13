using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationNeuron
    {
        public NeuronObj NeuronObj { get; }
        public double WeightDelta { get; set; }

        private List<EducationSynapse> educationSynapses = new List<EducationSynapse>();
        
        public EducationNeuron(NeuronObj neuronObj)
        {
            NeuronObj = neuronObj;
        }

        public void AddEducationSynapse(EducationSynapse educationSynapse)
        {
            this.educationSynapses.Add(educationSynapse);
        }

        public List<EducationSynapse> GetEducationSynapses()
        {
            return this.educationSynapses;
        }
    }
}