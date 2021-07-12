using System;
using System.Web.Services;
using System.Data;
using System.Xml;

using System.IO;
namespace FELFactura
{
    /// <summary>
    /// Summary description for AnnularDocumentWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AnnularDocumentWS : System.Web.Services.WebService
    {
        AnnularDocument ws = new AnnularDocument();
        XMLAnular xml = new XMLAnular();
        DataSet strreponsexml = new DataSet();

        [WebMethod]
        public DataSet annularDocument(String token, String XMLInvoice, String path, String fac_num,String url)
        {
            try
            {
                //VALIDAR QUE NO ESTEN VACIOS LOS  DATOS ENVIADOS
                //if (!validateEmply(token, XMLInvoice))
                //{
                //    return strreponsexml;
                //}

                
                //SE ENVIA DATOS PARA QUE ARME LA ESTRUCTURA DE XML
                String xmlDoc = xml.getXML(XMLInvoice, path, fac_num);

                //SE ENVIA XML PARA REGISTRAR DOCUMENTO
                XmlDocument register = ws.annularDte(token, xmlDoc,url);

                //SE VALIDA RESPUESTA DEL SERVICIO
                XmlNodeList resReg = register.GetElementsByTagName("tipo_respuesta");
                string errorRes = resReg[0].InnerXml;

                // SI EL SERVICIO RETORNA ERROR SE ARMA LA ESTRUCTURA PARA RESPONDER LOS ERRORES A PROFIT
                if ("1".Equals(errorRes.ToString()))
                {

                    String errorDescp = getError(register);
                    strreponsexml = GetResponseXML(errorDescp, errorRes, this.strreponsexml);
                    return strreponsexml;
                }

                //SI EL SERVICIO FUE RETORNA EXITOSO RETORNA UUID GENERADO POR EL FIRMADO ELECTRONICO
                XmlNodeList uuidNodo = register.GetElementsByTagName("uuid");
                string uuid = uuidNodo[0].InnerXml;
                strreponsexml = GetResponseXML("Transacción Exitosa", uuid, errorRes, this.strreponsexml);

                return strreponsexml;
            }
            catch (Exception e)
            {
                this.strreponsexml = GetResponseXML("Ha ocurrido una excepción no controlada en el sistema \n " + e.Message, "1", this.strreponsexml);
                return strreponsexml;

            }
        }

        private bool validateEmply(String token, String XMLInvoice)
        {
            try
            {

                if (string.IsNullOrEmpty(token))
                {
                    this.strreponsexml = GetResponseXML("Error, token no puede ser vacío", "1", this.strreponsexml);
                    return false;
                }

                if (string.IsNullOrEmpty(XMLInvoice))
                {
                    this.strreponsexml = GetResponseXML("Error, Datos de Factura no han sido enviados ", "1", this.strreponsexml);
                    return false;
                }


            }
            catch (Exception e)
            {
                this.strreponsexml = GetResponseXML("Ha ocurrido una excepción no controlada en el sistema \n " + e.Message, "1", this.strreponsexml);
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
