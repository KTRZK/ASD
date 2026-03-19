using System;
using ASD.Graphs;
using ASD;
using System.Collections.Generic;

namespace ASD
{
    public class Lab04 : System.MarshalByRefObject
    {
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
                        if (u == end_v) { foundState = (u, nextDay); goto done; }
                        queue.Enqueue((u, nextDay));
                    }
                }
            }
        done:

            if (foundState.v == -1)
                return (false, null);

            var routeList = new List<int>();
            (int curV, int curD) = foundState;
            while (curV != -2)
            {
                routeList.Add(curV);
                (curV, curD) = prev[curV, curD];
            }
            routeList.Reverse();

            return (true, routeList.ToArray());
        }

        public (bool result, int[] route) Lab04_FindRouteSets(DiGraph<int> g, int[] start_v, int[] end_v, int days_number)
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

            var isEnd = new bool[V];
            foreach (int v in end_v)
                isEnd[v] = true;

            var prev = new (int fromV, int fromD)[V, days_number];
            for (int v = 0; v < V; v++)
                for (int d = 0; d < days_number; d++)
                    prev[v, d] = (-1, -1);

            var queue = new Queue<(int v, int d)>();
            foreach (int s in start_v)
                for (int d = 0; d < days_number; d++)
                {
                    prev[s, d] = (-2, -2);
                    queue.Enqueue((s, d));
                }

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
                        if (isEnd[u]) { foundState = (u, nextDay); goto done; }
                        queue.Enqueue((u, nextDay));
                    }
                }
            }
        done:

            if (foundState.v == -1)
                return (false, null);

            var routeList = new List<int>();
            (int curV, int curD) = foundState;
            while (curV != -2)
            {
                routeList.Add(curV);
                (curV, curD) = prev[curV, curD];
            }
            routeList.Reverse();

            return (true, routeList.ToArray());
        }
    }
}
