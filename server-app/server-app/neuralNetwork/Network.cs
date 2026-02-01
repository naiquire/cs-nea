using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
	public class Network
	{
		public static readonly int[] layerSizes = [784, 144, 72, 26];
		public static readonly int layerCount = layerSizes.Length;

		public double[][] neuronValues = new double[layerCount][];
		public double[][] activatedValues = new double[layerCount][];

		private int result;

		public int GetResult() => result;
		public double GetAccuracy(int letter) => activatedValues[layerCount - 1][letter];
		public Network(double[] input)
		{
			// initialise input layer
			neuronValues[0] = input;
			activatedValues[0] = input;

			EvaluateNetwork();
		}
		public Network(double[] input, double[][,] weights, double[][] biases)
		{
			// initialises input layer
			neuronValues[0] = input;
			activatedValues[0] = input;

			// build matrices from loaded weights and biases
			Data.BuildMatrices(weights, biases);

			EvaluateNetwork();
		}

		private void EvaluateNetwork()
		{
			// for each layer excluding the input layer
			for (int layer = 1; layer < layerCount; layer++)
			{
				// calculate neuron values
				Vector<double> neuronsMatrix = Vector<double>.Build.DenseOfArray(activatedValues[layer - 1]);
				neuronValues[layer] = (neuronsMatrix * Data.weightsMatrices[layer - 1] + Data.biasesMatrices[layer - 1]).AsArray();

				// calculate activated values
				activatedValues[layer] = new double[layerSizes[layer]];
				if (layer == layerCount - 1)
				{
					activatedValues[layer] = Data.softmax(neuronValues[layer]);
				}
				else
				{
					for (int i = 0; i < neuronValues[layer].Length; i++)
					{
						activatedValues[layer][i] = Data.sigmoid(neuronValues[layer][i]);
					}
				}
			}

			(int index, double maxValue) = (-1, -1);
			for (int i = 0; i < activatedValues[layerCount - 1].Length; i++)
			{
				if (activatedValues[layerCount - 1][i] > maxValue)
				{
					maxValue = activatedValues[layerCount - 1][i];
					index = i;
				}
			}

			// output letter as integer from 0-25
			result = index;
		}
	}
}
