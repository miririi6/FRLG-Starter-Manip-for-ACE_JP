using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Gen3_Starter_Manip_for_ACE
{
    public static class NumberSender
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumChildProc lpEnumFunc, IntPtr lParam);
        public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
        
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const uint WM_SETTEXT = 0x000C;
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;
        public static void SendTextToTargetEdit(string parentHwndTitle, int index, string text, int targetX, int targetY)
        {
            IntPtr parentHwnd = FindWindow(null, parentHwndTitle);
            if (parentHwnd == IntPtr.Zero)
            {
                return;
            }

            IntPtr inputHwnd = FindEditByLocation(parentHwnd, targetX, targetY);
            if (inputHwnd != IntPtr.Zero)
            {
                SendMessage(inputHwnd, WM_SETTEXT, IntPtr.Zero, text);
                SetForegroundWindow(parentHwnd);
                SetFocus(inputHwnd);
            }
        }

        public static IntPtr FindEditByLocation(IntPtr parentHwnd, int targetX, int targetY)
        {
            IntPtr foundHandle = IntPtr.Zero;

            EnumChildWindows(parentHwnd, (hWnd, lParam) =>
            {
                // 1. クラス名を確認
                StringBuilder className = new StringBuilder(256);
                GetClassName(hWnd, className, className.Capacity);
                if (!className.ToString().Contains("WindowsForms10.EDIT")) return true;

                // 2. 親ウィンドウ内での相対座標（Client座標）を取得
                RECT rect = new RECT();
                GetWindowRect(hWnd, out rect);
                Point clientPoint = new Point(rect.Left, rect.Top);
                ScreenToClient(parentHwnd, ref clientPoint);

                // 3. 目的の座標に近いか判定
                if (Math.Abs(clientPoint.X - targetX) < 5 && Math.Abs(clientPoint.Y - targetY) < 5)
                {
                    foundHandle = hWnd;
                    return false; // 発見
                }
                return true;
            }, IntPtr.Zero);

            return foundHandle;
        }
    }
}
