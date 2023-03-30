namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Resize += Form1_Resize;

        webview.Size = this.ClientSize;
        webview.Source = new Uri("http://localhost:5000/Home");
    }

    private void Form1_Resize(object? sender, EventArgs e)
    {
        webview.Size = this.ClientSize;
    }
}