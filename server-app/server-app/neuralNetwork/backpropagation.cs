using System.Reflection.Emit;

namespace server_app.neuralNetwork
{
	public class @backpropagation
	{
		public static double epochs = 0;
		public static double correct = 0;

		private readonly double[][] neuronErrors = new double[evaluate.layerCount][];
		private readonly double learningRate = 0.01;
		private readonly int batchCount;

		public backpropagation(List<double[]> input, List<int> expected)
		{
			List<double[][,]> weightAdjustments = [];
			List<double[][]> biasAdjustments = [];

			List<double> loss = [];

			batchCount = input.Count;

			// load weights and biases
			var weights = data.loadWeights();
			var biases = data.loadBiases();

			// evaluate for each input
			for (int i = 0; i < input.Count; i++)
			{
				(double[][,] weights, double[][] biases, double loss) adjustments = backpropagate(input[i], expected[i], weights, biases);

				weightAdjustments.Add(adjustments.weights);
				biasAdjustments.Add(adjustments.biases);
				loss.Add(adjustments.loss);
			}

			// update weights and biases
			var updatedWeights = updateWeights(weights, ref weightAdjustments);
			var updatedBiases = updateBiases(biases, ref biasAdjustments);

            // save weights and biases
            data.saveWeights(updatedWeights);
            data.saveBiases(updatedBiases);

            // log cumulative percentage and average loss
            Console.WriteLine($"{correct / epochs * 100.0}%\t{loss.Sum() / loss.Count}");
        }
		
		private double[][,] updateWeights(double[][,] weights, ref List<double[][,]> weightAdjustments)
		{
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
						weights[i][j, k] -= (learningRate / batchCount) * weightSum;
					}
				}
			}
			return weights;
		}
		private double[][] updateBiases(double[][] biases, ref List<double[][]> biasAdjustments)
		{
			for (int i = 0; i < evaluate.layerCount - 1; i++)
			{
				for (int j = 0; j < evaluate.layerSizes[i + 1]; j++)
				{
					double biasSum = 0;
					foreach (var bias in biasAdjustments)
					{
						biasSum += bias[i + 1][j];
					}
					biases[i][j] -= (learningRate / batchCount) * biasSum;
				}
			}
			return biases;
		}

		private (double[][,], double[][], double) backpropagate(double[] inputValues, int expectedResult, double[][,] weights, double[][] biases)
		{
			// evaluate network
			evaluate network = new(inputValues, weights, biases);

			epochs++;
			if (network.result == expectedResult - 1)
			{
				correct++;
			}

			double loss = calculateOutputErrors(ref network, expectedResult);
			calculateHiddenErrors(ref network, ref weights);

            // weight gradients
            double[][,] weightGradients = new double[evaluate.layerCount - 1][,];
            for (int layer = 0; layer < evaluate.layerCount - 1; layer++)
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
            return (weightGradients, neuronErrors, loss);
        }

		private double calculateOutputErrors(ref evaluate network, int expectedResult)
		{
			// output layer errors
			int layer = evaluate.layerCount - 1;
			double loss = 0;
			neuronErrors[layer] = new double[evaluate.layerSizes[layer]];
			for (int i = 0; i < evaluate.layerSizes[layer]; i++)
			{
				// softmax error function
				int y = expectedResult - 1 == i ? 1 : 0;

				loss += Math.Pow(network.activatedValues[layer][i] - y, 2);
				neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - y);
			}
			return loss;
		}

		private void calculateHiddenErrors(ref evaluate network, ref double[][,] weights)
		{
			// for each remaining layer
			int layer = evaluate.layerCount - 2;
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
					// sigmoid error function
					neuronErrors[layer][i] = sum * data.dx_sigmoid(network.neuronValues[layer][i]);
				}
			}
		}    
	}
}
