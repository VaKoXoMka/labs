using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webLib
{
    public static class Lb3
    {
        public static void Pr3(string inputFilePath, string outputFilePath)
        {
            using (StreamReader sr = new StreamReader(inputFilePath))
            {
                string[] input = sr.ReadLine().Split();
                int n = int.Parse(input[0]);
                int m = int.Parse(input[1]);
                int k = int.Parse(input[2]);

                List<List<int>> corridors = new List<List<int>>();
                for (int i = 0; i < m; i++)
                {
                    input = sr.ReadLine().Split();
                    int u = int.Parse(input[0]);
                    int v = int.Parse(input[1]);
                    int c = int.Parse(input[2]);
                    corridors.Add(new List<int> { u, v, c });
                }

                int L = int.Parse(sr.ReadLine());
                int[] instructions = sr.ReadLine().Split().Select(int.Parse).ToArray();
                int s = int.Parse(sr.ReadLine());

                List<int> reachableRooms = FindReachableRooms(n, corridors, L, instructions, s);

                using (StreamWriter sw = new StreamWriter(outputFilePath))
                {
                    if (reachableRooms.Count == 0)
                    {
                        sw.WriteLine("Hangs");
                    }
                    else
                    {
                        sw.WriteLine("OK");
                        sw.WriteLine(reachableRooms.Count);
                        sw.WriteLine(string.Join(" ", reachableRooms.OrderBy(r => r)));
                    }
                }
            }
        }

        static List<int> FindReachableRooms(int n, List<List<int>> corridors, int L, int[] instructions, int s)
        {
            List<int>[] adjacencyList = new List<int>[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            foreach (var corridor in corridors)
            {
                int u = corridor[0];
                int v = corridor[1];
                int c = corridor[2];
                adjacencyList[u].Add(v);
            }

            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();

            queue.Enqueue(s);
            visited.Add(s);

            for (int step = 0; step < L; step++)
            {
                int currentInstruction = instructions[step];
                int count = queue.Count;

                for (int i = 0; i < count; i++)
                {
                    int currentRoom = queue.Dequeue();

                    foreach (int nextRoom in adjacencyList[currentRoom])
                    {
                        if (!visited.Contains(nextRoom) && corridors.Any(corridor => corridor[0] == currentRoom && corridor[1] == nextRoom && corridor[2] == currentInstruction))
                        {
                            queue.Enqueue(nextRoom);
                            visited.Add(nextRoom);
                        }
                    }
                }

                if (queue.Count == 0)
                {
                    return new List<int>();
                }
            }

            return queue.ToList();
        }
    }
}
