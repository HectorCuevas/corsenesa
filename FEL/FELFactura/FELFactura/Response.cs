using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.IO;
namespace FELFactura
{
    public class Response
    {

        public static DataSet GetResponseXML(String valor, string nombre, bool errores, DataSet strreponsexml)
        {
            try
            {
                //Vaciando Respuesta
                strreponsexml = new DataSet();
                //Evaluando token
                if (valor == null)
                { valor = " "; }

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
                XmlNode xvalor = xmlDoc.CreateElement(nombre);
                if (valor != null && valor.Length > 0)
                {
                    xvalor.InnerText = valor.ToString();
                }
                xtable.AppendChild(xvalor);

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

    }
}