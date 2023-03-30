namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webview.Source = new Uri("http://localhost:5000/Home");
        }
    }
}