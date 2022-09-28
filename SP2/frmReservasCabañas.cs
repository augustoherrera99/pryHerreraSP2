using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryHerreraSP2
{
    public partial class frmReservasCabañas : Form
    {
        // constantes para los cálculos
        private const float TIPOA = 20;
        public const float TIPOB = 34;
        const float COCINA = 1;
        const float HELADERA = 1.5f;
        const float TELEVISOR = 2;
        const float PORPERSONA = 1;
        const float RECARGO_10 = 10; // porcentaje de recargo de las tarjetas
        const float RECARGO_20 = 20;

        public frmReservasCabañas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // cargar los items en el primer control combobox
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Tipo A");
            cmbTipo.Items.Add("Tipo B");
            // esta acción provocará el disparo del evento "SelectedIndexChanged"
            cmbTipo.SelectedIndex = 0; 
            // se inicializa la cantidad de días en 1
            txtDias.Text = "1";
            // inicialzar los demás controles de la interfaz
            chkCocina.Checked = false;
            chkHeladera.Checked = false;
            chkTelevisor.Checked = false;
            // en los radiobuttons se asigna sólo el que debe quedar en true
            optEfectivo.Checked = true; 
            txtNombre.Text = "";
            txtTelefonos.Text = "";
            // cargar los items del combo de tarjetas
            cmbTarjeta.Items.Clear();
            cmbTarjeta.Items.Add("Card Red");
            cmbTarjeta.Items.Add("Card Green");
            cmbTarjeta.Items.Add("Card Blue");
            // deshabiliar el botón "Aceptar"
            btnAceptar.Enabled = false;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cuando ocurra el evento se debe actualizar el 
            // contenido del combo de personas
            int I = 0;
            // limpiar los items de personas
            cmbPersonas.Items.Clear();
            // si es tipo de cabaña A ->> cargar los items del 1 al 4
            if (cmbTipo.SelectedIndex == 0) // o cmbTipo.SelectedItem == "Tipo A"
            {
                for (I = 1; I <= 4; I++)
                {
                    cmbPersonas.Items.Add(I);
                }
            } 
            else { // si es tipo de cabaña B ->> cargar los items del 1 al 8
                for (I = 1; I <= 8; I++)
                {
                    cmbPersonas.Items.Add(I);
                }
            }
            // establecer como preseleccionado el item 0 del combo
            cmbPersonas.SelectedIndex = 0;
        }

        private void optEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            // se deshabilita el combo y no se muestra nada
            cmbTarjeta.Enabled = false;
            cmbTarjeta.SelectedIndex = -1;
        }

        private void optTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            // se habilita el combo y se muestra el primer item
            cmbTarjeta.Enabled = true;
            cmbTarjeta.SelectedIndex = 0;
        }

        private void txtDias_TextChanged(object sender, EventArgs e)
        {
            // condiciones para habilitar o no el botón "Aceptar"
            if (txtDias.Text != "" && txtDias.Text != "0" && 
                txtNombre.Text != "" && txtTelefonos.Text != "")
            {
                btnAceptar.Enabled = true;
            } else
            {
                btnAceptar.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            // condiciones para habilitar o no el botón "Aceptar"
            if (txtDias.Text != "" && txtDias.Text != "0" && 
                txtNombre.Text != "" && txtTelefonos.Text != "")
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }
        }

        private void txtTelefonos_TextChanged(object sender, EventArgs e)
        {
            // condiciones para habilitar o no el botón "Aceptar"
            if (txtDias.Text != "" && txtDias.Text != "0" && 
                txtNombre.Text != "" && txtTelefonos.Text != "")
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            float PrecioBase;
            float Opcionales;
            float Recargo;
            int Dias;
            float Total;

            // obtener la cantidad de dìas ingresados
            Dias = int.Parse(txtDias.Text);
            // controlar el tipo de cabaña para determinar el precio base
            if (cmbTipo.SelectedIndex == 0)
            {
                PrecioBase = TIPOA;
            } else
            {
                PrecioBase = TIPOB;
            }
            // sumar al precio base el importe extra por persona (US$ 1)
            PrecioBase = PrecioBase + (PORPERSONA * int.Parse(cmbPersonas.Text));
            // controlar los adicionales por las opciones
            Opcionales = 0;
            if (chkCocina.Checked == true)
            {
                Opcionales = Opcionales + COCINA;
            }
            if (chkHeladera.Checked == true)
            {
                Opcionales = Opcionales + HELADERA;
            }
            if (chkTelevisor.Checked == true)
            {
                Opcionales = Opcionales + TELEVISOR;
            }
            // determinar el total por la cantidad de dìas
            Total = (PrecioBase + Opcionales) * Dias;

            // controlar la forma de pago
            if (optTarjeta.Checked == true)
            {
                // si es pago con tarjeta hay que agregar el recargo 
                if (cmbTarjeta.SelectedIndex == 0)
                {
                    Recargo = Total * RECARGO_10 / 100; // Card Red: recargo del 10%
                } else {
                    Recargo = Total * RECARGO_20 / 100; // las otras 2 tarjetas: recargo del 20%
                }
                Total = Total + Recargo; // obtener el total final
            }

            // mostrar el resultado ( el operador '+' se usa para concatenar 2 textos en el mensaje)
            MessageBox.Show("Total = US$ " + Total.ToString(), "Importe de la reserva",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // inicialzar los controles de la interfaz al estado inicial del formulario
            cmbTipo.SelectedIndex = 0;
            txtDias.Text = "1";
            chkCocina.Checked = false;
            chkHeladera.Checked = false;
            chkTelevisor.Checked = false;
            // en los radiobuttons se asigna sólo el que debe quedar en true
            optEfectivo.Checked = true; 
            txtNombre.Text = "";
            txtTelefonos.Text = "";
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
