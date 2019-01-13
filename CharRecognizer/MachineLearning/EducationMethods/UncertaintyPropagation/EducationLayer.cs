using System.Collections.Generic;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationLayer
    {
        private List<EducationNeuron> neurons = new List<EducationNeuron>();

        public void AddNeuron(EducationNeuron neuron)
        {
            this.neurons.Add(neuron);
        }

        public List<EducationNeuron> GetListNeurons()
        {
            return this.neurons;
        }
    }
}