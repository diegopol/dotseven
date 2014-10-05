using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pescador.Core;

namespace Pescador.UI
{
    public partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void ConfigWindow_Load(object sender, EventArgs e)
        {
            var config = new ConfigurationManager();
            txtPassword.Text = config.ServicePassword;
            txtUsername.Text = config.ServiceUserName;
            txtUrl.Text = config.ServiceUrl;
            numCheck.Value = config.ServiceRetrySeconds;
            numTimeout.Value = (decimal)config.ServiceTimeout/1000;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var config = new ConfigurationManager
                             {
                                 ServicePassword = txtPassword.Text,
                                 ServiceUserName = txtUsername.Text,
                                 ServiceUrl = txtUrl.Text,
                                 ServiceRetrySeconds = (int) numCheck.Value,
                                 ServiceTimeout = (int) numTimeout.Value*1000
                             };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
