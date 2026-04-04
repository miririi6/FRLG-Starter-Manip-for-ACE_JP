using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class ScanRangeWindow : Form
    {
        public ScanRangeWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(EditSettings.rangeStartRECT.Left + 100, EditSettings.rangeStartRECT.Top + 100);
        }
        private const int WM_NCHITTEST = 0x84;
        private const int HTCAPTION = 0x2;
        private const int HTBOTTOMRIGHT = 17; // 右下
        private const int GRSZ = 10; // 判定の幅（ピクセル）

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // フォームのどこかを触ったら「それはタイトルバーですよ」とWindowsに嘘をつく
            if (m.Msg == WM_NCHITTEST)
            {
                Point pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                // 右下隅の10x10ピクセルの範囲内なら、リサイズ中として扱う
                if (pos.X >= this.ClientSize.Width - GRSZ && pos.Y >= this.ClientSize.Height - GRSZ)
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                else
                    m.Result = (IntPtr)HTCAPTION;
            }
        }

        private void ScanRangeWindow_Paint(object sender, PaintEventArgs e)
        {
            // 現在の表示領域の幅と高さを取得
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            using (Pen guidePen = new Pen(Color.Red, 1))
            {
                // 点線にする（よりガイド線っぽくなる）
                guidePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                // 5分割なので、境界線は4本
                for (int i = 1; i <= 4; i++)
                {
                    int x = (w * i) / 5;
                    e.Graphics.DrawLine(guidePen, x, 0, x, h);
                }
            }

            // 外枠も引いておくと、どこが認識範囲か分かりやすい
            e.Graphics.DrawRectangle(Pens.Red, 0, 0, w - 1, h - 1);
        }

        private void ScanRangeWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
