namespace CharRecognizer.MachineLearning.TeachingMethods.ErrorMethods
{
    public interface IBase
    {
        double GetError(double[] expected, double[] results);
    }
}