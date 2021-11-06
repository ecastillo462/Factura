using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factura2021_1400.Modelos.DAO;
using Factura2021_1400.Modelos.Entidades;
using Factura2021_1400.Vistas;

namespace Factura2021_1400.Controladores
{
    public class UsuariosController
    {

        UsuarioDAO userDAO = new UsuarioDAO();
        Usuario user = new Usuario();
        UsuariosView vista;
        string operacion = string.Empty;
        public UsuariosController(UsuariosView view)
        {
            vista = view;
            vista.Nuevobutton.Click += new EventHandler(Nuevo);
            vista.Guardarbutton.Click += new EventHandler(Guardar);
            vista.Load += new EventHandler(Load);
            vista.Modificarbutton.Click += new EventHandler(Modificar);
            vista.Eliminarbutton.Click += new EventHandler(Eliminar);
        }

        private void Nuevo(object serder, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";
        }
        private void Guardar(object serder, EventArgs e)
        {
            if (vista.NombreTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Ingrese un nombre");
                vista.NombreTextBox.Focus();
                return;
            }
            if (vista.EmailTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailTextBox, "Ingrese un email");
                vista.EmailTextBox.Focus();
                return;
            }
            if (vista.ClaveTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.ClaveTextBox, "Ingrese una clave");
                vista.ClaveTextBox.Focus();
                return;
            }

            user.Nombre = vista.NombreTextBox.Text;
            user.Email = vista.EmailTextBox.Text;
            user.Clave = vista.ClaveTextBox.Text;
            user.EsAdministrador = vista.EsAdminCheckBox.Checked;       

            if (operacion =="Nuevo")
            {
                bool inserto = userDAO.InsertarNuevoUsuario(user);
                if (inserto)
                {
                    DeshabilitarControles();
                    LimpiarControles();
                    MessageBox.Show("Usuario creado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el usuario", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(operacion == "Modificar")
            {
                user.Id = Convert.ToInt32(vista.IdTextBox.Text);
                bool modifico = userDAO.ActualizarUsuario(user);
                if (modifico)
                {
                    DeshabilitarControles();
                    LimpiarControles();
                    MessageBox.Show("Usuario modificado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el usuario", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void Modificar(object serder, EventArgs e)
        {
            operacion = "Modificar";
            if (vista.UsuariosdataGridView.SelectedRows.Count > 0)
            {
                vista.IdTextBox.Text = vista.UsuariosdataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.UsuariosdataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                vista.EmailTextBox.Text = vista.UsuariosdataGridView.CurrentRow.Cells["EMAIL"].Value.ToString();
                vista.EsAdminCheckBox.Checked = Convert.ToBoolean(vista.UsuariosdataGridView.CurrentRow.Cells["ESADMINISTRADOR"].Value);
                HabilitarControles();
            }
        }
        private void Eliminar(object serder, EventArgs e)
        {
            operacion = "Eliminar";
            if (vista.UsuariosdataGridView.SelectedRows.Count > 0)
            {
                bool elimino = userDAO.EliminarUsuario(Convert.ToInt32(vista.UsuariosdataGridView.CurrentRow.Cells[0].Value.ToString()));
                if (elimino)
                {
                    DeshabilitarControles();
                    LimpiarControles();
                    MessageBox.Show("Usuario eliminado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUsuarios();
                }

            }
        }
        private void Load(object serder, EventArgs e)
        {
            ListarUsuarios();
        }
        public void ListarUsuarios()
        {
            vista.UsuariosdataGridView.DataSource = userDAO.GetUsuarios();
        }
        private void LimpiarControles()
        {
            vista.IdTextBox.Clear();
            vista.NombreTextBox.Clear() ;
            vista.EmailTextBox.Clear();
            vista.ClaveTextBox.Clear();
            vista.EsAdminCheckBox.Enabled = false;
        }
        private void HabilitarControles()
        {
            vista.IdTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.ClaveTextBox.Enabled = true;
            vista.EsAdminCheckBox.Enabled = true;

            vista.Guardarbutton.Enabled = true;
            vista.Cancelarbutton.Enabled = true;

            vista.Modificarbutton.Enabled = false;
            vista.Nuevobutton.Enabled = false;
        }
        private void DeshabilitarControles()
        {
            vista.IdTextBox.Enabled = false;
            vista.NombreTextBox.Enabled = false;
            vista.EmailTextBox.Enabled = false;
            vista.ClaveTextBox.Enabled = false;
            vista.EsAdminCheckBox.Enabled = false;

            vista.Guardarbutton.Enabled = false;
            vista.Cancelarbutton.Enabled = false;

            vista.Modificarbutton.Enabled = true;
            vista.Nuevobutton.Enabled = true;
        }
    }
}
