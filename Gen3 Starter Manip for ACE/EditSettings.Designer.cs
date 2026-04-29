namespace Gen3_Starter_Manip_for_ACE
{
    partial class EditSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FRButton = new RadioButton();
            LGButton = new RadioButton();
            SelectPokemonGroup = new GroupBox();
            Squirtle = new RadioButton();
            Charmander = new RadioButton();
            Bulbasaur = new RadioButton();
            VersionBox = new GroupBox();
            SerchModeBox = new GroupBox();
            AceModeButton = new RadioButton();
            NoAceModeButton = new RadioButton();
            SeedPokemonBox = new GroupBox();
            SeedBlastoise = new RadioButton();
            SeedWartortle = new RadioButton();
            SeedSquirtle = new RadioButton();
            SeedCharizard = new RadioButton();
            SeedCharmeleon = new RadioButton();
            SeedCharmander = new RadioButton();
            SeedVenusaur = new RadioButton();
            SeedIvysaur = new RadioButton();
            SeedBulbasaur = new RadioButton();
            ScanSettingGroup = new GroupBox();
            scanHotKeyCheckbox = new CheckBox();
            scanHotKeyText = new TextBox();
            ThresholdNum = new NumericUpDown();
            WaitTimeNum = new NumericUpDown();
            WindowNameInputBox = new TextBox();
            WindowNameInputLabel = new Label();
            ThresholdLabel = new Label();
            WaitTimeLabel = new Label();
            RangeViewBox = new PictureBox();
            DecideRangeButton = new Button();
            LaunchScanAreaButton = new Button();
            SelectWindowsListForSettings = new ComboBox();
            ConnectTimerCheckBox = new CheckBox();
            mainTopMostCheckbox = new CheckBox();
            SelectPokemonGroup.SuspendLayout();
            VersionBox.SuspendLayout();
            SerchModeBox.SuspendLayout();
            SeedPokemonBox.SuspendLayout();
            ScanSettingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WaitTimeNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RangeViewBox).BeginInit();
            SuspendLayout();
            // 
            // FRButton
            // 
            FRButton.AutoSize = true;
            FRButton.Location = new Point(6, 22);
            FRButton.Name = "FRButton";
            FRButton.Size = new Size(83, 19);
            FRButton.TabIndex = 0;
            FRButton.TabStop = true;
            FRButton.Text = "ファイアレッド";
            FRButton.UseVisualStyleBackColor = true;
            FRButton.CheckedChanged += FRButton_CheckedChanged;
            // 
            // LGButton
            // 
            LGButton.AutoSize = true;
            LGButton.Location = new Point(100, 22);
            LGButton.Name = "LGButton";
            LGButton.Size = new Size(83, 19);
            LGButton.TabIndex = 1;
            LGButton.TabStop = true;
            LGButton.Text = "リーフグリーン";
            LGButton.UseVisualStyleBackColor = true;
            LGButton.CheckedChanged += LGButton_CheckedChanged;
            // 
            // SelectPokemonGroup
            // 
            SelectPokemonGroup.Controls.Add(Squirtle);
            SelectPokemonGroup.Controls.Add(Charmander);
            SelectPokemonGroup.Controls.Add(Bulbasaur);
            SelectPokemonGroup.Location = new Point(12, 12);
            SelectPokemonGroup.Name = "SelectPokemonGroup";
            SelectPokemonGroup.Size = new Size(195, 65);
            SelectPokemonGroup.TabIndex = 3;
            SelectPokemonGroup.TabStop = false;
            SelectPokemonGroup.Text = "ポケモン選択";
            // 
            // Squirtle
            // 
            Squirtle.AutoSize = true;
            Squirtle.CheckAlign = ContentAlignment.BottomCenter;
            Squirtle.Location = new Point(135, 22);
            Squirtle.Name = "Squirtle";
            Squirtle.Size = new Size(48, 32);
            Squirtle.TabIndex = 0;
            Squirtle.Text = "ゼニガメ";
            Squirtle.UseVisualStyleBackColor = true;
            Squirtle.CheckedChanged += Spieces_ChekedChanged;
            // 
            // Charmander
            // 
            Charmander.AutoSize = true;
            Charmander.CheckAlign = ContentAlignment.BottomCenter;
            Charmander.Location = new Point(75, 22);
            Charmander.Name = "Charmander";
            Charmander.Size = new Size(46, 32);
            Charmander.TabIndex = 0;
            Charmander.Text = "ヒトカゲ";
            Charmander.UseVisualStyleBackColor = true;
            Charmander.CheckedChanged += Spieces_ChekedChanged;
            // 
            // Bulbasaur
            // 
            Bulbasaur.AutoSize = true;
            Bulbasaur.CheckAlign = ContentAlignment.BottomCenter;
            Bulbasaur.Location = new Point(6, 22);
            Bulbasaur.Name = "Bulbasaur";
            Bulbasaur.Size = new Size(57, 32);
            Bulbasaur.TabIndex = 0;
            Bulbasaur.Text = "フシギダネ";
            Bulbasaur.UseVisualStyleBackColor = true;
            Bulbasaur.CheckedChanged += Spieces_ChekedChanged;
            // 
            // VersionBox
            // 
            VersionBox.Controls.Add(FRButton);
            VersionBox.Controls.Add(LGButton);
            VersionBox.Location = new Point(12, 143);
            VersionBox.Name = "VersionBox";
            VersionBox.Size = new Size(195, 54);
            VersionBox.TabIndex = 4;
            VersionBox.TabStop = false;
            VersionBox.Text = "バージョン";
            // 
            // SerchModeBox
            // 
            SerchModeBox.Controls.Add(AceModeButton);
            SerchModeBox.Controls.Add(NoAceModeButton);
            SerchModeBox.Location = new Point(12, 83);
            SerchModeBox.Name = "SerchModeBox";
            SerchModeBox.Size = new Size(195, 54);
            SerchModeBox.TabIndex = 5;
            SerchModeBox.TabStop = false;
            SerchModeBox.Text = "検索モード";
            // 
            // AceModeButton
            // 
            AceModeButton.AutoSize = true;
            AceModeButton.Location = new Point(6, 22);
            AceModeButton.Name = "AceModeButton";
            AceModeButton.Size = new Size(71, 19);
            AceModeButton.TabIndex = 6;
            AceModeButton.TabStop = true;
            AceModeButton.Text = "ACEモード";
            AceModeButton.UseVisualStyleBackColor = true;
            AceModeButton.CheckedChanged += AceModeButton_CheckedChanged;
            // 
            // NoAceModeButton
            // 
            NoAceModeButton.AutoSize = true;
            NoAceModeButton.Location = new Point(94, 22);
            NoAceModeButton.Name = "NoAceModeButton";
            NoAceModeButton.Size = new Size(89, 19);
            NoAceModeButton.TabIndex = 7;
            NoAceModeButton.TabStop = true;
            NoAceModeButton.Text = "バグ無しモード";
            NoAceModeButton.UseVisualStyleBackColor = true;
            NoAceModeButton.CheckedChanged += NoAceModeButton_CheckedChanged;
            // 
            // SeedPokemonBox
            // 
            SeedPokemonBox.Controls.Add(SeedBlastoise);
            SeedPokemonBox.Controls.Add(SeedWartortle);
            SeedPokemonBox.Controls.Add(SeedSquirtle);
            SeedPokemonBox.Controls.Add(SeedCharizard);
            SeedPokemonBox.Controls.Add(SeedCharmeleon);
            SeedPokemonBox.Controls.Add(SeedCharmander);
            SeedPokemonBox.Controls.Add(SeedVenusaur);
            SeedPokemonBox.Controls.Add(SeedIvysaur);
            SeedPokemonBox.Controls.Add(SeedBulbasaur);
            SeedPokemonBox.Location = new Point(12, 203);
            SeedPokemonBox.Name = "SeedPokemonBox";
            SeedPokemonBox.Size = new Size(195, 138);
            SeedPokemonBox.TabIndex = 6;
            SeedPokemonBox.TabStop = false;
            SeedPokemonBox.Text = "バグ素材ポケモン選択";
            // 
            // SeedBlastoise
            // 
            SeedBlastoise.AutoSize = true;
            SeedBlastoise.CheckAlign = ContentAlignment.BottomCenter;
            SeedBlastoise.Location = new Point(136, 98);
            SeedBlastoise.Name = "SeedBlastoise";
            SeedBlastoise.Size = new Size(54, 32);
            SeedBlastoise.TabIndex = 8;
            SeedBlastoise.TabStop = true;
            SeedBlastoise.Text = "カメックス";
            SeedBlastoise.UseVisualStyleBackColor = true;
            SeedBlastoise.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedWartortle
            // 
            SeedWartortle.AutoSize = true;
            SeedWartortle.CheckAlign = ContentAlignment.BottomCenter;
            SeedWartortle.Location = new Point(75, 98);
            SeedWartortle.Name = "SeedWartortle";
            SeedWartortle.Size = new Size(46, 32);
            SeedWartortle.TabIndex = 7;
            SeedWartortle.TabStop = true;
            SeedWartortle.Text = "カメール";
            SeedWartortle.UseVisualStyleBackColor = true;
            SeedWartortle.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedSquirtle
            // 
            SeedSquirtle.AutoSize = true;
            SeedSquirtle.CheckAlign = ContentAlignment.BottomCenter;
            SeedSquirtle.Location = new Point(11, 98);
            SeedSquirtle.Name = "SeedSquirtle";
            SeedSquirtle.Size = new Size(48, 32);
            SeedSquirtle.TabIndex = 6;
            SeedSquirtle.TabStop = true;
            SeedSquirtle.Text = "ゼニガメ";
            SeedSquirtle.UseVisualStyleBackColor = true;
            SeedSquirtle.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedCharizard
            // 
            SeedCharizard.AutoSize = true;
            SeedCharizard.CheckAlign = ContentAlignment.BottomCenter;
            SeedCharizard.Location = new Point(135, 60);
            SeedCharizard.Name = "SeedCharizard";
            SeedCharizard.Size = new Size(54, 32);
            SeedCharizard.TabIndex = 5;
            SeedCharizard.TabStop = true;
            SeedCharizard.Text = "リザードン";
            SeedCharizard.UseVisualStyleBackColor = true;
            SeedCharizard.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedCharmeleon
            // 
            SeedCharmeleon.AutoSize = true;
            SeedCharmeleon.CheckAlign = ContentAlignment.BottomCenter;
            SeedCharmeleon.Location = new Point(75, 60);
            SeedCharmeleon.Name = "SeedCharmeleon";
            SeedCharmeleon.Size = new Size(45, 32);
            SeedCharmeleon.TabIndex = 4;
            SeedCharmeleon.TabStop = true;
            SeedCharmeleon.Text = "リザード";
            SeedCharmeleon.UseVisualStyleBackColor = true;
            SeedCharmeleon.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedCharmander
            // 
            SeedCharmander.AutoSize = true;
            SeedCharmander.CheckAlign = ContentAlignment.BottomCenter;
            SeedCharmander.Location = new Point(12, 60);
            SeedCharmander.Name = "SeedCharmander";
            SeedCharmander.Size = new Size(46, 32);
            SeedCharmander.TabIndex = 3;
            SeedCharmander.TabStop = true;
            SeedCharmander.Text = "ヒトカゲ";
            SeedCharmander.UseVisualStyleBackColor = true;
            SeedCharmander.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedVenusaur
            // 
            SeedVenusaur.AutoSize = true;
            SeedVenusaur.CheckAlign = ContentAlignment.BottomCenter;
            SeedVenusaur.Location = new Point(132, 22);
            SeedVenusaur.Name = "SeedVenusaur";
            SeedVenusaur.Size = new Size(58, 32);
            SeedVenusaur.TabIndex = 2;
            SeedVenusaur.TabStop = true;
            SeedVenusaur.Text = "フシギバナ";
            SeedVenusaur.UseVisualStyleBackColor = true;
            SeedVenusaur.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedIvysaur
            // 
            SeedIvysaur.AutoSize = true;
            SeedIvysaur.CheckAlign = ContentAlignment.BottomCenter;
            SeedIvysaur.Location = new Point(69, 22);
            SeedIvysaur.Name = "SeedIvysaur";
            SeedIvysaur.Size = new Size(57, 32);
            SeedIvysaur.TabIndex = 1;
            SeedIvysaur.TabStop = true;
            SeedIvysaur.Text = "フシギソウ";
            SeedIvysaur.UseVisualStyleBackColor = true;
            SeedIvysaur.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // SeedBulbasaur
            // 
            SeedBulbasaur.AutoSize = true;
            SeedBulbasaur.CheckAlign = ContentAlignment.BottomCenter;
            SeedBulbasaur.Location = new Point(6, 22);
            SeedBulbasaur.Name = "SeedBulbasaur";
            SeedBulbasaur.Size = new Size(57, 32);
            SeedBulbasaur.TabIndex = 0;
            SeedBulbasaur.TabStop = true;
            SeedBulbasaur.Text = "フシギダネ";
            SeedBulbasaur.UseVisualStyleBackColor = true;
            SeedBulbasaur.CheckedChanged += SeedPokemons_CheckedChanged_1;
            // 
            // ScanSettingGroup
            // 
            ScanSettingGroup.Controls.Add(scanHotKeyCheckbox);
            ScanSettingGroup.Controls.Add(scanHotKeyText);
            ScanSettingGroup.Controls.Add(ThresholdNum);
            ScanSettingGroup.Controls.Add(WaitTimeNum);
            ScanSettingGroup.Controls.Add(WindowNameInputBox);
            ScanSettingGroup.Controls.Add(WindowNameInputLabel);
            ScanSettingGroup.Controls.Add(ThresholdLabel);
            ScanSettingGroup.Controls.Add(WaitTimeLabel);
            ScanSettingGroup.Controls.Add(RangeViewBox);
            ScanSettingGroup.Controls.Add(DecideRangeButton);
            ScanSettingGroup.Controls.Add(LaunchScanAreaButton);
            ScanSettingGroup.Controls.Add(SelectWindowsListForSettings);
            ScanSettingGroup.Location = new Point(213, 12);
            ScanSettingGroup.Name = "ScanSettingGroup";
            ScanSettingGroup.Size = new Size(145, 321);
            ScanSettingGroup.TabIndex = 8;
            ScanSettingGroup.TabStop = false;
            ScanSettingGroup.Text = "画像認識設定";
            // 
            // scanHotKeyCheckbox
            // 
            scanHotKeyCheckbox.AutoSize = true;
            scanHotKeyCheckbox.Location = new Point(6, 294);
            scanHotKeyCheckbox.Name = "scanHotKeyCheckbox";
            scanHotKeyCheckbox.Size = new Size(69, 19);
            scanHotKeyCheckbox.TabIndex = 10;
            scanHotKeyCheckbox.Text = "ホットキー";
            scanHotKeyCheckbox.UseVisualStyleBackColor = true;
            scanHotKeyCheckbox.CheckedChanged += scanHotKeyCheckbox_CheckedChanged;
            // 
            // scanHotKeyText
            // 
            scanHotKeyText.Location = new Point(81, 292);
            scanHotKeyText.Name = "scanHotKeyText";
            scanHotKeyText.Size = new Size(56, 23);
            scanHotKeyText.TabIndex = 10;
            scanHotKeyText.Enter += scanHotKeyText_Enter;
            scanHotKeyText.KeyDown += scanHotKeyText_KeyDown;
            // 
            // ThresholdNum
            // 
            ThresholdNum.DecimalPlaces = 2;
            ThresholdNum.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            ThresholdNum.Location = new Point(87, 182);
            ThresholdNum.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            ThresholdNum.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            ThresholdNum.Name = "ThresholdNum";
            ThresholdNum.Size = new Size(52, 23);
            ThresholdNum.TabIndex = 7;
            ThresholdNum.Value = new decimal(new int[] { 90, 0, 0, 131072 });
            ThresholdNum.ValueChanged += ThresholdNum_ValueChanged;
            // 
            // WaitTimeNum
            // 
            WaitTimeNum.DecimalPlaces = 1;
            WaitTimeNum.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            WaitTimeNum.Location = new Point(87, 153);
            WaitTimeNum.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            WaitTimeNum.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            WaitTimeNum.Name = "WaitTimeNum";
            WaitTimeNum.Size = new Size(52, 23);
            WaitTimeNum.TabIndex = 7;
            WaitTimeNum.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            WaitTimeNum.ValueChanged += WaitTimeNum_ValueChanged;
            // 
            // WindowNameInputBox
            // 
            WindowNameInputBox.Location = new Point(6, 252);
            WindowNameInputBox.Name = "WindowNameInputBox";
            WindowNameInputBox.Size = new Size(131, 23);
            WindowNameInputBox.TabIndex = 6;
            WindowNameInputBox.TextChanged += WindowNameInputBox_TextChanged;
            // 
            // WindowNameInputLabel
            // 
            WindowNameInputLabel.Location = new Point(6, 217);
            WindowNameInputLabel.Name = "WindowNameInputLabel";
            WindowNameInputLabel.Size = new Size(131, 32);
            WindowNameInputLabel.TabIndex = 5;
            WindowNameInputLabel.Text = "起動時取得ウィンドウ名\r\n※部分一致";
            // 
            // ThresholdLabel
            // 
            ThresholdLabel.AutoSize = true;
            ThresholdLabel.Location = new Point(6, 184);
            ThresholdLabel.Name = "ThresholdLabel";
            ThresholdLabel.Size = new Size(63, 15);
            ThresholdLabel.TabIndex = 5;
            ThresholdLabel.Text = "閾値(0～1)";
            // 
            // WaitTimeLabel
            // 
            WaitTimeLabel.AutoSize = true;
            WaitTimeLabel.Location = new Point(6, 157);
            WaitTimeLabel.Name = "WaitTimeLabel";
            WaitTimeLabel.Size = new Size(75, 15);
            WaitTimeLabel.TabIndex = 5;
            WaitTimeLabel.Text = "実行間隔(秒)";
            // 
            // RangeViewBox
            // 
            RangeViewBox.Location = new Point(6, 82);
            RangeViewBox.Name = "RangeViewBox";
            RangeViewBox.Size = new Size(131, 33);
            RangeViewBox.SizeMode = PictureBoxSizeMode.Zoom;
            RangeViewBox.TabIndex = 4;
            RangeViewBox.TabStop = false;
            // 
            // DecideRangeButton
            // 
            DecideRangeButton.Enabled = false;
            DecideRangeButton.Location = new Point(6, 121);
            DecideRangeButton.Name = "DecideRangeButton";
            DecideRangeButton.Size = new Size(131, 23);
            DecideRangeButton.TabIndex = 3;
            DecideRangeButton.Text = "範囲決定";
            DecideRangeButton.UseVisualStyleBackColor = true;
            DecideRangeButton.Click += DecideRangeButton_Click;
            // 
            // LaunchScanAreaButton
            // 
            LaunchScanAreaButton.AutoSize = true;
            LaunchScanAreaButton.Enabled = false;
            LaunchScanAreaButton.Location = new Point(6, 51);
            LaunchScanAreaButton.Name = "LaunchScanAreaButton";
            LaunchScanAreaButton.Size = new Size(133, 25);
            LaunchScanAreaButton.TabIndex = 2;
            LaunchScanAreaButton.Text = "範囲選択ウィンドウ起動";
            LaunchScanAreaButton.UseVisualStyleBackColor = true;
            LaunchScanAreaButton.Click += LaunchScanAreaButton_Click;
            // 
            // SelectWindowsListForSettings
            // 
            SelectWindowsListForSettings.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectWindowsListForSettings.DropDownWidth = 360;
            SelectWindowsListForSettings.FormattingEnabled = true;
            SelectWindowsListForSettings.Location = new Point(6, 22);
            SelectWindowsListForSettings.Name = "SelectWindowsListForSettings";
            SelectWindowsListForSettings.Size = new Size(131, 23);
            SelectWindowsListForSettings.TabIndex = 1;
            SelectWindowsListForSettings.SelectedIndexChanged += SelectWindowsListForSettings_SelectedIndexChanged;
            SelectWindowsListForSettings.Enter += SelectWindowsListForSettings_Enter;
            // 
            // ConnectTimerCheckBox
            // 
            ConnectTimerCheckBox.Location = new Point(364, 52);
            ConnectTimerCheckBox.Name = "ConnectTimerCheckBox";
            ConnectTimerCheckBox.Size = new Size(137, 39);
            ConnectTimerCheckBox.TabIndex = 9;
            ConnectTimerCheckBox.Text = "FlowTimerに自動送信\r\n※Build47限定";
            ConnectTimerCheckBox.UseVisualStyleBackColor = true;
            ConnectTimerCheckBox.CheckedChanged += ConnectTimerCheckBox_CheckedChanged;
            // 
            // mainTopMostCheckbox
            // 
            mainTopMostCheckbox.AutoSize = true;
            mainTopMostCheckbox.Location = new Point(364, 12);
            mainTopMostCheckbox.Name = "mainTopMostCheckbox";
            mainTopMostCheckbox.Size = new Size(95, 34);
            mainTopMostCheckbox.TabIndex = 9;
            mainTopMostCheckbox.Text = "メインフォームを\r\n最前面に固定";
            mainTopMostCheckbox.UseVisualStyleBackColor = true;
            mainTopMostCheckbox.CheckedChanged += mainTopMostCheckbox_CheckedChanged;
            // 
            // EditSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 352);
            Controls.Add(mainTopMostCheckbox);
            Controls.Add(ConnectTimerCheckBox);
            Controls.Add(ScanSettingGroup);
            Controls.Add(SeedPokemonBox);
            Controls.Add(SerchModeBox);
            Controls.Add(VersionBox);
            Controls.Add(SelectPokemonGroup);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "EditSettings";
            Text = "設定変更";
            TopMost = true;
            FormClosing += EditSettings_FormClosing;
            SelectPokemonGroup.ResumeLayout(false);
            SelectPokemonGroup.PerformLayout();
            VersionBox.ResumeLayout(false);
            VersionBox.PerformLayout();
            SerchModeBox.ResumeLayout(false);
            SerchModeBox.PerformLayout();
            SeedPokemonBox.ResumeLayout(false);
            SeedPokemonBox.PerformLayout();
            ScanSettingGroup.ResumeLayout(false);
            ScanSettingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ThresholdNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)WaitTimeNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)RangeViewBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton FRButton;
        private RadioButton LGButton;
        private GroupBox SelectPokemonGroup;
        private RadioButton Squirtle;
        private RadioButton Charmander;
        private RadioButton Bulbasaur;
        private GroupBox VersionBox;
        private GroupBox SerchModeBox;
        private RadioButton AceModeButton;
        private RadioButton NoAceModeButton;
        private GroupBox SeedPokemonBox;
        private RadioButton SeedCharizard;
        private RadioButton SeedCharmeleon;
        private RadioButton SeedCharmander;
        private RadioButton SeedVenusaur;
        private RadioButton SeedIvysaur;
        private RadioButton SeedBulbasaur;
        private RadioButton SeedBlastoise;
        private RadioButton SeedWartortle;
        private RadioButton SeedSquirtle;
        private GroupBox ScanSettingGroup;
        private ComboBox SelectWindowsListForSettings;
        private Button LaunchScanAreaButton;
        private PictureBox RangeViewBox;
        private Button DecideRangeButton;
        private Label ThresholdLabel;
        private Label WaitTimeLabel;
        private Label WindowNameInputLabel;
        private TextBox WindowNameInputBox;
        private NumericUpDown ThresholdNum;
        private NumericUpDown WaitTimeNum;
        private CheckBox ConnectTimerCheckBox;
        private CheckBox mainTopMostCheckbox;
        private CheckBox scanHotKeyCheckbox;
        private TextBox scanHotKeyText;
    }
}