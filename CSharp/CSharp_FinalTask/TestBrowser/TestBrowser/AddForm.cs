using BinarySearchTreeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestResultsBrowser
{
    public partial class AddForm : Form
    {
        public StudentData StudentData;
        public AddForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            this.ActiveControl = tbName;
            this.MinimumSize = new Size(500, 380);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var result = ValidateChildren();
            if (!result)
                return;

            this.DialogResult = DialogResult.OK;
            this.StudentData = new StudentData(tbName.Text, tbLastName.Text, tbTestName.Text, DateTime.Parse(dtpDateOfTest.Text), int.Parse(tbTestScore.Text));
            Close();
        }

        private void tbTestScore_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(tbTestScore.Text, out int score))
            {
                errorProvider1.SetError(tbTestScore, "Wrong test score");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbTestScore, null);
        }
        private void tbTestName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbTestName.Text))
            {
                errorProvider1.SetError(tbTestName, "Wrong test name");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbTestName, null);
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbLastName.Text))
            {
                errorProvider1.SetError(tbLastName, "Wrong last name");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbLastName, null);
        }
        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbName.Text))
            {
                tbName.Focus();
                errorProvider1.SetError(tbName, "Wrong name");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbName, null);
        }
    }
}
