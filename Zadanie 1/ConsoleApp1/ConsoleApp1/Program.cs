using ConsoleApp1;

//Console.WriteLine("Hello, World!");

Class1 obj = new Class1();

int a = 1, b = 2, c = 3;
int[] tablica = {1, 2, 3, 4};
int mnoznik = 2;
double r = 2.0;
int liczba = 123;
int i = 1, j = 2, warunek = 1;

string wynikPotega = obj.Potega(a, b, c);
Console.WriteLine($"Potega: {wynikPotega}");

int[] wynikZapiszWTablicy = obj.ZapiszWTablicy(tablica, mnoznik);
Console.WriteLine($"ZapiszWTablicy: {string.Join(", ", wynikZapiszWTablicy)}");

string wynikPoleKola = obj.PoleKola(r);
Console.WriteLine($"PoleKola: {wynikPoleKola}");

string wynikSumaCyfr = obj.SumaCyfr(liczba);
Console.WriteLine($"SumaCyfr: {wynikSumaCyfr}");

string wynikZamienElementy = obj.ZamienElementy(tablica, i, j, warunek);
Console.WriteLine($"ZamienElementy: {string.Join(", ",wynikZamienElementy)}");