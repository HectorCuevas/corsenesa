namespace AplicacionFirma
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCertificado = new System.Windows.Forms.Button();
            this.btnArchivo = new System.Windows.Forms.Button();
            this.txtRutaCertificado = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaArchivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFirma = new System.Windows.Forms.Button();
            this.lblMensae = new System.Windows.Forms.Label();
            this.btnAnulacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCertificado
            // 
            this.btnCertificado.Location = new System.Drawing.Point(31, 26);
            this.btnCertificado.Margin = new System.Windows.Forms.Padding(4);
            this.btnCertificado.Name = "btnCertificado";
            this.btnCertificado.Size = new System.Drawing.Size(168, 28);
            this.btnCertificado.TabIndex = 0;
            this.btnCertificado.Text = "Seleccionar certificado";
            this.btnCertificado.UseVisualStyleBackColor = true;
            this.btnCertificado.Click += new System.EventHandler(this.btnCertificado_Click);
            // 
            // btnArchivo
            // 
            this.btnArchivo.Location = new System.Drawing.Point(31, 127);
            this.btnArchivo.Margin = new System.Windows.Forms.Padding(4);
            this.btnArchivo.Name = "btnArchivo";
            this.btnArchivo.Size = new System.Drawing.Size(168, 28);
            this.btnArchivo.TabIndex = 1;
            this.btnArchivo.Text = "Seleccionar archivo";
            this.btnArchivo.UseVisualStyleBackColor = true;
            this.btnArchivo.Click += new System.EventHandler(this.btnArchivo_Click);
            // 
            // txtRutaCertificado
            // 
            this.txtRutaCertificado.Location = new System.Drawing.Point(207, 30);
            this.txtRutaCertificado.Margin = new System.Windows.Forms.Padding(4);
            this.txtRutaCertificado.Name = "txtRutaCertificado";
            this.txtRutaCertificado.Size = new System.Drawing.Size(479, 22);
            this.txtRutaCertificado.TabIndex = 2;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(207, 62);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(4);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(228, 22);
            this.txtContraseña.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Contraseña del certificado";
            // 
            // txtRutaArchivo
            // 
            this.txtRutaArchivo.Location = new System.Drawing.Point(207, 130);
            this.txtRutaArchivo.Margin = new System.Windows.Forms.Padding(4);
            this.txtRutaArchivo.Name = "txtRutaArchivo";
            this.txtRutaArchivo.Size = new System.Drawing.Size(479, 22);
            this.txtRutaArchivo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ruta del certificado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ruta del archivo";
            // 
            // btnFirma
            // 
            this.btnFirma.Location = new System.Drawing.Point(280, 174);
            this.btnFirma.Margin = new System.Windows.Forms.Padding(4);
            this.btnFirma.Name = "btnFirma";
            this.btnFirma.Size = new System.Drawing.Size(168, 28);
            this.btnFirma.TabIndex = 8;
            this.btnFirma.Text = "Firmar documento XML";
            this.btnFirma.UseVisualStyleBackColor = true;
            this.btnFirma.Click += new System.EventHandler(this.btnFirma_Click);
            // 
            // lblMensae
            // 
            this.lblMensae.AutoSize = true;
            this.lblMensae.Location = new System.Drawing.Point(27, 229);
            this.lblMensae.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensae.Name = "lblMensae";
            this.lblMensae.Size = new System.Drawing.Size(12, 17);
            this.lblMensae.TabIndex = 9;
            this.lblMensae.Text = " ";
            // 
            // btnAnulacion
            // 
            this.btnAnulacion.Location = new System.Drawing.Point(483, 177);
            this.btnAnulacion.Name = "btnAnulacion";
            this.btnAnulacion.Size = new System.Drawing.Size(165, 25);
            this.btnAnulacion.TabIndex = 10;
            this.btnAnulacion.Text = "Firmar Anulación XML";
            this.btnAnulacion.UseVisualStyleBackColor = true;
            this.btnAnulacion.Click += new System.EventHandler(this.btnAnulacion_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 290);
            this.Controls.Add(this.btnAnulacion);
            this.Controls.Add(this.lblMensae);
            this.Controls.Add(this.btnFirma);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRutaArchivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtRutaCertificado);
            this.Controls.Add(this.btnArchivo);
            this.Controls.Add(this.btnCertificado);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Firma de documentos FEL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCertificado;
        private System.Windows.Forms.Button btnArchivo;
        private System.Windows.Forms.TextBox txtRutaCertificado;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaArchivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFirma;
        private System.Windows.Forms.Label lblMensae;
        private System.Windows.Forms.Button btnAnulacion;
    }
}

