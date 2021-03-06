﻿using System;
using System.Collections.Generic;
using CharRecognizer.MachineLearning.NeuralNetwork;

namespace CharRecognizer.MachineLearning
{
    [Serializable]
    public class NeuralNetworkObj
    {
        public string Name { get; }

        private List<Layer> layers    = new List<Layer>();
        private bool isSetInputVector = false;
        private int countEpochPassed = 0;

        public NeuralNetworkObj(string name)
        {
            this.Name = name;
        }

        public Layer GetFirstLayer()
        {
            if (this.GetCountLayers() == 0)
            {
                throw new Exception("Layers is empty.");
            }

            return layers[0];
        }

        public Layer GetLastLayer()
        {
            if (this.GetCountLayers() == 0)
            {
                throw new Exception("Layers is empty.");
            }

            return layers[layers.Count - 1];
        }

        public List<Layer> GetListLayers()
        {
            return layers;
        }
        
        public int GetCountLayers()
        {
            return layers.Count;
        }

        public Layer GetLayerById(int id)
        {
            foreach (Layer layer in layers)
            {
                if (layer.Id == id)
                {
                    return layer;
                }
            }

            return null;
        }

        public void AddLayer(Layer layer)
        {
            if (this.GetLayerById(layer.Id) != null)
            {
                throw new Exception("Layer with this id has already added.");
            }

            this.layers.Add(layer);
        }

        public void SetInputVector(double[] v)
        {
            if (v.Length != this.GetFirstLayer().GetCountNeurons())
            {
                throw new Exception("Invalid input vector.");
            }

            this.isSetInputVector = true;

            List<NeuronObj> neurons = this.GetFirstLayer().GetListNeurons();
            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].AddInputData(v[i]);
            }
        }

        public void Process()
        {
            if (!this.isSetInputVector)
            {
                throw new Exception("The input vector is not set.");
            }

            foreach (Layer layer in this.layers)
            {
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    neuron.SendSignals();
                }
            }
        }

        public void EpochPassed()
        {
            this.countEpochPassed++;
        }

        public int GetCountEpochPassed()
        {
            return countEpochPassed;
        }

        public void Clear()
        {
            this.isSetInputVector = false;

            foreach (Layer layer in this.layers)
            {
                foreach (NeuronObj neuron in layer.GetListNeurons())
                {
                    neuron.ClearData();
                }
            }
        }
    }
}
