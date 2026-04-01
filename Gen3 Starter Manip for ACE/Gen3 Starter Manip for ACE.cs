using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static Gen3_Starter_Manip_for_ACE.MainForm;
using static Gen3_Starter_Manip_for_ACE.Types;
using static Gen3_Starter_Manip_for_ACE.Constants;
using static Gen3_Starter_Manip_for_ACE.ConfigData;
using static Gen3_Starter_Manip_for_ACE.ConfigUtils;
using static Gen3_Starter_Manip_for_ACE.SearchEngine;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigUtils.loadConfigData("config.json");
            setConfigData();
            Resources.loadResources("words.csv", "poke_words.csv");
            string filePath;
            if (ConfigData.Instance.version == RomVersionType.FireRed)
                filePath = "corrupted_pokemon_FR.csv";
            else
                filePath = "corrupted_pokemon_LG.csv";
            Resources.loadCorruptedPokemonData(filePath);
            CalcList.RowTemplate.Height = 16;
            TIDText.Focus();
        }
        public void NoAceModeView()
        {
            WordEXPList.Visible = false;
            ClientSize = new Size(682, 495);
            MinExpText.Enabled = false;
            MaxExpText.Enabled = false;
        }
        public void AceModeView()
        {
            WordEXPList.Visible = true;
            ClientSize = new Size(958, 495);
            MinExpText.Enabled = true;
            MaxExpText.Enabled = true;
        }

        bool ClickedCalcButton = false;
        public void CalcButton_Click(object sender, EventArgs e)
        {
            ClickedCalcButton = true;
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
                    uint pid = (uint)CalcList.Rows[0].Cells[3].Value;
                    var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                    WordEXPList.DataSource = wordExpData;
                    WordEXPList.Columns["経験値"].Width = 50;
                    WordEXPList.Columns["ワード3"].Width = 70;
                    WordEXPList.Columns["ワード5"].Width = 70;
                    WordEXPList.Columns["パターン"].Width = 50;
                    WordEXPList.Columns[WordEXPList.ColumnCount - 1].MinimumWidth = 50;
                    WordEXPList.Columns[WordEXPList.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
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

        private void CalcList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            showStatus(e.RowIndex);
            if (ConfigData.Instance.isSearchForACE)
            {
                ushort tid = ushort.TryParse(TIDText.Text, out tid) ? tid : (ushort)0;
                uint pid = (uint)CalcList.Rows[e.RowIndex].Cells[3].Value;
                var wordExpData = SearchEngine.GetWordExpData(tid, pid);
                WordEXPList.DataSource = wordExpData;
            }
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
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            // sender を TextBox にキャストして汎用的に使う
            if (sender is TextBox tb)
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
            if (sender is TextBox tb)
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
                uint pid = (uint)CalcList.Rows[CalcList.CurrentCell.RowIndex].Cells[3].Value;
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
            Types.NatureType nature = (Types.NatureType)CalcList.Rows[rowIndex].Cells[2].Value;
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
            MinExpText.Text = config.minExp.ToString();
            MaxExpText.Text = config.maxExp.ToString();
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
        private bool isNumberOrActionKey(Keys key)
        {
            bool isNumber = (key >= Keys.D0 && key <= Keys.D9) ||
                            (key >= Keys.NumPad0 && key <= Keys.NumPad1);
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
            ConfigData.Instance.minFrame = int.TryParse(MinFrame.Text, out int minFrame) ? minFrame : 0;
        }

        private void MaxFrame_TextChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.maxFrame = int.TryParse(MaxFrame.Text, out int maxFrame) ? maxFrame : 0;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSettings settingsForm = new EditSettings(this);
            settingsForm.Show();
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
            ConfigData.Instance.minExp = int.TryParse(MinExpText.Text, out int minExp) ? minExp : 0;
        }

        private void MaxExpText_TextChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.maxExp = int.TryParse(MaxExpText.Text, out int maxExp) ? maxExp : 0;
        }
    }
}