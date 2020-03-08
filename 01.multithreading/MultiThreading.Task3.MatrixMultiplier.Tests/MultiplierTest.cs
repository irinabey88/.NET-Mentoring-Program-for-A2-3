using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier.Tests
{
    [TestClass]
    public class MultiplierTest
    {
        [TestMethod]
        public void MultiplyMatrix3On3Test()
        {
            TestMatrix3On3(new MatricesMultiplier());
            TestMatrix3On3(new MatricesMultiplierParallel());
        }

        [DataTestMethod]
        [DataRow(41, 41, 4)]
        [DataRow(50, 50, 4)]
        [DataRow(100, 100, 4)]
        public void ParallelEfficiencyTest(int rank1, int rank2, long stubValue)
        {
            var m1 = GetStubMatrix(rank1, rank2, stubValue);
            var m2 = GetStubMatrix(rank1, rank2, stubValue);

            long executionTimeParallel = MultiplierExecute(new MatricesMultiplierParallel(), m1, m2);
            long executionTimeRegular = MultiplierExecute(new MatricesMultiplier(), m1, m2);

            Assert.IsTrue(executionTimeParallel < executionTimeRegular);
        }

        #region private methods

        void TestMatrix3On3(IMatricesMultiplier matrixMultiplier)
        {
            if (matrixMultiplier == null)
            {
                throw new ArgumentNullException(nameof(matrixMultiplier));
            }

            var m1 = new Matrix(3, 3);
            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);

            var m2 = new Matrix(3, 3);
            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);

            var multiplied = matrixMultiplier.Multiply(m1, m2);
            Assert.AreEqual(448, multiplied.GetElement(0, 0));
            Assert.AreEqual(1826, multiplied.GetElement(0, 1));
            Assert.AreEqual(3052, multiplied.GetElement(0, 2));

            Assert.AreEqual(350, multiplied.GetElement(1, 0));
            Assert.AreEqual(712, multiplied.GetElement(1, 1));
            Assert.AreEqual(1127, multiplied.GetElement(1, 2));

            Assert.AreEqual(109, multiplied.GetElement(2, 0));
            Assert.AreEqual(213, multiplied.GetElement(2, 1));
            Assert.AreEqual(728, multiplied.GetElement(2, 2));
        }

        private Matrix GetStubMatrix(int rank1, int rank2, long stubElementValue)
        {
            if(rank1 < 2)
            {
                throw new ArgumentException("Invalid rank1 value");
            }
            if (rank2 < 2)
            {
                throw new ArgumentException("Invalid rank2 value");
            }

            var matrix = new Matrix(rank1, rank2);

            for(int i = 0; i < rank1; i++)
            {
                for(int j = 0; j < rank2; j++)
                {
                    matrix.SetElement(i, j, stubElementValue);
                }
            }

            return matrix;
        }

        private long MultiplierExecute(IMatricesMultiplier matrixMultiplier, Matrix m1, Matrix m2)
        {
            if (matrixMultiplier == null)
            {
                throw new ArgumentNullException(nameof(matrixMultiplier));
            }
            if (m1 == null)
            {
                throw new ArgumentNullException(nameof(m1));
            }
            if (m2 == null)
            {
                throw new ArgumentNullException(nameof(m2));
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IMatrix multiplied = matrixMultiplier.Multiply(m1, m2);
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        #endregion
    }
}
