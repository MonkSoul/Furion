using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        webview.Source = new Uri("http://localhost:5000/Home");
    }
}