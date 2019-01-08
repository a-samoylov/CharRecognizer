using System;

namespace CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc
{
    [Serializable]
    public class Sigmoid : IDifferentiable
    {
        public double GetValue(double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        }

        public double GetDerivativeValue(double x)
        {
            return GetValue(x) * (1 - GetValue(x));
        }
    }
}