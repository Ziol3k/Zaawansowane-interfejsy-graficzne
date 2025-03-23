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

    private void PotegaButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int a = text_a.Text != "" ? int.Parse(text_a.Text) : 0;
            int b = text_b.Text != "" ? int.Parse(text_b.Text) : 0;
            int c = text_c.Text != "" ? int.Parse(text_c.Text) : 0;

            Potega.Content = Class1.Potega(a, b, c);
        }
        catch (FormatException)
        {
            MessageBox.Show("Wprowadz poprawne liczby.", "Blad formatu", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystapil blad: {ex.Message}", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ZapiszWTablicyButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int[] tablica = tablica_text.Text.Split(',').Select(int.Parse).ToArray();

            int mnoznik = mnoznik_text.Text != "" ? int.Parse(mnoznik_text.Text) : 0;

            ZapiszWtablicy.Content = Class1.ZapiszWTablicy(tablica, mnoznik);
        }
        catch (FormatException)
        {
            MessageBox.Show("Wprowadz poprawne liczby oddzielone przecinkami.", "Blad formatu", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystapil blad: {ex.Message}", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void PoleKolaButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            double r = promien_text.Text != "" ? double.Parse(promien_text.Text) : 0;
            PoleKola.Content = Class1.PoleKola(r);
        }
        catch (FormatException)
        {
            MessageBox.Show("Wprowadz poprawna liczbe dla promienia.", "Blad formatu", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystapil blad: {ex.Message}", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SumaCyfrButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int liczba = liczba_text.Text != "" ? int.Parse(liczba_text.Text) : 0;
            SumaCyfr.Content = Class1.SumaCyfr(liczba);
        }
        catch (FormatException)
        {
            MessageBox.Show("Wprowadz poprawna liczbe.", "Blad formatu", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystapil blad: {ex.Message}", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ZamienElementyButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int[] tablica2 = tablica2_text.Text.Split(',').Select(int.Parse).ToArray();

            int i = i_text.Text != "" ? int.Parse(i_text.Text) : 0;
            int j = j_text.Text != "" ? int.Parse(j_text.Text) : 0;
            int warunek = warunek_text.Text != "" ? int.Parse(warunek_text.Text) : 0;

            ZamienElementy.Content = Class1.ZamienElementy(tablica2, i, j, warunek);
        }
        catch (FormatException)
        {
            MessageBox.Show("Wprowadz poprawne liczby oddzielone przecinkami.", "Blad formatu", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystapil blad: {ex.Message}", "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}