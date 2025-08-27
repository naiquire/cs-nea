using System.IO.Compression;
using System.Text;

namespace server_app.neuralNetwork
{
    public static class @data
    {
        public static readonly string location = Environment.GetEnvironmentVariable("cs-nea-neural-net-data") ?? @"H:\Subjects\Computer Science\git\CS-NEA\server-app\server-app\neuralNetwork\data\";
        public static double sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        public static double dx_sigmoid(double x) => sigmoid(x) * (1 - sigmoid(x));
        public static double[] softmax(double[] input)
        {
            double[] output = new double[input.Length];
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Math.Exp(input[i]);
                sum += output[i];
            }
            for (int i = 0; i < output.Length; i++)
            {
                output[i] /= sum;
            }
            return output;
        }
        public static void initialiseParameters()
        {
            // initialise weights
            double[][,] weights = new double[evaluate.layerCount - 1][,];
            for (int i = 0; i < weights.Length; i++)
            {
                Random rnd = new();
                weights[i] = new double[evaluate.layerSizes[i], evaluate.layerSizes[i + 1]];
                double limit = Math.Sqrt(6 / (double)(evaluate.layerSizes[i] + evaluate.layerSizes[i + 1]));
                for (int j = 0; j < evaluate.layerSizes[i]; j++)
                {
                    for (int k = 0; k < evaluate.layerSizes[i + 1]; k++)
                    {
                        // uniform distribution between -limit and limit
                        weights[i][j, k] = (rnd.NextDouble() * 2 - 1) * limit;
                    }
                }
            }

            // initialise biases
            double[][] biases = new double[evaluate.layerCount - 1][];
            for (int i = 0; i < biases.Length; i++)
            {
                biases[i] = new double[evaluate.layerSizes[i + 1]];
            }

            // save weights and biases
            saveWeights(weights);
            saveBiases(biases);
        }
        public static double[][,] loadWeights()
        {
            double[][,] weights = new double[evaluate.layerCount - 1][,];
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                weights[i] = new double[evaluate.layerSizes[i], evaluate.layerSizes[i + 1]];
                using (StreamReader sr = new($@"{location}weights\{i}.txt"))
                {
                    for (int j = 0; j < evaluate.layerSizes[i]; j++)
                    {
                        string[] s = sr.ReadLine().Split(',');
                        for (int k = 0; k < evaluate.layerSizes[i + 1]; k++)
                        {
                            weights[i][j, k] = double.Parse(s[k]);
                        }
                    }
                }
            }
            return weights;
        }
        public static double[][] loadBiases()
        {
            double[][] biases = new double[evaluate.layerCount - 1][];
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                biases[i] = new double[evaluate.layerSizes[i + 1]];
                using (StreamReader sr = new($@"{location}biases\{i}.txt"))
                {
                    string[] s = sr.ReadLine().Split(',');
                    for (int j = 0; j < evaluate.layerSizes[i + 1]; j++)
                    {
                        biases[i][j] = double.Parse(s[j]);
                    }
                }
            }
            return biases;
        }
        public static void saveWeights(double[][,] weights)
        {
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                StringBuilder build = new();
                for (int j = 0; j < weights[i].GetLength(0); j++)
                {
                    for (int k = 0; k < weights[i].GetLength(1) - 1; k++)
                    {
                        build.Append($"{weights[i][j, k]},");
                    }
                    build.Append($"{weights[i][j, weights[i].GetLength(1) - 1]}\n");
                }
                using (StreamWriter sw = new($@"{location}weights\{i}.txt"))
                {
                    sw.Write(build);
                }
            }
        }
        public static void saveBiases(double[][] biases)
        {
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                StringBuilder build = new();
                for (int j = 0; j < biases[i].GetLength(0); j++)
                {
                    build.Append($"{biases[i][j]},");
                }
                build.Append($"{biases[i][^1]}");
                using (StreamWriter sw = new($@"{location}biases\{i}.txt"))
                {
                    sw.Write(build);
                }
            }
        }
		public static (List<double[]>, List<int>) loadImages()
		{
			FileStream fs = new($@"{location}training\images.gz", FileMode.Open, FileAccess.ReadWrite);
			CompressionMode cm = CompressionMode.Decompress;

			List<double[]> images = [];
			List<int> labels = [];

			using (GZipStream gz = new(fs, cm))
			{
				// discard header info
				byte[] header = new byte[16];
				gz.ReadExactly(header, 0, 16);

				// read 100,000 images
				for (int i = 0; i < 100000; i++)
				{
					var image = new byte[784];
					gz.ReadExactly(image, 0, 784);

					var a = new double[784];
					int count = 0;
					for (int row = 0; row < 28; row++)
					{
						for (int column = 0; column < 28; column++, count++)
						{
							a[column * 28 + row] = (double)image[count] / 255;
						}
					}

					images.Add(a);
				}
			}

			fs = new($@"{location}training\labels.gz", FileMode.Open, FileAccess.ReadWrite);

			using (GZipStream gz = new(fs, cm))
			{
				// discard header info
				byte[] header = new byte[8];
				gz.ReadExactly(header, 0, 8);

				// read 100,000 labels
				for (int i = 0; i < 100000; i++)
				{
					labels.Add(gz.ReadByte());
				}
			}

			return (images, labels);
		}
	}
}
