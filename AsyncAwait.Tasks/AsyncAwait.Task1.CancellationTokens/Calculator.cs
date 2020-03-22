using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    static class Calculator
    {
        public async static Task<long> CalculateAsync(int n, CancellationToken token)
        {
            if(token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            if (n <= 0)
            {
                throw new ArgumentException("Number of elements cannot be less than 0");
            }

            return await Task.Run(() => CalculateSum(n, token)).ConfigureAwait(false);
        }

        private static long CalculateSum(int n, CancellationToken token)
        {
            long sum = 0;

            for (int i = 0; i < n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return sum;
                }
                // i + 1 is to allow 2147483647 (Max(Int32)) 
                sum = sum + (i + 1);
                Thread.Sleep(100);
            }

            return sum;
        }
    }
}
