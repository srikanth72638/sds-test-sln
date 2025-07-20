using System;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            if (n <= 0)
                throw new ArgumentException("400: invalid input");

            return n == 1 ? 1 : n * GetFactorial(n - 1); 
        }

        public static string FormatSeparators(params string[] items)
        {
            //TODO: currently returning an empty string. can be treated as invalid input
            if (items == null || items.Length == 0) return string.Empty;

            if (items.Length == 1) return items[0];

            if (items.Length == 2) return $"{items[0]} and {items[1]}";

            //TODO: what if the last item is empty or whitespace?
            string lastItem = items[items.Length - 1];
            string result = string.Join(", ", items, 0, items.Length - 1);
            return $"{result} and {lastItem}";
        }
    }
}