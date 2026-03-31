using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static Gen3_Starter_Manip_for_ACE.Form1;
using static Gen3_Starter_Manip_for_ACE.Types;
using static Gen3_Starter_Manip_for_ACE.Constants;
using static Gen3_Starter_Manip_for_ACE.ConfigData;
using static Gen3_Starter_Manip_for_ACE.ConfigUtils;
using static Gen3_Starter_Manip_for_ACE.SearchEngine;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class Form1 : Form
    {
        public Form1()
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
            {
                filePath = "corrupted_pokemon_FR.csv";
            }
            else
            {
                filePath = "corrupted_pokemon_LG.csv";
            }
            Resources.loadCorruptedPokemonData(filePath);
            CalcList.RowTemplate.Height = 16;
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            ushort tid;
            if (!ushort.TryParse(TIDText.Text, out tid))
            {
                tid = 0;
            }
            var results = SearchEngine.SearchPokemons(tid, ConfigData.Instance.isSearchForACE);

            CalcList.DataSource = results;

            CalcList.Columns["フレーム"].Width = 55;
            CalcList.Columns["時間"].Width = 55;
            CalcList.Columns["PID"].Width = 60;
            CalcList.Columns["PID"].DefaultCellStyle.Format = "X8";
            CalcList.Columns["H"].Width = 24;
            CalcList.Columns["A"].Width = 24;
            CalcList.Columns["B"].Width = 24;
            CalcList.Columns["C"].Width = 24;
            CalcList.Columns["D"].Width = 24;
            CalcList.Columns["S"].Width = 24;
            CalcList.Columns["性格"].Width = 60;
            CalcList.Columns["性別"].Width = 40;
            CalcList.Columns["EXP"].Width = 40;
            CalcList.Columns[CalcList.ColumnCount - 1].MinimumWidth = 60;
            CalcList.Columns[CalcList.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CalcList.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            if (ConfigData.Instance.isSearchForACE)
            {
                CalcList.Columns["EXP"].Visible = true;
            }
            else
            {
                CalcList.Columns["EXP"].Visible = false;
            }
            CalcList.RowHeadersVisible = false;

            if (CalcList.Rows.Count > 0)
            {
                showStatus(0);
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
            if (!isNumberOrActionKey(e.KeyCode))
                e.SuppressKeyPress = true;
        }

        private void CalcList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            showStatus(e.RowIndex);
        }

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

            var results = SearchEngine.SerchAroundFrames(tid, CurrentFrame);

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

        private void Spieces_ChekedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    switch (rb.Name)
                    {
                        case "Balbasaur":
                            ConfigData.Instance.starter = StarterPokemonType.Bulbasaur;
                            break;
                        case "Charmander":
                            ConfigData.Instance.starter = StarterPokemonType.Charmander;
                            break;
                        case "Squirtle":
                            ConfigData.Instance.starter = StarterPokemonType.Squirtle;
                            break;
                    }
                }
            }
            if (CalcList.CurrentCell == null) return;
            showStatus(CalcList.CurrentCell.RowIndex);
        }

        private void LoadConfigsFromFile_Click(object sender, EventArgs e)
        {

        }
        private void SaveConfigs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // ダイアログのタイトル
                sfd.Title = "設定ファイルを保存";
                // 初期ファイル名
                sfd.FileName = "config.json";
                // 選択できるファイルの種類（フィルタ）
                sfd.Filter = "JSONファイル(*.json)|*.json|すべてのファイル(*.*)|*.*";
                // 最初に見せるフォルダ（デスクトップなど）
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // ダイアログを表示し、「保存」が押されたら処理
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // 選択されたパスを取得
                    string filePath = sfd.FileName;

                    // ここでファイル書き込み処理（例：JSONシリアライズ）
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

            var effect = Constants.natureEffects[(int)nature];
            var upStatus = effect.UpStatus;
            var downStatus = effect.DownStatus;

            int spieceIndex;
            if (Balbasaur.Checked) { spieceIndex = 0; }
            else if (Charmander.Checked) { spieceIndex = 1; }
            else { spieceIndex = 2; }
            int baseH = Constants.starterPokemonBaseStats[spieceIndex].H;
            int baseA = Constants.starterPokemonBaseStats[spieceIndex].A;
            int baseB = Constants.starterPokemonBaseStats[spieceIndex].B;
            int baseC = Constants.starterPokemonBaseStats[spieceIndex].C;
            int baseD = Constants.starterPokemonBaseStats[spieceIndex].D;
            int baseS = Constants.starterPokemonBaseStats[spieceIndex].S;
            int h = (baseH * 2 + ivH) * 5 / 100 + 15;
            int a = (int)(((baseA * 2 + ivA) * 5 / 100 + 5) * (upStatus == StatusType.A ? 1.1 : (downStatus == StatusType.A ? 0.9 : 1)));
            int b = (int)(((baseB * 2 + ivB) * 5 / 100 + 5) * (upStatus == StatusType.B ? 1.1 : (downStatus == StatusType.B ? 0.9 : 1)));
            int c = (int)(((baseC * 2 + ivC) * 5 / 100 + 5) * (upStatus == StatusType.C ? 1.1 : (downStatus == StatusType.C ? 0.9 : 1)));
            int d = (int)(((baseD * 2 + ivD) * 5 / 100 + 5) * (upStatus == StatusType.D ? 1.1 : (downStatus == StatusType.D ? 0.9 : 1)));
            int s = (int)(((baseS * 2 + ivS) * 5 / 100 + 5) * (upStatus == StatusType.S ? 1.1 : (downStatus == StatusType.S ? 0.9 : 1)));
            StatusHNum.Text = h.ToString();
            StatusANum.Text = a.ToString();
            StatusBNum.Text = b.ToString();
            StatusCNum.Text = c.ToString();
            StatusDNum.Text = d.ToString();
            StatusSNum.Text = s.ToString();
            if (upStatus == StatusType.A)
            {
                StatusANum.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.A)
            {
                StatusANum.ForeColor = Color.Cyan;
            }
            else
            {
                StatusANum.ForeColor = Color.White;
            }
            if (upStatus == StatusType.B)
            {
                StatusBNum.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.B)
            {
                StatusBNum.ForeColor = Color.Cyan;
            }
            else
            {
                StatusBNum.ForeColor = Color.White;
            }
            if (upStatus == StatusType.C)
            {
                StatusCNum.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.C)
            {
                StatusCNum.ForeColor = Color.Cyan;
            }
            else
            {
                StatusCNum.ForeColor = Color.White;
            }
            if (upStatus == StatusType.D)
            {
                StatusDNum.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.D)
            {
                StatusDNum.ForeColor = Color.Cyan;
            }
            else
            {
                StatusDNum.ForeColor = Color.White;
            }
            if (upStatus == StatusType.S)
            {
                StatusSNum.ForeColor = Color.Salmon;
            }
            else if (downStatus == StatusType.S)
            {
                StatusSNum.ForeColor = Color.Cyan;
            }
            else
            {
                StatusSNum.ForeColor = Color.White;
            }
        }
        private void setConfigData()
        {
            // コンフィグデータの内容をフォームの各コントロールに反映させる
            var config = ConfigData.Instance;
            switch (config.starter)
            {
                case StarterPokemonType.Bulbasaur:
                    Balbasaur.Checked = true;
                    break;
                case StarterPokemonType.Charmander:
                    Charmander.Checked = true;
                    break;
                case StarterPokemonType.Squirtle:
                    Squirtle.Checked = true;
                    break;
            }
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
        }

        private void MinFrame_TextChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.minFrame = int.TryParse(MinFrame.Text, out int minFrame) ? minFrame : 0;
        }

        private void MaxFrame_TextChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.maxFrame = int.TryParse(MaxFrame.Text, out int maxFrame) ? maxFrame : 0;
        }
    }
}