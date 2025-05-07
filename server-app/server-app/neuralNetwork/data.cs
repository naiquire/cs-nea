using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using static System.Net.Mime.MediaTypeNames;

namespace server_app.neuralNetwork
{
    public static class @data
    {
        //public static readonly string location = @"C:\Users\boyss\Documents\General\Relay\github\cs-nea-app\server-app\server-app\neuralNetwork\data\";
        public static readonly string location = @"H:\Subjects\Computer Science\git\CS-NEA\server-app\server-app\neuralNetwork\data\";
        public static void initialiseParameters()
        {
            // initialise weights
            Random rnd = new();
            double[][,] weights = new double[evaluate.layerCount - 1][,];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = new double[evaluate.networkLayers[i], evaluate.networkLayers[i + 1]];
                for (int j = 0; j < evaluate.networkLayers[i]; j++)
                {
                    for (int k = 0; k < evaluate.networkLayers[i + 1]; k++)
                    {
                        double low = - (Math.Sqrt(6) / Math.Sqrt(evaluate.networkLayers[i] + evaluate.networkLayers[i + 1]));
                        double high = Math.Sqrt(6) / Math.Sqrt(evaluate.networkLayers[i] + evaluate.networkLayers[i + 1]);
                        weights[i][j, k] = (rnd.NextDouble() * 2 - 1) * high;
                    }
                }
            }

            // initialise biases
            double[][] biases = new double[evaluate.layerCount - 1][];
            for (int i = 0; i < biases.Length; i++)
            {
                biases[i] = new double[evaluate.networkLayers[i + 1]];
            }
            saveWeights(weights);
            saveBiases(biases);
        }
        public static double[][,] loadWeights()
        {
            double[][,] weights = new double[evaluate.layerCount - 1][,];
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                weights[i] = new double[evaluate.networkLayers[i], evaluate.networkLayers[i + 1]];
                using (StreamReader sr = new($@"{location}weights\{i}.txt"))
                {
                    for (int j = 0; j < evaluate.networkLayers[i]; j++)
                    {
                        string[] s = sr.ReadLine().Split(',');
                        for (int k = 0; k < evaluate.networkLayers[i + 1]; k++)
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
                biases[i] = new double[evaluate.networkLayers[i + 1]];
                using (StreamReader sr = new($@"{location}weights\{i}.txt"))
                {
                    string[] s = sr.ReadLine().Split(',');
                    for (int j = 0; j < evaluate.networkLayers[i + 1]; j++)
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
                using (StreamWriter sw = new($@"{location}weights\{i}.txt"))
                {
                    for (int j = 0; j < weights[i].GetLength(0); j++)
                    {
                        for (int k = 0; k < weights[i].GetLength(1) - 1; k++)
                        {
                            sw.Write($"{weights[i][j, k]},");
                        }
                        sw.WriteLine($"{weights[i][j, weights[i].GetLength(1) - 1]}");
                    }
                }
            }
        }
        public static void saveBiases(double[][] biases)
        {
            for (int i = 0; i < evaluate.layerCount - 1; i++)
            {
                using (StreamWriter sw = new($@"{location}biases\{i}.txt"))
                {
                    for (int j = 0; j < biases[i].GetLength(0); j++)
                    {
                        sw.Write($"{biases[i][j]},");
                    }
                    sw.Write($"{biases[i][biases[i].Length - 1]}");
                }
            }
        }
    }
    public class @training
    {
        public training()
        {
            (List<double[]> images, List<int> results) = loadImages();
            for (int i = 0; i < images.Count; i += 50)
            {
                var subimages = images.GetRange(i, 50);
                var subresults = results.GetRange(i, 50);
                var network = new backpropagation(subimages, subresults);

                Console.WriteLine($"{backpropagation.correct / backpropagation.epochs * 100}%");
            }
        }
        public static (List<double[]>, List<int>) loadImages()
        {
            FileStream fs = new FileStream($@"{data.location}training\images.gz", FileMode.Open, FileAccess.ReadWrite);
            CompressionMode cm = CompressionMode.Decompress;

            List<double[]> images = [];
            List<int> labels = [];

            using (GZipStream gz = new(fs, cm))
            {
                // discard header info
                byte[] header = new byte[16];
                gz.ReadExactly(header, 0, 16);

                // read 50,000 images
                for (int i = 0; i < 50000; i++)
                {
                    var image = new byte[784];
                    gz.ReadExactly(image, 0, 784);

                    var a = new double[784];
                    for (int j = 0; j < a.Length; j++)
                    {
                        a[j] = image[j] / 255;
                    }
                    images.Add(a);

                    int count = 0;
                    for (int j = 0; j < 28; j++)
                    {
                        for (int k = 0; k < 28; k++)
                        {
                            Console.Write(a[count]);
                            count++;
                        }
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                }
            }

            fs = new FileStream($@"{data.location}training\labels.gz", FileMode.Open, FileAccess.ReadWrite);

            using (GZipStream gz = new(fs, cm))
            {
                // discard header info
                byte[] header = new byte[16];
                gz.ReadExactly(header, 0, 8);

                // read 50,000 labels
                for (int i = 0; i < 50000; i++)
                {
                    labels.Add(gz.ReadByte());
                }
            }

            return (images, labels);
        }
    }
}
