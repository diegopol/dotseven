using Microsoft.Win32;
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
    public partial class LegalesWindow : Form
    {
        private bool Handled;
        public LegalesWindow()
        {
            InitializeComponent();
            
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Handled = true;
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", 1);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Handled = true;
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", 0);
            this.Close();
        }

        private void LegalesWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Handled)
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", 0);
        }


    }
}
