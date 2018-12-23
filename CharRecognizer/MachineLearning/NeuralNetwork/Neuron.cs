using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.NeuralNetworkNS.RelationNS;

namespace MachineLearning.NeuralNetworkNS
{
    [Serializable]
    class Neuron
    {
        public int Id { get; }
        public bool IsActive { get; set; }

        private List<Relation> relations = new List<Relation>();
        
        private double inputData = 0;

        public Neuron(int id)
        {
            Id = id;
            IsActive = false;
        }

        public void SendSignals()
        {
            foreach (Relation relation in relations)
            {
                Neuron neuron = relation.Neuron;

                neuron.AddSignal(GetData() * relation.Weight);
            }
        }

        public void AddSignal(double signal)
        {
            this.inputData += signal;
        }

        public double GetData()
        {
            return ActivationFunction(inputData);
        }

        public double GetInputData()
        {
            return ActivationFunction(inputData);
        }


        public void ClearData()
        {
            this.inputData = 0;
        }

        public List<Relation> GetRelations()
        {
            return relations;
        }

        public void AddRelation(Relation relation)
        {
            relations.Add(relation);
        }
                
        private double ActivationFunction(double signal)
        {
            //todo aggregate ActivationFunction like object 

            //return RelU(signal);
            return Sigmoid(signal);
            //return func(signal);
        }

        private double RelU(double x)
        {
            if (x < 0)
            {
                return 0;
            }

            return x;
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        }

        private double Func(double x)
        {
            if (x >= 0.5)
            {
                return 1;
            }

            return 0;
        }
    }
}
