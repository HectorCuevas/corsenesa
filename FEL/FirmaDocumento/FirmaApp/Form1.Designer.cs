namespace FirmaApp
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
            this.DragAndDrop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFirma = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.DragAndDrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // DragAndDrop
            // 
            this.DragAndDrop.AllowDrop = true;
            this.DragAndDrop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DragAndDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DragAndDrop.Controls.Add(this.label1);
            this.DragAndDrop.Location = new System.Drawing.Point(228, 12);
            this.DragAndDrop.Name = "DragAndDrop";
            this.DragAndDrop.Size = new System.Drawing.Size(398, 150);
            this.DragAndDrop.TabIndex = 0;
            this.DragAndDrop.Click += new System.EventHandler(this.DragAndDrop_Click);
            this.DragAndDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragAndDrop_DragDrop);
            this.DragAndDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragAndDrop_DragEnter);
            this.DragAndDrop.DragOver += new System.Windows.Forms.DragEventHandler(this.DragAndDrop_DragOver);
            this.DragAndDrop.DragLeave += new System.EventHandler(this.DragAndDrop_DragLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Click o arrastra el documento aqui ";
            // 
            // btnFirma
            // 
            this.btnFirma.Location = new System.Drawing.Point(228, 168);
            this.btnFirma.Name = "btnFirma";
            this.btnFirma.Size = new System.Drawing.Size(112, 31);
            this.btnFirma.TabIndex = 1;
            this.btnFirma.Text = "Firmar documentos";
            this.btnFirma.UseVisualStyleBackColor = true;
            this.btnFirma.Click += new System.EventHandler(this.btnFirma_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(157, 319);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 343);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnFirma);
            this.Controls.Add(this.DragAndDrop);
            this.Name = "Form1";
            this.Text = "Firma documento";
            this.DragAndDrop.ResumeLayout(false);
            this.DragAndDrop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DragAndDrop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFirma;
        private System.Windows.Forms.ListView listView1;
    }
}

