using System;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Xml;
namespace FELFactura
{
    /// <summary>
    /// Descripción breve de RegisterDocumentWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class RegisterDocumentWS : System.Web.Services.WebService
    {
        RegisterDocument ws = new RegisterDocument();
        ValidateDocument wsvalidate = new ValidateDocument();
        IDocumentRegister xml = null;
        DataSet strreponsexml = new DataSet();

        [WebMethod]
        public DataSet registerDocument(String token, String XMLInvoice, String XMLDetailInvoce, String path, String fac_num, String url, string tipo)
        {
            try
            {

                //VALIDAR QUE NO ESTEN VACIOS LOS  DATOS ENVIADOS
                if (!validateEmply(token, XMLInvoice, XMLDetailInvoce))
                {
                    return strreponsexml;
                }


                abstractFactory(tipo);
                //TODO VALIDAR QUE  SI EL DOCUMENTO YA EXISTE SOLO DEVUELVA EL UUID

                //SE ENVIA DATOS PARA QUE ARME LA ESTRUCTURA DE XML
                String xmlDoc = xml.getXML(XMLInvoice, XMLDetailInvoce, path, fac_num);



                //SE ENVIA XML PARA REGISTRAR DOCUMENTO
                XmlDocument register = ws.registerDte(token, xmlDoc, url);

                //SE VALIDA RESPUESTA DEL SERVICIO
                XmlNodeList resReg = register.GetElementsByTagName("tipo_respuesta");
                string errorRes = resReg[0].InnerXml;

                // SI EL SERVICIO RETORNA ERROR SE ARMA LA ESTRUCTURA PARA RESPONDER LOS ERRORES A PROFIT
                if ("1".Equals(errorRes.ToString()))
                {

                    String errorDescp = getError(register);
                    strreponsexml = GetResponseXML(errorDescp, errorRes, strreponsexml);
                    return strreponsexml;
                }

                //SI EL SERVICIO FUE RETORNA EXITOSO RETORNA UUID GENERADO POR EL FIRMADO ELECTRONICO
                XmlNodeList uuidNodo = register.GetElementsByTagName("uuid");
                string uuid = uuidNodo[0].InnerXml;
                strreponsexml = GetResponseXML("Transacción Exitosa", uuid, errorRes, strreponsexml);
                return strreponsexml;
            }
            catch (Exception ex)
            {
                // this.strreponsexml = GetResponseXML(e.ToString(), "1", this.strreponsexml);
                //  strreponsexml.Tables.Add(new DataTable());
                // strreponsexml.Tables[0].Columns.Add("res", typeof(string));
                DataSet dsError = new DataSet();
                DataTable dt = new DataTable("resultado");
                dt.Columns.Add(new DataColumn("resultado", typeof(string)));
                //dt.Columns.Add(new DataColumn("xmlGenerado", typeof(string)));

                DataRow dr = dt.NewRow();
                dr["resultado"] = ex.ToString();
                //  dr["xmlGenerado"] =xmlDoc;
                dt.Rows.Add(dr);
                dsError.Tables.Add(dt);
                return dsError;

            }
        }

        private IDocumentRegister abstractFactory(string tipo)
        {
            IDocumentRegister notReg = null;
            switch (tipo)
            {
                case "FACT":
                    xml = new XMLFactura();
                    break;
                case "FCAM":
                    xml = new XMLFacturaCambiaria();
                    break;
                case "FACAM":
                    xml = new XMLFacturaCambiaria();
                    break;
                default:
                    xml = new XMLFactura();
                    break;
            }


            return notReg;
        }

        private bool validateEmply(String token, String XMLInvoice, String XMLDetailInvoce)
        {
            try
            {

                if (string.IsNullOrEmpty(token))
                {
                    strreponsexml = GetResponseXML("Error, token no puede ser vacío", "1", strreponsexml);
                    return false;
                }

                if (string.IsNullOrEmpty(XMLInvoice))
                {
                    strreponsexml = GetResponseXML("Error, Datos de Factura no han sido enviados ", "1", strreponsexml);
                    return false;
                }


                if (string.IsNullOrEmpty(XMLDetailInvoce))
                {
                    strreponsexml = GetResponseXML("Error, Detalles de Factura no han sido enviados ", "1", strreponsexml);
                    return false;
                }


            }
            catch (Exception e)
            {
                strreponsexml = GetResponseXML("Funcion vacio \n " + e.ToString(), "1", strreponsexml);
                return false;

            }
            return true;
        }



        private DataSet GetResponseXML(String valor, string errores, DataSet strreponsexml)
        {
            try
            {
                strreponsexml = new DataSet();

                if (valor == null)
                { valor = " "; }

                XmlDocument xmlDoc = new XmlDocument();

                XmlNode rootNode = xmlDoc.CreateElement("NewDataset");
                xmlDoc.AppendChild(rootNode);

                XmlNode xtable = xmlDoc.CreateElement("Table");
                rootNode.AppendChild(xtable);

                XmlNode xvalor = xmlDoc.CreateElement("respuesta");
                if (valor != null && valor.Length > 0)
                {
                    xvalor.InnerText = valor.ToString();
                }
                xtable.AppendChild(xvalor);

                XmlNode xerror = xmlDoc.CreateElement("blnerror");
                xerror.InnerText = errores.ToString();
                xtable.AppendChild(xerror);
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlDoc.WriteTo(xmlTextWriter);
                StringReader reader = new StringReader(stringWriter.ToString());
                strreponsexml.ReadXml(reader);

            }
            catch
            {

            }
            return strreponsexml;
        }

        private DataSet GetResponseXML(String valor, String uuid, string errores, DataSet strreponsexml)
        {
            try
            {

                strreponsexml = new DataSet();
                if (valor == null)
                { valor = " "; }
                if (uuid == null)
                { uuid = " "; }

                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("NewDataset");
                xmlDoc.AppendChild(rootNode);

                XmlNode xtable = xmlDoc.CreateElement("Table");
                rootNode.AppendChild(xtable);

                XmlNode xvalor = xmlDoc.CreateElement("respuesta");
                if (valor != null && valor.Length > 0)
                {
                    xvalor.InnerText = valor.ToString();
                }
                xtable.AppendChild(xvalor);

                XmlNode xuuid = xmlDoc.CreateElement("uuid");
                if (uuid != null && uuid.Length > 0)
                {
                    xuuid.InnerText = uuid.ToString();
                }
                xtable.AppendChild(xuuid);

                XmlNode xerror = xmlDoc.CreateElement("blnerror");
                xerror.InnerText = errores.ToString();
                xtable.AppendChild(xerror);
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlDoc.WriteTo(xmlTextWriter);
                StringReader reader = new StringReader(stringWriter.ToString());
                strreponsexml.ReadXml(reader);

            }
            catch
            {

            }
            return strreponsexml;
        }

        private String getError(XmlDocument doc)
        {


            XmlNode unEmpleado;
            String errores = "";
            XmlNodeList lst = doc.GetElementsByTagName("error");


            int count = lst.Count;
            for (int i = 0; i < count; i++)
            {

                unEmpleado = lst.Item(i);

                string id = unEmpleado.SelectSingleNode("cod_error").InnerText;
                string error = unEmpleado.SelectSingleNode("desc_error").InnerText;

                errores += " Código: " + id + ", Error: " + error + "\n";

            }
            return errores;
        }
    }
}
