using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
    // neural network
    public class @evaluate
    {
        public static readonly int[] networkLayers = [784, 144, 72, 72, 26];
        public static readonly int layerCount = 5;

        public double[][] neuronValues = new double[5][];
        public double[][] activatedValues = new double[5][];

        public double[][,] weights = new double[4][,];
        public double[][] biases = new double[4][];

        public char? result;
        public evaluate(double[] input)
        {
            neuronValues[0] = input;
            activatedValues[0] = input;

            evaluateNetwork();
        }
        public void evaluateNetwork()
        {
            // evaluates the network
            weights = data.loadWeights();
            biases = data.loadBiases();

            // for each layer excluding the input layer
            for (int layer = 1; layer < layerCount; layer++)
            {
                Vector<double> neuronsMatrix = Vector<double>.Build.DenseOfArray(activatedValues[layer]);

                Matrix<double> weightsMatrix = Matrix<double>.Build.DenseOfArray(weights[layer - 1]);
                Vector<double> biasesMatrix = Vector<double>.Build.DenseOfArray(biases[layer - 1]);

                neuronValues[layer] = (neuronsMatrix * weightsMatrix + biasesMatrix).ToArray();
                var activatedNeurons = new double[networkLayers[layer]];

                activatedValues[layer] = new double[neuronValues.Length];
                for (int i = 0; i < neuronValues.Length; i++)
                {
                    activatedValues[layer][i] = sigmoid(neuronValues[layer][i]);
                }
            }
            result = (char) (activatedValues[layerCount - 1].ToList().IndexOf(activatedValues[layerCount - 1].Max()) + 65);
        }
        private static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
    }
}
