using System.Collections.Generic;
using System.Media;
using System.Text;
using Pescador.Core;
using Pescador.Core.Database;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Pescador.Support.Exceptions;
using Pescador.UI.Properties;
using ConfigurationManager = Pescador.Core.ConfigurationManager;

namespace Pescador.UI
{
    public partial class MainWindow : Form
    {
        #region Propiedades, Eventos y Constructor

        /// <summary>
        /// Indica si se sufrió cambios en la configuración general de la aplicación
        /// </summary>
        public bool ConfigHasChanged { get; set; }
        
        /// <summary>
        /// Delegado para ser utilizado para intercambiar mensajes desde el Core
        /// </summary>
        /// <param name="log">Mensajes</param>
        public delegate void LogDelegate(List<string> log);

        /// <summary>
        /// Evento que sucede cuando el Core notifica con nuevos mensajes
        /// </summary>
        public event LogDelegate OnNewLogRecord;

        /// <summary>
        /// Lista de Logs del aplicativo
        /// </summary>
        public List<string> Log { get; set; }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public MainWindow()
        {
            // Initialize UI components
            InitializeComponent();

            this.Log = new List<string>();

            // Start peldar service thread
            var peldarServiceWorker = new BackgroundWorker();
            peldarServiceWorker.DoWork += PeldarServiceWorkerAsync;
            peldarServiceWorker.RunWorkerAsync();

            //Actualizamos la grilla
            UpdateGrid();
        } 
        #endregion

        #region Métodos asincrónicos que son ejecutados por otro Thread.
        /// <summary>
        /// Servicio encargado de realizar las operaciones generales de llamadas al Core.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeldarServiceWorkerAsync(object sender, DoWorkEventArgs e)
        {
            ChangeConnectionStatus(PeldarServicePage.EConnectionStatus.Offline, "Desconectado", "");
            SystemSounds.Exclamation.Play();
            while (true)
            {
                try
                {
                    // Get current truck service offers
                    this.CheckOffersAndBookAsync();
                }
                catch (ConnectionException ex)
                {
                    var logFileName = string.Format(@"{0}\ErrorLog.txt",
                                                    Application.StartupPath);
                    try
                    {
                        System.IO.File.WriteAllText(logFileName, ex.TraceContent);
                    }
                    catch (Exception)
                    {
                    }

                    ChangeConnectionStatus(PeldarServicePage.EConnectionStatus.Error, "Error de Conexión", string.Format("Ver detalle en {0}", logFileName));
                }
                catch (Exception ex)
                {
                    ChangeConnectionStatus(PeldarServicePage.EConnectionStatus.Error, "Error No Controlado", ex.Message);
                }
                //Intentamos en 30segundos
                Thread.Sleep(30000);
            }
        }

        /// <summary>
        /// Método encargado de consultar si existen ofertas, y si existen: realiza las reservas correspondientes
        /// </summary>
        private void CheckOffersAndBookAsync()
        {

            Thread.Sleep(2500);
            // Declare and initialize variables
            var config = new ConfigurationManager();
            string peldarUrl = config.ServiceUrl;
            int timeout = config.ServiceTimeout;
            int siteCheckSeconds = config.ServiceRetrySeconds;
            string username = config.ServiceUserName;
            string password = config.ServicePassword;
            var peldarWebSiteService = new PeldarServicePage(peldarUrl) { Timeout = timeout };
            

            try
            {
                //Seteamos el método delegado ánte el Evento de Notificaciones
                peldarWebSiteService.OnStatusChanged += OnConnectionChangeStatusAsync;
                peldarWebSiteService.RuntimePath = Application.StartupPath;

                // Authenticate to the peldar web site
                if (peldarWebSiteService.Authenticate(username, password))
                {
                    while (true)
                    {
                        //Verificamos si la configuracion general ha cambiado.
                        if (ConfigHasChanged)
                        {
                            ConfigHasChanged = false;
                            ChangeConnectionStatus(PeldarServicePage.EConnectionStatus.Offline, "Desconectado", "Se cambió los parámetros de configuración");
                            return;
                        }
                        //Obtener el resultado de una consulta de Ofertas y posible Reservación
                        peldarWebSiteService.Trace.Clear();
                        var reservationResult = peldarWebSiteService.CheckAndBook();

                        //Si tenemos nuevas reservas, entonces acutalizamos la Grilla
                        if (reservationResult.HaveReservations)
                        {
                            this.Invoke((MethodInvoker)delegate
                                                           {
                                                                lock (this)
                                                                {
                                                                    UpdateGrid();
                                                                }
                                                            }
                                );
                        }

                        //this.TryAgainOnSeconds = DateTime.Now.AddSeconds(t);
                        Thread.Sleep(1000 * siteCheckSeconds);
                    }
                }
                else
                    throw new SecurityException(1001, "Error al intentar de autenticarse");
            }
            catch (Exception ex)
            {
                //Si existen errores, procesamos la información para poder guardar bien la info en un archivo de texto.
                var newEx = new ConnectionException(100,
                                                                              "Error No controlado en CheckOffersAndBookAsync",
                                                                              ex);
                var contentToSave = new StringBuilder();
                Exception exDet = newEx;
                int i = 1;
                contentToSave.AppendLine(string.Format("DateTime: {0}", arg0: DateTime.Now.ToString()));
                contentToSave.AppendLine("");
                while (exDet != null)
                {
                    contentToSave.AppendLine(string.Format("{1}º ErrorMessage: {0}", exDet.Message, i));
                    contentToSave.AppendLine(string.Format("{1}º ErrorType: {0}", exDet.GetType().ToString(), i));
                    contentToSave.AppendLine("");
                    i++;
                    exDet = exDet.InnerException;
                }


                foreach (var t in peldarWebSiteService.Trace)
                {
                    contentToSave.AppendLine("");
                    contentToSave.AppendLine("#########################################################");
                    contentToSave.AppendLine(t.Title);
                    contentToSave.AppendLine("#########################################################");
                    contentToSave.AppendLine("");
                    contentToSave.AppendLine(t.Value);
                }
                newEx.TraceContent = contentToSave.ToString();
                throw newEx;
            }
        }

