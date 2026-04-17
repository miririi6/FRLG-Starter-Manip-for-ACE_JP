using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

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

                using (Mat scene = bmp.ToMat())
                {
                    // グレースケール化
                    Cv2.CvtColor(scene, scene, ColorConversionCodes.BGR2GRAY);
                    int[] results = new int[5];

                    double cellWidth = scene.Width / 5;
                    double x = 0;

                    for (int i = 0; i < 5; i++)
                    {
                        // i番目のエリアを切り出す (Rectの範囲外エラーを防ぐため幅を調整)
                        Rect cellRect = new Rect((int)Math.Max(0, x - cellWidth * 0.15), 0, (int)Math.Min(cellWidth * 1.15, scene.Width - x), scene.Height);

                        int digit;
                        using (Mat cell = new Mat(scene, cellRect))
                        {
                            // そのエリアで一番似ている数字を1つ特定する
                            digit = GetBestDigit(cell, threshold);
                            results[i] = digit;
                        }
                        if (digit == 1)
                        {
                            x += cellWidth * 8.0 / 9.0;
                        }
                        else
                        {
                            x += cellWidth;
                        }
                    }

                    // 1つでも認識失敗(-1)があれば、不正な結果として空を返す
                    if (results.Any(r => r == -1)) return new int[0];
                    return results;
                }
            }
        }
        private static int GetBestDigit(Mat cell, double threshold)
        {
            int bestDigit = -1;
            double bestScore = -1.0;

            for (int i = 0; i <= 9; i++)
            {
                string templatePath = $"NumberImages/{i}.png";
                if (!File.Exists(templatePath)) continue;

                using var template = Cv2.ImRead(templatePath, ImreadModes.Grayscale);
                using var res = new Mat();

                // テンプレートの方が大きい場合はエラーになるのでリサイズかスキップが必要
                if (template.Width > cell.Width || template.Height > cell.Height) continue;

                Cv2.MatchTemplate(cell, template, res, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(res, out _, out double maxVal, out _, out _);

                // 「しきい値を超えている」かつ「これまでのどの数字よりも似ている」場合
                if (maxVal > threshold && maxVal > bestScore)
                {
                    bestScore = maxVal;
                    bestDigit = i;
                }
            }

            return bestDigit;
        }
    }
}

