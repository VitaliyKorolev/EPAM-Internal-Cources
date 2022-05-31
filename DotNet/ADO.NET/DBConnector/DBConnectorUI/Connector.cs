using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBConnectorLib;

namespace DBConnectorUI
{
    public partial class Connector : Form
    {
        public Connector()
        {
            InitializeComponent();
            cbProvider.DataSource = Enum.GetValues(typeof(DataProvider)).Cast<DataProvider>(); 
        }

        private async void btnConnect_ClickAsync(object sender, EventArgs e)
        {
            var connector = new DBConnector();
            string version = string.Empty;
            try
            {
                if (cbProvider.SelectedItem is DataProvider.SQL)
                {
                    version = await connector.ConnectAndGetVersionAsync(DataProvider.SQL, tbConnectionString.Text);
                }
                if (cbProvider.SelectedItem is DataProvider.OLE_DB)
                {
                    version = await connector.ConnectAndGetVersionAsync(DataProvider.OLE_DB, tbConnectionString.Text);
                }
                lblVersion.Text = $"MS SQL Server version : {version}";
            }
            catch (Exception ex)
            {
                lblVersion.Text = ex.Message;
            }
        }
    }
}
