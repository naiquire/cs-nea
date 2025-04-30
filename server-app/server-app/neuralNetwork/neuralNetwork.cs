using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
    // neural network
    public class neuralNetwork
    {
        private readonly int[] networkLayers = [784, 144, 72, 72, 26];
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
                Vector<double> neuronsMatrix = Vector<double>.Build.DenseOfArray(activatedValues[layer]);

                Matrix<double> weightsMatrix = Matrix<double>.Build.DenseOfArray(weights[layer - 1]);
                Vector<double> biasesMatrix = Vector<double>.Build.DenseOfArray(biases[layer - 1]);

                neuronValues[layer] = (neuronsMatrix * weightsMatrix + biasesMatrix).ToArray();
                var activatedNeurons = new double[networkLayers[layer]];

                for (int i = 0; i < neuronValues.Length; i++)
                {
                    activatedValues[layer][i] = sigmoid(neuronValues[layer][i]);
                }
            }
        }
        private static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
    }
}
