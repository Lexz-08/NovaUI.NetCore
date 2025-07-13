using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace NovaUI.NetCore.Helpers.LibMain
{
    internal struct Win32
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(nint hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern nint LoadCursorFromFile(string lpFilename);

        [DllImport("gdi32.dll")]
        public static extern nint CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(nint hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(nint hWnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        public const int CS_DROPSHADOW = 0x00020000;

        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_RESTORE = 0xF120;

        public enum RegistryCursor
        {
            AppStarting,
            Arrow,
            Crosshair,
            Hand,
            Help,
            IBeam,
            No,
            SizeAll,
            SizeNESW,
            SizeNS,
            SizeNWSE,
            SizeWE,
            UpArrow,
            Wait
        }

        private static readonly Dictionary<RegistryCursor, (Cursor Cursor, string? Path)> cursors = [];

        private static T GetMember<T>(Type t, string name)
        {
            System.Reflection.PropertyInfo? prop = t.GetType().GetProperty(name) ?? throw new NullReferenceException($"Property '{name}' of '{t.Name}' does not exist.");

            object? val = prop.GetValue(null) ?? throw new NullReferenceException($"Property '{name}' of '{t.Name}' is not static.");
            return (T)val;
        }

        public static void GetRegistryCursor(RegistryCursor cursor, Control control)
        {
            using (RegistryKey curKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Cursors")!)
            {
                string? cursorPath = (string?)curKey.GetValue(cursor.ToString());
                bool flag = cursors.TryGetValue(cursor, out (Cursor Cursor, string? Path) pair);


				if (!flag || pair.Path != cursorPath)
                {
                    if (flag) pair.Cursor.Dispose();

                    nint handle = !string.IsNullOrEmpty(cursorPath)
                        ? LoadCursorFromFile(cursorPath)
                        : GetMember<Cursor>(typeof(Cursor), cursor.ToString()).Handle;

                    Cursor newCur = new(handle);
                    cursors[cursor] = (newCur, null);
                    control.Cursor = newCur;
                }
                else control.Cursor = pair.Cursor;
            }
        }

        public static bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);

                return enabled == 1;
            }

            return false;
        }

        public static string NumberToHex(int number, bool caps = true) => number.ToString(caps ? "X" : "x");
    }
}
