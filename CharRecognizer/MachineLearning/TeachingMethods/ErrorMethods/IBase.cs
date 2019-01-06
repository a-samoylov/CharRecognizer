namespace CharRecognizer.MachineLearning.TeachingMethods.ErrorMethods
{
    public interface IBase
    {
        double Get(double[] expected, double[] results);
    }
}