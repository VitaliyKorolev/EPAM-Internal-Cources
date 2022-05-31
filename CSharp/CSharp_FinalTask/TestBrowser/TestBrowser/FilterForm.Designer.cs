
namespace TestResultsBrowser
{
    partial class FilterForm
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
            this.components = new System.ComponentModel.Container();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbTestName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpLower = new System.Windows.Forms.DateTimePicker();
            this.dtpUpper = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLowerScore = new System.Windows.Forms.TextBox();
            this.tbUpperScore = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.cbScore = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(122, 22);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(275, 23);
            this.tbName.TabIndex = 0;
            // 
            // tbTestName
            // 
            this.tbTestName.Location = new System.Drawing.Point(122, 109);
            this.tbTestName.Name = "tbTestName";
            this.tbTestName.Size = new System.Drawing.Size(275, 23);
            this.tbTestName.TabIndex = 0;
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(122, 63);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(275, 23);
            this.tbLastName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last name";
            // 
            // dtpLower
            // 
            this.dtpLower.Location = new System.Drawing.Point(122, 156);
            this.dtpLower.Name = "dtpLower";
            this.dtpLower.Size = new System.Drawing.Size(111, 23);
            this.dtpLower.TabIndex = 2;
            // 
            // dtpUpper
            // 
            this.dtpUpper.Location = new System.Drawing.Point(286, 156);
            this.dtpUpper.Name = "dtpUpper";
            this.dtpUpper.Size = new System.Drawing.Size(111, 23);
            this.dtpUpper.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Test name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Date range";
            // 
            // tbLowerScore
            // 
            this.tbLowerScore.Location = new System.Drawing.Point(122, 203);
            this.tbLowerScore.Name = "tbLowerScore";
            this.tbLowerScore.Size = new System.Drawing.Size(111, 23);
            this.tbLowerScore.TabIndex = 0;
            this.tbLowerScore.Validating += new System.ComponentModel.CancelEventHandler(this.tbLowerScore_Validating);
            // 
            // tbUpperScore
            // 
            this.tbUpperScore.Location = new System.Drawing.Point(286, 203);
            this.tbUpperScore.Name = "tbUpperScore";
            this.tbUpperScore.Size = new System.Drawing.Size(111, 23);
            this.tbUpperScore.TabIndex = 0;
            this.tbUpperScore.Validating += new System.ComponentModel.CancelEventHandler(this.tbUpperScore_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "to";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Score range";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(195, 258);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(426, 160);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(94, 19);
            this.cbDate.TabIndex = 6;
            this.cbDate.Text = "Filter by date";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // cbScore
            // 
            this.cbScore.AutoSize = true;
            this.cbScore.Location = new System.Drawing.Point(426, 206);
            this.cbScore.Name = "cbScore";
            this.cbScore.Size = new System.Drawing.Size(99, 19);
            this.cbScore.TabIndex = 6;
            this.cbScore.Text = "Filter by score";
            this.cbScore.UseVisualStyleBackColor = true;
            this.cbScore.CheckedChanged += new System.EventHandler(this.cbScore_CheckedChanged);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 311);
            this.Controls.Add(this.cbScore);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dtpUpper);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpLower);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.tbUpperScore);
            this.Controls.Add(this.tbLowerScore);
            this.Controls.Add(this.tbTestName);
            this.Controls.Add(this.tbName);
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbTestName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpLower;
        private System.Windows.Forms.DateTimePicker dtpUpper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLowerScore;
        private System.Windows.Forms.TextBox tbUpperScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox cbScore;
        private System.Windows.Forms.CheckBox cbDate;
    }
}