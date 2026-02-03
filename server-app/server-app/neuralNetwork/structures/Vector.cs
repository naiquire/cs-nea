namespace server_app.neuralNetwork.structures
{
	public class Vector
	{
		private readonly double[] values;
		public double this[int i]
		{
			get {
				return values[i];
			}
			set {
				values[i] = value;
			}
		}

		public int GetLength() => values.GetLength(0);
		public double[] GetArray() => values;

		public Vector(int x)
		{
			values = new double[x];
		}
		public Vector(double[] vector)
		{
			values = vector;
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			// check if addition is valid
			if (v1.GetLength() != v2.GetLength())
			{
				throw new InvalidOperationException();
			}

			int length = v1.GetLength();
			Vector result = new(length);

			for (int i = 0; i < length; i++)
			{
				result.values[i] = v1.values[i] + v2.values[i];
			}

			return result;
		}
		public static Vector operator -(Vector v1, Vector v2)
		{
			// check if subtraction is valid
			if (v1.GetLength() != v2.GetLength())
			{
				throw new InvalidOperationException();
			}

			int length = v1.GetLength();
			Vector result = new(length);

			for (int i = 0; i < length; i++)
			{
				result.values[i] = v1.values[i] - v2.values[i];
			}

			return result;
		}
	}
}
