using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pescador.Core.Database;
using Pescador.UI.Properties;

namespace Pescador.UI
{
    public partial class TruckWindow : Form
    {
        /// <summary>
        /// Indica si estamos en Modo de Edición (True) o si es modo Nuevo (False)
        /// </summary>
        public bool EditorMode { get; set; }
        
        /// <summary>
        /// ID del camión a editar o del camión nuevo recientemente generado
        /// </summary>
        public int TruckID { get; set; }

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public TruckWindow()
        {
            InitializeComponent();
            EditorMode = false;
            this.LicencePlateTextBox.TextChanged += new System.EventHandler(this.LicencePlateTextBox_TextChanged);
            this.TruckID = -1;
            this.Text = "Nuevo Camión";
            ClearValidationErrors();
        }

        /// <summary>
        /// Construtor a utilizar para editar un camión existen
        /// </summary>
        /// <param name="editTruckID"></param>
        public TruckWindow(int editTruckID)
        {
            InitializeComponent();
            EditorMode = true;
            this.TruckID = editTruckID;
            LicencePlateTextBox.Enabled = false;
            this.Text = "Editar Camión";
            ClearValidationErrors();
        }

        /// <summary>
        /// Click sobre el boton de Salvar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTruckButton_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            this.UseWaitCursor = true;
            if (this.EditorMode == false)
                AddNewTruck();
            else
                EditTruck();

            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Load del Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckWindow_Load(object sender, EventArgs e)
        {
            FillDestinations();

            //Si es MODO NEW
            if (EditorMode == false)
            {
                using (var db = new PescadorDBEntities())
                {
                    var placas = db.Trucks
                                   .GroupBy(a => a.Plate)
                                   .Select(a => a.Key)
                                   .ToArray();

                    // Create the list to use as the custom source.  
                    var source = new AutoCompleteStringCollection();
                    source.AddRange(placas);

                    LicencePlateTextBox.AutoCompleteCustomSource = source;
                    LicencePlateTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    LicencePlateTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
            //Si es Modo EDIT
            else
            {
                using (var db = new PescadorDBEntities())
                {
                    var truckInfo = db.Trucks.FirstOrDefault(a => a.ID == this.TruckID);

                    LicencePlateTextBox.Text = truckInfo.Plate;
                    DriverDocumentNumberTextBox.Text = truckInfo.DriverDocumentNumber;
                    DriverMobileNumberTextBox.Text = truckInfo.DriverMobilePhone;
                    DriverNameTextBox.Text = truckInfo.DriveName;

                    foreach (var dest in truckInfo.Destinations)
                    {
                        int i = chkLstDestinations.Items.IndexOf(dest.Destination1);
                        if (i >= 0)
                        {
                            chkLstDestinations.SetItemChecked(i, true);
                        }
                    }

                    if (truckInfo.Status == "A")
                    //Si el camión ya fue asignado..entonces NO deberíamos poder editar nada
                    {
                        DisableTruckInputFields();
                        btnSave.Enabled = false;
                    }
                }
            }
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Cuando el usuario va escribiendo sobre el TextBox de Placa,. se dispara éste evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicencePlateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LicencePlateTextBox.Text.Length < 4)
                return;


            using (var db = new PescadorDBEntities())
            {
                if (db.Trucks.Any(a => a.Plate == LicencePlateTextBox.Text))
                {
                    var truckInfo = db.Trucks.Where(a => a.Plate == LicencePlateTextBox.Text).ToArray();

                    DriverDocumentNumberTextBox.Text = truckInfo.Last().DriverDocumentNumber;
                    DriverMobileNumberTextBox.Text = truckInfo.Last().DriverMobilePhone;
                    DriverNameTextBox.Text = truckInfo.Last().DriveName;
                }
                else
                {
                    DriverDocumentNumberTextBox.Text = string.Empty;
                    DriverMobileNumberTextBox.Text = string.Empty;
                    DriverNameTextBox.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Click sobre el boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click sobre el boton de agregar más destinos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDestinations_Click(object sender, EventArgs e)
        {
            var newDialog = new TextBoxDialogWindow();
            newDialog.ShowDialog();
            if (string.IsNullOrEmpty(newDialog.NewCity))
                return;

            if (chkLstDestinations.Items.IndexOf(newDialog.NewCity) != -1)
                return;

            chkLstDestinations.Items.Add(newDialog.NewCity, CheckState.Checked);
        }

        /// <summary>
        /// Validar si todos los campos estan correctos (si está OK devuelve: True, caso contrario: false)
        /// </summary>
        /// <returns>Si está OK devuelve: True, caso contrario: false</returns>
        private bool ValidateFields()
        {
            lblErrors.Text = string.Empty;
            if (LicencePlateTextBox.Text == "")
                ShowErrorValidation(Resources.TruckWindow_ValidateFields_Debe_indicar_la_placa_);

            if (chkLstDestinations.CheckedItems.Count == 0)
                ShowErrorValidation(Resources.TruckWindow_ValidateFields_Debe_indicar_un_destino_);

            return lblErrors.Text == string.Empty;
        }
        
        /// <summary>
        /// Agregar nuevo camión a la BD
        /// </summary>
        private void AddNewTruck()
        {
            // Get truck data from input fields
            var newTruck = new Truck
            {
                Plate = LicencePlateTextBox.Text,
                DriveName = DriverNameTextBox.Text,
                DriverDocumentNumber = DriverDocumentNumberTextBox.Text,
                DriverMobilePhone = DriverMobileNumberTextBox.Text,
                DateAdded = DateTime.Now,
                Status = "S"
            };

            // Disable input fields
            DisableTruckInputFields();

            // Save the truck
            using (var db = new PescadorDBEntities())
            {
                foreach (string selectedDest in chkLstDestinations.CheckedItems)
                {
                    var dbDest = db.Destinations.FirstOrDefault(d =>d.Destination1.Equals(selectedDest, StringComparison.InvariantCultureIgnoreCase));
                    //Si no lo encontramos en la BD, es porque es un nuevo destino que debemos de agregar a la BD
                    if (dbDest == null)
                    {
                        db.Destinations.Add(new Destination()
                        {
                            Destination1 = selectedDest
                        });
                        db.SaveChanges();
                        dbDest = db.Destinations.FirstOrDefault(d => d.Destination1.Equals(selectedDest, StringComparison.InvariantCultureIgnoreCase));
                    }
                    newTruck.Destinations.Add(dbDest);
                }

                // Add the truck to the database
                db.Trucks.Add(newTruck);
                db.SaveChanges();
                this.TruckID = newTruck.ID;
            }

            //UpdateGrid();

            // Enable input fields
            EnableTruckInputFields();
            this.Close();
        }

        /// <summary>
        /// Editar un camión sobre la BD
        /// </summary>
        private void EditTruck()
        {
            DisableTruckInputFields();
            using (var db = new PescadorDBEntities())
            {
                var truckInfo = db.Trucks.FirstOrDefault(a => a.ID == this.TruckID);
                truckInfo.DriveName = DriverNameTextBox.Text;
                truckInfo.DriverDocumentNumber = DriverDocumentNumberTextBox.Text;
                truckInfo.DriverMobilePhone = DriverMobileNumberTextBox.Text;
                truckInfo.Destinations.Clear();
                //truckInfo.Status = "A";

                foreach (string selectedDest in chkLstDestinations.CheckedItems)
                {
                    var dbDest = db.Destinations.FirstOrDefault(d => d.Destination1.Equals(selectedDest, StringComparison.InvariantCultureIgnoreCase));
                    
                    //Si no lo encontramos en la BD, es porque es un nuevo destino que debemos de agregar a la BD
                    if (dbDest == null)
                    {
                        db.Destinations.Add(new Destination()
                                                {
                                                    Destination1 = selectedDest
                                                });
                        db.SaveChanges();
                        dbDest = db.Destinations.FirstOrDefault(d => d.Destination1.Equals(selectedDest, StringComparison.InvariantCultureIgnoreCase));
                    }
                    truckInfo.Destinations.Add(dbDest);
                }

                db.SaveChanges();
            }
            EnableTruckInputFields();
            this.Close();

        }

        /// <summary>
        /// Desactivar todos los controles para edición
        /// </summary>
        private void DisableTruckInputFields()
        {
            this.LicencePlateTextBox.Enabled = false;
            this.DriverNameTextBox.Enabled = false;
            this.DriverDocumentNumberTextBox.Enabled = false;
            this.DriverMobileNumberTextBox.Enabled = false;
            this.chkLstDestinations.Enabled = false;
            this.btnAddDestinations.Enabled = false;
        }

        /// <summary>
        /// Activar todos los controles para edición
        /// </summary>
        private void EnableTruckInputFields()
        {
            this.LicencePlateTextBox.Enabled = true;
            this.DriverNameTextBox.Enabled = true;
            this.DriverDocumentNumberTextBox.Enabled = true;
            this.DriverMobileNumberTextBox.Enabled = true;
            this.chkLstDestinations.Enabled = true;
            this.btnAddDestinations.Enabled = true;
        }

        /// <summary>
        /// Cargar la lista de destinos guardado en la BD
        /// </summary>
        private void FillDestinations()
        {
            using (var db = new PescadorDBEntities())
            {
                chkLstDestinations.Items.Clear();
                foreach (var d in db.Destinations.OrderBy(a => a.Destination1))
                {
                    chkLstDestinations.Items.Add(d.Destination1);
                }
            }
        }

        /// <summary>
        /// Mostrar mensaje de error/validación
        /// </summary>
        /// <param name="message"></param>
        private void ShowErrorValidation(string message)
        {
            lblErrors.Text = message;
            this.Height = 427;
            lblErrors.Visible = true;
        }

        /// <summary>
        /// Eliminar todos los mensajes de errores/validaciones que se muestren en el formulario
        /// </summary>
        private void ClearValidationErrors()
        {
            lblErrors.Text = "";
            this.Height = 404;
            lblErrors.Visible = false;
        }
    }
}
