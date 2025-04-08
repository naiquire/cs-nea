using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
    public class neuralNetwork
    {
        private readonly int[] networkLayers = [784, 288, 144, 64, 36];
        private static readonly int layerCount = 5;

        private double[][] neuronValues = new double[5][];
        private double[][] activatedValues = new double[5][];

        private double[][,] weights = new double[4][,];
        private double[][] biases = new double[4][];

        public string? result;
        public neuralNetwork(double[] input)
        {
            neuronValues[0] = input;
            activatedValues[0] = input;
        }
        public void evaluate()
        {
            // evaluates the network

            // for each layer excluding the input layer
            for (int layer = 1; layer < layerCount; layer++)
            {
                // manual or package matrices?
            }
        }
    }
}
