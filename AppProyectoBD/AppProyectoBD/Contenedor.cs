using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Threading;
using MySql.Data.MySqlClient;
using Inicio;
using PruebaA;

namespace AppProyectoBD
{
    public partial class Form1 : Form
    {
        bool estado;
        Form fh;
        bool inic, emp, trab, proy, pag,act,utilidades;
        int idUsr;
        int idSesion;

        Conexion co;
        public Form1(int idUsr, int idSesion,Conexion co)
        {
            InitializeComponent();
            Region = Funciones.redondear(Width, Height);
            this.idUsr = idUsr;
            this.idSesion = idSesion;
            this.co = co;
            inic = true;
            emp = false;
            trab = false;
            pag = false;
			act = false;
            estado = false;
            utilidades = false;
            //Coloca el frame en el centro
            this.StartPosition = FormStartPosition.CenterScreen;
            //Mando llamar el panel de inicio
            AbrirForm(new VentanaPrincipal(co));
        }
        
        public static class Util
        {
            public enum Effect {Roll, Slide, Center, Blend}
            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];

                if (ctl.Visible)
                {
                    flags |= 0x10000; angle += 180;
                }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }

                flags |= dirmap[(angle % 360) / 45];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
                
            }

            private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Se carga el usuario que ingreso         
            co.Comando("SELECT usr_login FROM Usuarios WHERE usr_id = "+ idUsr+";");
            if (co.LeerRead)
                usuario.Text = co.Leer.GetString(0);
            while (usuario.Width > sideBar.Width)
            {
                usuario.Font = new Font("Gothic A1", usuario.Font.Size - 0.5f, usuario.Font.Style);
            }
            int x = sideBar.Width / 2;
            usuario.Location = new Point(x - usuario.Width / 2, usuario.Location.Y);
        }


        private void menu_Click_1(object sender, EventArgs e)
        {
            Util.Animate(sideBar, Util.Effect.Roll, 40, 360);
        }


        private void pbCerrar_Click(object sender, EventArgs e)
        {
            //Actualiza la ultima sesión
            co.Comando("UPDATE Sesiones " +
                       "SET ses_fin = now() " +
                       "WHERE ses_id = " + idSesion + " and usr_id = " + idUsr + ";");
            Application.Exit();
        }

        private void pbMax_Click(object sender, EventArgs e)
        {

            if (estado == false)
            {
                this.WindowState = FormWindowState.Maximized;
                estado = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                estado = false;
            }

        }

        private void pbMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        } 
        
        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            Funciones.ReleaseCapture();
            Funciones.SendMessage(this.Handle, 0x112, 0xf012, 0);
           
        }

        private void empleados_Click(object sender, EventArgs e)
        {
            if (!emp)
            {
                lbTitulo.Text = "EMPLEADOS";
                AbrirForm(new Empleados(co));
                emp = true;
                inic = false;
                trab = false;
                proy = false;
                pag = false;
				act = false;
                utilidades = false;
            }
           
        }

        private void proyectos_Click(object sender, EventArgs e)
        {
            if (!proy)
            {
                lbTitulo.Text = "PROYECTOS";
                AbrirForm(new Proyectos(co));
                emp = false;
                inic = false;
                trab = false;
                proy = true;
                pag = false;
				act = false;
                utilidades = false;
            }
        }

        private void trabajos_Click(object sender, EventArgs e)
        {
            if (!trab)
            {
                lbTitulo.Text = "TRABAJOS";
                AbrirForm(new Trabajos(co));
                emp = false;
                inic = false;
                trab = true;
                proy = false;
                pag = false;
				act = false;
                utilidades = false;
            }
        }

		private void panel7_Paint(object sender, PaintEventArgs e)
		{

		}

		private void activos_Click(object sender, EventArgs e)
		{
			if (!act)
			{
				lbTitulo.Text = "ACTIVOS";
				AbrirForm(new activos(co));
				emp = false;
				inic = false;
				trab = false;
				proy = false;
				pag = false;
				act = true;
				utilidades = false;
			}
		}

		private void reportes_Click(object sender, EventArgs e)
        {
            if (!pag)
            {
                lbTitulo.Text = "PASIVOS";
                AbrirForm(new pagos(co));
                //Util.Animate(sideBar, Util.Effect.Roll, 150, 360);
                emp = false;
                inic = false;
                trab = false;
                proy = false;
                pag = true;
				act = false;
                utilidades = false;
            }
        }

        private void inicio_Click(object sender, EventArgs e)
        {
            if (!inic)
            {
                lbTitulo.Text = "INICIO";
                AbrirForm(new VentanaPrincipal(co));
                emp = false;
                inic = true;
                trab = false;
                proy = false;
                pag = false;
				act = false;
                utilidades = false;
            }
        }
        public void AbrirForm(object form2)
        {
            bool instanciaAbierta = false;
            if (!instanciaAbierta)
            {
                VentanaPrincipal frm2 = Application.OpenForms.OfType<VentanaPrincipal>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                Empleados frm2 = Application.OpenForms.OfType<Empleados>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                Proyectos frm2 = Application.OpenForms.OfType<Proyectos>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                Trabajos frm2 = Application.OpenForms.OfType<Trabajos>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                pagos frm2 = Application.OpenForms.OfType<pagos>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                Utilidades frm2 = Application.OpenForms.OfType<Utilidades>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (!instanciaAbierta)
            {
                activos frm2 = Application.OpenForms.OfType<activos>().FirstOrDefault();
                if (frm2 != null)//Si encuentra una instancia abierta
                {
                    frm2.Close();
                }
            }
            if (this.panelInfo.Controls.Count > 0)
                this.panelInfo.Controls.RemoveAt(0);
            fh = form2 as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelInfo.Controls.Add(fh);
            this.panelInfo.Tag = fh;
            fh.Show();
            fh.StartPosition = FormStartPosition.CenterParent;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!utilidades)
            {
                lbTitulo.Text = "UTILIDADES";
                AbrirForm(new Utilidades(co));
                emp = false;
                inic = false;
                trab = false;
                proy = false;
                pag = false;
				act = false;
                utilidades = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Actualiza la ultima sesion 
            co.Comando("UPDATE Sesiones " +
                       "SET ses_fin = now() " +
                       "WHERE ses_id = " + idSesion + " and usr_id = " + idUsr + ";");
           
            Login lo = new Login();
            lo.Show();
            this.Close();
        }
    }
}
