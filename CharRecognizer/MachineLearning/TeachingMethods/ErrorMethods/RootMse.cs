using System;

namespace CharRecognizer.MachineLearning.TeachingMethods.ErrorMethods
{
    public class RootMse : IBase
    {
        public double Get(double[] expected, double[] results)
        {
            if (expected.Length != results.Length)
            {
                throw new Exception("Invalid input data.");
            }

            double sum = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                sum += Math.Pow(expected[i] - results[i], 2);
            }
            
            return sum / expected.Length;
        }
    }
}