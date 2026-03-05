using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD
{
    public class Lab02 : MarshalByRefObject
    {
        /// <summary>
        /// Optymalne rozmieszczenie parasolek w wariancie, w którym każda parasolka ma taki sam promień
        /// oraz mamy do dyspozycji tylko zadaną liczbę parasolek (rozmieszczenie parasolek nie wiąże się z żadnym kosztem)
        /// </summary>
        /// <param name="Z">Tablica zysków, Z[i] to zysk za pokrycie punktu o numerze i</param>
        /// <param name="umbrellaCount">Liczba dostępnych parasolek</param>
        /// <param name="umbrellaRadius">Promień parasolki (parasolka o promieniu r umieszczona w punkcie i pokrywa punkty i-r, i-r+1, ..., i+r)</param>
        /// <returns></returns>
        public (int profit, int[] umbrellaPosition) Stage1(int[] Z, int umbrellaCount, int umbrellaRadius)
        {
            //int[] dp = new int[Z.Length];
            //for (int i = 0;i < dp.Length; i++)
            //{
            //    if (i < umbrellaRadius)
            //    {
            //        if (i > umbrellaCount - umbrellaRadius)
            //        {
            //            int sum = 0;
            //            for (int j = 0; j < Z.Length; j++)
            //            {
            //                sum += Z[j];
            //            }
            //            return (sum, new int[0]);
            //        }
            //        for (int j = 0; j < i; j++)
            //        {
            //            dp[j] += Z[j];
            //        }
            //        for (int j = 0;j < dp.Length; j++) 
            //    }
            //    else if (i + umbrellaRadius > umbrellaCount)
            //    {
            //        for (int j = 0; j < umbrellaCount - i; j++)
            //        {
            //            dp[j] += Z[j];
            //        }
            //    }
            //    else
            //    {
            //        for (i = 0; i < Z.Length; i++)
            //        {
            //            dp[i] += Z[i];
            //        }
            //    }
            //}
            int n = Z.Length;
            int r = umbrellaRadius;

            if (n == 0 || umbrellaCount == 0)
                return (0, new int[0]);
            int d = Math.Min(umbrellaCount, n);
            long[] prefix = new long[n + 1];
            for (int i = 0; i < n; i++)
                prefix[i + 1] = prefix[i] + Z[i];
            long[,] dp = new long[d + 1, n + 1];

            for (int i = 1; i <= d; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    dp[i, j] = dp[i, j - 1];

                    int leftEdge = j - 1 - 2 * r;
                    int prevBound = Math.Max(0, leftEdge);
                    long gain = prefix[j] - prefix[prevBound];
                    long candidate = dp[i - 1, prevBound] + gain;
                    if (candidate > dp[i, j])
                        dp[i, j] = candidate;
                }
            }
            long maxProfit = dp[d, n];
            var positions = new List<int>();
            int ci = d, cj = n;
            while (ci > 0 && cj > 0)
            {
                if (dp[ci, cj] == dp[ci, cj - 1])
                {
                    cj--;
                }
                else
                {
                    int center = cj - 1 - r;
                    int leftEdge = cj - 1 - 2 * r;
                                        positions.Add(Math.Max(0, Math.Min(n, center)));

                    cj = Math.Max(0, leftEdge);
                    ci--;
                }
            }

            positions.Reverse();
            return ((int)maxProfit, positions.ToArray());


            //return (0, null);
        }


        /// <summary>
        /// Optymalne rozmieszczenie parasolek w wariancie, w którym mamy dostępne modele parasolek o różnych promieniach.
        /// Każdego modelu możemy użyć dowolną liczbę razy, jednak za każdym razem musimy ponieść jego koszt.
        /// </summary>
        /// <param name="Z">Tablica zysków, Z[i] to zysk za pokrycie punktu o numerze i</param>
        /// <param name="umbrellaType">Tablice dostępnych modeli parasolek, gdzie i-ty model ma promień umbrellaType[i].radius i koszt umbrellaType[i].cost</param>
        /// <returns></returns>
        public (int profit, (int position, int model)[] umbrellas) Stage2(int[] Z, (int radius, int cost)[] umbrellaType)
        {
            return (0, null);
        }
    }
}
