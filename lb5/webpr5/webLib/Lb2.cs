using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webLib
{
    public static class Lb2
    {
        public static void Pr2(string inputFilePath, string outputFilePath)
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
    }
}
