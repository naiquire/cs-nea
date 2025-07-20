using System.IO.Compression;

namespace server_app.neuralNetwork
{
    public class @training
    {
        public training()
        {
            // load training data
            (List<double[]> images, List<int> results) = data.loadImages();

            // random sample of 50 images
            Random rnd = new();
            for (int i = 0; i < images.Count / 50; i++)
            {
                // load sample index
                int index = rnd.Next(images.Count - 50 - 1);

                // load sampled images and labels
                var subimages = images.GetRange(index, 50);
                var subresults = results.GetRange(index, 50);

                // forward propagation and backpropagation
                var network = new backpropagation(subimages, subresults);
            }
        }
    }
}
