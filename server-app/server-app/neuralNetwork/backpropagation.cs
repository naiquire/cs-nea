namespace server_app.neuralNetwork
{
	public class Backpropagation
	{
		public static double epochs = 0;
		public static double correct = 0;

		private readonly double[][] _neuronErrors = new double[Network.layerCount][];
		private const double learningRate = 0.01;
		private readonly int _batchCount;

		public Backpropagation(List<double[]> input, List<int> expected)
		{
			List<double[][,]> weightAdjustments = [];
			List<double[][]> biasAdjustments = [];

			List<double> loss = [];

			_batchCount = input.Count;

			// load weights and biases
			var weights = data.LoadWeights();
			var biases = data.LoadBiases();

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
            data.SaveWeights(updatedWeights);
            data.SaveBiases(updatedBiases);

            // log cumulative percentage and average loss
            Console.WriteLine($"{correct / epochs * 100.0}%\t{loss.Sum() / loss.Count}");
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
						weights[i][j, k] -= (learningRate / _batchCount) * weightSum;
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
					biases[i][j] -= (learningRate / _batchCount) * biasSum;
				}
			}
			return biases;
		}

		private (double[][,], double[][], double) Backpropagate(double[] inputValues, int expectedResult, double[][,] weights, double[][] biases)
		{
			// evaluate network
			Network network = new(inputValues, weights, biases);

			epochs++;
			if (network.result == expectedResult - 1)
			{
				correct++;
			}

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
                        weightGradients[layer][neuron, weight] = _neuronErrors[layer + 1][weight] * network.activatedValues[layer][neuron];
                    }
                }
            }

            // bias gradients are equal to the neuron errors
            return (weightGradients, _neuronErrors, loss);
        }

		private double CalculateOutputErrors(ref Network network, int expectedResult)
		{
			// output layer errors
			int layer = Network.layerCount - 1;
			double loss = 0;
			_neuronErrors[layer] = new double[Network.layerSizes[layer]];
			for (int i = 0; i < Network.layerSizes[layer]; i++)
			{
				// softmax error function
				int y = expectedResult - 1 == i ? 1 : 0;

				loss += Math.Pow(network.activatedValues[layer][i] - y, 2);
				_neuronErrors[layer][i] = 2 * (network.activatedValues[layer][i] - y);
			}
			return loss;
		}

		private void CalculateHiddenErrors(ref Network network, ref double[][,] weights)
		{
			// for each remaining layer
			int layer = Network.layerCount - 2;
			for (layer -= 1; layer > 0; layer--)
			{
				_neuronErrors[layer] = new double[Network.layerSizes[layer]];
				for (int i = 0; i < Network.layerSizes[layer]; i++)
				{
					double sum = 0;
					for (int j = 0; j < Network.layerSizes[layer + 1]; j++)
					{
						sum += weights[layer][i, j] * _neuronErrors[layer + 1][j];
					}
					// sigmoid error function
					_neuronErrors[layer][i] = sum * data.dx_sigmoid(network.neuronValues[layer][i]);
				}
			}
		}    
	}
}
