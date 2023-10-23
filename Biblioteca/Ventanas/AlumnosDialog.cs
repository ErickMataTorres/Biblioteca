using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.Ventanas
{
    public partial class AlumnosDialog : Form
    {
        public AlumnosDialog(Clases.Alumno alumno)
        {
            InitializeComponent();
            if(alumno != null)
            {
                txtMatricula.Text = alumno.Matrícula;
                txtNombre.Text = alumno.Nombre;
                txtCarrera.Text = alumno.Carrera;
                cbSexo.SelectedItem = alumno.Sexo;
                txtEdad.Text = alumno.Edad.ToString();
                txtMatricula.ReadOnly = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AlumnosDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (txtMatricula.Text == string.Empty ||txtNombre.Text==string.Empty||txtCarrera.Text==string.Empty||cbSexo.SelectedItem==null||txtEdad.Text==string.Empty)
            {
                MessageBox.Show("Por favor ingresar todos los campos", "Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            Clases.Alumno alumno=new Clases.Alumno();
            alumno.Matrícula= txtMatricula.Text;
            alumno.Nombre=txtNombre.Text;
            alumno.Carrera=txtCarrera.Text;
            alumno.Sexo=cbSexo.SelectedItem.ToString()=="Mujer"?"M":"H";
            alumno.Edad = int.Parse(txtEdad.Text);
            string respuesta = alumno.Guardar();
            if (respuesta == "Ok")
            {
                MessageBox.Show("Se ha guardado correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error: " + respuesta);
            }
        }
    }
}
