namespace AppProyectoBD
{
    partial class ingresos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label9 = new System.Windows.Forms.Label();
            this.metodoPago = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.monto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textConcepto = new System.Windows.Forms.TextBox();
            this.labConcepto = new System.Windows.Forms.Label();
            this.cerrar = new System.Windows.Forms.Button();
            this.aceptar = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 21);
            this.label9.TabIndex = 74;
            this.label9.Text = "Método";
            // 
            // metodoPago
            // 
            this.metodoPago.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metodoPago.FormattingEnabled = true;
            this.metodoPago.Location = new System.Drawing.Point(140, 87);
            this.metodoPago.Name = "metodoPago";
            this.metodoPago.Size = new System.Drawing.Size(228, 29);
            this.metodoPago.TabIndex = 73;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(118, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 21);
            this.label8.TabIndex = 72;
            this.label8.Text = "$";
            // 
            // panel7
            // 
            this.panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(393, 30);
            this.panel7.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Gothic A1", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 30);
            this.label1.TabIndex = 33;
            this.label1.Text = "INGRESO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // monto
            // 
            this.monto.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monto.Location = new System.Drawing.Point(140, 131);
            this.monto.Name = "monto";
            this.monto.Size = new System.Drawing.Size(102, 24);
            this.monto.TabIndex = 71;
            this.monto.TextChanged += new System.EventHandler(this.monto_TextChanged);
            this.monto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.monto_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 21);
            this.label7.TabIndex = 70;
            this.label7.Text = "Monto";
            // 
            // textConcepto
            // 
            this.textConcepto.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textConcepto.Location = new System.Drawing.Point(140, 47);
            this.textConcepto.Multiline = true;
            this.textConcepto.Name = "textConcepto";
            this.textConcepto.Size = new System.Drawing.Size(228, 30);
            this.textConcepto.TabIndex = 69;
            // 
            // labConcepto
            // 
            this.labConcepto.AutoSize = true;
            this.labConcepto.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labConcepto.Location = new System.Drawing.Point(19, 49);
            this.labConcepto.Name = "labConcepto";
            this.labConcepto.Size = new System.Drawing.Size(66, 21);
            this.labConcepto.TabIndex = 68;
            this.labConcepto.Text = "Concepto";
            // 
            // cerrar
            // 
            this.cerrar.BackColor = System.Drawing.Color.Transparent;
            this.cerrar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cerrar.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cerrar.Location = new System.Drawing.Point(23, 178);
            this.cerrar.Name = "cerrar";
            this.cerrar.Size = new System.Drawing.Size(72, 34);
            this.cerrar.TabIndex = 66;
            this.cerrar.Text = "Cerrar";
            this.cerrar.UseVisualStyleBackColor = false;
            this.cerrar.Click += new System.EventHandler(this.cerrar_Click);
            // 
            // aceptar
            // 
            this.aceptar.BackColor = System.Drawing.Color.Transparent;
            this.aceptar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aceptar.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aceptar.Location = new System.Drawing.Point(296, 178);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(72, 34);
            this.aceptar.TabIndex = 65;
            this.aceptar.Text = "Aceptar";
            this.aceptar.UseVisualStyleBackColor = false;
            this.aceptar.Click += new System.EventHandler(this.aceptar_Click);
            // 
            // guardar
            // 
            this.guardar.BackColor = System.Drawing.Color.Transparent;
            this.guardar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.Font = new System.Drawing.Font("Gothic A1", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardar.Location = new System.Drawing.Point(296, 178);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(72, 34);
            this.guardar.TabIndex = 75;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // ingresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 229);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.metodoPago);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.monto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textConcepto);
            this.Controls.Add(this.labConcepto);
            this.Controls.Add(this.cerrar);
            this.Controls.Add(this.aceptar);
            this.Controls.Add(this.guardar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ingresos";
            this.Text = "ingresos";
            this.Load += new System.EventHandler(this.ingresos_Load);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox metodoPago;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox monto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textConcepto;
        private System.Windows.Forms.Label labConcepto;
        private System.Windows.Forms.Button cerrar;
        private System.Windows.Forms.Button aceptar;
        private System.Windows.Forms.Button guardar;
    }
}