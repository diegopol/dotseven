using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pescador.UI
{
    public partial class TextBoxDialogWindow : Form
    {
        internal string NewCity { get; set; }

        public TextBoxDialogWindow()
        {
            InitializeComponent();
            ClearValidationErrors();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            ClearValidationErrors();
            if (txtCity.Text == "")
            {
                ShowErrorValidation("Debe indicar una ciudad válida.");
                return;
            }
            this.NewCity = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCity.Text.ToLower());
            this.Close();
        }

        private void ShowErrorValidation(string message)
        {
            lblErrors.Text = message;
            this.Height = 140;
            lblErrors.Visible = true;
        }
        private void ClearValidationErrors()
        {
            lblErrors.Text = "";
            this.Height = 120;
            lblErrors.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
