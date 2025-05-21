using Soneta.Tools;
using System;

namespace Rekrutacja.Workers.Utilities
{
    public static class StringParser
    {
        public static int ParseToInt(string input)
        {
            bool jestUjemna = false;
            int startIndex = 0;
            int wynik = 0;

            if (input.IsNullOrEmpty())
                throw new ArgumentNullException("Brak wartości");

            if (input[0] == '-')
            {
                jestUjemna = true;
                startIndex = 1;
            }

            for (int i = startIndex; i < input.Length; i++)
            {
                char c = input[i];

                if (!Char.IsDigit(c))
                    throw new FormatException("input musi być w całości liczbą");

                int charToInt = c - '0';

                wynik = wynik * 10 + charToInt;
            }

            return jestUjemna ? -wynik : wynik;
        }
    }
}
