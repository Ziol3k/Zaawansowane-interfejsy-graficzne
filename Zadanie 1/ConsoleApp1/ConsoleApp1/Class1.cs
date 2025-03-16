using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    {
        public Class1()
        {
        }

        public string Potega(int a, int b, int c)
        {
            if (c <= 0)
            {
                return "trzeci argument jest mniejszy od 0";
            }

            if (b < 0)
            {
                return "wykladnik mniejszy od 0";
            }

            return Math.Pow(a, b).ToString();
        }

        public int[] ZapiszWTablicy(int[] tablica, int mnoznik)
        {
            int[] nowaTablica = new int[tablica.Length];
            for (int i = 0; i < tablica.Length; i++)
            {
                nowaTablica[i] = tablica[i] * mnoznik;
            }
            return nowaTablica;
        }

        public string PoleKola(double r)
        {
            if (r <= 0)
            {
                return "promien musi byc wiekszy od 0";
            }

            double pole = Math.PI * Math.Pow(r, 2);
            return pole.ToString();
        }

        public string SumaCyfr(int liczba)
        {
            if (liczba < 100 || liczba > 999)
            {
                return "podana liczba nie jest trzycyfrowa";
            }

            int suma = 0;
            int temp = liczba;
            while (temp > 0)
            {
                suma += temp % 10;
                temp /= 10;
            }

            if (suma % 3 == 0)
            {
                return "liczba podzielna przez 3";
            }
            else
            {
                return "liczba nie jest podzielna przez 3";
            }
        }

        public string ZamienElementy(int[] tablica, int i, int j, int warunek)
        {
            if (i < 0 || i >= tablica.Length || j < 0 || j >= tablica.Length)
            {
                return "indeks spoza zakresu";
            }

            if (warunek > 0)
            {
                int temp = tablica[i];
                tablica[i] = tablica[j];
                tablica[j] = temp;
            }

            string wynik = "";
            for (int k = 0; k < tablica.Length; k++)
            {
                wynik += tablica[k] + (k < tablica.Length - 1 ? ", " : "");
            }

            return wynik;
        }
    }
}

