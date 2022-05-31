using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageDownloaderLib;
using NLog;

namespace ImageDownloaderUI
{
    public partial class MainForm : Form
    {
        private readonly CancellationTokenSource cts;
        private readonly Downloader downloader;
        private Logger logger;
        public MainForm(Logger logger)
        {
            InitializeComponent();
            this.logger = logger;
            cts = new CancellationTokenSource();
            downloader = new Downloader(logger);
            downloader.OnDownloadStarted += (s, e) =>
            {
                labelProgress.Text = "Response was received";
            };

            downloader.OnDownloadProgressChanged += (s, e) =>
            {
                labelPercents.Text = $"{e.ProgressPercentage} %";
                progressBar1.Value = e.ProgressPercentage;
                progressBar1.Value = e.ProgressPercentage + 1;
            };

            downloader.OnDownloadImageCompleted += (s, e) =>
            {
                if (e.Cancelled == true)
                    labelProgress.Text = "Download was cancelled";
                else
                {
                    labelProgress.Text = "Download completed";
                }

            };

            StartPosition = FormStartPosition.CenterScreen;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            progressBar1.Maximum = 101;
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbLink.Text))
            {
                labelProgress.Text = "Paste your link";
                return;
            }
            try 
            {
                btnDownload.Enabled = false;
                btnClear.Enabled = false;
                var image = await downloader.DownloadImageTaskAsync(tbLink.Text, cts.Token);
                pictureBox.Image = image;
                labelSize.Text = $"Size of image : {pictureBox.Image?.Height} x {pictureBox.Image?.Width}";
                
            }
            catch (ArgumentException ex)
            {
                labelProgress.Text = ex.Message;
            }
            finally
            {
                btnDownload.Enabled = true;
                btnClear.Enabled = true;
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            tbLink.Text = string.Empty;
            labelProgress.Text = string.Empty;
            labelSize.Text = string.Empty;
            progressBar1.Value = progressBar1.Minimum;
            labelPercents.Text = string.Empty;
            logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] All fieldes of the form were cleared");
        }

        private void tbLink_EnterAsync(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.BeginInvoke(new Action(textBox.SelectAll));
        }
    }
}
