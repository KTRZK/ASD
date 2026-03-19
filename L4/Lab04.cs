using System;
using ASD.Graphs;
using ASD;
using System.Collections.Generic;

namespace ASD
{

    public class Lab04 : System.MarshalByRefObject
    {
        /// <summary>
        /// Etap 1 - szukanie trasy z miasta start_v do miasta end_v, startując w dniu day
        /// </summary>
        /// <param name="g">Ważony graf skierowany będący mapą</param>
        /// <param name="start_v">Indeks wierzchołka odpowiadającego miastu startowemu</param>
        /// <param name="end_v">Indeks wierzchołka odpowiadającego miastu docelowemu</param>
        /// <param name="day">Dzień startu (w tym dniu należy wyruszyć z miasta startowego)</param>
        /// <param name="days_number">Liczba dni uwzględnionych w rozkładzie (tzn. wagi krawędzi są z przedziału [0, days_number-1])</param>
        /// <returns>(result, route) - result ma wartość true gdy podróż jest możliwa, wpp. false, 
        /// route to tablica z indeksami kolejno odwiedzanych miast (pierwszy indeks to indeks miasta startowego, ostatni to indeks miasta docelowego),
        /// jeżeli result == false to route ustawiamy na null</returns>
        public (bool result, int[] route) Lab04_FindRoute(DiGraph<int> g, int start_v, int end_v, int day, int days_number)
        {
            int V = g.VertexCount;
            var adjByDay = new List<int>[V][];
            for (int v = 0; v < V; v++)
            {
                adjByDay[v] = new List<int>[days_number];
                for (int d = 0; d < days_number; d++)
                    adjByDay[v][d] = new List<int>();
            }
            for (int v = 0; v < V; v++)
                foreach (var edge in g.OutEdges(v))
                    adjByDay[v][edge.Weight].Add(edge.To);
            var prev = new (int fromV, int fromD)[V, days_number];
            for (int v = 0; v < V; v++)
                for (int d = 0; d < days_number; d++)
                    prev[v, d] = (-1, -1);
            prev[start_v, day] = (-2, -2);
            var queue = new Queue<(int v, int d)>();
            queue.Enqueue((start_v, day));

            (int v, int d) foundState = (-1, -1);

            while (queue.Count > 0)
            {
                var (v, d) = queue.Dequeue();
                int nextDay = (d + 1) % days_number;

                foreach (int u in adjByDay[v][d])
                {
                    if (prev[u, nextDay] == (-1, -1))
                    {
                        prev[u, nextDay] = (v, d);
                        if (u == end_v)
                        {
                            foundState = (u, nextDay);;
                            //return (true,null);
                            goto done;
                        }
                        queue.Enqueue((u, nextDay));
                    }
                }
            }
            done:
            if (foundState.v == -1)
                return (false, null);

            // Rekonstrukcja: idziemy wstecz po prev aż do sentinela (-2, -2)
            var routeList = new List<int>();
            (int curV, int curD) = foundState;
            while (curV != -2)
            {
                routeList.Add(curV);
                (curV, curD) = prev[curV, curD];
            }
            routeList.Reverse();

            return (true, routeList.ToArray());
        
            //return (false, null);
        }

        /// <summary>
        /// Etap 2 - szukanie trasy z jednego z miast z tablicy start_v do jednego z miast z tablicy end_v (startować można w dowolnym dniu)
        /// </summary>
        /// <param name="g">Ważony graf skierowany będący mapą</param>
        /// <param name="start_v">Tablica z indeksami wierzchołków startowych (trasę trzeba zacząć w jednym z nich)</param>
        /// <param name="end_v">Tablica z indeksami wierzchołków docelowych (trasę trzeba zakończyć w jednym z nich)</param>
        /// <param name="days_number">Liczba dni uwzględnionych w rozkładzie (tzn. wagi krawędzi są z przedziału [0, days_number-1])</param>
        /// <returns>(result, route) - result ma wartość true gdy podróż jest możliwa, wpp. false, 
        /// route to tablica z indeksami kolejno odwiedzanych miast (pierwszy indeks to indeks miasta startowego, ostatni to indeks miasta docelowego),
        /// jeżeli result == false to route ustawiamy na null</returns>
        public (bool result, int[] route) Lab04_FindRouteSets(DiGraph<int> g, int[] start_v, int[] end_v, int days_number)
        {
            // TODO
            return (false, null);
        }
    }
}
