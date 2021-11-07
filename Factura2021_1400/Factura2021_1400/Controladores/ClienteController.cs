using Factura2021_1400.Modelos.DAO;
using Factura2021_1400.Modelos.Entidades;
using Factura2021_1400.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factura2021_1400.Controladores
{
    public class ClienteController
    {
        ClientesView vista;
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();
        string operacion = string.Empty;

        public ClienteController(ClientesView view)
        {
            vista = view;
            vista.Nuevobutton.Click += new EventHandler(Nuevo);
            vista.Guardarbutton.Click += new EventHandler(Guardar);
        }


        private void Nuevo(object sender, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";
        }
        private void Guardar(object sender, EventArgs e)
        {
            if (vista.IdentidadMaskedtextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.IdentidadMaskedtextBox, "Ingrese una identidad");
                vista.IdentidadMaskedtextBox.Focus();
                return;
            }
            if (vista.NombretextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombretextBox, "Ingrese un nombre");
                vista.NombretextBox.Focus();
                return;
            }
            if (vista.EmailtextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailtextBox, "Ingrese un email");
                vista.EmailtextBox.Focus();
                return;
            }
            if (vista.DirecciontextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DirecciontextBox, "Ingrese unu dirección");
                vista.DirecciontextBox.Focus();
                return;
            }

            cliente.Identidad = vista.IdentidadMaskedtextBox.Text;
            cliente.Nombre = vista.NombretextBox.Text;
            cliente.Email = vista.EmailtextBox.Text;
            cliente.Direccion = vista.DirecciontextBox.Text;

            if (operacion == "Nuevo")
            {
                bool inserto = clienteDAO.InsertarNuevoCliente(cliente);
                if (inserto)
                {
                    MessageBox.Show("Cliente creado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo insertar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void HabilitarControles()
        {
            vista.IdentidadMaskedtextBox.Enabled = true;
            vista.NombretextBox.Enabled = true;
            vista.DirecciontextBox.Enabled = true;
            vista.EmailtextBox.Enabled = true;

            vista.Guardarbutton.Enabled = true;
            vista.Cancelarbutton.Enabled = true;
            vista.Imagenbutton.Enabled = true;
            vista.Modificarbutton.Enabled = false;
            vista.Nuevobutton.Enabled = false;

        }
    }
}
