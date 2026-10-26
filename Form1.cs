using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroPacientes
{
    public partial class Form1 : Form
    {
        // Cola de prioridad (lista enlazada ordenada por prioridad y orden de llegada).
        private ColaPacientes cola = new ColaPacientes();

        public Form1()
        {
            InitializeComponent();

            cmbPrioridad.Items.Add("P1");
            cmbPrioridad.Items.Add("P2");
            cmbPrioridad.Items.Add("P3");

            cmbPrioridad.SelectedIndex = -1;
        }


        // Muestra los pacientes pendientes en el orden en que seran atendidos.
        private void MostrarPendientes()
        {
            lstPacientes.Items.Clear();
            foreach (Paciente p in cola.ObtenerPendientes())
            {
                lstPacientes.Items.Add(p.ToString());
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            if (nombre == "")
            {
                MessageBox.Show(
                    "Debe escribir el nombre del paciente.",
                    "Dato requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNombre.Focus();
                return;
            }

            if (cmbPrioridad.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Debe seleccionar una prioridad: P1, P2 o P3.",
                    "Dato requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbPrioridad.Focus();
                return;
            }

            string prioridad = cmbPrioridad.SelectedItem.ToString();

            cola.Encolar(nombre, prioridad);
            MostrarPendientes();

            MessageBox.Show(
                "Paciente registrado correctamente.",
                "Registro exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            txtNombre.Clear();
            cmbPrioridad.SelectedIndex = -1;
            txtNombre.Focus();
        }
    }
}
