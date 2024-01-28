using System.Runtime.InteropServices;
using System.Windows;
using URA_ColorBot_API;
using URA_ColorBot.Static;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace URA_ColorBot.Graphics.GUI;

public partial class MainInterface : Window
{
    private ColorBot colorBot;

    public MainInterface()
    {
        InitializeComponent();
        Loaded += MainInterface_Loaded;
    }

    private void MainInterface_Loaded(object sender, RoutedEventArgs e)
    {
        // Start ColorBot
        colorBot = new ColorBot();
        colorBot.Data += ColorBot_BotDataUpdated;

        // Start the bot in a separate thread
        Thread botThread = new(colorBot.Run);
        botThread.Start();

        // Set the top window bar into a "dark" mode
        EnableNcRendering();
    }

    private void ColorBot_BotDataUpdated(object sender, BotDataEventArgs e)
    {
        ColorData data = e.BotData;

        if (data == null || data.ColorBotVision == null)
        {
            return;
        }

        // Update the Image control with the processed image
        Dispatcher.Invoke(() =>
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                data.ColorBotVision.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            ColorImage.Source = bitmapSource;
        });
    }

    private void EnableNcRendering()
    {
        var value = true;

        Interop.DwmSetWindowAttribute(
            new System.Windows.Interop.WindowInteropHelper(this).Handle, 
            20, ref value, Marshal.SizeOf(value)
            );
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }
}
