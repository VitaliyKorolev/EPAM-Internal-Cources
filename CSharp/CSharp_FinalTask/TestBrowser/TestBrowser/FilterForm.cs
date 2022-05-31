using BinarySearchTreeLib;
using FilterLib;
using Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestResultsBrowser
{
    public partial class FilterForm : Form
    {

        public FilterService Filter;
        public FilterForm(FilterService filter)
        {
            InitializeComponent();
            Filter = filter;
            dtpLower.Enabled = false;
            dtpUpper.Enabled = false;
            tbLowerScore.Enabled = false;
            tbUpperScore.Enabled = false;

            cbScore.CausesValidation = false;
            cbDate.CausesValidation = false;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            this.MinimumSize = new Size(560, 350);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var result = ValidateChildren(ValidationConstraints.Enabled);
            if (!result)
                return;

            this.DialogResult = DialogResult.OK;

            if (!string.IsNullOrEmpty(tbName.Text))
            {
                Filter.AddCondition("Name", FilterMethods.PropertyEqualsToValue, tbName.Text);
            }
            if (!string.IsNullOrEmpty(tbLastName.Text))
            {
                Filter.AddCondition("LastName", FilterMethods.PropertyEqualsToValue, tbLastName.Text);
            }
            if (!string.IsNullOrEmpty(tbTestName.Text))
            {
                Filter.AddCondition("TestName", FilterMethods.PropertyEqualsToValue, tbTestName.Text);
            }
            if (cbDate.Checked)
            {
                Filter.AddCondition("DateOfTest", FilterMethods.PropertyInRange, dtpLower.Text, dtpUpper.Text);
            }
            if (cbScore.Checked)
            {
                Filter.AddCondition("TestScore", FilterMethods.PropertyInRange, tbLowerScore.Text, tbUpperScore.Text);
            }
            Close();
        }

        private void tbLowerScore_Validating(object sender, CancelEventArgs e)
        {
            if (cbScore.Checked && !int.TryParse(tbLowerScore.Text, out int score))
            {
                errorProvider1.SetError(tbLowerScore, "Wrong test score");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbLowerScore, null);
        }

        private void tbUpperScore_Validating(object sender, CancelEventArgs e)
        {
            if (cbScore.Checked && !int.TryParse(tbUpperScore.Text, out int score))
            {
                errorProvider1.SetError(tbUpperScore, "Wrong test score");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbUpperScore, null);
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if(!dtpLower.Enabled && !dtpUpper.Enabled)
            {
                dtpLower.Enabled = true;
                dtpUpper.Enabled = true;
            }
            else
            {
                dtpLower.Enabled = false;
                dtpUpper.Enabled = false;
                errorProvider1.Clear();
            }
        }

        private void cbScore_CheckedChanged(object sender, EventArgs e)
        {
            if(!tbLowerScore.Enabled && !tbUpperScore.Enabled)
            {
                tbLowerScore.Enabled = true;
                tbUpperScore.Enabled = true;
            }
            else
            {
                tbLowerScore.Enabled = false;
                tbUpperScore.Enabled = false;
                errorProvider1.Clear();
            }
        }
    }
}
