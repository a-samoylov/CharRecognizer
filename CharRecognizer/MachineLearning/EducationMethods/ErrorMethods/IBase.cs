namespace CharRecognizer.MachineLearning.EducationMethods.ErrorMethods
{
    public interface IBase
    {
        double GetError(double[] expected, double[] results);
    }
}