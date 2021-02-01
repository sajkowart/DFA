using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DFA
{
    class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE pRawInputDevices, uint uiNumDevices, uint cbSize);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

    }
}
