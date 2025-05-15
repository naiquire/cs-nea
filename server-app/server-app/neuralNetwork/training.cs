using System.IO.Compression;

namespace server_app.neuralNetwork
{
    public class @training
    {
        public training()
        {
            (List<double[]> images, List<int> results) = loadImages();
            Random rnd = new Random();
            for (int i = 0; i < images.Count / 50; i++)
            {
                int index = rnd.Next(images.Count - 50 - 1);

                var subimages = images.GetRange(index, 50);
                var subresults = results.GetRange(index, 50);
                var network = new backpropagation(subimages, subresults);


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

                // read 100,000 images
                for (int i = 0; i < 100000; i++)
                {
                    var image = new byte[784];
                    gz.ReadExactly(image, 0, 784);

                    var a = new double[784];
                    //for (int j = 0; j < a.Length; j++)
                    //{
                    //    a[j] = image[j];
                    //    a[j] /= 255;
                    //}
                    int count = 0;
                    for (int row = 0; row < 28; row++)
                    {
                        for (int column = 0; column < 28; column++, count++)
                        {
                            a[column * 28 + row] = (double)image[count] / 255;
                        }
                    }

                    images.Add(a);

                    //int count = 0;
                    //Console.Clear();
                    //for (int j = 0; j < 28; j++)
                    //{
                    //    for (int k = 0; k < 28; k++)
                    //    {
                    //        //Console.Write(image[count]);
                    //        if (a[count] > 0.9)
                    //        {
                    //            Console.Write("X");
                    //        }
                    //        else { Console.Write(" "); }
                    //        count++;
                    //    }
                    //    Console.WriteLine();
                    //}

                }
            }

            fs = new FileStream($@"{data.location}training\labels.gz", FileMode.Open, FileAccess.ReadWrite);

            using (GZipStream gz = new(fs, cm))
            {
                // discard header info
                byte[] header = new byte[16];
                gz.ReadExactly(header, 0, 8);

                // read 50,000 labels
                for (int i = 0; i < 100000; i++)
                {
                    labels.Add(gz.ReadByte());
                }
            }

            return (images, labels);
        }
    }
}
