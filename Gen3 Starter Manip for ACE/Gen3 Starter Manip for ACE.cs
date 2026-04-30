using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static Gen3_Starter_Manip_for_ACE.ConfigData;
using static Gen3_Starter_Manip_for_ACE.ConfigUtils;
using static Gen3_Starter_Manip_for_ACE.Constants;
using static Gen3_Starter_Manip_for_ACE.MainForm;
using static Gen3_Starter_Manip_for_ACE.SearchEngine;
using static Gen3_Starter_Manip_for_ACE.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        int initWidth;
        private void Form1_Load(object sender, EventArgs e)
        {
            initWidth = this.Width;
            ConfigUtils.loadConfigData("config.json");
            setConfigData();
            applyTopMostConfig();
            setScanHotKey();
            Resources.loadResources("words.csv", "poke_words.csv");
            string filePath;
            if (ConfigData.Instance.version == RomVersionType.FireRed)
                filePath = "corrupted_pokemon_FR.csv";
            else
                filePath = "corrupted_pokemon_LG.csv";
            Resources.loadCorruptedPokemonData(filePath);
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            SoftVersionLabel.Text = $"バージョン: {version.Major}.{version.Minor}";
            if (!string.IsNullOrEmpty(ConfigData.Instance.scanWindowTitle))
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.MainWindowTitle.Contains(ConfigData.Instance.scanWindowTitle))
                    {
                        ScreenScaner.windowList.Add(new ScreenScaner.WindowType { Hwnd = p.MainWindowHandle, Title = p.MainWindowTitle });
                        SelectWindowList.Items.Add(p.MainWindowTitle);
                        SelectWindowList.SelectedIndex = 0;
                    }
                }
            }
            if (ScreenScaner.selectedWindow.Hwnd == IntPtr.Zero)
            {
                {
                    SelectWindowList.Items.Add("ゲーム画面ウィンドウを選択してください");
                    SelectWindowList.SelectedIndex = 0;
                }
            }
            else
            {
                ScanStartButton.Enabled = true;
            }
            CalcList.RowTemplate.Height = 16;
            TIDText.Focus();
        }
        public void NoAceModeView()
        {
            WordEXPList.Visible = false;
            this.Width = (int)(initWidth * 684 / 958);
            MinExpText1.Enabled = false;
            MinExpText2.Enabled = false;
            MaxExpText.Enabled = false;
        }
        public void AceModeView()
        {
            WordEXPList.Visible = true;
            this.Width = initWidth;
            MinExpText1.Enabled = true;
            MinExpText2.Enabled = true;
            MaxExpText.Enabled = true;
        }

        bool ClickedCalcButton = false;
        public void CalcButton_Click(object sender, EventArgs e)
        {
            ClickedCalcButton = true;
            SendToTimer = true;
            ushort tid;
            if (!ushort.TryParse(TIDText.Text, out tid))
            {
                tid = 0;
            }
            var results = SearchEngine.SearchPokemons(tid, ConfigData.Instance.isSearchForACE);

            CalcList.DataSource = results;

            CalcList.Columns["フレーム"].Width = 50;
            CalcList.Columns["時間"].Width = 54;
            CalcList.Columns["性格"].Width = 56;
            CalcList.Columns["性格値"].Width = 65;
            CalcList.Columns["性格値"].DefaultCellStyle.Format = "X8";
            CalcList.Columns["H"].Width = 24;
            CalcList.Columns["A"].Width = 24;
            CalcList.Columns["B"].Width = 24;
            CalcList.Columns["C"].Width = 24;
            CalcList.Columns["D"].Width = 24;
            CalcList.Columns["S"].Width = 24;
            CalcList.Columns["性別"].Width = 37;
            CalcList.Columns["経験値"].Width = 40;
            if (ConfigData.Instance.isSearchForACE)
            {
                CalcList.Columns["経験値"].Visible = true;
                CalcList.Columns[CalcList.ColumnCount - 1].MinimumWidth = 40;
                CalcList.Columns[CalcList.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                CalcList.Columns["経験値"].Visible = false;
                CalcList.Columns[CalcList.ColumnCount - 2].MinimumWidth = 40;
                CalcList.Columns[CalcList.ColumnCount - 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (CalcList.Rows.Count > 0)
            {
                showStatus(0);
                if (ConfigData.Instance.isSearchForACE)
                {
                    uint pid = (uint)CalcList.Rows[0].Cells[2].Value;
                    var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                    WordEXPList.DataSource = wordExpData;
                    WordEXPList.Columns["経験値"].Width = 50;
                    WordEXPList.Columns["ワード3"].Width = 70;
                    WordEXPList.Columns["ワード5"].Width = 70;
                    WordEXPList.Columns["パターン"].Width = 50;
                    WordEXPList.Columns[WordEXPList.ColumnCount - 1].MinimumWidth = 50;
                    WordEXPList.Columns[WordEXPList.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    if (isCountHelperOpen && WordEXPList.Rows.Count > 0)
                    {
                        helperForm.SetTargetExp((int)WordEXPList.Rows[0].Cells[0].Value);
                    }
                }
                CallSendToTimer();
            }
            if (results.Count == 0)
            {
                WordEXPList.DataSource = null;
            }
        }

        public void CallSendToTimer()
        {
            if (ConfigData.Instance.isAutoConnectTimer)
            {
                NumberSender.SendTextToTargetEdit(ConfigData.Instance.FlowTimerWindowName, 0, CalcList.Rows[CalcList.CurrentCell.RowIndex].Cells[0].Value.ToString(), ConfigData.Instance.FlowTimerOffsetX, ConfigData.Instance.FlowTimerOffsetY);
            }
        }

        public void CalcStartCall()
        {
            if (ClickedCalcButton)
            {
                CalcButton_Click(null, null);
            }
        }

        private void TIDText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CalcButton_Click(sender, e);
                return;
            }
            if (e.Control)
                return;
            if (!isNumberOrActionKey(e.KeyCode))
                e.SuppressKeyPress = true;
        }
        private void Frames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CalcButton_Click(sender, e);
                return;
            }
            if (e.Control)
                return;
            if (!isNumberOrActionKey(e.KeyCode))
                e.SuppressKeyPress = true;
        }
        private void IVReq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CalcButton_Click(sender, e);
                return;
            }
            if (e.Control)
                return;
            if (!isNumberOrActionKey(e.KeyCode))
                e.SuppressKeyPress = true;
        }

        bool SendToTimer = true;
        private void CalcList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            showStatus(e.RowIndex);
            if (ConfigData.Instance.isSearchForACE)
            {
                ushort tid = ushort.TryParse(TIDText.Text, out tid) ? tid : (ushort)0;
                uint pid = (uint)CalcList.Rows[e.RowIndex].Cells[2].Value;
                var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                WordEXPList.DataSource = wordExpData;
                if (isCountHelperOpen && WordEXPList.Rows.Count > 0)
                {
                    helperForm.SetTargetExp((int)WordEXPList.Rows[0].Cells[0].Value);
                }
            }
            if (SendToTimer)
                CallSendToTimer();
        }
        /*
        public void RefreshWordExpList()
        {
            if (CalcList.CurrentCell != null)
            {
                ushort tid = ushort.TryParse(TIDText.Text, out tid) ? tid : (ushort)0;
                uint pid = (uint)CalcList.Rows[CalcList.CurrentCell.RowIndex].Cells[3].Value;
                var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                WordEXPList.DataSource = wordExpData;
            }
        }
        */
        private void SerchAroundFramesButton_Click(object sender, EventArgs e)
        {
            if (CalcList.CurrentCell == null) return;
            int rowIndex = CalcList.CurrentCell.RowIndex;
            int CurrentFrame = (int)CalcList.Rows[rowIndex].Cells[0].Value;
            ushort tid;
            if (!ushort.TryParse(TIDText.Text, out tid))
            {
                tid = 0;
            }

            var results = SearchEngine.SerchAroundFrames(tid, CurrentFrame, ConfigData.Instance.isSearchForACE);

            CalcList.DataSource = results;
            int focusIndex = 49;
            int displayedRowCount = CalcList.DisplayedRowCount(false);
            CalcList.CurrentCell = CalcList.Rows[focusIndex].Cells[0];
            CalcList.FirstDisplayedScrollingRowIndex = focusIndex - displayedRowCount / 2;
            SendToTimer = false;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            // sender を TextBox にキャストして汎用的に使う
            if (sender is System.Windows.Forms.TextBox tb)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    tb.SelectAll();
                });
            }
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            // sender を TextBox にキャストして汎用的に使う
            if (sender is System.Windows.Forms.TextBox tb)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    tb.SelectAll();
                });
            }
        }

        public void SpiecesChanged()
        {
            if (CalcList.CurrentCell == null) return;
            showStatus(CalcList.CurrentCell.RowIndex);
            if (ConfigData.Instance.isSearchForACE)
            {
                ushort tid = ushort.TryParse(TIDText.Text, out tid) ? tid : (ushort)0;
                uint pid = (uint)CalcList.Rows[CalcList.CurrentCell.RowIndex].Cells[2].Value;
                var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                WordEXPList.DataSource = wordExpData;
            }
        }

        private void LoadConfigsFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "設定ファイルを開く";
                ofd.Filter = "JSONファイル(*.json)|*.json|すべてのファイル(*.*)|*.*";
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    loadConfigData(filePath);
                }
            }
        }
        private void SaveConfigs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "設定ファイルを保存";
                sfd.FileName = "config.json";
                sfd.Filter = "JSONファイル(*.json)|*.json|すべてのファイル(*.*)|*.*";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    saveConfigData(filePath);
                }
            }
        }
        #region IV要求値のテキストボックスの変更イベント
        private void Hreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Hreq.Text, out int hpReq))
            {
                if (hpReq < 0)
                {
                    hpReq = 0;
                    Hreq.Text = "0";
                }
                else if (hpReq > 31)
                {
                    hpReq = 31;
                    Hreq.Text = "31";
                }
                ConfigData.Instance.requiredHIV = hpReq;
            }
            else
            {
                ConfigData.Instance.requiredHIV = 0;
                Hreq.Text = "";
            }
        }

        private void ADreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ADreq.Text, out int atkDReq))
            {
                if (atkDReq < 0)
                {
                    atkDReq = 0;
                    ADreq.Text = "0";
                }
                else if (atkDReq > 31)
                {
                    atkDReq = 31;
                    ADreq.Text = "31";
                }
                ConfigData.Instance.requiredAIV[0] = atkDReq;
            }
            else
            {
                ConfigData.Instance.requiredAIV[0] = 0;
                ADreq.Text = "";
            }
        }

        private void Areq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Areq.Text, out int atkNReq))
            {
                if (atkNReq < 0)
                {
                    atkNReq = 0;
                    Areq.Text = "0";
                }
                else if (atkNReq > 31)
                {
                    atkNReq = 31;
                    Areq.Text = "31";
                }
                ConfigData.Instance.requiredAIV[1] = atkNReq;
            }
            else
            {
                ConfigData.Instance.requiredAIV[1] = 0;
                Areq.Text = "";
            }
        }

        private void AUreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(AUreq.Text, out int atkPReq))
            {
                if (atkPReq < 0)
                {
                    atkPReq = 0;
                    AUreq.Text = "0";
                }
                else if (atkPReq > 31)
                {
                    atkPReq = 31;
                    AUreq.Text = "31";
                }
                ConfigData.Instance.requiredAIV[2] = atkPReq;
            }
            else
            {
                ConfigData.Instance.requiredAIV[2] = 0;
                AUreq.Text = "";
            }
        }

        private void BDreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BDreq.Text, out int defDReq))
            {
                if (defDReq < 0)
                {
                    defDReq = 0;
                    BDreq.Text = "0";
                }
                else if (defDReq > 31)
                {
                    defDReq = 31;
                    BDreq.Text = "31";
                }
                ConfigData.Instance.requiredBIV[0] = defDReq;
            }
            else
            {
                ConfigData.Instance.requiredBIV[0] = 0;
                BDreq.Text = "";
            }
        }

        private void Breq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Breq.Text, out int defNReq))
            {
                if (defNReq < 0)
                {
                    defNReq = 0;
                    Breq.Text = "0";
                }
                else if (defNReq > 31)
                {
                    defNReq = 31;
                    Breq.Text = "31";
                }
                ConfigData.Instance.requiredBIV[1] = defNReq;
            }
            else
            {
                ConfigData.Instance.requiredBIV[1] = 0;
                Breq.Text = "";
            }
        }

        private void BUreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BUreq.Text, out int defPReq))
            {
                if (defPReq < 0)
                {
                    defPReq = 0;
                    BUreq.Text = "0";
                }
                else if (defPReq > 31)
                {
                    defPReq = 31;
                    BUreq.Text = "31";
                }
                ConfigData.Instance.requiredBIV[2] = defPReq;
            }
            else
            {
                ConfigData.Instance.requiredBIV[2] = 0;
                BUreq.Text = "";
            }
        }

        private void CDreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(CDreq.Text, out int spADReq))
            {
                if (spADReq < 0)
                {
                    spADReq = 0;
                    CDreq.Text = "0";
                }
                else if (spADReq > 31)
                {
                    spADReq = 31;
                    CDreq.Text = "31";
                }
                ConfigData.Instance.requiredCIV[0] = spADReq;
            }
            else
            {
                ConfigData.Instance.requiredCIV[0] = 0;
                CDreq.Text = "";
            }
        }

        private void Creq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Creq.Text, out int spANReq))
            {
                if (spANReq < 0)
                {
                    spANReq = 0;
                    Creq.Text = "0";
                }
                else if (spANReq > 31)
                {
                    spANReq = 31;
                    Creq.Text = "31";
                }
                ConfigData.Instance.requiredCIV[1] = spANReq;
            }
            else
            {
                ConfigData.Instance.requiredCIV[1] = 0;
                Creq.Text = "";
            }
        }

        private void CUreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(CUreq.Text, out int spAPReq))
            {
                if (spAPReq < 0)
                {
                    spAPReq = 0;
                    CUreq.Text = "0";
                }
                else if (spAPReq > 31)
                {
                    spAPReq = 31;
                    CUreq.Text = "31";
                }
                ConfigData.Instance.requiredCIV[2] = spAPReq;
            }
            else
            {
                ConfigData.Instance.requiredCIV[2] = 0;
                CUreq.Text = "";
            }
        }

        private void DDreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(DDreq.Text, out int spDDReq))
            {
                if (spDDReq < 0)
                {
                    spDDReq = 0;
                    DDreq.Text = "0";
                }
                else if (spDDReq > 31)
                {
                    spDDReq = 31;
                    DDreq.Text = "31";
                }
                ConfigData.Instance.requiredDIV[0] = spDDReq;
            }
            else
            {
                ConfigData.Instance.requiredDIV[0] = 0;
                DDreq.Text = "";
            }
        }

        private void Dreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Dreq.Text, out int spDNReq))
            {
                if (spDNReq < 0)
                {
                    spDNReq = 0;
                    Dreq.Text = "0";
                }
                else if (spDNReq > 31)
                {
                    spDNReq = 31;
                    Dreq.Text = "31";
                }
                ConfigData.Instance.requiredDIV[1] = spDNReq;
            }
            else
            {
                ConfigData.Instance.requiredDIV[1] = 0;
                Dreq.Text = "";
            }
        }

        private void DUreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(DUreq.Text, out int spDPReq))
            {
                if (spDPReq < 0)
                {
                    spDPReq = 0;
                    DUreq.Text = "0";
                }
                else if (spDPReq > 31)
                {
                    spDPReq = 31;
                    DUreq.Text = "31";
                }
                ConfigData.Instance.requiredDIV[2] = spDPReq;
            }
            else
            {
                ConfigData.Instance.requiredDIV[2] = 0;
                DUreq.Text = "";
            }
        }

        private void SDreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SDreq.Text, out int spdDReq))
            {
                if (spdDReq < 0)
                {
                    spdDReq = 0;
                    SDreq.Text = "0";
                }
                else if (spdDReq > 31)
                {
                    spdDReq = 31;
                    SDreq.Text = "31";
                }
                ConfigData.Instance.requiredSIV[0] = spdDReq;
            }
            else
            {
                ConfigData.Instance.requiredSIV[0] = 0;
                SDreq.Text = "";
            }
        }

        private void Sreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Sreq.Text, out int spdNReq))
            {
                if (spdNReq < 0)
                {
                    spdNReq = 0;
                    Sreq.Text = "0";
                }
                else if (spdNReq > 31)
                {
                    spdNReq = 31;
                    Sreq.Text = "31";
                }
                ConfigData.Instance.requiredSIV[1] = spdNReq;
            }
            else
            {
                ConfigData.Instance.requiredSIV[1] = 0;
                Sreq.Text = "";
            }
        }

        private void SUreq_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SUreq.Text, out int spdPReq))
            {
                if (spdPReq < 0)
                {
                    spdPReq = 0;
                    SUreq.Text = "0";
                }
                else if (spdPReq > 31)
                {
                    spdPReq = 31;
                    SUreq.Text = "31";
                }
                ConfigData.Instance.requiredSIV[2] = spdPReq;
            }
            else
            {
                ConfigData.Instance.requiredSIV[2] = 0;
                SUreq.Text = "";
            }
        }
        #endregion

        private void showStatus(int rowIndex)
        {
            Types.NatureType nature = (Types.NatureType)CalcList.Rows[rowIndex].Cells[3].Value;
            int ivH = (int)CalcList.Rows[rowIndex].Cells[4].Value;
            int ivA = (int)CalcList.Rows[rowIndex].Cells[5].Value;
            int ivB = (int)CalcList.Rows[rowIndex].Cells[6].Value;
            int ivC = (int)CalcList.Rows[rowIndex].Cells[7].Value;
            int ivD = (int)CalcList.Rows[rowIndex].Cells[8].Value;
            int ivS = (int)CalcList.Rows[rowIndex].Cells[9].Value;

            IVsHNum.Text = ivH.ToString();
            IVsANum.Text = ivA.ToString();
            IVsBNum.Text = ivB.ToString();
            IVsCNum.Text = ivC.ToString();
            IVsDNum.Text = ivD.ToString();
            IVsSNum.Text = ivS.ToString();

            var effect = Constants.natureEffects[(int)nature];
            var upStatus = effect.UpStatus;
            var downStatus = effect.DownStatus;
            int[] lv5 = getStatus(nature, ivH, ivA, ivB, ivC, ivD, ivS, 5, upStatus, downStatus);
            int[] lv6 = getStatus(nature, ivH, ivA, ivB, ivC, ivD, ivS, 6, upStatus, downStatus);

            StatusHNum5.Text = lv5[0].ToString();
            StatusANum5.Text = lv5[1].ToString();
            StatusBNum5.Text = lv5[2].ToString();
            StatusCNum5.Text = lv5[3].ToString();
            StatusDNum5.Text = lv5[4].ToString();
            StatusSNum5.Text = lv5[5].ToString();
            StatusHNum6.Text = lv6[0].ToString();
            StatusANum6.Text = lv6[1].ToString();
            StatusBNum6.Text = lv6[2].ToString();
            StatusCNum6.Text = lv6[3].ToString();
            StatusDNum6.Text = lv6[4].ToString();
            StatusSNum6.Text = lv6[5].ToString();

            if (upStatus == StatusType.A)
            {
                IVsANum.ForeColor = Color.Salmon;
                StatusANum5.ForeColor = Color.Salmon;
                StatusANum6.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.A)
            {
                IVsANum.ForeColor = Color.Cyan;
                StatusANum5.ForeColor = Color.Cyan;
                StatusANum6.ForeColor = Color.Cyan;
            }
            else
            {
                IVsANum.ForeColor = Color.White;
                StatusANum5.ForeColor = Color.White;
                StatusANum6.ForeColor = Color.White;
            }
            if (upStatus == StatusType.B)
            {
                IVsBNum.ForeColor = Color.Salmon;
                StatusBNum5.ForeColor = Color.Salmon;
                StatusBNum6.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.B)
            {
                IVsBNum.ForeColor = Color.Cyan;
                StatusBNum5.ForeColor = Color.Cyan;
                StatusBNum6.ForeColor = Color.Cyan;
            }
            else
            {
                IVsBNum.ForeColor = Color.White;
                StatusBNum5.ForeColor = Color.White;
                StatusBNum6.ForeColor = Color.White;
            }
            if (upStatus == StatusType.C)
            {
                IVsCNum.ForeColor = Color.Salmon;
                StatusCNum5.ForeColor = Color.Salmon;
                StatusCNum6.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.C)
            {
                IVsCNum.ForeColor = Color.Cyan;
                StatusCNum5.ForeColor = Color.Cyan;
                StatusCNum6.ForeColor = Color.Cyan;
            }
            else
            {
                IVsCNum.ForeColor = Color.White;
                StatusCNum5.ForeColor = Color.White;
                StatusCNum6.ForeColor = Color.White;
            }
            if (upStatus == StatusType.D)
            {
                IVsDNum.ForeColor = Color.Salmon;
                StatusDNum5.ForeColor = Color.Salmon;
                StatusDNum6.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.D)
            {
                IVsDNum.ForeColor = Color.Cyan;
                StatusDNum5.ForeColor = Color.Cyan;
                StatusDNum6.ForeColor = Color.Cyan;
            }
            else
            {
                IVsDNum.ForeColor = Color.White;
                StatusDNum5.ForeColor = Color.White;
                StatusDNum6.ForeColor = Color.White;
            }
            if (upStatus == StatusType.S)
            {
                IVsSNum.ForeColor = Color.Salmon;
                StatusSNum5.ForeColor = Color.Salmon;
                StatusSNum6.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.S)
            {
                IVsSNum.ForeColor = Color.Cyan;
                StatusSNum5.ForeColor = Color.Cyan;
                StatusSNum6.ForeColor = Color.Cyan;
            }
            else
            {
                IVsSNum.ForeColor = Color.White;
                StatusSNum5.ForeColor = Color.White;
                StatusSNum6.ForeColor = Color.White;
            }
        }
        private void setConfigData()
        {
            // コンフィグデータの内容をフォームの各コントロールに反映させる
            var config = ConfigData.Instance;
            Hreq.Text = config.requiredHIV.ToString();
            ADreq.Text = config.requiredAIV[0].ToString();
            Areq.Text = config.requiredAIV[1].ToString();
            AUreq.Text = config.requiredAIV[2].ToString();
            BDreq.Text = config.requiredBIV[0].ToString();
            Breq.Text = config.requiredBIV[1].ToString();
            BUreq.Text = config.requiredBIV[2].ToString();
            CDreq.Text = config.requiredCIV[0].ToString();
            Creq.Text = config.requiredCIV[1].ToString();
            CUreq.Text = config.requiredCIV[2].ToString();
            DDreq.Text = config.requiredDIV[0].ToString();
            Dreq.Text = config.requiredDIV[1].ToString();
            DUreq.Text = config.requiredDIV[2].ToString();
            SDreq.Text = config.requiredSIV[0].ToString();
            Sreq.Text = config.requiredSIV[1].ToString();
            SUreq.Text = config.requiredSIV[2].ToString();
            Hardy.Checked = config.checkedNatures.Contains(NatureType.Hardy);
            Lonely.Checked = config.checkedNatures.Contains(NatureType.Lonely);
            Brave.Checked = config.checkedNatures.Contains(NatureType.Brave);
            Adamant.Checked = config.checkedNatures.Contains(NatureType.Adamant);
            Naughty.Checked = config.checkedNatures.Contains(NatureType.Naughty);
            Bold.Checked = config.checkedNatures.Contains(NatureType.Bold);
            Docile.Checked = config.checkedNatures.Contains(NatureType.Docile);
            Relaxed.Checked = config.checkedNatures.Contains(NatureType.Relaxed);
            Impish.Checked = config.checkedNatures.Contains(NatureType.Impish);
            Lax.Checked = config.checkedNatures.Contains(NatureType.Lax);
            Timid.Checked = config.checkedNatures.Contains(NatureType.Timid);
            Hasty.Checked = config.checkedNatures.Contains(NatureType.Hasty);
            Serious.Checked = config.checkedNatures.Contains(NatureType.Serious);
            Jolly.Checked = config.checkedNatures.Contains(NatureType.Jolly);
            Naive.Checked = config.checkedNatures.Contains(NatureType.Naive);
            Modest.Checked = config.checkedNatures.Contains(NatureType.Modest);
            Mild.Checked = config.checkedNatures.Contains(NatureType.Mild);
            Quiet.Checked = config.checkedNatures.Contains(NatureType.Quiet);
            Bashful.Checked = config.checkedNatures.Contains(NatureType.Bashful);
            Rash.Checked = config.checkedNatures.Contains(NatureType.Rash);
            Calm.Checked = config.checkedNatures.Contains(NatureType.Calm);
            Gentle.Checked = config.checkedNatures.Contains(NatureType.Gentle);
            Sassy.Checked = config.checkedNatures.Contains(NatureType.Sassy);
            Careful.Checked = config.checkedNatures.Contains(NatureType.Careful);
            Quirky.Checked = config.checkedNatures.Contains(NatureType.Quirky);
            MinFrame.Text = config.minFrame.ToString();
            MaxFrame.Text = config.maxFrame.ToString();
            MinExpText1.Text = config.minExp.ToString();
            MinExpText2.Text = config.minExp2.ToString();
            MaxExpText.Text = config.maxExp.ToString();
            this.TopMost = config.TopMost;
            // 性格のチェックボックスを更新
            foreach (var nature in Constants.natureEffects)
            {
                var checkBoxName = nature.Name.ToString() + "CheckBox";
                var checkBox = this.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;
                if (checkBox != null)
                {
                    checkBox.Checked = config.checkedNatures.Contains(nature.Name);
                }
            }
            if (!config.isSearchForACE)
                NoAceModeView();
        }
        public void applyTopMostConfig()
        {
            this.TopMost = ConfigData.Instance.TopMost;
        }
        Keys scanHotKey = Keys.None;
        public void setScanHotKey()
        {
            scanHotKey = (Keys)Enum.Parse(typeof(Keys), ConfigData.Instance.scanHotKeyStr, true);
        }
        private bool isNumberOrActionKey(Keys key)
        {
            bool isNumber = (key >= Keys.D0 && key <= Keys.D9) ||
                            (key >= Keys.NumPad0 && key <= Keys.NumPad9);
            bool isActionKey = key == Keys.Back ||
                               key == Keys.Delete ||
                               key == Keys.Left ||
                               key == Keys.Right ||
                               key == Keys.Tab ||
                               key == Keys.Enter;
            return isNumber || isActionKey;
        }
        private void Natures_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox cb)
            {
                var natureName = cb.Name;
                if (Enum.TryParse(natureName, out NatureType nature))
                {
                    if (cb.Checked)
                    {
                        if (!ConfigData.Instance.checkedNatures.Contains(nature))
                        {
                            ConfigData.Instance.checkedNatures.Add(nature);
                        }
                    }
                    else
                    {
                        ConfigData.Instance.checkedNatures.Remove(nature);
                    }
                }
                else
                {
                    MessageBox.Show("性格のチェックボックスの名前から性格を特定できませんでした。");
                }
            }
        }

        private void ClearTIDButton_Click(object sender, EventArgs e)
        {
            TIDText.Text = "";
            TIDText.Focus();
        }

        private void CalcList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (CalcList.Columns[e.ColumnIndex].DataPropertyName == "時間" && e.Value is double time)
            {
                e.Value = time.ToString("F2") + "秒";
                e.FormattingApplied = true;
            }
            if (CalcList.Columns[e.ColumnIndex].DataPropertyName == "性格" && e.Value is NatureType nature)
            {
                e.Value = Types.GetDescription(nature);
                e.FormattingApplied = true;
            }
            if (CalcList.Columns[e.ColumnIndex].DataPropertyName == "経験値")
            {
                if (int.TryParse((string?)e.Value, out int exp) && exp == 65535)
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }
        }

        private void MinFrame_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MinFrame.Text, out int minFrame))
            {
                if (minFrame < 0)
                {
                    minFrame = 0;
                    MinFrame.Text = "0";
                }
                ConfigData.Instance.minFrame = minFrame;
            }
            else
            {
                ConfigData.Instance.minFrame = 0;
                MinFrame.Text = "";
            }
        }

        private void MaxFrame_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MaxFrame.Text, out int maxFrame))
            {
                if (maxFrame < 0)
                {
                    maxFrame = 0;
                    MaxFrame.Text = "0";
                }
                ConfigData.Instance.maxFrame = maxFrame;
            }
            else
            {
                ConfigData.Instance.maxFrame = 0;
                MaxFrame.Text = "";
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSettings settingsForm = new EditSettings(this);
            settingsForm.Show();
            SettingsToolStripMenuItem.Enabled = false;
        }
        public void SettingsFormClosed()
        {
            SettingsToolStripMenuItem.Enabled = true;
        }
        private int[] getStatus(NatureType nature, int ivH, int ivA, int ivB, int ivC, int ivD, int ivS, int level, StatusType upStatus, StatusType downStatus)
        {
            int spieceIndex;
            if (ConfigData.Instance.starter == StarterPokemonType.Bulbasaur) { spieceIndex = 0; }
            else if (ConfigData.Instance.starter == StarterPokemonType.Charmander) { spieceIndex = 1; }
            else { spieceIndex = 2; }
            int baseH = Constants.starterPokemonBaseStats[spieceIndex].H;
            int baseA = Constants.starterPokemonBaseStats[spieceIndex].A;
            int baseB = Constants.starterPokemonBaseStats[spieceIndex].B;
            int baseC = Constants.starterPokemonBaseStats[spieceIndex].C;
            int baseD = Constants.starterPokemonBaseStats[spieceIndex].D;
            int baseS = Constants.starterPokemonBaseStats[spieceIndex].S;
            int h = (baseH * 2 + ivH) * level / 100 + level + 10;
            int a = (int)(((baseA * 2 + ivA) * level / 100 + 5) * (upStatus == StatusType.A ? 1.1 : (downStatus == StatusType.A ? 0.9 : 1)));
            int b = (int)(((baseB * 2 + ivB) * level / 100 + 5) * (upStatus == StatusType.B ? 1.1 : (downStatus == StatusType.B ? 0.9 : 1)));
            int c = (int)(((baseC * 2 + ivC) * level / 100 + 5) * (upStatus == StatusType.C ? 1.1 : (downStatus == StatusType.C ? 0.9 : 1)));
            int d = (int)(((baseD * 2 + ivD) * level / 100 + 5) * (upStatus == StatusType.D ? 1.1 : (downStatus == StatusType.D ? 0.9 : 1)));
            int s = (int)(((baseS * 2 + ivS) * level / 100 + 5) * (upStatus == StatusType.S ? 1.1 : (downStatus == StatusType.S ? 0.9 : 1)));
            return new int[] { h, a, b, c, d, s };
        }

        private void MinExpText_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MinExpText1.Text, out int minExp))
            {
                if (minExp < 0)
                {
                    minExp = 0;
                    MinExpText1.Text = "";
                }
                ConfigData.Instance.minExp = minExp;
            }
            else
            {
                ConfigData.Instance.minExp = 0;
                MinExpText1.Text = "";
            }
        }
        private void MinExpText2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MinExpText2.Text, out int minExp2))
                ConfigData.Instance.minExp2 = minExp2;
            else
            {
                ConfigData.Instance.minExp2 = 0;
                MinExpText2.Text = "";
            }
        }
        private void MinExpText1_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(MinExpText1.Text, out int minExp1))
            {
                if (ConfigData.Instance.minExp2 < minExp1)
                {
                    ConfigData.Instance.minExp2 = minExp1;
                    MinExpText2.Text = minExp1.ToString();
                }
            }
        }
        private void MinExpText2_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(MinExpText2.Text, out int minExp2))
            {
                if (minExp2 < ConfigData.Instance.minExp)
                {
                    minExp2 = ConfigData.Instance.minExp;
                    MinExpText2.Text = ConfigData.Instance.minExp.ToString();
                }
                ConfigData.Instance.minExp2 = minExp2;
            }
            else
            {
                minExp2 = ConfigData.Instance.minExp;
                ConfigData.Instance.minExp2 = minExp2;
            }
        }

        private void MaxExpText_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MaxExpText.Text, out int maxExp))
            {
                if (maxExp < 0)
                {
                    maxExp = 0;
                    MaxExpText.Text = "";
                }
                ConfigData.Instance.maxExp = maxExp;
            }
            else
            {
                ConfigData.Instance.maxExp = 0;
                MaxExpText.Text = "";
            }
        }

        private void MaxExpText_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(MaxExpText.Text, out int maxExp) && maxExp < ConfigData.Instance.minExp)
            {
                maxExp = ConfigData.Instance.minExp;
                MaxExpText.Text = ConfigData.Instance.minExp.ToString();
                ConfigData.Instance.maxExp = maxExp;
            }
        }

        private void MaxFrame_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(MaxFrame.Text, out int maxFrame) && maxFrame < ConfigData.Instance.minFrame)
            {
                maxFrame = ConfigData.Instance.minFrame;
                MaxFrame.Text = ConfigData.Instance.minFrame.ToString();
                ConfigData.Instance.maxFrame = maxFrame;
            }
        }

        private void TIDText_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TIDText.Text, out int tid))
            {
                if (65535 < tid)
                    TIDText.Text = "65535";
            }
            else if (TIDText.Text == "画像認識中...")
            {
                // 何もしない
            }
            else
            {
                TIDText.Text = "";
            }
        }

        private void SelectWindowList_Enter(object sender, EventArgs e)
        {
            ScreenScaner.windowList.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                // 「メインウィンドウのタイトル」があるものだけをリストアップ
                if (!string.IsNullOrEmpty(p.MainWindowTitle))
                {
                    ScreenScaner.windowList.Add(new ScreenScaner.WindowType { Hwnd = p.MainWindowHandle, Title = p.MainWindowTitle });
                }
            }
            SelectWindowList.Items.Clear();
            foreach (var window in ScreenScaner.windowList)
                SelectWindowList.Items.Add(window.Title);
        }

        private void SelectWindowList_Click(object sender, EventArgs e)
        {
            if (!SelectWindowList.DroppedDown)
            {
                SelectWindowList.DroppedDown = true;
            }
        }

        private void SelectWindowList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectWindowList.SelectedItem.ToString() == "ゲーム画面ウィンドウを選択してください") return;
            ScreenScaner.selectedWindow = ScreenScaner.windowList[SelectWindowList.SelectedIndex];
            ScanStartButton.Enabled = true;
            this.ActiveControl = null;
        }

        private CancellationTokenSource _cts;
        private bool _isScanning = false;
        private async void ScanStartButton_Click(object sender, EventArgs e)
        {
            if (ScreenScaner.isScanning)
            {
                ScreenScaner.isScanning = false;
                ScanStartButton.Text = "画像認識";
                TIDText.Text = "";
                _cts.Cancel();
                return;
            }
            ScreenScaner.isScanning = true;
            ScanStartButton.Text = "画像認識終了";
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                // UIを固めないために Task.Run で別スレッドへ
                await Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        // ここで画像認識を実行
                        // 先ほど統合した Scanner クラスのメソッドを呼ぶ
                        var result = ScreenScaner.Scan(ScreenScaner.selectedWindow.Hwnd, ConfigData.Instance.searchArea, ConfigData.Instance.scanThreshold);

                        // UI側のコントロール（ラベルなど）を更新する場合は Invoke が必要
                        this.Invoke(new Action(() =>
                        {
                            if (result == -1)
                            {
                                MessageBox.Show("ウィンドウの位置の取得に失敗しました。");
                                ScreenScaner.isScanning = false;
                                ScanStartButton.Text = "画像認識";
                                _cts.Cancel();
                                return;
                            }
                            else if (result == -2)
                            {
                                TIDText.Text = "画像認識中...";
                            }
                            else
                            {
                                TIDText.Text = result.ToString();
                                CalcButton_Click(null, null);
                                ScreenScaner.isScanning = false;
                                ScanStartButton.Text = "画像認識";
                                _cts.Cancel();
                                return;
                            }
                        }));

                        // CPU負荷を抑えるための待機時間（ミリ秒）
                        // ループ速度を調整してください
                        await Task.Delay(500, token);
                    }
                }, token);
            }
            catch (OperationCanceledException)
            {
                // キャンセル時はここを通る（正常終了）
            }
            catch (Exception ex)
            {
                MessageBox.Show($"スキャン中にエラーが発生しました: {ex.Message}");
            }
            finally
            {
                _isScanning = false;
                ScanStartButton.Text = "画像認識";
            }
        }

        bool isCountHelperOpen = false;
        StepCountHelper helperForm;
        private void ConutHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            helperForm = new StepCountHelper();

            helperForm.Show();
            ConutHelperToolStripMenuItem.Enabled = false;
            isCountHelperOpen = true;
            if (WordEXPList.CurrentCell != null)
            {
                helperForm.SetTargetExp((int)WordEXPList.CurrentRow.Cells[0].Value);
            }
            helperForm.FormClosed += (s, args) => { ConutHelperToolStripMenuItem.Enabled = true; isCountHelperOpen = false; };
        }

        private void WordEXPList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isCountHelperOpen && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                helperForm.SetTargetExp((int)WordEXPList.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (ConfigData.Instance.ScanHotKeyEnable)
            {
                if (scanHotKey == Keys.None) return;

                if (e.KeyData == scanHotKey)
                {
                    ScanStartButton_Click(sender, e);

                    // 他のコントロールにキー入力を渡さない
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }
    }
}