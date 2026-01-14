namespace server_app.neuralNetwork
{
    public class Training
    {
        public Training()
        {
            // load training data
            (List<double[]> images, List<int> labels) = Data.FilterImages(Data.LoadImages());

			//random sample of 50 images
			Random rnd = new();
			for (int i = 0; i < images.Count / 50; i++)
			{
				// load sample index
				int index = rnd.Next(images.Count - 50 - 1);

				// load sampled images and labels
				var subimages = images.GetRange(index, 50);
				var subresults = labels.GetRange(index, 50);

				// forward propagation and backpropagation
				_ = new Backpropagation(subimages, subresults);
			}
		}
	}
}
