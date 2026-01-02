using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
	public class Network
	{
		public static readonly int[] layerSizes = [784, 144, 72, 26];
		public static readonly int layerCount = layerSizes.Length;

		public double[][] neuronValues = new double[layerCount][];
		public double[][] activatedValues = new double[layerCount][];

		private readonly double[][,] weights = new double[layerCount - 1][,];
		private readonly double[][] biases = new double[layerCount - 1][];

		private int result;

		public int GetResult() => result;
		public double GetAccuracy(int letter) => activatedValues[layerCount - 1][letter];
		public Network(double[] input)
		{
			// initialise input layer
			neuronValues[0] = input;
			activatedValues[0] = input;

			// load weights and biases
			weights = Data.weights;
			biases = Data.biases;

			EvaluateNetwork();
		}
		public Network(double[] input, double[][,] weights, double[][] biases)
		{
			// initialises input layer
			neuronValues[0] = input;
			activatedValues[0] = input;

			// assign loaded weights and biases
			this.weights = weights;
			this.biases = biases;

			EvaluateNetwork();
		}
		private void EvaluateNetwork()
		{
			// for each layer excluding the input layer
			for (int layer = 1; layer < layerCount; layer++)
			{
				// build matrices
				Vector<double> neuronsMatrix = Vector<double>.Build.DenseOfArray(activatedValues[layer - 1]);

				Matrix<double> weightsMatrix = Matrix<double>.Build.DenseOfArray(weights[layer - 1]);
				Vector<double> biasesMatrix = Vector<double>.Build.DenseOfArray(biases[layer - 1]);

				// calculate neuron values
				neuronValues[layer] = [.. neuronsMatrix * weightsMatrix + biasesMatrix];

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

			// output letter as integer from 0-25
			result = activatedValues[layerCount - 1].ToList().IndexOf(activatedValues[layerCount - 1].Max());
		}
	}
}
