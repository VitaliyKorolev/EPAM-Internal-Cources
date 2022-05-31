
namespace DownloadManagerUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbLink = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dgvDownloads = new System.Windows.Forms.DataGridView();
            this.lblValidate = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloads)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(12, 84);
            this.tbLink.Name = "tbLink";
            this.tbLink.Size = new System.Drawing.Size(588, 23);
            this.tbLink.TabIndex = 0;
            this.tbLink.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(662, 84);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_ClickAsync);
            // 
            // dgvDownloads
            // 
            this.dgvDownloads.AllowUserToAddRows = false;
            this.dgvDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDownloads.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDownloads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownloads.Location = new System.Drawing.Point(12, 169);
            this.dgvDownloads.Name = "dgvDownloads";
            this.dgvDownloads.ReadOnly = true;
            this.dgvDownloads.RowTemplate.Height = 25;
            this.dgvDownloads.Size = new System.Drawing.Size(725, 272);
            this.dgvDownloads.TabIndex = 2;
            // 
            // lblValidate
            // 
            this.lblValidate.AutoSize = true;
            this.lblValidate.Location = new System.Drawing.Point(13, 125);
            this.lblValidate.Name = "lblValidate";
            this.lblValidate.Size = new System.Drawing.Size(0, 15);
            this.lblValidate.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 476);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(774, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 498);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblValidate);
            this.Controls.Add(this.dgvDownloads);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.tbLink);
            this.MinimumSize = new System.Drawing.Size(790, 537);
            this.Name = "MainForm";
            this.Text = "Download manager";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLink;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridView dgvDownloads;
        private System.Windows.Forms.Label lblValidate;
        private System.Windows.Forms.StatusStrip statusStrip;
    }
}

