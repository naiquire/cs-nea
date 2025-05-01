namespace server_app.neuralNetwork
{
    public static class @data
    {
        public static double[][,] loadWeights()
        {
            return
            [
                new double[784, 144],
                new double[144, 72],
                new double[72, 72],
                new double[72, 26]
            ];
        }
        public static double[][] loadBiases()
        {
            return
            [
                new double[144],
                new double[72],
                new double[72],
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
