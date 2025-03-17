using ConsoleApp1;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Przycisk1_Click(object sender, RoutedEventArgs e)
    {

        int a = text_a.Text != "" ? int.Parse(text_a.Text) : 0;
        int b = text_b.Text != "" ? int.Parse(text_b.Text) : 0;
        int c = text_c.Text != "" ? int.Parse(text_c.Text) : 0;

        int[] tablica = tablica_text.Text.Split(',').Select(int.Parse).ToArray();
        tablica.OrderByDescending(x => x).First();

        int mnoznik = mnoznik_text.Text != "" ? int.Parse(mnoznik_text.Text) : 0;
        double r = promien_text.Text != "" ? double.Parse(promien_text.Text) : 0;
        int liczba = liczba_text.Text != "" ? int.Parse(liczba_text.Text) : 0;
        int i = i_text.Text != "" ? int.Parse(i_text.Text) : 0;
        int j = j_text.Text != "" ? int.Parse(j_text.Text) : 0;
        int warunek = warunek_text.Text != "" ? int.Parse(warunek_text.Text) : 0;

        Etykieta1.Content = Class1.Potega(a, b, c);

        Etykieta2.Content = Class1.ZapiszWTablicy(tablica, mnoznik);

        Etykieta3.Content = Class1.PoleKola(r);

        Etykieta4.Content = Class1.SumaCyfr(liczba);

        Etykieta5.Content = Class1.ZamienElementy(tablica, i, j, warunek);

    }


}