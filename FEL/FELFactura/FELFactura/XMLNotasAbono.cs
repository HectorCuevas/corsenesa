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
    public class XMLNotasAbono : IDocumentRegister
    {
        
        private DataSet dstinvoicexml = new DataSet();
        private DataSet dstdetailinvoicexml = new DataSet();
        private DatosGenerales datosGenerales = new DatosGenerales();
        private Emisor emisor = new Emisor();
        private Receptor receptor = new Receptor();
        private List<Item> items = new List<Item>();
        private Totales totales = new Totales();
        string v_rootxml ="";
        string fac_num = "";
        public String getXML(string XMLInvoice, string XMLDetailInvoce, string path, string fac_num)
        {
            
            v_rootxml = path;
            this.fac_num = fac_num;
            //convertir a dataset los string para mayor manupulacion
            XmlToDataSet( XMLInvoice,  XMLDetailInvoce);
            //llenar estructuras
            ReaderDataset();
            
            //armar xml
            getXML();
            
            //firmar xml por certificado
            var nombre = fac_num.Trim() + ".xml";
            v_rootxml = v_rootxml + @"\" + nombre;

            XmlDocument myXML = FirmaDocumento.FirmarDocumento(Constants.URL_CERTIFICADO, Constants.URL_CERTIFICADO_CONTRASENIA, path, nombre,  path);

            return myXML.InnerXml;

        }


        //Convertir XML a DataSet
        private bool XmlToDataSet( string XMLInvoice, string XMLDetailInvoce)
        {
            try
            {
                      
                //Convieriendo XMl a DataSet Factura
                System.IO.StringReader rdinvoice = new System.IO.StringReader(XMLInvoice);
                dstinvoicexml.ReadXml(rdinvoice);

                //Convieritiendo XML a DataSet Detalle Factura
                System.IO.StringReader rddetailinvoice = new System.IO.StringReader(XMLDetailInvoce);
                dstdetailinvoicexml.ReadXml(rddetailinvoice);
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }


        //Lectura de Documentos
        private void ReaderDataset()
        {

            LlenarEstructuras.DatosGenerales(dstinvoicexml, datosGenerales, Constants.TIPO_NOTA_ABONO);
            LlenarEstructuras.DatosEmisor(dstinvoicexml,  emisor);
            LlenarEstructuras.DatosReceptor( dstinvoicexml,  receptor, datosGenerales);
            LlenarEstructuras.DatosItems(dstdetailinvoicexml, items);
            LlenarEstructuras.Totales(dstinvoicexml, totales,items);
        }

       



           private String getXML()
        {
            XNamespace dte = XNamespace.Get("http://www.sat.gob.gt/dte/fel/0.2.0");
            XNamespace xd = XNamespace.Get("http://www.w3.org/2000/09/xmldsig#");
          
            //Encabezado del Documento
            XDeclaration declaracion = new XDeclaration("1.0", "utf-8", "no");

            //GTDocumento
            XElement parameters = new XElement(dte + "GTDocumento",
                            new XAttribute(XNamespace.Xmlns + "dte", dte.NamespaceName),
                           new XAttribute(XNamespace.Xmlns + "xd", xd.NamespaceName),
                           new XAttribute("Version", "0.1"));
            //SAT
            XElement SAT = new XElement(dte + "SAT", new XAttribute("ClaseDocumento", "dte"));
            parameters.Add(SAT);

            // formando dte
            XElement DTE = new XElement(dte + "DTE", new XAttribute("ID", "DatosCertificados"));
            SAT.Add(DTE);

            //datos de emision
            XElement DatosEmision = new XElement(dte + "DatosEmision", new XAttribute("ID", "DatosEmision"));
            DTE.Add(DatosEmision);

            //datos generales
            XElement DatosGenerales = new XElement(dte + "DatosGenerales", new XAttribute("CodigoMoneda", this.datosGenerales.CodigoMoneda),
                 new XAttribute("FechaHoraEmision", this.datosGenerales.FechaHoraEmision), new XAttribute("Tipo", this.datosGenerales.Tipo));
            DatosEmision.Add(DatosGenerales);

            //datos emisor
            XElement Emisor = new XElement(dte + "Emisor", new XAttribute("AfiliacionIVA", this.emisor.AfiliacionIVA),
                new XAttribute("CodigoEstablecimiento", this.emisor.CodigoEstablecimiento), 
                new XAttribute("CorreoEmisor", this.emisor.CorreoEmisor), new XAttribute("NITEmisor", this.emisor.NITEmisor), 
                new XAttribute("NombreComercial", this.emisor.NombreComercial), new XAttribute("NombreEmisor", this.emisor.NombreEmisor));
            DatosEmision.Add(Emisor);
            //direccion del emisor
            XElement DireccionEmisor = new XElement(dte + "DireccionEmisor");
            Emisor.Add(DireccionEmisor);
            //elementos dentro de direccion de emisor, dirección, codigopostal, municipio, departamento, pais
            XElement Direccion = new XElement(dte + "Direccion", this.emisor.Direccion);
            XElement CodigoPostal = new XElement(dte + "CodigoPostal", this.emisor.CodigoPostal);
            XElement Municipio = new XElement(dte + "Municipio", this.emisor.Municipio);
            XElement Departamento = new XElement(dte + "Departamento", this.emisor.Departamento);
            XElement Pais = new XElement(dte + "Pais", this.emisor.Pais);
            DireccionEmisor.Add(Direccion);
            DireccionEmisor.Add(CodigoPostal);
            DireccionEmisor.Add(Municipio);
            DireccionEmisor.Add(Departamento);
            DireccionEmisor.Add(Pais);

            //datos Receptor
            XElement Receptor = new XElement(dte + "Receptor", new XAttribute("CorreoReceptor", this.receptor.CorreoReceptor), 
                new XAttribute("IDReceptor", this.receptor.IDReceptor),
                new XAttribute("NombreReceptor", this.receptor.NombreReceptor));
            DatosEmision.Add(Receptor);
            //direccion del receptor
            XElement DireccionReceptor = new XElement(dte + "DireccionReceptor");
            Receptor.Add(DireccionReceptor);
            //elementos dentro de direccion de emisor, dirección, codigopostal, municipio, departamento, pais
            XElement DireccionRecp = new XElement(dte + "Direccion", this.receptor.Direccion);
            XElement CodigoPostalReceptor = new XElement(dte + "CodigoPostal", this.receptor.CodigoPostal);
            XElement MunicipioReceptor = new XElement(dte + "Municipio", this.receptor.Municipio);
            XElement DepartamentoReceptor = new XElement(dte + "Departamento", this.receptor.Departamento);
            XElement PaisReceptor = new XElement(dte + "Pais", this.receptor.Pais);
            DireccionReceptor.Add(DireccionRecp);
            DireccionReceptor.Add(CodigoPostalReceptor);
            DireccionReceptor.Add(MunicipioReceptor);
            DireccionReceptor.Add(DepartamentoReceptor);
            DireccionReceptor.Add(PaisReceptor);
            // detalle de factura 
            XElement Items = new XElement(dte + "Items");
            DatosEmision.Add(Items);
            if (this.items != null)
            {
                foreach (Item item in this.items)
                {

                    //item
                    XElement Item = new XElement(dte + "Item", new XAttribute("BienOServicio", item.BienOServicio), new XAttribute("NumeroLinea", item.NumeroLinea));
                    XElement Cantidad = new XElement(dte + "Cantidad", item.Cantidad);
                    XElement UnidadMedida = new XElement(dte + "UnidadMedida", item.UnidadMedida);
                    XElement Descripcion = new XElement(dte + "Descripcion", item.Descripcion);
                    XElement PrecioUnitario = new XElement(dte + "PrecioUnitario", item.PrecioUnitario);
                    XElement Precio = new XElement(dte + "Precio", item.Precio);
                    XElement Descuento = new XElement(dte + "Descuento", item.Descuento);
                    XElement TotalItem = new XElement(dte + "Total", item.Total);

                    Item.Add(Cantidad);
                    Item.Add(UnidadMedida);
                    Item.Add(Descripcion);
                    Item.Add(PrecioUnitario);
                    Item.Add(Precio);
                    Item.Add(Descuento);
                    Item.Add(TotalItem);
                    Items.Add(Item);

                }
            }

            //Totales
            XElement Totales = new XElement(dte + "Totales");
            DatosEmision.Add(Totales);

            //total general
            XElement GranTotal = new XElement(dte + "GranTotal", totales.GranTotal);
            Totales.Add(GranTotal);



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
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "docelec.txt";
                System.IO.File.WriteAllText(path, ex.Message);
            }
            return res;
        }
        }
}