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
    public partial class StepCountHelper : Form
    {
        public StepCountHelper()
        {
            InitializeComponent();
        }
        int InitialExp = 1;
        int FixedStepCount = 1;
        int Offset = 1;
        int StepsPerCycle = 1;
        int TargetExp = 1;
        private void StepCountHelper_Load(object sender, EventArgs e)
        {
            InitialExpNum.Value = ConfigData.Instance.InitialExp1;
            FixedStepCountNum.Value = ConfigData.Instance.FixedStepCount1;
            OffsetNum.Value = 0;
            StepsPerCycleNum.Value = ConfigData.Instance.StepsPerCycle;
            SetInitialExpNum1.Text = ConfigData.Instance.InitialExp1.ToString();
            SetInitialExpNum2.Text = ConfigData.Instance.InitialExp2.ToString();
            SetFixedStepCount1.Text = ConfigData.Instance.FixedStepCount1.ToString();
            SetFixedStepCount2.Text = ConfigData.Instance.FixedStepCount2.ToString();
            InitialExp = (int)InitialExpNum.Value;
            FixedStepCount = (int)FixedStepCountNum.Value;
            Offset = (int)OffsetNum.Value;
            StepsPerCycle = (int)StepsPerCycleNum.Value;
            TargetExp = (int)TargetExpNum.Value;
            SetStepCounts();
        }

        private void SetInitialExpNum1_Click(object sender, EventArgs e)
        {
            InitialExpNum.Value = ConfigData.Instance.InitialExp1;
        }

        private void SetInitialExpNum2_Click(object sender, EventArgs e)
        {
            InitialExpNum.Value = ConfigData.Instance.InitialExp2;
        }

        private void SetFixedStepCount1_Click(object sender, EventArgs e)
        {
            FixedStepCountNum.Value = ConfigData.Instance.FixedStepCount1;
        }

        private void SetFixedStepCount2_Click(object sender, EventArgs e)
        {
            FixedStepCountNum.Value = ConfigData.Instance.FixedStepCount2;
        }

        private void InitialExpNum_ValueChanged(object sender, EventArgs e)
        {
            InitialExp = (int)InitialExpNum.Value;
            SetStepCounts();
        }
        private void TargetExpNum_ValueChanged(object sender, EventArgs e)
        {
            TargetExp = (int)TargetExpNum.Value;
            SetStepCounts();
        }
        private void FixedStepCountNum_ValueChanged(object sender, EventArgs e)
        {
            FixedStepCount = (int)FixedStepCountNum.Value;
            SetStepCounts();
        }

        private void OffsetNum_ValueChanged(object sender, EventArgs e)
        {
            Offset = (int)OffsetNum.Value;
            SetStepCounts();
        }

        private void StepsPerCycleNum_ValueChanged(object sender, EventArgs e)
        {
            StepsPerCycle = (int)StepsPerCycleNum.Value;
            SetStepCounts();
        }
        public void SetTargetExp(int exp)
        {
            TargetExp = exp;
            TargetExpNum.Value = exp;
            SetStepCounts();
        }
        private void SetStepCounts()
        {
            int requiredSteps = TargetExp - InitialExp;
            int TotalOffset = FixedStepCount + Offset;
            int AdjustedRequiredSteps = requiredSteps - TotalOffset;
            if (StepsPerCycle == 0)
                StepsPerCycle = 1;//0で割るのを防止
            int CycleCount = (int)Math.Floor((double)AdjustedRequiredSteps / StepsPerCycle);
            CycleCountNumLabel.Text = CycleCount.ToString();
            ModNumLabel.Text = (AdjustedRequiredSteps % StepsPerCycle).ToString();
        }
    }
}
