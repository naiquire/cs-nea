namespace server_app.neuralNetwork
{
	public class Backpropagation
	{
		private static int epochs = 0;
		private static int correct = 0;

		private readonly double[][] neuronErrors = new double[Network.layerCount][];
		private const double learningRate = 0.01;
		private readonly int batchCount;

		public Backpropagation(List<double[]> input, List<int> expected)
		{
			List<double[][,]> weightAdjustments = [];
			List<double[][]> biasAdjustments = [];

			List<double> loss = [];

			batchCount = input.Count;

			// load weights and biases
			var weights = Data.LoadWeights();
			var biases = Data.LoadBiases();

			// evaluate for each input
			for (int i = 0; i < input.Count; i++)
			{
				(double[][,] weights, double[][] biases, double loss) adjustments = Backpropagate(input[i], expected[i], weights, biases);

				weightAdjustments.Add(adjustments.weights);
				biasAdjustments.Add(adjustments.biases);
				loss.Add(adjustments.loss);
			}

			// update weights and biases
			var updatedWeights = UpdateWeights(weights, ref weightAdjustments);
			var updatedBiases = UpdateBiases(biases, ref biasAdjustments);

			// save weights and biases
			Data.SaveWeights(updatedWeights);
			Data.SaveBiases(updatedBiases);

			// log cumulative percentage and average loss
			Console.WriteLine($"{Math.Round((double)correct / (double)epochs * 100.0), 3}%\t{loss.Sum() / loss.Count}");
		}
		
		private double[][,] UpdateWeights(double[][,] weights, ref List<double[][,]> weightAdjustments)
		{
			// update weights and biases
			for (int i = 0; i < Network.layerCount - 1; i++)
			{
				for (int j = 0; j < Network.layerSizes[i]; j++)
				{
					for (int k = 0; k < Network.layerSizes[i + 1]; k++)
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
		private double[][] UpdateBiases(double[][] biases, ref List<double[][]> biasAdjustments)
		{
			for (int i = 0; i < Network.layerCount - 1; i++)
			{
				for (int j = 0; j < Network.layerSizes[i + 1]; j++)
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

		private (double[][,], double[][], double) Backpropagate(double[] inputValues, int expectedResult, double[][,] weights, double[][] biases)
		{
			// evaluate network
			Network network = new(inputValues, weights, biases);

			epochs++;
			if (network.GetResult() == expectedResult - 1)
			{
				correct++;
			}

			// calculate neuron errors
			double loss = CalculateOutputErrors(ref network, expectedResult);
			CalculateHiddenErrors(ref network, ref weights);

			// weight gradients
			double[][,] weightGradients = new double[Network.layerCount - 1][,];
			for (int layer = 0; layer < Network.layerCount - 1; layer++)
			{
				weightGradients[layer] = new double[Network.layerSizes[layer], Network.layerSizes[layer + 1]];
				for (int neuron = 0; neuron < Network.layerSizes[layer]; neuron++)
				{
					for (int weight = 0; weight < Network.layerSizes[layer + 1]; weight++)
					{
						weightGradients[layer][neuron, weight] = neuronErrors[layer + 1][weight] * network.activatedValues[layer][neuron];
					}
				}
			}

			// bias gradients are equal to the neuron errors
			return (weightGradients, neuronErrors, loss);
		}
		private double CalculateOutputErrors(ref Network network, int expectedResult)
		{
			// output layer errors
			int layer = Network.layerCount - 1;
			double loss = 0;
			neuronErrors[layer] = new double[Network.layerSizes[layer]];
			for (int i = 0; i < Network.layerSizes[layer]; i++)
			{
				// softmax error function
				int y = expectedResult - 1 == i ? 1 : 0;

				loss += Math.Pow(network.activatedValues[layer][i] - y, 2);
				neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - y);
			}
			return loss;
		}
		private void CalculateHiddenErrors(ref Network network, ref double[][,] weights)
		{
			// for each hidden layer
			for (int layer = Network.layerCount - 2; layer > 0; layer--)
			{
				neuronErrors[layer] = new double[Network.layerSizes[layer]];
				for (int i = 0; i < Network.layerSizes[layer]; i++)
				{
					double sum = 0;
					for (int j = 0; j < Network.layerSizes[layer + 1]; j++)
					{
						sum += weights[layer][i, j] * neuronErrors[layer + 1][j];
					}
					// sigmoid error function
					neuronErrors[layer][i] = sum * Data.dx_sigmoid(network.neuronValues[layer][i]);
				}
			}
		}    
	}
}
