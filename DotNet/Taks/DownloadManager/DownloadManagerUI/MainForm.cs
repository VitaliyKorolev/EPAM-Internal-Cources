using DownloadManagerLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadManagerUI
{
    public partial class MainForm : Form
    {
        private int id = 0;

        private int total = 0;
        private int completed = 0;
        private int inProgress = 0;
        private int inQueue = 0;
        private int error = 0;

        private const int kiloByte = 1024;
        private int taskNumber;

        ToolStripLabel totalDownloads;
        ToolStripLabel completedDownloads;
        ToolStripLabel inProgressDownloads;
        ToolStripLabel inQueueDownloads;
        ToolStripLabel errorDownloads;

        public MainForm()
        {
            InitializeComponent();
            DataGridViewProgressColumn progCol = new DataGridViewProgressColumn();
            progCol.ProgressBarColor = Color.Green;

            dgvDownloads.Columns.Add("Id", "Id");
            dgvDownloads.Columns.Add("FileName", "File name");
            dgvDownloads.Columns.Add("Status", "Status");
            dgvDownloads.Columns.Add("FileSize", "File size");
            dgvDownloads.Columns.Add(progCol);
            progCol.HeaderText = "Progress";
            progCol.Name = "Progress";

            dgvDownloads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDownloads.AllowUserToAddRows = false;

            totalDownloads = new ToolStripLabel();
            completedDownloads = new ToolStripLabel();
            inProgressDownloads = new ToolStripLabel();
            inQueueDownloads = new ToolStripLabel();
            errorDownloads = new ToolStripLabel();

            statusStrip.Items.Add(totalDownloads);
            statusStrip.Items.Add(completedDownloads);
            statusStrip.Items.Add(inProgressDownloads);
            statusStrip.Items.Add(inQueueDownloads);
            statusStrip.Items.Add(errorDownloads);

            taskNumber = Convert.ToInt32(ConfigurationManager.AppSettings["parallelTasksNumber"]);
        }

        private async void btnDownload_ClickAsync(object sender, EventArgs e)
        {
            lblValidate.Text = "";

            var downloadManager = new DownloadManager(taskNumber);
            int Id = id;
            bool progressIndicator = false;
            downloadManager.OnDownloadStarted += (s, e) =>
            {
                if (e.FileSize == 0)
                {
                    lblValidate.Text = e.Message;
                    return;
                }
                id++;
                int divizor = kiloByte;

                if (Convert.ToDouble(e.FileSize / divizor) > kiloByte)
                    divizor *= kiloByte; 

                if(Convert.ToDouble(e.FileSize / divizor) > kiloByte)
                    divizor *= kiloByte;

                string size;
                switch (divizor)
                {
                    case kiloByte:
                        size = ((double)e.FileSize / divizor).ToString("#.## Kb");
                        break;

                    case kiloByte * kiloByte:
                        size = ((double)e.FileSize / divizor).ToString("#.## Mb");
                        break;

                    case kiloByte * kiloByte * kiloByte:
                        size = ((double)e.FileSize / divizor).ToString("#.## Gb");
                        break;

                    default:
                        size = "";
                        break;
                }

                dgvDownloads.Rows.Add(Id, e.FileName,"In Queue", size,  0);
                totalDownloads.Text = $"Total downloads: {++total} ";
                inQueueDownloads.Text = $"In queue: {++inQueue} ";
            };

            downloadManager.OnDownloadProgressChanged += (s, e) =>
            {
                if(!progressIndicator)
                {
                    dgvDownloads.Rows[Id].Cells["Status"].Value = "In Progress";
                    inQueueDownloads.Text = $"In queue: {--inQueue} ";
                    inProgressDownloads.Text = $"In progress: {++inProgress} ";
                    progressIndicator = true;
                }

                dgvDownloads.Rows[Id].Cells["Progress"].Value = e.ProgressPercentage;
            };

            downloadManager.OnDownloadCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    if(dgvDownloads.Rows.Count != 0)
                    {
                        dgvDownloads.Rows[Id].Cells["Status"].Value = "Error";
                        inProgressDownloads.Text = $"In progress: {--inProgress} ";
                        errorDownloads.Text = $"Errors: {++error} ";
                    }
                }
                else
                {
                    dgvDownloads.Rows[Id].Cells["Status"].Value = "Completed";
                    inProgressDownloads.Text = $"In progress: {--inProgress} ";
                    completedDownloads.Text = $"Completed: {++completed} ";
                }
            };
            Uri uriResult = null;
            if(ValidateUrl(tbLink.Text,  out uriResult))
            {
                await downloadManager.DownloadFileAsync(uriResult);
            }
            else
            {
                lblValidate.Text = "Invalid Url";
            }
        }

        private bool ValidateUrl(string uri, out Uri uriResult)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.BeginInvoke(new Action(tbLink.SelectAll));
        }


    }
}
