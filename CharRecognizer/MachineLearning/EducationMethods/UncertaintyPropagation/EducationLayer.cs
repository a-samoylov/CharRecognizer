using System.Collections.Generic;

namespace CharRecognizer.MachineLearning.EducationMethods.UncertaintyPropagation
{
    public class EducationLayer
    {
        public int Id { get; }
        
        private List<EducationNeuron> neurons = new List<EducationNeuron>();

        public EducationLayer(int id)
        {
            Id = id;
        }
        
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