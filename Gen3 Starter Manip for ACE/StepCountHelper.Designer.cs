namespace Gen3_Starter_Manip_for_ACE
{
    partial class StepCountHelper
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
            InitialExpLabel = new Label();
            FixedStepCountLabel = new Label();
            OffsetLabel = new Label();
            StepsPerCycleLabel = new Label();
            InitialExpNum = new NumericUpDown();
            FixedStepCountNum = new NumericUpDown();
            OffsetNum = new NumericUpDown();
            StepsPerCycleNum = new NumericUpDown();
            SetFixedStepCount1 = new Button();
            SetFixedStepCount2 = new Button();
            SetInitialExpNum1 = new Button();
            SetInitialExpNum2 = new Button();
            CycleCountLabel = new Label();
            CycleCountNumLabel = new Label();
            ModLabel = new Label();
            ModNumLabel = new Label();
            TargetExpNum = new NumericUpDown();
            TargetExpLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)InitialExpNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FixedStepCountNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OffsetNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StepsPerCycleNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TargetExpNum).BeginInit();
            SuspendLayout();
            // 
            // InitialExpLabel
            // 
            InitialExpLabel.AutoSize = true;
            InitialExpLabel.Location = new Point(12, 11);
            InitialExpLabel.Name = "InitialExpLabel";
            InitialExpLabel.Size = new Size(95, 15);
            InitialExpLabel.TabIndex = 0;
            InitialExpLabel.Text = "預けた時の経験値";
            // 
            // FixedStepCountLabel
            // 
            FixedStepCountLabel.AutoSize = true;
            FixedStepCountLabel.Location = new Point(12, 69);
            FixedStepCountLabel.Name = "FixedStepCountLabel";
            FixedStepCountLabel.Size = new Size(55, 15);
            FixedStepCountLabel.TabIndex = 0;
            FixedStepCountLabel.Text = "基本歩数";
            // 
            // OffsetLabel
            // 
            OffsetLabel.AutoSize = true;
            OffsetLabel.Location = new Point(12, 98);
            OffsetLabel.Name = "OffsetLabel";
            OffsetLabel.Size = new Size(31, 15);
            OffsetLabel.TabIndex = 0;
            OffsetLabel.Text = "増減";
            // 
            // StepsPerCycleLabel
            // 
            StepsPerCycleLabel.AutoSize = true;
            StepsPerCycleLabel.Location = new Point(12, 127);
            StepsPerCycleLabel.Name = "StepsPerCycleLabel";
            StepsPerCycleLabel.Size = new Size(102, 15);
            StepsPerCycleLabel.TabIndex = 0;
            StepsPerCycleLabel.Text = "1サイクルあたり歩数";
            // 
            // InitialExpNum
            // 
            InitialExpNum.Location = new Point(120, 9);
            InitialExpNum.Maximum = new decimal(new int[] { 1250000, 0, 0, 0 });
            InitialExpNum.Name = "InitialExpNum";
            InitialExpNum.Size = new Size(63, 23);
            InitialExpNum.TabIndex = 1;
            InitialExpNum.Value = new decimal(new int[] { 13130, 0, 0, 0 });
            InitialExpNum.ValueChanged += InitialExpNum_ValueChanged;
            // 
            // FixedStepCountNum
            // 
            FixedStepCountNum.Location = new Point(120, 67);
            FixedStepCountNum.Maximum = new decimal(new int[] { 1250000, 0, 0, 0 });
            FixedStepCountNum.Name = "FixedStepCountNum";
            FixedStepCountNum.Size = new Size(63, 23);
            FixedStepCountNum.TabIndex = 1;
            FixedStepCountNum.Value = new decimal(new int[] { 1316, 0, 0, 0 });
            FixedStepCountNum.ValueChanged += FixedStepCountNum_ValueChanged;
            // 
            // OffsetNum
            // 
            OffsetNum.Location = new Point(120, 96);
            OffsetNum.Maximum = new decimal(new int[] { 1250000, 0, 0, 0 });
            OffsetNum.Name = "OffsetNum";
            OffsetNum.Size = new Size(63, 23);
            OffsetNum.TabIndex = 1;
            OffsetNum.ValueChanged += OffsetNum_ValueChanged;
            // 
            // StepsPerCycleNum
            // 
            StepsPerCycleNum.Location = new Point(120, 125);
            StepsPerCycleNum.Maximum = new decimal(new int[] { 1250000, 0, 0, 0 });
            StepsPerCycleNum.Name = "StepsPerCycleNum";
            StepsPerCycleNum.Size = new Size(63, 23);
            StepsPerCycleNum.TabIndex = 1;
            StepsPerCycleNum.Value = new decimal(new int[] { 14, 0, 0, 0 });
            StepsPerCycleNum.ValueChanged += StepsPerCycleNum_ValueChanged;
            // 
            // SetFixedStepCount1
            // 
            SetFixedStepCount1.Location = new Point(189, 65);
            SetFixedStepCount1.Name = "SetFixedStepCount1";
            SetFixedStepCount1.Size = new Size(58, 23);
            SetFixedStepCount1.TabIndex = 2;
            SetFixedStepCount1.Text = "セット";
            SetFixedStepCount1.UseVisualStyleBackColor = true;
            SetFixedStepCount1.Click += SetFixedStepCount1_Click;
            // 
            // SetFixedStepCount2
            // 
            SetFixedStepCount2.Location = new Point(253, 65);
            SetFixedStepCount2.Name = "SetFixedStepCount2";
            SetFixedStepCount2.Size = new Size(58, 23);
            SetFixedStepCount2.TabIndex = 3;
            SetFixedStepCount2.Text = "セット";
            SetFixedStepCount2.UseVisualStyleBackColor = true;
            SetFixedStepCount2.Click += SetFixedStepCount2_Click;
            // 
            // SetInitialExpNum1
            // 
            SetInitialExpNum1.Location = new Point(189, 7);
            SetInitialExpNum1.Name = "SetInitialExpNum1";
            SetInitialExpNum1.Size = new Size(58, 23);
            SetInitialExpNum1.TabIndex = 2;
            SetInitialExpNum1.Text = "セット";
            SetInitialExpNum1.UseVisualStyleBackColor = true;
            SetInitialExpNum1.Click += SetInitialExpNum1_Click;
            // 
            // SetInitialExpNum2
            // 
            SetInitialExpNum2.Location = new Point(253, 7);
            SetInitialExpNum2.Name = "SetInitialExpNum2";
            SetInitialExpNum2.Size = new Size(58, 23);
            SetInitialExpNum2.TabIndex = 3;
            SetInitialExpNum2.Text = "セット";
            SetInitialExpNum2.UseVisualStyleBackColor = true;
            SetInitialExpNum2.Click += SetInitialExpNum2_Click;
            // 
            // CycleCountLabel
            // 
            CycleCountLabel.AutoSize = true;
            CycleCountLabel.Font = new Font("Yu Gothic UI", 16F);
            CycleCountLabel.Location = new Point(12, 159);
            CycleCountLabel.Name = "CycleCountLabel";
            CycleCountLabel.Size = new Size(125, 30);
            CycleCountLabel.TabIndex = 4;
            CycleCountLabel.Text = "サイクル回数";
            // 
            // CycleCountNumLabel
            // 
            CycleCountNumLabel.Font = new Font("Yu Gothic UI", 16F);
            CycleCountNumLabel.Location = new Point(131, 159);
            CycleCountNumLabel.Name = "CycleCountNumLabel";
            CycleCountNumLabel.Size = new Size(52, 30);
            CycleCountNumLabel.TabIndex = 5;
            CycleCountNumLabel.Text = "0";
            CycleCountNumLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // ModLabel
            // 
            ModLabel.AutoSize = true;
            ModLabel.Font = new Font("Yu Gothic UI", 16F);
            ModLabel.Location = new Point(12, 199);
            ModLabel.Name = "ModLabel";
            ModLabel.Size = new Size(94, 30);
            ModLabel.TabIndex = 4;
            ModLabel.Text = "余り歩数";
            // 
            // ModNumLabel
            // 
            ModNumLabel.Font = new Font("Yu Gothic UI", 16F);
            ModNumLabel.Location = new Point(131, 199);
            ModNumLabel.Name = "ModNumLabel";
            ModNumLabel.Size = new Size(52, 30);
            ModNumLabel.TabIndex = 5;
            ModNumLabel.Text = "0";
            ModNumLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // TargetExpNum
            // 
            TargetExpNum.Location = new Point(120, 38);
            TargetExpNum.Maximum = new decimal(new int[] { 1250000, 0, 0, 0 });
            TargetExpNum.Name = "TargetExpNum";
            TargetExpNum.Size = new Size(63, 23);
            TargetExpNum.TabIndex = 1;
            TargetExpNum.Value = new decimal(new int[] { 15000, 0, 0, 0 });
            TargetExpNum.ValueChanged += TargetExpNum_ValueChanged;
            // 
            // TargetExpLabel
            // 
            TargetExpLabel.AutoSize = true;
            TargetExpLabel.Location = new Point(12, 40);
            TargetExpLabel.Name = "TargetExpLabel";
            TargetExpLabel.Size = new Size(67, 15);
            TargetExpLabel.TabIndex = 0;
            TargetExpLabel.Text = "目標経験値";
            // 
            // StepCountHelper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 248);
            Controls.Add(ModNumLabel);
            Controls.Add(CycleCountNumLabel);
            Controls.Add(ModLabel);
            Controls.Add(CycleCountLabel);
            Controls.Add(SetInitialExpNum2);
            Controls.Add(SetFixedStepCount2);
            Controls.Add(SetInitialExpNum1);
            Controls.Add(SetFixedStepCount1);
            Controls.Add(StepsPerCycleNum);
            Controls.Add(OffsetNum);
            Controls.Add(FixedStepCountNum);
            Controls.Add(TargetExpNum);
            Controls.Add(InitialExpNum);
            Controls.Add(StepsPerCycleLabel);
            Controls.Add(OffsetLabel);
            Controls.Add(FixedStepCountLabel);
            Controls.Add(TargetExpLabel);
            Controls.Add(InitialExpLabel);
            Name = "StepCountHelper";
            Text = "歩数カウントヘルパー";
            Load += StepCountHelper_Load;
            ((System.ComponentModel.ISupportInitialize)InitialExpNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)FixedStepCountNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)OffsetNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)StepsPerCycleNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)TargetExpNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InitialExpLabel;
        private Label FixedStepCountLabel;
        private Label OffsetLabel;
        private Label StepsPerCycleLabel;
        private NumericUpDown InitialExpNum;
        private NumericUpDown FixedStepCountNum;
        private NumericUpDown OffsetNum;
        private NumericUpDown StepsPerCycleNum;
        private Button SetFixedStepCount1;
        private Button SetFixedStepCount2;
        private Button SetInitialExpNum1;
        private Button SetInitialExpNum2;
        private Label CycleCountLabel;
        private Label CycleCountNumLabel;
        private Label ModLabel;
        private Label ModNumLabel;
        private NumericUpDown TargetExpNum;
        private Label TargetExpLabel;
    }
}