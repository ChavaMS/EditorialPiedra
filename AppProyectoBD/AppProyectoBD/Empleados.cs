﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AppProyectoBD;

namespace PruebaA
{
	public partial class Empleados : Form
	{
		//PARA OBTENER EL ID DEL EMPLEADO DEL CUAL QUEREMOS VISUALIZAR INFORMACION
		//var idTabla;
		public static int id;
		public int idOtroForm;
		public string nombre = "";
		public string email = "";
		public string calle = "";
		public string colonia = "";
		public string cp;
		public string ciudad = "";
		public string estado = "";
		public string rfc = "";
		public string fnac = "";
		public int sexo;
		public string locImgTrab = "";

		//QUERIES PARA OBTENER CADA ELEMENTO
		public string queryNombre;
		public string queryEmail;
		public string queryCalle;
		public string queryColonia;
		public string queryCP;
		public string queryCiudad;
		public string queryEstado;
		public string queryRFC;
		public string queryFN;
		public string querySexo;
		public string queryImgEmpleado;


		bool clickEmpleado = false;

        Conexion co;
        public bool resultado2 = false;
		public Empleados(Conexion co)
		{
			//SE INICIALIZA LA CONEXION
			InitializeComponent();
            this.co = co;
			this.CenterToParent();


        }

		private void Empleados_Load(object sender, EventArgs e)
		{
			//CARGA LOS DATOS DEL EMPLEADO A LA TABLA
			tablaEmpleados.DataSource = getEmpleadosInfo();
            tablaEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            Object idTabla = tablaEmpleados[0, 0].Value;
            if (tablaEmpleados[0, 0].Value != null)
            {
                id = Convert.ToInt32(idTabla.ToString());
                idOtroForm = id;
				visualizarSimple();
            }
		}

		private DataTable getEmpleadosInfo()
		{
			//OBTIENE LOS DATOS DEL EMPLEADO GRACIAS A UN DATATABLE 
			DataTable dbEmpleados = new DataTable();

			string query = "SELECT emp.ID AS ID,emp.Nombre as NOMBRE,emp.email as EMAIL," +
							"t.Telefono as TELEFONO,(DATEDIFF(CURDATE(),emp.FechaNacimiento))DIV 365 as EDAD FROM Empleado as emp " +
							"INNER JOIN Telefonos as t ON(t.EmpleadoID = emp.ID)" +
							"GROUP BY NOMBRE;";
			try
			{
				//SE EJECUTA EL COMANDO Y SE PASA A LA TABLA CON LEER
				co.Comando(query);

				dbEmpleados.Load(co.Leer);
            }

			catch (Exception)
			{
                //System.Windows.Forms.MessageBox.Show("No se ha encontrado la información\nPor ende, no se puede mostrar");
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Error al mostrar la información", 3);
                mens.ShowDialog();
			}

			return dbEmpleados;

		}

		public void buscarEmpleado(string nombreEmpleado)
		{
			//FUNCION PARA BUSCAR AL EMPLEADO DEPENDIENDO DEL NOMBRE QUE SE OBTIENE
			DataTable tabla = new DataTable();

			string query = "SELECT emp.ID AS ID,emp.Nombre as NOMBRE,emp.email as EMAIL," +
					"t.Telefono as TELEFONO,(DATEDIFF(CURDATE(),emp.FechaNacimiento))DIV 365 as EDAD FROM Empleado as emp " +
					"INNER JOIN Telefonos as t ON(t.EmpleadoID = emp.ID) " +
					"WHERE emp.Nombre LIKE @NombreEmpleado GROUP BY NOMBRE;";

			try
			{
				co.Comando(query,"@NombreEmpleado",nombreEmpleado+"%");

				tabla.Load(co.Leer);
			}

			catch (Exception)
			{
                //System.Windows.Forms.MessageBox.Show("No se ha encontrado la información\nPor ende, no se puede mostrar");
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Error al mostrar la información", 3);
                mens.ShowDialog();
            }

			finally
			{
				//if (co != null) co.Cerrar();
				tablaEmpleados.DataSource = tabla;
			}
		}

		private void agregarEmpleado_Click(object sender, EventArgs e)
		{//SE ABRE UN NUEVO FORM DE REGISTRO
            if (co.permiso.Equals(co.administrador))
            {
                registroEmpleado form1 = new registroEmpleado(this, co);
                form1.ShowDialog();
            }
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
		}

