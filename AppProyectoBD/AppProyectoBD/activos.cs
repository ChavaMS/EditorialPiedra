using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppProyectoBD
{
	public partial class activos : Form
	{
        Conexion co;
        public bool aceptar = false;

		//VARIABLE PARA ENCONTRAR EL ID
		public int id;
		public int idPagos;

		//VARIABLES QUE ALMACENARÁN LA CONSULTA DEL MONTO, MÉTODO,CONCEPTO Y LA FECHA EN LA QUE SE REALIZÓ EL REGISTRO
		public string queryMonto, queryMetodo, queryConcepto, queryFecha;

		//VARIABLES QUE ALMACENARÁN LOS RESULTADOS DE LAS CONSULTAS HECHAS
		public string monto, metodo, concepto, fecha;

		//VARIABLE QUE ALMACENA EL SALDO
		public string saldo;


		public activos(Conexion co)
		{
			InitializeComponent();
            this.co = co;
			getActivosInfo();
			getSaldo();
		}

		public void getActivosInfo()
		{
			int rows = 0;

			co.Comando("SELECT COUNT(*) " +
						"FROM Ingresos as i " +
						"INNER JOIN pagos as p on(p.ingresoID = i.ID) " +
						"where p.FechaPago >= CURDATE();");

			if (co.LeerRead)
				rows = co.Leer.GetInt32(0);
			if (rows > 0)
				dataGridView1.RowCount = rows;

			else
			{
				dataGridView1.RowCount = 1;
				dataGridView1[0, 0].Value = "";
				dataGridView1[1, 0].Value = "";
				dataGridView1[2, 0].Value = "";
			}

			int i = 0;

			co.Comando("SELECT i.ID, p.Monto, DATE_FORMAT(p.FechaPago,'%d/%m/%Y') " +
						"FROM Ingresos AS i " + 
						"INNER JOIN Pagos as p on(p.ingresoID = i.ID);");

			while (co.LeerRead)
			{
				dataGridView1[0, i].Value = co.Leer.GetInt32(0);
				dataGridView1[1, i].Value = co.Leer.GetString(1);
				dataGridView1[2, i].Value = co.Leer.GetString(2);
				i++;
			}
		}

		public void getSaldo()
		{
			saldo = queryToString("SELECT saldo()");


                //EN CASO DE QUE EL SALDO SEA POSITIVO (ganancias)
                if ((Convert.ToInt32(saldo)) > 0)
                {
                    label7.ForeColor = System.Drawing.Color.Green;
                    label7.Text = "SALDO: $" + saldo;
                }

                else if ((Convert.ToInt32(saldo)) < 0)
                {
                    label7.Text = "SALDO: -$" + (Math.Abs(Convert.ToInt32(saldo))).ToString();
                    label7.ForeColor = System.Drawing.Color.Red;
                }


                else // SALDO = $0
                {
                    label7.Text = "SALDO: $" + 0;
                    label7.ForeColor = System.Drawing.Color.Orange;
                }
            

		
		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

        private void button2_Click(object sender, EventArgs e)
        {
            if (co.permiso.Equals(co.administrador))
            {
                ingresos ing = new ingresos(co, 1, 0, this);
                ing.ShowDialog();
            }
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
        }

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
            if (!dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.Equals(""))
            {
                id = (int)dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value;
                visualizar();
            }
		}

		private void btnEditar_Click(object sender, EventArgs e)
        {
            if (co.permiso.Equals(co.administrador))
            {
                try
                {
                    if (!dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.Equals(""))
                    {
						idPagos = Convert.ToInt32(queryToString("SELECT ID FROM Pagos WHERE ingresoID = " + id + ";"));
                        ingresos ing = new ingresos(co, 2, idPagos, this);
                        ing.ShowDialog();
                    }
                }
                catch (System.NullReferenceException)
                {
                    Form mensaje = new MessageBox("Seleccione un pago", 2);
                    mensaje.ShowDialog();
                }
            }
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (co.permiso.Equals(co.administrador))
            {
                try
                {
                    if (!dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.Equals(""))
                    {
                        MessageBox mens = new MessageBox("¿Seguro que desea eliminar el pago?", 1);
                        mens.ShowDialog();
                        if (aceptar)
                        {
                            //DELETE CASCADE 
                            co.Comando("DELETE FROM Ingresos WHERE ID = " + (int)dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value + ";");
							getActivosInfo();
							getSaldo();
                            id = 0;
                            aceptar = false;
                        }
                    }
                }
                catch (System.NullReferenceException)
                {
                    Form mensaje = new MessageBox("Seleccione un pago", 2);
                    mensaje.ShowDialog();
                }
            }
            else
            {
                AppProyectoBD.MessageBox mens = new AppProyectoBD.MessageBox("No cuenta con los permisos para realizar esta acción", 3);
                mens.ShowDialog();
            }
        }

        private void activos_SizeChanged(object sender, EventArgs e)
        {
            label8.Location = new Point(27, label8.Location.Y);
            dataGridView1.Location = new Point(27, dataGridView1.Location.Y);
            dataGridView1.Width = Convert.ToInt32(Math.Round(this.Width * .5138f));

            button3.Location = new Point(dataGridView1.Location.X + dataGridView1.Width - 66, button3.Location.Y);
            btnEditar.Location = new Point(button3.Location.X - 72, btnEditar.Location.Y);
            button2.Location = new Point(btnEditar.Location.X - 72, btnEditar.Location.Y);

            label7.Location = new Point(dataGridView1.Location.X + dataGridView1.Width + 36, label7.Location.Y);
            panel1.Location = new Point(dataGridView1.Location.X + dataGridView1.Width + 36, panel1.Location.Y);
            panel1.Width = Convert.ToInt32(Math.Round(this.Width * .40856));
        }

		private void activos_Load(object sender, EventArgs e)
		{
			Object idTabla = dataGridView1[0, 0].Value;
			if (!dataGridView1[0, 0].Value.Equals(""))
			{
				id = Convert.ToInt32(idTabla.ToString());
				visualizar();
			}
		}

		public void visualizar()
		{
            if (id != 0)
            {
                queryMonto = "SELECT Monto FROM Pagos " +
                             "WHERE ingresoID = " + id + ";";

                queryMetodo = "SELECT Metodo FROM Metodo as m " +
                              "INNER JOIN Pagos as p on(p.MetodoID = m.ID) " +
                              "where p.ingresoID = " + id + ";";

                queryConcepto = "SELECT Concepto FROM Ingresos WHERE ID = " + id + ";";

                queryFecha = "SELECT DATE_FORMAT(FechaPago,'%d/%m/%Y') FROM Pagos WHERE ingresoID = " + id + ";";

                monto = queryToString(queryMonto);
                metodo = queryToString(queryMetodo);
                concepto = queryToString(queryConcepto);
                fecha = queryToString(queryFecha);

                lblMonto.Text = "$"+monto;
                lblMetodo.Text = metodo;
                richTextBox1.Text = "  "+concepto;
                lblFecha.Text = fecha;
            }
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
	}
}
