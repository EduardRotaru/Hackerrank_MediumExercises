using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace Hackerrank_MediumExercises
{
    class Program
    {
        private static string Alpha => "abcdefghijklmnopqrstuvwxyz";

        static void Main(string[] args)
        {
            var sSample = "abccddde";

            var ss = File.ReadAllLines(@"C:\Users\test\source\repos\Hackerrank_MediumExercises\Hackerrank_MediumExercises\cacat.txt");
            var arrr = Array.ConvertAll(ss, s => int.Parse(s));
            var ar2 = new int[] { 1, 3, 12, 5, 9, 10 };

            foreach (var s in WeightedUniformStrings_GoodSolution(sSample, ar2))
            {
                WriteLine(s);
            }

            var sticks = new int[] { 1, 2, 3 };

            MaximumPerimeterTriangle(sticks);
        }

        static string Chiper(string s, int k)
        {
            var alphaUpper = Alpha.ToUpper();

            if (k > Alpha.Length) k = k % 26;

            var chiper = String.Join("", Alpha.Substring(k), Alpha.Substring(0, k));

            var sr = new StringBuilder();

            foreach (var charElem in s)
            {
                if (!char.IsLetter(charElem))
                {
                    sr.Append(charElem);
                    continue;
                }

                sr.Append(char.IsUpper(charElem)
                    ? char.ToUpper(chiper[alphaUpper.IndexOf(charElem)])
                    : chiper[Alpha.IndexOf(charElem)]);
            }
            return sr.ToString();
        }

        static string[] WeightedUniformStrings_GoodSolution(string s, int[] queries)
        {
            char prev = s[0];
            int count = 1;

            var weights2 = new HashSet<int>();
            weights2.Add(prev - 96);

            for (int i = 1; i < s.Length; i++)
            {
                if (prev == s[i])
                {
                    count++;
                    weights2.Add(count * (prev - 96));
                }
                else
                {
                    prev = s[i];
                    count = 1;
                    weights2.Add(count * (prev - 96));
                }
            }

            var res = new string[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                if (weights2.Contains(queries[i]))
                    res[i] = "Yes";
                else
                    res[i] = "No";
            }

            return res;
        }

        static string[] WeightedUniformStringsBadSolution(string s, int[] queries)
        {
            var alphabetWeights = new Dictionary<char, int>()
            {
                {'a', 1},
                {'b', 2},
                {'c', 3},
                {'d', 4},
                {'e', 5},
                {'f', 6},
                {'g', 7},
                {'h', 8},
                {'i', 9},
                {'j', 10},
                {'k', 11},
                {'l', 12},
                {'m', 13},
                {'n', 14},
                {'o', 15},
                {'p', 16},
                {'q', 17},
                {'r', 18},
                {'s', 19},
                {'t', 20},
                {'u', 21},
                {'v', 22},
                {'w', 23},
                {'x', 24},
                {'y', 25},
                {'z', 26}
            };

            var charsCount = new Dictionary<char, int>();

            // try to get sum of weights directly in a list
            var sr = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length;)
                {
                    if (s[i] == s[j])
                    {
                        sr.Append($"{s[i]}");
                        break;
                    }

                    sr.Append($"{s[i]} ");
                    break;
                }

                if (i + 1 == s.Length) sr.Append($"{s[i]}");
            }

            var hash = new HashSet<string>(sr.ToString().Split(' '));

            // calculate weights
            var listOfSums = new List<int>();
            var sum = 0;
            foreach (var strings in hash)
            {
                foreach (var chars in strings)
                {
                    sum += alphabetWeights[chars];
                    listOfSums.Add(sum);
                }

                sum = 0;
            }

            var res = new string[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                if (listOfSums.Contains(queries[i]))
                {
                    res[i] = "Yes";
                }
                else
                {
                    res[i] = "No";
                }
            }

            return res;
        }

        static int MaximumToys(List<int> prices, int k)
        {
            var count = 0;
            var sum = 0;

            var list = prices.Where(t => t <= k).OrderBy(x => x).ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                sum += list[i];
                if (sum > k) break;
                count++;
            }

            return count;
        }

        static int[] MaximumPerimeterTriangle(int[] sticks)
        {
            Array.Sort(sticks);
            var triangle = new int[3];

            var listOfValidTriangles = new List<Triangle>();
            for (int i = 0; i < sticks.Length - 2; i++)
            {
                if (sticks[i] + sticks[i + 1] > sticks[i + 2])
                {
                    listOfValidTriangles.Add(new Triangle(sticks[i], sticks[i + 1], sticks[i + 2]));
                }
            }

            var res = listOfValidTriangles.FirstOrDefault(x => x.Sum() == listOfValidTriangles.Max(y => y.Sum()));

            if (res.Sum() == 0) return new[] { -1 };

            triangle[0] = res.X;
            triangle[1] = res.Y;
            triangle[2] = res.Z;

            return triangle;
        }
    }

    public struct Triangle
    {
        public int X, Y, Z;

        public Triangle(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int Sum()
        {
            return X + Y + Z;
        }
    }
}
