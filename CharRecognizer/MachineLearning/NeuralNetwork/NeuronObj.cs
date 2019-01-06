using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    [Serializable]
    class NeuronObj
    {
        public int Id { get; }

        private bool          isInFirsLayer = false;
        private bool          isSendSignals = false;
        private List<Synapse> relations = new List<Synapse>();
        private double        inputData = 0;

        private IBase activationFunc;
        
        public NeuronObj(int id, bool isInFirsLayer = false)
        {
            Id = id;
            this.isInFirsLayer = isInFirsLayer;
            this.activationFunc = new Sigmoid();//todo make choice
        }

        public void SendSignals()
        {
            if (isSendSignals)
            {
                throw new Exception("This neuron has already send signals.");
            } 

            foreach (Synapse relation in relations)
            {
                NeuronObj nextNeuronObj = relation.NeuronObj;

                nextNeuronObj.AddSignal(this.GetOutputData() * relation.Weight);
            }

            isSendSignals = true;
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
