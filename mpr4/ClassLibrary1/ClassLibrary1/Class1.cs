namespace ClassLibrary1
{
    public class Class1
    {
        public void Pr1(string inputFilePath, string outputFilePath)
        {
            // Читаємо вхідні дані з файлу INPUT.TXT
            string[] inputLines = File.ReadAllLines(inputFilePath);
            int N = int.Parse(inputLines[0].Split()[0]);
            int M = int.Parse(inputLines[0].Split()[1]);
            int[] A = inputLines[1].Split().Select(int.Parse).ToArray();

            // Знаходимо відповідь на задачу
            int result = Solve(N, A);

            // Записуємо результат у файл OUTPUT.TXT
            File.WriteAllText(outputFilePath, result.ToString());
        }

        static int Solve(int N, int[] A)
        {
            int[] dp = new int[N + 1];
            for (int i = 1; i <= N; i++)
            {
                dp[i] = int.MaxValue;
            }
            dp[0] = 0;

            for (int i = 1; i <= N; i++)
            {
                foreach (int coin in A)
                {
                    if (i - coin >= 0 && dp[i - coin] != int.MaxValue)
                    {
                        // Перевіряємо, що не використовуємо більше 2 монет кожного номіналу
                        if (dp[i - coin] + 1 <= 2)
                        {
                            dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                        }
                    }
                }
            }

            if (A.Sum() * 2 < N)
            {
                return -1; // Сума монет менша за загальну суму
            }
            else if (dp[N] == int.MaxValue)
            {
                return 0; // Сума монет більша за N
            }
            else
            {
                return dp[N]; // Виводимо кількість монет
            }
        }

        public void Pr2(string inputFilePath, string outputFilePath)
        {
            // Зчитуємо дані з файлу вхідних даних
            string[] lines = File.ReadAllLines(inputFilePath);
            int n = int.Parse(lines[0]); // Кількість учнів, не використовується в цьому коді
            string s = lines[1]; // Рядок з хлопчиками та дівчатами

            int result = CountGroupVariants(s);

            // Записуємо результат у файл
            File.WriteAllText(outputFilePath, result.ToString());
        }

        static int CountGroupVariants(string s)
        {
            int boyCount = 0;
            int girlCount = 0;
            int result = 0;
            int[] diffCounts = new int[s.Length + 1];

            for (int i = 0; i < s.Length; i++)
            {
                char currentChar = s[i];
                if (currentChar == 'a')
                    girlCount++;
                else if (currentChar == 'b')
                    boyCount++;

                int diff = girlCount - boyCount;

                if (diff == 0)
                    result++;

                // Збільшуємо кількість можливих груп для поточної різниці
                if (diffCounts[diff + s.Length / 2] > 0)
                    result += diffCounts[diff + s.Length / 2];

                // Збільшуємо кількість можливих груп для попередніх різниць
                diffCounts[diff + s.Length / 2]++;
            }

            return result;
        }

        public void Pr3(string inputFilePath, string outputFilePath)
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
