﻿using Rekrutacja.Workers.KalkulatorPola;
using Soneta.Business;
using Soneta.Kadry;
using System;

namespace Rekrutacja.Workers.Utilities
{
    public static class Utils
    {
        public static double Oblicz(double zmiennaA, double zmiennaB, string operacja)
        {
            switch (operacja)
            {
                case "+":
                    return zmiennaA + zmiennaB;
                case "-":
                    return zmiennaA - zmiennaB;
                case "*":
                    return zmiennaA * zmiennaB;
                case "/":
                    if (zmiennaB == 0)
                    {
                        throw new Exception("Dzielenie przez 0 nie jest możliwe.");
                    }
                    return zmiennaA / zmiennaB;
                default:
                    throw new Exception("Zmienna operacyjna może przyjąć tylko: +, -, *, /.");
            }
        }

        public static int ObliczPole(double zmiennaA, double zmiennaB, Figura operacja)
        {
            switch (operacja)
            {
                case Figura.Kwadrat:
                    return (int)Math.Pow(zmiennaA, 2);
                case Figura.Prostokat:
                    return (int)(zmiennaA * zmiennaB);
                case Figura.Trojkat:
                    return (int)(zmiennaA * zmiennaB) / 2;
                case Figura.Kolo:
                    return (int)(Math.Pow(zmiennaA, 2) * Math.PI);
                default:
                    return 0;
            }
        }

        public static Pracownik[] ZwrocZaznaczonychPracownikow(Context cx)
        {
            Pracownik[] pracownik = null;
            if (cx.Contains(typeof(Pracownik[])))
            {
                pracownik = (Pracownik[])cx[typeof(Pracownik[])];
            }

            return pracownik;
        }
    }
}
