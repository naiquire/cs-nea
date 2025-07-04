using System.Text;

namespace server_app.neuralNetwork
{
    public static class @data
    {
        public static readonly string location = Environment.GetEnvironmentVariable("CS-NEA-FP") ?? throw new Exception("Environment variable not found");
        //public static readonly string location = @"H:\Subjects\Computer Science\git\CS-NEA\server-app\server-app\neuralNetwork\data\";
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
    }
}
