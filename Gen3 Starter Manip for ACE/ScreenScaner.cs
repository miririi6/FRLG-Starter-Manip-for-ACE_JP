using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Gen3_Starter_Manip_for_ACE
{
    public static class ScreenScaner
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public struct RECT { public int Left, Top, Right, Bottom; }
        public struct WindowType { public IntPtr Hwnd; public string Title; }
        public static List<WindowType> windowList = new List<WindowType>();
        public static WindowType selectedWindow;
        public static bool isScanning = false;
        
        public static Bitmap CaptureScreen(IntPtr targetHwnd, Rectangle relativeRect)
        {
            // 1. ターゲットウィンドウの現在の位置を取得
            if (!GetWindowRect(targetHwnd, out RECT windowRect)) return null;

            // 2. ウィンドウの左上に相対座標を足して、デスクトップ上の「真の座標」を出す
            int absoluteX = windowRect.Left + relativeRect.X;
            int absoluteY = windowRect.Top + relativeRect.Y;
            Bitmap bmp = new Bitmap(relativeRect.Width, relativeRect.Height);
            
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(absoluteX, absoluteY, 0, 0, relativeRect.Size);
            }
            
            return bmp;
        }
        public static int Scan(IntPtr targetHwnd, Rectangle relativeRect, double threshold)
        {
            // 1. ターゲットウィンドウの現在の位置を取得
            if (!GetWindowRect(targetHwnd, out RECT windowRect)) return -1;

            // 2. ウィンドウの左上に相対座標を足して、デスクトップ上の「真の座標」を出す
            int absoluteX = windowRect.Left + relativeRect.X;
            int absoluteY = windowRect.Top + relativeRect.Y;

            Rectangle captureArea = new Rectangle(absoluteX, absoluteY, relativeRect.Width, relativeRect.Height);

            // 3. その範囲をキャプチャして解析へ
            var digits = ExecuteScan(captureArea, threshold);
            if (digits.Length == 0) return -2; // 数字が見つからなかった場合は -2 を返す
            return int.Parse(string.Join("", digits)); // 数字を連結して整数に変換
        }
        private static int[] ExecuteScan(Rectangle selectionRect, double threshold)
        {
            using (Bitmap bmp = new Bitmap(selectionRect.Width, selectionRect.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(selectionRect.X, selectionRect.Y, 0, 0, selectionRect.Size);
                }

                // Bitmapから直接Matに変換してScanImageに渡す
                using (Mat scene = bmp.ToMat())
                {
                    Cv2.ImWrite("check_area.png", scene);
                    // グレースケール化
                    Cv2.CvtColor(scene, scene, ColorConversionCodes.BGR2GRAY);
                    return ScanImage(scene, threshold);
                }
            }
        }
        private static int[] ScanImage(Mat scene, double threshold)
        {
            var foundDigits = new List<(int Digit, int X)>(); // (見つかった数字, X座標)

            // 2. 0～9のテンプレートをループして探す
            for (int i = 0; i <= 9; i++)
            {
                string templatePath = $"NumberImages/{i}.png"; // テンプレート画像のパス
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show($"テンプレート画像が見つかりません: {templatePath}");
                    return new int[0];
                }
                using var template = Cv2.ImRead(templatePath, ImreadModes.Grayscale);
                using var res = scene.MatchTemplate(template, TemplateMatchModes.CCoeffNormed);

                // 一致度がしきい値以上の場所をすべて探す
                Cv2.MinMaxLoc(res, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal > threshold)
                {
                    // 見つかった場所を記録
                    foundDigits.Add((i, maxLoc.X));

                    // 【重要】見つけた場所を黒（0）で塗りつぶして、二度と同じ場所で見つからないようにする
                    // これにより、同じ数字が複数並んでいても順番に見つけられる
                    Rect rect = new Rect(maxLoc.X, maxLoc.Y, template.Width, template.Height);
                    scene.Rectangle(rect, Scalar.Black, -1); // -1は「塗りつぶし」

                    // 同じ数字がまだあるかもしれないので、i をデクリメントしてもう一度同じ数字で探す
                    i--;
                }
            }
            if (foundDigits.Count != 5) return new int[0]; // 5文字見つからなかった場合は空の配列を返す

            // 3. X座標が小さい順（左から右）に並び替える
            return foundDigits.OrderBy(d => d.X).Select(d => d.Digit).ToArray();
        }
    }
}

