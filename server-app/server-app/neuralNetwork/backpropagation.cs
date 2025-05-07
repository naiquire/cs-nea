namespace server_app.neuralNetwork
{
    public class @backpropagation
    {
        public static int epochs = 0;
        public static int correct = 0;

        private double[][] neuronErrors = new double[5][];
        private double learningRate = 0.05;
        public backpropagation(List<double[]> input, List<int> expected)
        {
            List<double[][,]> weightAdjustments = new List<double[][,]>();
            List<double[][]> biasAdjustments = new List<double[][]>();

            for (int i = 0; i < input.Count; i++)
            {
                (double[][,] weights, double[][] biases) adjustments = backpropagate(input[i], expected[i]);

                weightAdjustments.Add(adjustments.weights);
                biasAdjustments.Add(adjustments.biases);
            }

            var weights = data.loadWeights();
            var biases = data.loadBiases();

            // update weights and biases
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                for (int j = 0; j < evaluate.networkLayers[i + 1]; j++)
                {
                    for (int k = 0; k < evaluate.networkLayers[i + 1]; k++)
                    {
                        double weightSum = 0;
                        foreach (var weight in weightAdjustments)
                        {
                            weightSum += weight[i][j, k];
                        }
                        weights[i][j, k] -= (learningRate / input.Count) * weightSum;
                    }

                    double biasSum = 0;
                    foreach (var bias in biasAdjustments)
                    {
                        biasSum += bias[i][j];
                    }
                    biases[i][j] -= (learningRate / input.Count) * biasSum;
                }
            }
            data.saveWeights(weights);
            data.saveBiases(biases);
        }
        private (double[][,], double[][]) backpropagate(double[] inputValues, int expectedResult)
        {
            evaluate network = new evaluate(inputValues);
            if (network.result == expectedResult) { correct++; }
            epochs++;

            // output layer errors
            int layer = evaluate.layerCount - 1;
            neuronErrors[layer] = new double[evaluate.networkLayers[layer]];
            for (int i = 0; i < evaluate.networkLayers[layer]; i++)
            {
                neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - (network.result == expectedResult ? 1 : 0)) * dx_sigmoid(network.neuronValues[layer][i]);
            }

            // for each remaining layer
            for (layer -= 1; layer > 0; layer--)
            {
                neuronErrors[layer] = new double[evaluate.networkLayers[layer]];
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

            // calculate weight and bias gradients
            double[][,] weightGradients = new double[evaluate.layerCount - 1][,];
            double[][] biasGradients = new double[evaluate.layerCount - 1][];

            for (layer = 0; layer < evaluate.layerCount - 1; layer++)
            {
                weightGradients[layer] = new double[evaluate.networkLayers[layer], evaluate.networkLayers[layer + 1]];
                biasGradients[layer] = new double[evaluate.networkLayers[layer + 1]];

                for (int neuron = 0; neuron < evaluate.networkLayers[layer + 1]; neuron++)
                {
                    for (int weight = 0; weight < evaluate.networkLayers[layer + 1]; weight++)
                    {
                        weightGradients[layer][neuron, weight] = neuronErrors[layer + 1][weight] * network.activatedValues[layer][neuron];
                    }

                    biasGradients[layer][neuron] -= neuronErrors[layer + 1][neuron];
                }
            }
            return (weightGradients, biasGradients);
        }
        private static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        private static double dx_sigmoid(double x) => sigmoid(x) * (1 - sigmoid(x));
    }
}
