using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Читаємо вхідні дані з файлу INPUT.TXT
        string[] inputLines = File.ReadAllLines("C:/Users/Vadim/Desktop/labs/mpr1/INPUT.TXT");
        int N = int.Parse(inputLines[0].Split()[0]);
        int M = int.Parse(inputLines[0].Split()[1]);
        int[] A = inputLines[1].Split().Select(int.Parse).ToArray();

        // Знаходимо відповідь на задачу
        int result = Solve(N, A);

        // Записуємо результат у файл OUTPUT.TXT
        File.WriteAllText("C:/Users/Vadim/Desktop/labs/mpr1/OUTPUT.TXT", result.ToString());
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
}
