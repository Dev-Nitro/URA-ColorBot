
using System.Runtime.InteropServices;

namespace URA_ColorBot.Static;

public static class Interop
{
    [DllImport("dwmapi.dll", PreserveSig = true)]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref bool attrValue, int attrSize);
}
