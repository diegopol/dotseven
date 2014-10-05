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
    public partial class ConnectionDetailWindow : Form
    {
        /// <summary>
        /// Información del último log
        /// </summary>
        public string LastLog { get; set; }

        /// <summary>
        /// Referencia al Form padre que contendrá a éste form
        /// </summary>
        public MainWindow ParentWindow { get; set; }
        
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="parentWindow"></param>
        public ConnectionDetailWindow(MainWindow parentWindow)
        {
            this.ParentWindow = parentWindow;
            InitializeComponent();
                    }

        /// <summary>
        /// Evento que sucede ánte un nuevo Log lanzado por el Parent
        /// </summary>
        /// <param name="Log"></param>
        private void ConnectionDetailWindow_OnNewLogRecord(List<string> Log)
        {
            try
            {
                lock (Log)
                {
                    int lastSel = lstLog.SelectedIndex;
                    if (Log.Count > 0)
                    {
                        if (Log.LastOrDefault() != LastLog)
                        {
                            lstLog.Items.Clear();
                            foreach (var i in Log)
                                lstLog.Items.Insert(0,i);

                            LastLog = Log.LastOrDefault();
                        }
                    }
                    lstLog.SelectedIndex = lastSel;
                }
            }
            catch (Exception)
            {
            }

        }

        /// <summary>
        /// Evento Load del Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionDetailWindow_Load(object sender, EventArgs e)
        {
            ConnectionDetailWindow_OnNewLogRecord(this.ParentWindow.Log);
        }

        /// <summary>
        /// Por cáda 1 segundo, se dispara éste evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ConnectionDetailWindow_OnNewLogRecord(this.ParentWindow.Log);
        }
    }
}