		private void campoNombre_TextChanged(object sender, EventArgs e)
		{//UN EVENTO QUE NOS AYUDA A BUSCAR EL NOMBRE DEPENDIENDO DE LO QUE SE ESCRIBE
			buscarEmpleado(campoNombre.Text);
		}

		public void refreshTable()
		{//REFRESCA LA TABLA CADA QUE SE BUSCA A UN EMPLEADO
			tablaEmpleados.DataSource = getEmpleadosInfo();
		}

		public void refreshTableDELETE()
		{//REFRESCA LA TABLA CADA QUE SE BUSCA A UN EMPLEADO
			tablaEmpleados.DataSource = getEmpleadosInfo();
		}

		public string queryToString(string query)
		{//NOS TRANSFORMA EL RESULTADO DE UNA QUERY A STRING
			DataTable tabla = new DataTable();


			try
			{
				co.Comando(query);
				tabla.Load(co.Leer);
			}

			catch (Exception)
			{
                //System.Windows.Forms.MessageBox.Show("No se ha encontrado la información\nPor ende, no se puede mostrar");
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Error al mostrar la información", 3);
                mens.ShowDialog();
            }

			finally
			{
				//if (co != null) co.Cerrar();
				
			}

			//RETORNA EL RESULTADO DE LA CONSULTA EN STRING
			return tabla.Rows[0][0].ToString();
		}

		private void tablaEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//NO SE USA
		}

		private void btnEliminarEmp_Click(object sender, EventArgs e)
		{
			if (co.permiso.Equals(co.administrador))
			{
				try
				{
					idOtroForm = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
					id = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
					resultado2 = false;
					int numTrabajos = 0;
					int numProyectos = 0;
					string nombreEmpleado = "";
					co.Comando("SELECT COUNT(*) FROM Empleado_Trabajos WHERE EmpleadoID = " + id + ";");
					if (co.LeerRead)
						numTrabajos = co.Leer.GetInt32(0);

					co.Comando("SELECT Nombre FROM Empleado WHERE ID = " + id + ";");
					if (co.LeerRead)
						nombreEmpleado = co.Leer.GetString(0);

					co.Comando("SELECT COUNT(*) FROM Proyectos WHERE Encargado LIKE @NombreEmpleado;", "@NombreEmpleado", nombreEmpleado);
					if (co.LeerRead)
						numProyectos = co.Leer.GetInt32(0);



					if (!(numTrabajos > 0) && !(numProyectos > 0))
					{
						//Checar esto

						AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("¿Seguro que desea eliminar el elemento?", 1);
						mens.ShowDialog();
						if (resultado2)
						{
							//LO USAMOS PARA ELIMINAR A UN EMPLEADO SELECCIONAND DESDE LA TABLA DE EMPLEADOS
							string delete = "DELETE FROM Empleado WHERE ID = " + id + ";";
							try
							{
								co.Comando(delete);
                                resultado2 = false;
							}
							catch (MySqlException)
							{//en caso de no poder borrarse
								AppProyectoBD.MessageBox mens1 = new AppProyectoBD.MessageBox("Error al borrar el usuario", 3);
								mens1.ShowDialog();
							}

							finally
							{
								refreshTableDELETE();
							}
						}
					}
					else
					{
						AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("El empleado tiene trabajos o proyectos asociados", 2);
						mens.ShowDialog();
					}
				}

				//NullReferenceException
				catch (Exception ex)
				{
					AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Seleccione un elemento", 3);
					mens.ShowDialog();
				}
            }
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
			
		}

		private void btnEditar_Click(object sender, EventArgs e)
		{
            if(co.permiso.Equals(co.administrador))
                editar();
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
        }

