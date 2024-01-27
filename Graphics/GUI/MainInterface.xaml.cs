using System.Runtime.InteropServices;
using System.Windows;
using URA_ColorBot.Static;

namespace URA_ColorBot.Graphics.GUI;

public partial class MainInterface : Window
{
    public MainInterface()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Set the top window bar into a "dark" mode
        EnableNcRendering();
    }

    private void EnableNcRendering()
    {
        var value = true;

        Interop.DwmSetWindowAttribute(
            new System.Windows.Interop.WindowInteropHelper(this).Handle, 
            20, ref value, Marshal.SizeOf(value)
            );
    }
}
