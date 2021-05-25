using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AppProyectoBD
{
    public partial class ingresos : Form
    {
        List<int> metodosID;
        List<string> metodosNom;
        Conexion co;
        int opcion;
        int id;
		activos form;

        //1 --- Crear pago --- 2 --- EditarPago
        public ingresos(Conexion co, int opcion, int id, activos form)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Region = Funciones.redondear(Width, Height);
			this.form = form;
            this.id = id;
            this.opcion = opcion;
            this.co = co;
            //Metodos de pago
            metodosID = new List<int>();
            metodosNom = new List<string>();
            co.Comando("SELECT ID FROM Metodo;");
            while (co.LeerRead)
                metodosID.Add(co.Leer.GetInt32(0));
            for (int i = 0; i < metodosID.Count; i++)
            {
                co.Comando("SELECT Metodo FROM Metodo WHERE ID = " + metodosID[i] + ";");
                if (co.LeerRead)
                    metodosNom.Add(co.Leer.GetString(0));
            }
            metodoPago.DataSource = metodosNom;
            if (opcion == 1)
            {
                guardar.Visible = false;
                aceptar.Visible = true;
            }
            else
            {
                guardar.Visible = true;
                aceptar.Visible = false;

                int metodoID = 0;
                co.Comando("SELECT i.Concepto, p.monto, p.metodoID FROM Ingresos as i INNER JOIN Pagos as p ON (i.ID = p.ingresoID)" +
                            "WHERE p.ID = "+id+";");
                if (co.LeerRead)
                {
                    textConcepto.Text = co.Leer.GetString(0);
                    monto.Text = co.Leer.GetInt32(1).ToString();
                    metodoID = co.Leer.GetInt32(2);
                }

                int i = 0;
                while (i < metodosID.Count)
                {
                    if (metodosID[i] == metodoID)
                        metodoPago.SelectedIndex = i;
                    i++;
                }
            }
        }

        public bool validacion()
        {
            if (!monto.Equals("") && !textConcepto.Equals(""))
                return true;
            return false;
        }
        private void aceptar_Click(object sender, EventArgs e)
        {
            if (validacion())
            {
                if (opcion == 1)
                {
                    co.Comando("INSERT INTO Ingresos (Concepto, ses_id) VALUES('" + textConcepto.Text + "'," + co.sesion + ")");
                    int maxIngreso = 0;

                    co.Comando("SELECT MAX(ID) FROM Ingresos;");
                    if (co.LeerRead)
                        maxIngreso = co.Leer.GetInt32(0);

                    co.Comando("INSERT INTO Pagos(numPago, Monto, ingresoID, MetodoID, FechaPago, ses_id) VALUES(1," + Convert.ToInt32(monto.Text)
                                           + "," + maxIngreso + "," + metodosID[metodoPago.SelectedIndex] + ",CURDATE(), " + co.sesion +  ");");

                    MessageBox mens = new MessageBox("Guardado con éxito", 2);
                    mens.ShowDialog();
					form.getActivosInfo();
                    form.getSaldo();
                    form.visualizar();
                    form.aceptar = false;
                }
                this.Close();
            }
            else
            {
                MessageBox mens = new MessageBox("Rellene los campos faltantes", 2);
                mens.ShowDialog();
            }
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monto_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            Funciones.ReleaseCapture();
            Funciones.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void ingresos_Load(object sender, EventArgs e)
        {

        }

        private void guardar_Click(object sender, EventArgs e)
        {

            if (validacion())
            {
                int idIngre = 0;
                //Se actualiza el pago
                co.Comando("CALL update_PagosMonto(" + Convert.ToInt32(monto.Text) + "," + metodosID[metodoPago.SelectedIndex] + "," + id + ", " + co.sesion + ");");
                //co.Comando("UPDATE Pagos SET Monto = "+ Convert.ToInt32(monto.Text) + ", MetodoID = "+ metodosID[metodoPago.SelectedIndex] + ", ses_id = "+co.sesion+" WHERE ID = "+id+";");


                //Selecciono el id del ingreso asociado al pago
                co.Comando("SELECT ingresoID FROM Pagos WHERE ID = " + id + ";");
                if (co.LeerRead)
                    idIngre = co.Leer.GetInt32(0);

                //Actualizo la info del ingreso
                co.Comando("UPDATE Ingresos SET Concepto = '" + textConcepto.Text + "', ses_id = " + co.sesion + " WHERE ID = " + idIngre + ";");


                MessageBox mens = new MessageBox("Guardado con éxito", 2);
                mens.ShowDialog();
				form.getActivosInfo();
                form.getSaldo();
                form.visualizar();
                this.Close();
            }
            else
            {
                MessageBox mens = new MessageBox("Rellene los campos faltantes", 2);
                mens.ShowDialog();
            }
        }
    }
}