        public void editar()
        {
            try
            {
                idOtroForm = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
                id = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
                //if (clickEmpleado)
                {
                    //OBTIENE LOS DATOS DEL EMPLEADO CON CONSULTAS, UTILZANDO LA FUNCION QUERYTOSTRING
                    queryNombre = "SELECT Nombre FROM Empleado WHERE ID = " + id + ";";
                    queryEmail = "SELECT email FROM Empleado WHERE ID = " + id + ";";
                    queryCalle = "SELECT Calle FROM Empleado WHERE ID = " + id + ";";
                    queryColonia = "SELECT Colonia FROM Empleado WHERE ID = " + id + ";";
                    queryCP = "SELECT CP FROM Empleado WHERE ID = " + id + ";";
                    queryCiudad = "SELECT Ciudad FROM Empleado WHERE ID = " + id + ";";
                    queryEstado = "SELECT Estado FROM Empleado WHERE ID = " + id + ";";
                    queryRFC = "SELECT RFC FROM Empleado WHERE ID = " + id + ";";
                    queryFN = "SELECT DATE_FORMAT(FechaNacimiento,'%d/%m/%Y') FROM Empleado WHERE ID = " + id + ";";
                    querySexo = "SELECT Sexo FROM Empleado WHERE ID = " + id + ";";
                    queryImgEmpleado = "SELECT imagenEmpleado FROM Empleado WHERE ID = " + id + ";";
                    //EL RESULTADO PASA A SER STRING CON LA FUNCION ANTERIORMENTE MENCIONADA Y SE ALMACENA EN UNA VARIABLE STRING
                    nombre = queryToString(queryNombre);
                    email = queryToString(queryEmail);
                    calle = queryToString(queryCalle);
                    colonia = queryToString(queryColonia);
                    cp = queryToString(queryCP);
                    ciudad = queryToString(queryCiudad);
                    estado = queryToString(queryEstado);
                    rfc = queryToString(queryRFC);
                    fnac = queryToString(queryFN);
                    locImgTrab = queryToString(queryImgEmpleado);
                    sexo = Convert.ToInt32(queryToString(querySexo));

                    //ABRE EL FORM DE EDICION 
                    edicionEmpleado form = new edicionEmpleado(this, co);

                    form.ShowDialog();
                }
            }
            catch (NullReferenceException ex)
            {
                //System.Windows.Forms.MessageBox.Show("Seleccione un elemento");
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Seleccione un elemento", 3);
                mens.ShowDialog();
            }
        }

