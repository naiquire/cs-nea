namespace server_app.neuralNetwork
{
    public static class @data
    {
        public static double[][,] loadWeights()
        {
            return
            [
                new double[784, 288],
                new double[288, 144],
                new double[144, 64],
                new double[64, 36]
            ];
        }
        public static double[][] loadBiases()
        {
            return
            [
                new double[288],
                new double[144],
                new double[64],
                new double[36]
            ];
        }
        public static void saveWeights(double[][,] weights)
        {

        }
        public static void saveBiases(double[][,] biases)
        {

        }
    }
}
