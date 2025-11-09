using MathNet.Numerics.LinearAlgebra;

namespace server_app.neuralNetwork
{
	public class Network
	{
		public static readonly int[] layerSizes = [784, 144, 72, 26];
		public static readonly int layerCount = layerSizes.Length;

		public double[][] neuronValues = new double[layerCount][];
		public double[][] activatedValues = new double[layerCount][];

		public double[][,] weights = new double[layerCount - 1][,];
		public double[][] biases = new double[layerCount - 1][];

		public int result;
		public Network(double[] input)
		{
			// initialise input layer
			neuronValues[0] = input;
			activatedValues[0] = input;

			// load weights and biases
			weights = data.weights;
			biases = data.biases;

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
		public void EvaluateNetwork()
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
					activatedValues[layer] = data.softmax(neuronValues[layer]);
				}
				else
				{
					for (int i = 0; i < neuronValues[layer].Length; i++)
					{
						activatedValues[layer][i] = data.sigmoid(neuronValues[layer][i]);
					}
				}
			}

			// output letter as integer from 0-25
			result = activatedValues[layerCount - 1].ToList().IndexOf(activatedValues[layerCount - 1].Max());
		}
	}
}
