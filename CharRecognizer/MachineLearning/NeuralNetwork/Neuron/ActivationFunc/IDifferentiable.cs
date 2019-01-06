namespace CharRecognizer.MachineLearning.NeuralNetwork.Neuron.ActivationFunc
{
    interface IDifferentiable : IBase
    {
        double GetDerivativeValue(double x);
    }
}
