using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using System.Xml.Linq;
using System.IO;
using System.Data;
using Modelos;
using Firma;
namespace FELFactura
{
    public class XMLAnular
    {
        private DataSet dstcancelxml = new DataSet();
        private Anular anular = new Anular();
        string v_rootxml = "";
        string fac_num = "";
        public String getXML(string XMLCancel, string path, string fac_num)
        {

            v_rootxml = path;
            this.fac_num = fac_num;
            //convertir a dataset los string para mayor manupulacion
            XmlToDataSet(XMLCancel);
            //llenar estructuras
            ReaderDataset();

            //armar xml
            getXML();

            //firmar xml por certificado
            var nombre = fac_num.Trim() + ".xml";
            v_rootxml = v_rootxml + @"\" + nombre;

            XmlDocument myXML = FirmaDocumento.FirmarDocumentoAnulacion(Constants.URL_CERTIFICADO, Constants.URL_CERTIFICADO_CONTRASENIA,
               path, nombre, path);
            String data =getPostData(myXML.InnerXml);
            return data ;

        }


        private String getPostData(String data)
        {

            string uuid = Guid.NewGuid().ToString().ToUpper();
            String request = "<?xml version='1.0' encoding='UTF-8'?>\n" +
              "<AnulaDocumentoXMLRequest id=\"" + uuid + "\">\n" +
                        "<xml_dte>" +
                            " <![CDATA[" + data + "]]>\n" +
                            "</xml_dte>\n" +
                            "</AnulaDocumentoXMLRequest>";

            return request;
        }
        //Convertir XML a DataSet
        private bool XmlToDataSet(string XMLCancel)
        {
            try
            {

                System.IO.StringReader rdinvoice = new System.IO.StringReader(XMLCancel);
                dstcancelxml.ReadXml(rdinvoice);

                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();

                String path = "C:\\xml\\Anulados\\errores.txt";
                File.WriteAllText(path, ex.ToString());
                return false;
            }
        }


        //Lectura de Documentos
        private void ReaderDataset()
        {

            LlenarEstructuras.DatosAnular(dstcancelxml, anular);
        }





        private String getXML()
        {
            XNamespace dte = XNamespace.Get("http://www.sat.gob.gt/dte/fel/0.1.0");
            XNamespace xd = XNamespace.Get("http://www.w3.org/2000/09/xmldsig#");
            //Encabezado del Documento
            XDeclaration declaracion = new XDeclaration("1.0", "utf-8", "no");

            //GTDocumento
            XElement parameters = new XElement(dte + "GTAnulacionDocumento",
                            new XAttribute(XNamespace.Xmlns + "ns", dte.NamespaceName),
                           new XAttribute(XNamespace.Xmlns + "xd", xd.NamespaceName),
                           new XAttribute("Version", "0.1"));
            //SAT
            XElement SAT = new XElement(dte + "SAT");
            parameters.Add(SAT);

            // formando dte
            XElement DTE = new XElement(dte + "AnulacionDTE", new XAttribute("ID", "DatosCertificados"));
            SAT.Add(DTE);

            //datos de emision
   
            //datos generales
            XElement DatosGenerales = new XElement(dte + "DatosGenerales", new XAttribute("ID", "DatosAnulacion"),
                new XAttribute("NumeroDocumentoAAnular", this.anular.uuid),
                 new XAttribute("NITEmisor", this.anular.NITEmisor),
                 new XAttribute("IDReceptor", this.anular.IDReceptor),
                 new XAttribute("FechaEmisionDocumentoAnular", this.anular.FechaEmisionDocumentoAnular),
                 new XAttribute("FechaHoraAnulacion", this.anular.FechaHoraAnulacion),
                 new XAttribute("MotivoAnulacion", this.anular.MotivoAnulacion));
            DTE.Add(DatosGenerales);
            

            XDocument myXML = new XDocument(declaracion, parameters);
            String res = myXML.ToString();
          
            try
            {
                v_rootxml = string.Format(@"{0}\{1}.xml", v_rootxml, fac_num.Trim());
                if (!File.Exists(v_rootxml))
                {

                    myXML.Save(v_rootxml);
                }
                else
                {
                    System.IO.File.Delete(v_rootxml);
                    myXML.Save(v_rootxml);
                }
            }
            catch (Exception ex)
            {
                String path2 = "C:\\xml\\Anulados\\errores.txt";
                File.WriteAllText(path2, ex.ToString());
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "docelec.txt";
                System.IO.File.WriteAllText(path, ex.Message);
            }
            return res;
          }



        }


}