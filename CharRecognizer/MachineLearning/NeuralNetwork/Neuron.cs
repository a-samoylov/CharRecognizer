using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc;
using MachineLearning.NeuralNetworkNS.NeuronNS.SynapseNS;

namespace MachineLearning.NeuralNetworkNS
{
    [Serializable]
    class Neuron
    {
        public int Id { get; }

        private bool           isInFirsLayer = false;
        private bool           IsSendSignals = false;
        private List<Synapse> relations = new List<Synapse>();
        private double         inputData = 0;

        public Neuron(int id, bool isInFirsLayer = false)
        {
            Id = id;
            this.isInFirsLayer = isInFirsLayer;
            this.activationFunc = new Sigmoid();//todo make choice
        }

        public void SendSignals()
        {
            if (IsSendSignals)
            {
                throw new Exception("This neuron has already send signals.");
            } 

            foreach (Synapse relation in relations)
            {
                Neuron nextNeuron = relation.Neuron;

                nextNeuron.AddSignal(this.GetOutputData() * relation.Weight);
            }

            IsSendSignals = true;
        }

        public void AddSignal(double signal)
        {
            this.inputData += signal;
        }

        public double GetOutputData()
        {
            if (this.isInFirsLayer)
            {
                return inputData;
            }

            return ActivationFunction(inputData);
        }

        public double GetInputData()
        {
            return inputData;
        }

        public void ClearData()
        {
            this.inputData = 0;
        }

        public List<Synapse> GetRelations()
        {
            return relations;
        }

        public void AddRelation(Synapse relation)
        {
            relations.Add(relation);
        }

        private double ActivationFunction(double signal)
        {
            return this.activationFunc.GetValue(signal);
        }
    }
}
