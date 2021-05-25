using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppProyectoBD
{
    public class Conexion
    {
        private MySqlConnection conect;
        private MySqlCommand codigo;
        private MySqlDataReader leer;
        private String permisos;
        private String sesFi;
        private String admin = "FFFF";

		private int sesionId;

        public Conexion(MySqlConnection conect)
        {
            this.conect = conect;
            conect.Open();
            codigo = new MySqlCommand("SELECT MAX(ses_fin) FROM Sesiones;", conect);
            leer = codigo.ExecuteReader();
            if(leer.Read())
                sesFi = leer.GetString(0);
        }
        public String administrador
        {
            get { return admin; }
        }

        public String permiso
        {
            get { return permisos; }
            set { permisos = value; }
        }

        public String ultSesion
        {
            get { return sesFi; }
        }

		public int sesion
		{
			get { return sesionId; }
			set { sesionId = value; }
		}

        public bool LeerRead
        {
            get { return leer.Read(); }
        }
        public MySqlDataReader Leer
        {
            get { return leer; }
        }
        public void Cerrar()
        {    
            conect.Close();
        }
        public void Comando(String com)
        {
            codigo =  new MySqlCommand((com),conect);
            leer.Close();
            leer = codigo.ExecuteReader();          
        }

        public void Comando(String com,String param, String dato)
        {
            codigo = new MySqlCommand((com), conect);
            codigo.Parameters.AddWithValue(param, dato);
            leer.Close();
            leer = codigo.ExecuteReader();
        }


    }
}
