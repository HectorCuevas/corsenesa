using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using System.Data;
namespace FELFactura
{
    public class ValidateDocument
    {

        
        public XmlDocument validar(String token,String dataXml,String url)
        {
            //ENVIANDO DOCUMENTO
            var request = (HttpWebRequest)WebRequest.Create(url+Constants.URL_VAIDAR_DOCUMENTO);
            var postData = getPostData(dataXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(postData.ToString());
            var data = Encoding.UTF8.GetBytes(xmlDoc.InnerXml);
            request.Headers.Add("authorization", "Bearer " + token.ToString().Trim());
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
                
            
           var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(responseString);
            return xmlDoc2;

        



        }

        private DataSet GetResponseXML(string respuesta, bool errores)
        {
            DataSet strreponsexml = new DataSet();
            try
            {
                //Vaciando Respuesta
              
                //Evaluando respuesta
                if (respuesta == null)
                { respuesta = " "; }

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
                XmlNode xresp = xmlDoc.CreateElement("respuesta");
                if (respuesta != null && respuesta.Length > 0)
                {
                    xresp.InnerText = respuesta.ToString();
                }
                xtable.AppendChild(xresp);

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

            return strreponsexml;

        }
        private String getPostData(String data)
        {
    

            
             String request = "<?xml version='1.0' encoding='UTF-8'?>\n" + "<ValidaDocumentoRequest>" +
                            "<xml_dte>" +
                            " <![CDATA[" + data + "]]>" +
                            "</xml_dte>"+
                            "</ValidaDocumentoRequest>";
            return request;
        }
    }
}