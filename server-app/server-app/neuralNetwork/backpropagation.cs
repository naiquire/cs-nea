namespace server_app.neuralNetwork
{
    public class @backpropagation
    {
        public static double epochs = 0;
        public static double correct = 0;

        private double[][] neuronErrors = new double[5][];
        private double learningRate = 0.1;
        public backpropagation(List<double[]> input, List<int> expected)
        {
            List<double[][,]> weightAdjustments = new List<double[][,]>();
            List<double[][]> biasAdjustments = new List<double[][]>();

            var weights = data.loadWeights();
            var biases = data.loadBiases();

            for (int i = 0; i < input.Count; i++)
            {
                (double[][,] weights, double[][] biases) adjustments = backpropagate(input[i], expected[i], weights, biases);

                weightAdjustments.Add(adjustments.weights);
                biasAdjustments.Add(adjustments.biases);
            }

            

            // update weights and biases
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                for (int j = 0; j < evaluate.layerSizes[i]; j++)
                {
                    for (int k = 0; k < evaluate.layerSizes[i + 1]; k++)
                    {
                        double weightSum = 0;
                        foreach (var weight in weightAdjustments)
                        {
                            weightSum += weight[i][j, k];
                        }
                        weights[i][j, k] -= (learningRate / input.Count) * weightSum;
                    }

                    if (i != 0)
                    {
                        double biasSum = 0;
                        foreach (var bias in biasAdjustments)
                        {
                            biasSum += bias[i][j];
                        }
                        biases[i - 1][j] -= (learningRate / input.Count) * biasSum;
                    }
                }
            }
            data.saveWeights(weights);
            data.saveBiases(biases);

            Console.WriteLine($"{((double)(correct / epochs)) * (double)(100)}%");
        }
        private (double[][,], double[][]) backpropagate(double[] inputValues, int expectedResult, double[][,] weights, double[][] biases)
        {
            evaluate network = new evaluate(inputValues, weights, biases);
            if (network.result == expectedResult - 1)
            { correct++; }
            epochs++;
            
            // output layer errors
            int layer = evaluate.layerCount - 1;
            neuronErrors[layer] = new double[evaluate.layerSizes[layer]];
            for (int i = 0; i < evaluate.layerSizes[layer]; i++)
            {
                // softmax error function
                neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - (network.result == expectedResult - 1 ? 1 : 0));
            }

            // for each remaining layer
            for (layer -= 1; layer > 0; layer--)
            {
                neuronErrors[layer] = new double[evaluate.layerSizes[layer]];
                for (int i = 0; i < evaluate.layerSizes[layer]; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < evaluate.layerSizes[layer + 1]; j++)
                    {
                        sum += weights[layer][i, j] * neuronErrors[layer + 1][j];
                    }
                    neuronErrors[layer][i] = sum * dx_sigmoid(network.neuronValues[layer][i]);
                }
            }

            // calculate weight and bias gradients
            double[][,] weightGradients = new double[evaluate.layerCount - 1][,];

            // weight gradients
            for (layer = 0; layer < evaluate.layerCount - 1; layer++)
            {
                weightGradients[layer] = new double[evaluate.layerSizes[layer], evaluate.layerSizes[layer + 1]];
                for (int neuron = 0; neuron < evaluate.layerSizes[layer]; neuron++)
                {
                    for (int weight = 0; weight < evaluate.layerSizes[layer + 1]; weight++)
                    {
                        weightGradients[layer][neuron, weight] = neuronErrors[layer + 1][weight] * network.activatedValues[layer][neuron];
                    }
                }
            }
            // bias gradients are equal to the neuron errors
            return (weightGradients, neuronErrors);
        }
        private static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        private static double dx_sigmoid(double x) => sigmoid(x) * (1 - sigmoid(x));
    }
}
