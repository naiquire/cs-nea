namespace server_app.neuralNetwork.structures
{
	public readonly struct Matrix
	{
		private readonly double[,] values;
		public double this[int i, int j]
		{
			get { 
				return values[i, j];
			}
			set {
				values[i, j] = value;
			}
		}

		public int GetRowCount() => values.GetLength(0);
		public int GetColumnCount() => values.GetLength(1);
		public double[,] GetArray() => values;

		public Matrix(int x, int y)
		{
			values = new double[x, y];
		}	
		public Matrix(double[,] matrix)
		{
			values = matrix;
		}

		public static Matrix operator +(Matrix m1, Matrix m2)
		{
			// check if addition is valid
			if (m1.GetRowCount() != m2.GetRowCount() || m1.GetColumnCount() != m2.GetColumnCount())
			{
				throw new InvalidOperationException();
			}

			int rows = m1.GetRowCount();
			int columns = m1.GetColumnCount();
			Matrix result = new(rows, columns);

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					result[i, j] = m1[i, j] + m2[i, j];
				}
			}

			return result;
		}
		public static Matrix operator -(Matrix m1, Matrix m2)
		{
			// check if subtraction is valid
			if (m1.GetRowCount() != m2.GetRowCount() || m1.GetColumnCount() != m2.GetColumnCount())
			{
				throw new InvalidOperationException();
			}

			int rows = m1.GetRowCount();
			int columns = m1.GetColumnCount();
			Matrix result = new(rows, columns);

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					result[i, j] = m1[i, j] - m2[i, j];
				}
			}

			return result;
		}
		public static Matrix operator *(Matrix m1, Matrix m2)
		{
			// check if multiplication is valid
			if (m1.GetColumnCount() != m2.GetRowCount())
			{
				throw new InvalidOperationException();
			}

			int rows = m1.GetRowCount();
			int columns = m2.GetColumnCount();
			int multiplyLength = m1.GetColumnCount();

			Matrix result = new(rows, columns);

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					double sum = 0;
					for (int k = 0; k < multiplyLength; k++)
					{
						sum += m1[i, k] * m2[k, j];
					}
					result[i, j] = sum;
				}
			}

			return result;
		}
		
		public static Vector operator *(Matrix matrix, Vector vector)
		{
			// check if multiplication is valid
			if (matrix.GetColumnCount() != vector.GetLength())
			{
				throw new InvalidOperationException();
			}

			int length = matrix.GetRowCount();
			int multiplyLength = matrix.GetColumnCount();

			Vector result = new(length);

			for (int i = 0; i < length; i++)
			{
				double sum = 0;
				for (int j = 0; j < multiplyLength; j++)
				{
					sum += matrix[i, j] * vector[j];
				}
				result[i] = sum;
			}

			return result;
		}
		public static Vector operator *(Vector vector, Matrix matrix)
		{
			// check if multiplication is valid
			if (vector.GetLength() != matrix.GetRowCount())
			{
				throw new InvalidOperationException();
			}

			int length = matrix.GetColumnCount();
			int multiplyLength = matrix.GetRowCount();

			Vector result = new(length);

			for (int i = 0; i < length; i++)
			{
				double sum = 0;
				for (int j = 0; j < multiplyLength; j++)
				{
					sum += matrix[j, i] * vector[j];
				}
				result[i] = sum;
			}

			return result;
		}
	}
}
