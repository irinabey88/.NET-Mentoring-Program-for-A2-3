using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        private const byte MinValidMatrixRank = 2;

        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            if(m1 == null)
            {
                throw new ArgumentNullException(nameof(m1));
            }
            if(m2 == null)
            {
                throw new ArgumentNullException(nameof(m2));
            }
            if(m1.RowCount < MinValidMatrixRank)
            {
                throw new ArgumentException($"Matrix 'm1' RowCount value can not be less than {MinValidMatrixRank}");
            }
            if (m1.ColCount < MinValidMatrixRank)
            {
                throw new ArgumentException($"Matrix 'm1' ColCount value can not be less than {MinValidMatrixRank}");
            }
            if (m2.RowCount < MinValidMatrixRank)
            {
                throw new ArgumentException($"Matrix 'm2' RowCount value can not be less than {MinValidMatrixRank}");
            }
            if (m2.ColCount < MinValidMatrixRank)
            {
                throw new ArgumentException($"Matrix 'm2' ColCount value can not be less than {MinValidMatrixRank}");
            }

            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);

            Parallel.For(0, m1.RowCount, i =>
            {
                for (byte j = 0; j < m2.ColCount; j++)
                {
                    long sum = 0;
                    for (byte k = 0; k < m1.ColCount; k++)
                    {
                        sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }

                    resultMatrix.SetElement(i, j, sum);
                }
            });

            return resultMatrix;
        }
    }
}
