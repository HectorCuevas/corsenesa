using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.IO;

namespace FELFactura
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SolicitarToken : System.Web.Services.WebService
    {
        GetRequestToken ws = new GetRequestToken();
        DataSet strreponsexml = new DataSet();
        [WebMethod]
        public DataSet getToken(String user, String apikey, String url)
        {

            XmlDocument xmlResponse = ws.getToken(user, apikey,url);
            XmlNodeList tokenNodo = xmlResponse.GetElementsByTagName("token");
            string token = tokenNodo[0].InnerXml;
            XmlNodeList resNodo = xmlResponse.GetElementsByTagName("tipo_respuesta");
            string error = resNodo[0].InnerXml;
            bool errores = false;
            if ("0".Equals(error))
            {
                errores = true;
            }

            GetResponseXML(token, errores);
            return strreponsexml;
        }

        private void GetResponseXML(string token, bool errores)
        {
            try
            {
                //Vaciando Respuesta
                strreponsexml = new DataSet();
                //Evaluando token
                if (token == null)
                { token = " "; }

                //Evaluando Error Text
                //Creando XML
                //Documento XML
                XmlDocument xmlDoc = new XmlDocument();
                //Nombre de XML
                XmlNode rootNode = xmlDoc.CreateElement("NewDataset");
                xmlDoc.AppendChild(rootNode);
                //TABLE
                XmlNode xtable = xmlDoc.CreateElement("Table");
                rootNode.AppendChild(xtable);
                //token
                XmlNode xtoken = xmlDoc.CreateElement("token");
                if (token != null && token.Length > 0)
                {
                    xtoken.InnerText = token.ToString();
                }
                xtable.AppendChild(xtoken);

                //Error
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
        }

    }
}