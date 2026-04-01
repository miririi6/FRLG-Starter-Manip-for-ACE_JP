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
            SelectPokemonGroup.SuspendLayout();
            VersionBox.SuspendLayout();
            SerchModeBox.SuspendLayout();
            SeedPokemonBox.SuspendLayout();
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
            // EditSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(216, 352);
            Controls.Add(SeedPokemonBox);
            Controls.Add(SerchModeBox);
            Controls.Add(VersionBox);
            Controls.Add(SelectPokemonGroup);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "EditSettings";
            Text = "設定変更";
            SelectPokemonGroup.ResumeLayout(false);
            SelectPokemonGroup.PerformLayout();
            VersionBox.ResumeLayout(false);
            VersionBox.PerformLayout();
            SerchModeBox.ResumeLayout(false);
            SerchModeBox.PerformLayout();
            SeedPokemonBox.ResumeLayout(false);
            SeedPokemonBox.PerformLayout();
            ResumeLayout(false);
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
    }
}