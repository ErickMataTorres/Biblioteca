using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Clases
{
    public class Alumno
    {
        public string Matrícula { get; set; }
        public string Nombre { get; set; }
        public string Carrera { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }

        public static DataTable TablaAlumnos()
        {
            DataTable t = new DataTable();
            SqlConnection conx = Clases.Conexion.Conectar();
            SqlCommand command = new SqlCommand("spTablaAlumnos", conx);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da=new SqlDataAdapter(command);
            da.Fill(t);
            conx.Close();
            return t;
        }

        public string Guardar()
        {
            string respuesta="Ok";
            SqlConnection conx = Clases.Conexion.Conectar();
            SqlCommand command = new SqlCommand("spGuardarAlumno", conx);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Matricula", Matrícula);
            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Carrera", Carrera);
            command.Parameters.AddWithValue("@Sexo", Sexo);
            command.Parameters.AddWithValue("@Edad", Edad);
            try
            {
                conx.Open();
                command.ExecuteNonQuery();
                conx.Close();
            }
            catch(Exception error)
            {
                respuesta= error.Message;
                if(conx.State==ConnectionState.Open)
                {
                    conx.Close();
                }
            }
            return respuesta;
        }

        public string Borrar(string matricula)
        {
            string respuesta = "Ok";
            SqlConnection conx = Clases.Conexion.Conectar();
            SqlCommand command = new SqlCommand("spBorrarAlumno", conx);
            command.CommandType= CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Matricula", matricula);
            try
            {
                conx.Open();
                command.ExecuteNonQuery();
                conx.Close();
            }
            catch(Exception error)
            {
                respuesta = error.Message;
                if(conx.State == ConnectionState.Open) 
                {
                    conx.Close();
                }
            }
            return respuesta;
        }
    }
}