namespace WinFormsApp1;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        webview = new Microsoft.Web.WebView2.WinForms.WebView2();
        ((System.ComponentModel.ISupportInitialize)webview).BeginInit();
        SuspendLayout();
        // 
        // webview
        // 
        webview.AllowExternalDrop = true;
        webview.CreationProperties = null;
        webview.DefaultBackgroundColor = Color.White;
        webview.Location = new Point(0, 0);
        webview.Margin = new Padding(0);
        webview.Name = "webview";
        webview.Size = new Size(0, 0);
        webview.TabIndex = 0;
        webview.ZoomFactor = 1D;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(webview);
        Name = "Form1";
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)webview).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Microsoft.Web.WebView2.WinForms.WebView2 webview;
}