namespace CharRecognizer.MachineLearning.TeachingMethods.Error
{
    public interface IBase
    {
        double Get(double[] expected, double[] results);
    }
}