        /// <summary>
        /// Método que se invoca ante el evento de notificaciones del Core
        /// </summary>
        /// <param name="connStatus">Estao de la conexión</param>
        /// <param name="message"></param>
        /// <param name="subMessage"></param>
        private void OnConnectionChangeStatusAsync(PeldarServicePage.EConnectionStatus connStatus, string message,
                                                   string subMessage)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate()
                                                {
                                                    lock (this)
                                                    {
                                                        ChangeConnectionStatus(connStatus, message, subMessage);
                                                    }
                                                }
                    );
            }
            catch (Exception)
            {
            }
        }
        
        #endregion        
        
        #region Métodos
        /// <summary>
        /// Método que se invoca para el envío de notificaciones de cambio de estados
        /// </summary>
        /// <param name="connStatus"></param>
        /// <param name="message"></param>
        /// <param name="subMessage"></param>
        private void ChangeConnectionStatus(PeldarServicePage.EConnectionStatus connStatus, string message, string subMessage)
        {
            try
            {
                lock (this.Log)
                {
                    lock (ConnectionStatusLabel.Text)
                        ConnectionStatusLabel.Text = message;

                    lock (ConnectionStatusLabel.Image)
                    {
                        if (connStatus == PeldarServicePage.EConnectionStatus.Error)
                            ConnectionStatusLabel.Image = Resources.status_error_icon;
                        else if (connStatus == PeldarServicePage.EConnectionStatus.Offline)
                            ConnectionStatusLabel.Image = Resources.status_offline_icon;
                        else if (connStatus == PeldarServicePage.EConnectionStatus.OnLine)
                            ConnectionStatusLabel.Image = Resources.status_online_icon;
                    }
                    lock (ConnectionSubStatusLabel.Text)
                        ConnectionSubStatusLabel.Text = subMessage;




                    //TODO: TEMPORAL
                    if (subMessage.Contains("Consultando Peldar"))
                        return;

                    if (subMessage.Contains("No hay ofertas para los destinos publicados"))
                        return;
                    //TODO: TEMPORAL




                    string logToRecord = string.Format("{0} - {1}", DateTime.Now.ToString(), message);

                    if (subMessage != string.Empty)
                        logToRecord = string.Format("{0} - {1} - {2} ", DateTime.Now.ToString(), message, subMessage);

                    if (this.Log.Count > 250)
                        this.Log.RemoveAt(0);

                    this.Log.Add(logToRecord);

                    if (OnNewLogRecord != null)
                        OnNewLogRecord.Invoke(this.Log);



                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Actualizar la grilla con los datos de la BD e indicar qué item debe pre-seleccionarse luego de actualizado
        /// </summary>
        /// <param name="selectTruckId"></param>
        private void UpdateGrid(int selectTruckId = -1)
        {
            this.UseWaitCursor = true;
            int firstSelectionTag = 0;

            if (selectTruckId >= 0)
                firstSelectionTag = selectTruckId;
            else if (TrucksGridView.SelectedRows.Count == 1)
                firstSelectionTag = (int)TrucksGridView.SelectedRows[0].Tag;

            TrucksGridView.Rows.Clear();

            using (var db = new PescadorDBEntities())
            {
                //Traemos TODOS los camiones que NO fueron removidos lógicamente
                foreach (var truck in db.Trucks.Where(t => t.Status != "R").OrderBy(a => a.ID))
                {
                    var newRowIndex = TrucksGridView.Rows.Add();
                    var newRow = TrucksGridView.Rows[newRowIndex];

                    newRow.Cells[0].Value = truck.Plate;
                    newRow.Cells[1].Value = truck.DriveName;
                    newRow.Cells[2].Value = truck.DriverDocumentNumber;
                    newRow.Cells[3].Value = truck.DriverMobilePhone;

                    string destinos = "";
                    foreach (var des in truck.Destinations)
                    {
                        if (destinos == string.Empty)
                            destinos = des.Destination1;
                        else
                        {
                            destinos = destinos + ", " + des.Destination1;
                        }
                    }

                    newRow.Cells[4].Value = destinos;
                    newRow.Tag = truck.ID;
                    if (truck.ID == firstSelectionTag)
                        newRow.Selected = true;
                    switch (truck.Status)
                    {
                        case "S":
                            {
                                newRow.Cells[5].Value = "Sin Asignar";
                                break;
                            }
                        case "A":
                            {
                                newRow.Cells[5].Value = "Asignado";
                                newRow.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                                break;
                            }
                    }
                }
            }
            if (TrucksGridView.Rows.Count == 0)
            {
                btnEditTruck.Enabled = false;
                btnDeleteTruck.Enabled = false;
            }
            this.UseWaitCursor = false;

        }

        /// <summary>
        /// Desplegar el Form para Editar un Camión
        /// </summary>
        private void EditRow()
        {
            if (TrucksGridView.SelectedRows.Count != 1)
                return;

            this.UseWaitCursor = true;
            var truckId = (int)TrucksGridView.SelectedRows[0].Tag;
            var newTruckWindow = new TruckWindow(truckId);
            newTruckWindow.ShowDialog();
            UpdateGrid();
        }

        /// <summary>
        /// Desplegar el Form para Confirmar la eliminación un Camión
        /// </summary>
        private void DeleteRow()
        {
            if (TrucksGridView.SelectedRows.Count != 1)
                return;

            var truckId = (int)TrucksGridView.SelectedRows[0].Tag;

            var msgConfirmation = MessageBox.Show(
                                                    Resources.MainWindow_eliminarToolStripMenuItem_Click_,
                                                    Resources.MainWindow_eliminarToolStripMenuItem_Click_Eliminar_Camión,
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (msgConfirmation == DialogResult.Yes)
            {
                using (var db = new PescadorDBEntities())
                {
                    var truckToDelete = db.Trucks.FirstOrDefault(a => a.ID == truckId);
                    if (truckToDelete != null) truckToDelete.Status = "R";
                    db.SaveChanges();
                }
                UpdateGrid();
            }
        } 
        #endregion

        #region Métodos de Eventos
        /// <summary>
        /// Click sobre el botón de Agregar Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTruckClick(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            var newTruckWindow = new TruckWindow();
            newTruckWindow.ShowDialog();
            UpdateGrid(newTruckWindow.TruckID);
        }

        /// <summary>
        /// Click sobre el boton de Editar Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditTruckClick(object sender, EventArgs e)
        {
            EditRow();
        }

        /// <summary>
        /// Click sobre el menú desplegable para Editar Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditarToolStripMenuItemClick(object sender, EventArgs e)
        {
            EditRow();
        }

        /// <summary>
        /// Click sobre el menú desplegable para Eliminar un Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliminarToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteRow();
        }

        /// <summary>
        /// Doble click sobre un item de la grilla para Editar un Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrucksGridViewCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditRow();
        }

        /// <summary>
        /// Click sobre el boton de Eliminar Camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteTruckClick(object sender, EventArgs e)
        {
            DeleteRow();
        }

        /// <summary>
        /// Refrescado de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrucksGridViewPaint(object sender, PaintEventArgs e)
        {
            if (TrucksGridView.Rows.Count > 0)
            {
                btnEditTruck.Enabled = true;
                btnDeleteTruck.Enabled = true;
            }
            else
            {
                btnEditTruck.Enabled = false;
                btnDeleteTruck.Enabled = false;

            }

        }

        /// <summary>
        /// Click sobre el Status Bar, para abrir la ventana de Log de Conexión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionStatusLabelClick(object sender, EventArgs e)
        {
            var connWindow = new ConnectionDetailWindow(this);
            connWindow.Show();
        }

        /// <summary>
        /// Click sobre el botón de About para abrir la ventana de About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            var aboutW = new AboutWindow();
            aboutW.ShowDialog();
        }

        /// <summary>
        /// Click sobre el boton de Config (Tools), para abrir la ventana de Configuraciones Generales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            var configWindow = new ConfigWindow();
            //Si la persona presionó "Guardar", entonces....
            if (configWindow.ShowDialog() == DialogResult.OK)
            {
                //Mostramos el mensaje que nos desconectamos, para que se vuelva a recargar el servicio a Peldar
                ChangeConnectionStatus(PeldarServicePage.EConnectionStatus.Offline, "Desconectado", "Se cambió los parámetros de configuración");
                lock (this)
                    this.ConfigHasChanged = true;
            }
        }

        #endregion


    }
}
