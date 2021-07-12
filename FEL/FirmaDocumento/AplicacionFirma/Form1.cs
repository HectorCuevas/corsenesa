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

namespace AplicacionFirma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string certificatePath;
        string filePath;

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "p12 files (*.p12)|*.p12*";
                openFileDialog.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    certificatePath = openFileDialog.FileName;
                    txtRutaCertificado.Text = certificatePath;
                }
            }
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml*";
                openFileDialog.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    txtRutaArchivo.Text = filePath;
                }
            }
        }

        private void btnFirma_Click(object sender, EventArgs e)
        {
            if (certificatePath == "" || certificatePath == null)
            {
                MessageBox.Show("Se debe seleccionar el certifiado para realizar la firma");
                return;
            }

            if (txtContraseña.Text == "" || txtContraseña.Text == null)
            {
                MessageBox.Show("Se debe ingresar la contraseña del certificado");
                return;
            }

            if (filePath == "" || filePath == null)
            {
                MessageBox.Show("Se debe seleccionar un archivo xml para firmar");
                return;
            }
            
            var destinationPath = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            try
            {
                FirmaDocumento.FirmarDocumento(certificatePath, txtContraseña.Text, filePath, destinationPath);
            }
            catch (Exception es)
            {
                lblMensae.Text = "Error en la firma del documento, error reportado: " + es.Message;
                lblMensae.ForeColor = Color.Red;
                return;
            }

            lblMensae.Text = string.Format("Documento Firmado correctamente, ubicado en: {0}/{1}-Firmado.xml", destinationPath, fileName);
            lblMensae.ForeColor = Color.Green;
        }

        private void btnAnulacion_Click(object sender, EventArgs e)
        {
            if (certificatePath == "" || certificatePath == null)
            {
                MessageBox.Show("Se debe seleccionar el certifiado para realizar la firma");
                return;
            }

            if (txtContraseña.Text == "" || txtContraseña.Text == null)
            {
                MessageBox.Show("Se debe ingresar la contraseña del certificado");
                return;
            }

            if (filePath == "" || filePath == null)
            {
                MessageBox.Show("Se debe seleccionar un archivo xml para firmar");
                return;
            }

            var destinationPath = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            try
            {
                FirmaDocumento.FirmarAnulacion(certificatePath, txtContraseña.Text, filePath, destinationPath);
            }
            catch (Exception es)
            {
                lblMensae.Text = "Error en la firma del documento, error reportado: "+ es.Message;
                lblMensae.ForeColor = Color.Red;
                return;
            }

            lblMensae.Text = string.Format( "Documento Firmado correctamente, ubicado en: {0}/{1}-Firmado.xml", destinationPath, fileName);
            lblMensae.ForeColor = Color.Green;
        }

    }
}
