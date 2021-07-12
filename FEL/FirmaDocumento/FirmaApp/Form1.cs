using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firma;
using System.IO;

namespace FirmaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] documentos;

        #region DragDrop
        private void DragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void DragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            documentos = (string[])e.Data.GetData(DataFormats.FileDrop);
            DragAndDrop.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void DragAndDrop_DragOver(object sender, DragEventArgs e)
        {
            DragAndDrop.BackColor = Color.Aqua;
        }

        private void DragAndDrop_DragLeave(object sender, EventArgs e)
        {
            DragAndDrop.BackColor = System.Drawing.SystemColors.ControlLight;
        }
        #endregion

        private void btnFirma_Click(object sender, EventArgs e)
        {
            documentos.ToList().ForEach(d => {
                FirmaDocumento.FirmarDocumento("C:\\temp\\Firma\\50510231.p12", "Prueba123", d, "C:\\temp\\Firma");
            });
        }

        private void DragAndDrop_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    documentos = openFileDialog.FileNames;
                }
            }
        }

        
    }
}
