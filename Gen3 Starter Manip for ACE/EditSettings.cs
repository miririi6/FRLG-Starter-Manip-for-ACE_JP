using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static Gen3_Starter_Manip_for_ACE.ConfigData;
using static Gen3_Starter_Manip_for_ACE.Types;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class EditSettings : Form
    {
        public MainForm _parent;
        public EditSettings(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
            if (!ConfigData.Instance.isSearchForACE)
            {
                FRButton.Enabled = false;
                LGButton.Enabled = false;
                SeedBulbasaur.Enabled = false;
                SeedIvysaur.Enabled = false;
                SeedVenusaur.Enabled = false;
                SeedCharmander.Enabled = false;
                SeedCharmeleon.Enabled = false;
                SeedCharizard.Enabled = false;
                SeedSquirtle.Enabled = false;
                SeedWartortle.Enabled = false;
                SeedBlastoise.Enabled = false;
            }
            if (ConfigData.Instance.version == RomVersionType.FireRed)
                FRButton.Checked = true;
            else
                LGButton.Checked = true;
            switch (ConfigData.Instance.starter)
            {
                case StarterPokemonType.Bulbasaur:
                    Bulbasaur.Checked = true;
                    break;
                case StarterPokemonType.Charmander:
                    Charmander.Checked = true;
                    break;
                case StarterPokemonType.Squirtle:
                    Squirtle.Checked = true;
                    break;
            }
            if (ConfigData.Instance.isSearchForACE)
                AceModeButton.Checked = true;
            else
                NoAceModeButton.Checked = true;
            switch (ConfigData.Instance.seedPokemon)
            {
                case SeedPokemonType.Bulbasaur:
                    SeedBulbasaur.Checked = true;
                    break;
                case SeedPokemonType.Ivysaur:
                    SeedIvysaur.Checked = true;
                    break;
                case SeedPokemonType.Venusaur:
                    SeedVenusaur.Checked = true;
                    break;
                case SeedPokemonType.Charmander:
                    SeedCharmander.Checked = true;
                    break;
                case SeedPokemonType.Charmeleon:
                    SeedCharmeleon.Checked = true;
                    break;
                case SeedPokemonType.Charizard:
                    SeedCharizard.Checked = true;
                    break;
                case SeedPokemonType.Squirtle:
                    SeedSquirtle.Checked = true;
                    break;
                case SeedPokemonType.Wartortle:
                    SeedWartortle.Checked = true;
                    break;
                case SeedPokemonType.Blastoise:
                    SeedBlastoise.Checked = true;
                    break;
            }
            if(ConfigData.Instance.waitTime != null)
                WaitTimeNum.Value = (decimal)ConfigData.Instance.waitTime;
            if(ConfigData.Instance.scanThreshold != null)
                ThresholdNum.Value = (decimal)ConfigData.Instance.scanThreshold;
            if(ConfigData.Instance.scanWindowTitle != null)
                WindowNameInputBox.Text = ConfigData.Instance.scanWindowTitle;
            SelectWindowsListForSettings.Items.Add("ゲーム画面ウィンドウを選択してください");
            SelectWindowsListForSettings.SelectedIndex = 0;
        }

        private void FRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FRButton.Checked)
            {
                ConfigData.Instance.version = RomVersionType.FireRed;
                string filePath = "corrupted_pokemon_FR.csv";
                Resources.loadCorruptedPokemonData(filePath);
                if (_parent is MainForm mainForm)
                    mainForm.CalcStartCall();
            }
        }
        private void LGButton_CheckedChanged(object sender, EventArgs e)
        {
            if (LGButton.Checked)
            {
                ConfigData.Instance.version = RomVersionType.LeafGreen;
                string filePath = "corrupted_pokemon_LG.csv";
                Resources.loadCorruptedPokemonData(filePath);
                if (_parent is MainForm mainForm)
                    mainForm.CalcStartCall();
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
                        case "Bulbasaur":
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
            if (_parent is MainForm mainForm)
                mainForm.SpiecesChanged();
        }

        private void AceModeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AceModeButton.Checked)
            {
                ConfigData.Instance.isSearchForACE = true;
                FRButton.Enabled = true;
                LGButton.Enabled = true;
                SeedBulbasaur.Enabled = true;
                SeedIvysaur.Enabled = true;
                SeedVenusaur.Enabled = true;
                SeedCharmander.Enabled = true;
                SeedCharmeleon.Enabled = true;
                SeedCharizard.Enabled = true;
                SeedSquirtle.Enabled = true;
                SeedWartortle.Enabled = true;
                SeedBlastoise.Enabled = true;
            }
            if (_parent is MainForm mainForm)
            {
                mainForm.AceModeView();
                mainForm.CalcStartCall();
            }
        }

        private void NoAceModeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NoAceModeButton.Checked)
            {
                ConfigData.Instance.isSearchForACE = false;
                FRButton.Enabled = false;
                LGButton.Enabled = false;
                SeedBulbasaur.Enabled = false;
                SeedIvysaur.Enabled = false;
                SeedVenusaur.Enabled = false;
                SeedCharmander.Enabled = false;
                SeedCharmeleon.Enabled = false;
                SeedCharizard.Enabled = false;
                SeedSquirtle.Enabled = false;
                SeedWartortle.Enabled = false;
                SeedBlastoise.Enabled = false;
            }
            if (_parent is MainForm mainForm)
            {
                mainForm.NoAceModeView();
                mainForm.CalcStartCall();
            }
        }

        private void SeedPokemons_CheckedChanged_1(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    switch (rb.Name)
                    {
                        case "SeedBulbasaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Bulbasaur;
                            break;
                        case "SeedIvysaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Ivysaur;
                            break;
                        case "SeedVenusaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Venusaur;
                            break;
                        case "SeedCharmander":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charmander;
                            break;
                        case "SeedCharmeleon":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charmeleon;
                            break;
                        case "SeedCharizard":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charizard;
                            break;
                        case "SeedSquirtle":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Squirtle;
                            break;
                        case "SeedWartortle":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Wartortle;
                            break;
                        case "SeedBlastoise":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Blastoise;
                            break;
                    }
                }
            }
            if (_parent is MainForm mainForm)
                mainForm.CalcStartCall();
        }

        ScanRangeWindow scanRangeWindow;
        public static ScreenScaner.RECT rangeStartRECT;
        private async void LaunchScanAreaButton_Click(object sender, EventArgs e)
        {
            GetWindowRect(ScreenScaner.selectedWindow.Hwnd, out rangeStartRECT);
            scanRangeWindow = new ScanRangeWindow();
            scanRangeWindow.Owner = this;
            scanRangeWindow.Show();
            DecideRangeButton.Enabled = true;
            LaunchScanAreaButton.Enabled = false;
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                // UIを固めないために Task.Run で別スレッドへ
                await Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        selectedRelativeRect = GetRelativeRect(ScreenScaner.selectedWindow.Hwnd, scanRangeWindow);
                        this.Invoke((Action)(() =>
                        {
                            if (this.IsDisposed || RangeViewBox == null || RangeViewBox.IsDisposed) return;
                            Bitmap newBmp = ScreenScaner.CaptureScreen(ScreenScaner.selectedWindow.Hwnd, selectedRelativeRect);

                            if (newBmp != null)
                            {
                                if (!RangeViewBox.IsDisposed)
                                {
                                    // 1. 今貼ってある古い画像をメモリから解放する
                                    RangeViewBox.Image?.Dispose();
                                    // 2. 新しい画像を貼る
                                    RangeViewBox.Image = newBmp;
                                }
                                else
                                {
                                    newBmp.Dispose();
                                }

                            }
                        }));
                        await Task.Delay(100); // 100msごとに更新
                    }
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (ObjectDisposedException ex) when (_cts == null || _cts.IsCancellationRequested)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}");
            }
            finally
            {
                // 1. _cts は null チェック付きで Dispose して空にする
                _cts?.Dispose();
                _cts = null;

                // 2. ボタンの状態を戻す（これがないと、一度スキャンを止めると二度と押せなくなる）
                LaunchScanAreaButton.Enabled = true;
                DecideRangeButton.Enabled = false;

                // 3. 子ウィンドウを閉じる（既に閉じている場合の ObjectDisposedException を回避）
                if (scanRangeWindow != null && !scanRangeWindow.IsDisposed)
                {
                    scanRangeWindow.Close();
                }
            }
        }

        private void SelectWindowsListForSettings_Enter(object sender, EventArgs e)
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
            SelectWindowsListForSettings.Items.Clear();
            foreach (var window in ScreenScaner.windowList)
                SelectWindowsListForSettings.Items.Add(window.Title);
        }

        private CancellationTokenSource _cts;
        private Rectangle selectedRelativeRect;
        private void SelectWindowsListForSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectWindowsListForSettings.SelectedItem.ToString() == "ゲーム画面ウィンドウを選択してください") return;
            ScreenScaner.selectedWindow = ScreenScaner.windowList[SelectWindowsListForSettings.SelectedIndex];
            LaunchScanAreaButton.Enabled = true;
            this.ActiveControl = null;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out ScreenScaner.RECT lpRect);

        public static Rectangle GetRelativeRect(IntPtr targetHwnd, Form selectionForm)
        {
            // 1. ターゲットウィンドウの現在の位置を取得
            GetWindowRect(targetHwnd, out ScreenScaner.RECT windowRect);

            // 2. 選択枠の現在の位置を取得
            Rectangle selectionRect = selectionForm.Bounds;

            // 3. 相対座標（ターゲットの左上を0,0とした時の位置）を計算
            int relativeX = selectionRect.X - windowRect.Left;
            int relativeY = selectionRect.Y - windowRect.Top;

            // 4. この relativeX, relativeY, 幅, 高さ を保存！
            // 例: { "X": 100, "Y": 250, "W": 50, "H": 20 }
            var result = new Rectangle
            {
                X = relativeX,
                Y = relativeY,
                Width = selectionRect.Width,
                Height = selectionRect.Height
            };
            return result;
        }
        private async void DecideRangeButton_Click(object sender, EventArgs e)
        {
            ConfigData.Instance.searchArea = selectedRelativeRect;
            _cts.Cancel();
            MessageBox.Show("範囲の選択が完了しました。");
            DecideRangeButton.Enabled = false;
        }

        private void EditSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();
            if (_parent is MainForm mainForm)
                mainForm.SettingsFormClosed();
        }

        private void WaitTimeNum_ValueChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.waitTime = (double)WaitTimeNum.Value;
        }

        private void ThresholdNum_ValueChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.scanThreshold = (double)ThresholdNum.Value;
        }

        private void WindowNameInputBox_TextChanged(object sender, EventArgs e)
        {
            ConfigData.Instance.scanWindowTitle = WindowNameInputBox.Text;
        }
    }
}