		private void tablaEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}

        public void visualizar()
        {
            //SE OBTIENE EL ID DEL EMPLEADO QUE SE QUIERE VISUALIZAR O BORRAR
            try
            {
				idOtroForm = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
				id = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;

				queryNombre = "SELECT Nombre FROM Empleado WHERE ID = " + id + ";";
                queryEmail = "SELECT email FROM Empleado WHERE ID = " + id + ";";
                queryCalle = "SELECT Calle FROM Empleado WHERE ID = " + id + ";";
                queryColonia = "SELECT Colonia FROM Empleado WHERE ID = " + id + ";";
                queryCP = "SELECT CP FROM Empleado WHERE ID = " + id + ";";
                queryCiudad = "SELECT Ciudad FROM Empleado WHERE ID = " + id + ";";
                queryEstado = "SELECT Estado FROM Empleado WHERE ID = " + id + ";";
                queryRFC = "SELECT RFC FROM Empleado WHERE ID = " + id + ";";
                queryFN = "SELECT DATE_FORMAT(FechaNacimiento,'%d/%m/%Y') FROM Empleado WHERE ID = " + id + ";";
                querySexo = "SELECT Sexo FROM Empleado WHERE ID = " + id + ";";
                queryImgEmpleado = "SELECT imagenEmpleado FROM Empleado WHERE ID = " + id + ";";
                //EL RESULTADO PASA A SER STRING CON LA FUNCION ANTERIORMENTE MENCIONADA Y SE ALMACENA EN UNA VARIABLE STRING
                nombre = queryToString(queryNombre);
                email = queryToString(queryEmail);
                calle = queryToString(queryCalle);
                colonia = queryToString(queryColonia);
                cp = queryToString(queryCP);
                ciudad = queryToString(queryCiudad);
                estado = queryToString(queryEstado);
                rfc = queryToString(queryRFC);
                fnac = queryToString(queryFN);
                locImgTrab = queryToString(queryImgEmpleado);
                sexo = Convert.ToInt32(queryToString(querySexo));


                worker_project_registry form = new worker_project_registry(this);
                form.ShowDialog();
            }

            //EN CASO DE QUE NO SELECCIONE EL RENGLON
            catch (Exception)
            {
                //NO HACE NADA PARA NO ESTORBAR LA EXPERIENCIA DE USUARIO
            }
        }

		private void tablaEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
		{
            try
            {
                idOtroForm = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
                id = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
                visualizarSimple();
            }
            catch (Exception eh)
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("Seleccione un elemento", 3);
                mens.ShowDialog();
            }
            /*catch(InvalidCastException ef)
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No hay elementos", 3);
                mens.ShowDialog();
            }*/
            

                   
        }
        public void visualizarSimple()
        {
            //SE OBTIENE EL ID DEL EMPLEADO QUE SE QUIERE VISUALIZAR O BORRAR
            try
            {

				idOtroForm = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;
				id = (int)tablaEmpleados[0, tablaEmpleados.CurrentCell.RowIndex].Value;

				queryNombre = "SELECT Nombre FROM Empleado WHERE ID = " + id + ";";
                queryImgEmpleado = "SELECT imagenEmpleado FROM Empleado WHERE ID = " + id + ";";
                //EL RESULTADO PASA A SER STRING CON LA FUNCION ANTERIORMENTE MENCIONADA Y SE ALMACENA EN UNA VARIABLE STRING
                nombre = queryToString(queryNombre);
                locImgTrab = queryToString(queryImgEmpleado);
                int ren = 1;
                //Se colocan los trabajos del empleado
                co.Comando("SELECT COUNT(*) " +
                           "FROM Trabajos as t INNER JOIN Empleado_Trabajos AS et ON(et.TrabajosID = t.ID) " +
                           "INNER JOIN TipoTrabajos as tt ON(t.TipoTrabajosID = tt.ID) " +
                           "WHERE et.EmpleadoID = " + id + "; ");
                if (co.LeerRead)
                    ren = co.Leer.GetInt32(0);
                if (ren > 0)
                    dataGridView1.RowCount = ren;
                else
                {
                    dataGridView1.RowCount = 1;
                    dataGridView1[0, 0].Value = "";
                    dataGridView1[1, 0].Value = "";
                }

                co.Comando("SELECT t.Nombre, tt.NombreTipoTrab " +
                           "FROM Trabajos as t INNER JOIN Empleado_Trabajos AS et ON(et.TrabajosID = t.ID) " +
                           "INNER JOIN TipoTrabajos as tt ON(t.TipoTrabajosID = tt.ID) " +
                           "WHERE et.EmpleadoID = " + id + "; ");
                int i = 0;
                while (co.LeerRead)
                {
                    dataGridView1[0, i].Value = co.Leer.GetString(0);
                    dataGridView1[1, i].Value = co.Leer.GetString(1);
                    i++;
                }

                string tipoEmpleado = "| ";
                co.Comando("SELECT NombreTipo FROM TipoEmpleado AS te " +
                           "INNER JOIN Empleado_TipoEmpleado AS et ON(et.TipoEmpleadoID = te.ID) " +
                           "WHERE et.EmpleadoID = " + id + "; ");
                while (co.LeerRead)
                {
                    tipoEmpleado += co.Leer.GetString(0) + " | ";
                }

                //Se colocan el nombre, la imagen y su tipo de empleado
                nom.Text = nombre;
                imgTrab.ImageLocation = locImgTrab;
                tEmpleado.Text = tipoEmpleado;

				tEmpleado.Font = new Font("Gothic A1", 9.75f, tEmpleado.Font.Style);

				//Se adapta el tamaño del texto del label dependiendo del tamaño de la palabara
				while (tEmpleado.Width > panel1.Width)
                {
                    tEmpleado.Font = new Font("Gothic A1", tEmpleado.Font.Size - 0.5f, tEmpleado.Font.Style);
                }
            }

            //EN CASO DE QUE NO SELECCIONE EL RENGLON
            catch (Exception)
            {
                //NO HACE NADA PARA NO ESTORBAR LA EXPERIENCIA DE USUARIO
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            visualizar();

        }

        private void Empleados_SizeChanged(object sender, EventArgs e)
        {
            agregarEmpleado.Location = new Point(panel1.Location.X - 43, agregarEmpleado.Location.Y);
            btnEliminarEmp.Location = new Point(panel1.Location.X - 128, btnEliminarEmp.Location.Y);
            btnEditar.Location = new Point(panel1.Location.X - 200, btnEditar.Location.Y);

            tablaEmpleados.Location = new Point(40, tablaEmpleados.Location.Y);
            tablaEmpleados.Width = this.Width - 318;
        }
    }
}
