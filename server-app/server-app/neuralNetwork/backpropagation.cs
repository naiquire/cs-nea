namespace server_app.neuralNetwork
{
    public class @backpropagation
    {
        private double[][] neuronErrors = new double[5][];
        private double learningRate = 0.05;
        public backpropagation(double[] input, int expected)
        {
            List<double[][,]> weightAdjustments = new List<double[][,]>();
            List<double[][]> biasAdjustments = new List<double[][]>();

            var adjustments = backpropagate(input, expected);
        }
        private (double[][,], double[][]) backpropagate(double[] inputValues, int expectedResult)
        {
            evaluate network = new evaluate(inputValues);

            // output layer errors
            int layer = evaluate.layerCount - 1;
            for (int i = 0; i < evaluate.networkLayers[layer]; i++)
            {
                neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - network.result == expectedResult ? 1 : 0) * dx_sigmoid(network.neuronValues[layer][i]);
            }

            // for each remaining layer
            for (layer -= 1; layer > 0; layer--)
            {
                for (int i = 0; i < evaluate.networkLayers[layer]; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < evaluate.networkLayers[layer + 1]; j++)
                    {
                        sum += network.weights[layer][i, j] * neuronErrors[layer + 1][j];
                    }
                    neuronErrors[layer][i] = sum * dx_sigmoid(network.neuronValues[layer][i]);
                }
            }

            // update weights and biases
            double[][,] weightGradients = new double[evaluate.layerCount - 1][,];
            double[][] biasGradients = new double[evaluate.layerCount - 1][];

            for (layer = 1; layer < evaluate.layerCount; layer++)
            {
                for (int neuron = 0; neuron < evaluate.networkLayers[layer]; neuron++)
                {
                    for (int weight = 0; weight < evaluate.networkLayers[layer + 1]; weight++)
                    {
                        weightGradients[layer][neuron, weight] = neuronErrors[layer + 1][weight] * network.activatedValues[layer][neuron];
                    }

                    biasGradients[layer][neuron] -= neuronErrors[layer][neuron];
                }
            }
            return (weightGradients, biasGradients);
        }
        private static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        private static double dx_sigmoid(double x) => sigmoid(x) * (1 - sigmoid(x));
    }
}
