using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron;
using CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc;

namespace CharRecognizer.MachineLearning.NeuralNetwork
{
    [Serializable]
    public class NeuronObj
    {
        public int Id { get; }

        private bool          isInFirsLayer          = false;
        private bool          isInLastLayer          = false;
        private bool          isSendSignals          = false;
        private bool          isCalculatedOutputData = false;
        private List<Synapse> synapses               = new List<Synapse>();
        private double        inputData              = 0;
        private double        outputData             = 0;

        private IBase activationFunc;
        
        public NeuronObj(int id)
        {
            Id = id;
            this.activationFunc = new Sigmoid();//todo make choice
        }

        public bool IsInFirstLayer()
        {
            return isInFirsLayer;
        }
        
        public void SetPositionInFirstLayer()
        {
            isInFirsLayer = true;
        }

        public bool IsInLastLayer()
        {
            return isInLastLayer;
        }
        
        public void SetPositionInLastLayer()
        {
            isInLastLayer = true;
        }
        
        public void SendSignals()
        {
            if (isSendSignals)
            {
                throw new Exception("This neuron has already send signals.");
            } 

            foreach (Synapse synapse in synapses)
            {
                NeuronObj nextNeuron = synapse.NeuronObj;

                nextNeuron.AddInputData(this.GetOutputData() * synapse.Weight);
            }

            isSendSignals = true;
        }

        public void AddInputData(double data)
        {
            this.inputData += data;
        }

        public double GetOutputData()
        {
            if (this.isCalculatedOutputData)
            {
                return this.outputData;
            }
            
            if (this.isInFirsLayer)
            {
                this.outputData = inputData;
                
            }
            else
            {
                this.outputData = ActivationFunction(inputData);
            }

            return this.outputData;
        }

        public double GetInputData()
        {
            return inputData;
        }

        public void ClearData()
        {
            this.inputData = 0;
        }

        public List<Synapse> GetSynapses()
        {
            return synapses;
        }

        public void AddSynapse(Synapse synapse)
        {
            synapses.Add(synapse);
        }

        private double ActivationFunction(double signal)
        {
            return this.activationFunc.GetValue(signal);
        }
    }
}
