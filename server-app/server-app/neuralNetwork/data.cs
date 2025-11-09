using System.Drawing;
using System.IO.Compression;
using System.Text;

namespace server_app.neuralNetwork
{
	public static class data
	{
		public static readonly string location = $@"{Environment.GetEnvironmentVariable("cs-nea-server") ?? Environment.CurrentDirectory}\neuralNetwork\data\";

		public static readonly double[][,] weights = LoadWeights();
		public static readonly double[][] biases = LoadBiases();

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
			double[][,] weights = new double[Network.layerCount - 1][,];
			for (int i = 0; i < weights.Length; i++)
			{
				Random rnd = new();
				weights[i] = new double[Network.layerSizes[i], Network.layerSizes[i + 1]];
				double limit = Math.Sqrt(6 / (double)(Network.layerSizes[i] + Network.layerSizes[i + 1]));
				for (int j = 0; j < Network.layerSizes[i]; j++)
				{
					for (int k = 0; k < Network.layerSizes[i + 1]; k++)
					{
						// uniform distribution between -limit and limit
						weights[i][j, k] = (rnd.NextDouble() * 2 - 1) * limit;
					}
				}
			}

			// initialise biases
			double[][] biases = new double[Network.layerCount - 1][];
			for (int i = 0; i < biases.Length; i++)
			{
				biases[i] = new double[Network.layerSizes[i + 1]];
			}

			// save weights and biases
			SaveWeights(weights);
			SaveBiases(biases);
		}

