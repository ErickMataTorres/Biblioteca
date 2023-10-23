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
    public partial class Alumnos : Form
    {
        public Alumnos()
        {
            InitializeComponent();
        }

        private void Alumnos_Load(object sender, EventArgs e)
        {
            dgvAlumnos.DataSource = Clases.Alumno.TablaAlumnos();
            dgvAlumnos.Columns["Carrera"].Visible = false;
            dgvAlumnos.Columns["Sexo"].Visible = false;
            dgvAlumnos.Columns["Edad"].Visible = false;
            dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlumnos.RowsDefaultCellStyle.BackColor = Color.LightCyan;
            dgvAlumnos.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        }

        private void dgvAlumnos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtCarrera.Text = dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Carrera"].Value.ToString();
            txtSexo.Text = dgvAlumnos.Rows[e.RowIndex].Cells["Sexo"].Value.ToString() == "M" ? "Mujer" : "Hombre";
            txtEdad.Text = dgvAlumnos.Rows[e.RowIndex].Cells["Edad"].Value.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Ventanas.AlumnosDialog ventana=new Ventanas.AlumnosDialog(null);
            ventana.ShowDialog();
            if(ventana.DialogResult== DialogResult.OK)
            {
                Alumnos_Load(sender, e);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvAlumnos.Rows.Count < 1)
            {
                MessageBox.Show("No existe ningún registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Clases.Alumno alumno = new Clases.Alumno();

            alumno.Matrícula = dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Matrícula"].Value.ToString();
            alumno.Nombre = dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            alumno.Carrera = dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Carrera"].Value.ToString();
            alumno.Sexo = dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Sexo"].Value.ToString()=="M"?"Mujer":"Hombre";
            alumno.Edad = int.Parse(dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Edad"].Value.ToString());

            Ventanas.AlumnosDialog ventana = new Ventanas.AlumnosDialog(alumno);
            ventana.ShowDialog();
            if(ventana.DialogResult == DialogResult.OK)
            {
                Alumnos_Load(sender, e);
            }
        }

        private void dgvAlumnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar_Click(sender, e);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(dgvAlumnos.Rows.Count < 1)
            {
                MessageBox.Show("No existe ningún registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (MessageBox.Show("¿Estás seguro de borrar el alumno?\n" + dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Nombre"].Value, "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Clases.Alumno c = new Clases.Alumno();
                string respuesta = c.Borrar(dgvAlumnos.Rows[dgvAlumnos.CurrentRow.Index].Cells["Matrícula"].Value.ToString());
                if (respuesta=="Ok")
                {
                    MessageBox.Show("Se ha borrado correctamente", "Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Alumnos_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error: " + respuesta);
                }
                
            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            
            BuscarPorTexto($"Matrícula + Nombre + Carrera + Sexo + Edad LIKE '%"+txtBuscar.Text+"%'");
        }

        private void BuscarPorTexto(string texto)
        {
            if (dgvAlumnos.DataSource != null)
            {
                DataView dv = new DataView((DataTable)dgvAlumnos.DataSource);
                dv.RowFilter = texto;
                dgvAlumnos.DataSource = dv.ToTable();
            }
        }
    }
}