		public static double[] preprocessImage(Bitmap original)
		{
			Bitmap image = addPadding(
				extendToSquare(
					cropToContent(original)
				)
			);

			int width = image.Width;
			int height = image.Height;
			double[] pixels = new double[width * height];

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Color c = image.GetPixel(x, y);
					double gray = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
					pixels[y * width + x] = 1.0 - (gray / 255.0);
				}
			}

			return pixels;

			Bitmap cropToContent(Bitmap image)
			{
				int left = image.Width + 1;
				int top = image.Height + 1;
				int right = -1;
				int bottom = -1;

				bool pixelExists = false;
				for (int y = 0; y < image.Height; y++)
				{
					for (int x = 0; x < image.Width; x++)
					{
						if (image.GetPixel(y, x).Name != "ffffffff")
						{
							pixelExists = true;
							if (y < top) top = y;
							if (y > bottom) bottom = y;
							if (x < left) left = x;
							if (x > right) right = x;
						}
					}
				}

				if (!pixelExists)
				{
					// failsafe for empty image
					return image;
				}

				// return clone of image with cropped dimensions and same pixel format
				return image.Clone(new Rectangle(top, left, bottom - top + 1, right - left + 1), image.PixelFormat);
			}

			Bitmap extendToSquare(Bitmap image)
			{
				if (image.Width == image.Height)
				{
					return new Bitmap(image, 24, 24);
				}

				int largestDimension = Math.Max(image.Width, image.Height);
				int offset = Math.Abs((image.Width - image.Height) / 2);
				string extendDirection = largestDimension == image.Height ? "right" : "down";

				var square = new Bitmap(largestDimension, largestDimension);

				for (int x = 0; x < largestDimension; x++)
				{
					for (int y = 0; y < largestDimension; y++)
					{
						square.SetPixel(x, y, Color.White);
					}
				}

				for (int i = 0; i < largestDimension; i++)
				{
					for (int j = 0; j < largestDimension; j++)
					{
						// check pixel is not out of bounds
						if (i < image.Width && j < image.Height)
						{
							if (extendDirection == "right")
							{
								square.SetPixel(i + offset, j, image.GetPixel(i, j));
							}
							if (extendDirection == "down")
							{
								square.SetPixel(i, j + offset, image.GetPixel(i, j));
							}
						}
					}
				}
				return new Bitmap(square, 24, 24);
			}

			Bitmap addPadding(Bitmap image)
			{
				var square = new Bitmap(28, 28);

				for (int x = 0; x < 28; x++)
				{
					for (int y = 0; y < 28; y++)
					{
						square.SetPixel(x, y, Color.White);
					}
				}

				for (int i = 0; i < 28; i++)
				{
					for (int j = 0; j < 28; j++)
					{
						// check pixel is not out of bounds
						if (i < image.Width && j < image.Height)
						{
							square.SetPixel(i + 2, j + 2, image.GetPixel(i, j));
						}
					}
				}
				return square;
			}
		}
		public static double[][,] LoadWeights()
		{
			double[][,] weights = new double[Network.layerCount - 1][,];
			for (int i = 0; i < Network.layerCount - 1; i++)
			{
				weights[i] = new double[Network.layerSizes[i], Network.layerSizes[i + 1]];
				using (StreamReader sr = new($@"{location}weights\{i}.txt"))
				{
					string[] lines = sr.ReadToEnd().Split('\n');
					for (int j = 0; j < Network.layerSizes[i]; j++)
					{
						string[] s = lines[j].Split(',');
						for (int k = 0; k < Network.layerSizes[i + 1]; k++)
						{
							weights[i][j, k] = double.Parse(s[k]);
						}
					}
				}
			}
			return weights;
		}
		public static double[][] LoadBiases()
		{
			double[][] biases = new double[Network.layerCount - 1][];
			for (int i = 0; i < Network.layerCount - 1; i++)
			{
				biases[i] = new double[Network.layerSizes[i + 1]];
				using (StreamReader sr = new($@"{location}biases\{i}.txt"))
				{
					string[] s = sr.ReadToEnd().Split(',');
					for (int j = 0; j < Network.layerSizes[i + 1]; j++)
					{
						biases[i][j] = double.Parse(s[j]);
					}
				}
			}
			return biases;
		}
		public static void SaveWeights(double[][,] weights)
		{
			for (int i = 0; i < Network.layerCount - 1; i++)
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
		public static void SaveBiases(double[][] biases)
		{
			for (int i = 0; i < Network.layerCount - 1; i++)
			{
				using (StreamWriter sw = new($@"{location}biases\{i}.txt"))
				{
					sw.Write(string.Join(',', biases[i]));
				}
			}
		}
		public static (List<double[]>, List<int>) LoadImages()
		{
			int done = 0;
			const int total = 500000;

			Console.Write($"\r[ {100 * done / total,3}% ] Loading training images");

			FileStream fs = new($@"{location}training\images.gz", FileMode.Open, FileAccess.ReadWrite);
			CompressionMode cm = CompressionMode.Decompress;

			List<double[]> images = [];
			List<int> labels = [];

			using (GZipStream gz = new(fs, cm))
			{
				// discard header info
				byte[] header = new byte[16];
				gz.ReadExactly(header, 0, 16);

				// read images
				for (int i = 0; i < total; i++)
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

					int hold = 100 * done / total;
					done++;
					if (100 * done / total != hold)
					{
						Console.Write($"\r[ {100 * done / total,3}% ] Loading training images");
					}
				}
			}

			Console.WriteLine();

			done = 0;
			fs = new($@"{location}training\labels.gz", FileMode.Open, FileAccess.ReadWrite);

			Console.Write($"\r[ {100 * done / total,3}% ] Loading training labels");

			using (GZipStream gz = new(fs, cm))
			{
				// discard header info
				byte[] header = new byte[8];
				gz.ReadExactly(header, 0, 8);

				// read labels
				for (int i = 0; i < total; i++)
				{
					labels.Add(gz.ReadByte());

					int hold = 100 * done / total;
					done++;
					if (100 * done / total != hold)
					{
						Console.Write($"\r[ {100 * done / total,3}% ] Loading training labels");
					}
				}
			}

			Console.WriteLine('\n');

			return (images, labels);
		}
		public static (List<double[]>, List<int>) FilterImages((List<double[]> images, List<int> labels) fullData)
		{
			int length = fullData.labels.Count;

			List<double[]> filteredImages = [];
			List<int> filteredLabels = [];

			for (int i = 0; i < length; i++)
			{
				// extract uppercase only
				if (10 <= fullData.labels[i] && fullData.labels[i] <= 35)
				{
					filteredImages.Add(fullData.images[i]);
					filteredLabels.Add(fullData.labels[i] - 9);
				}
			}

			return (filteredImages, filteredLabels);
		}
	}
}
