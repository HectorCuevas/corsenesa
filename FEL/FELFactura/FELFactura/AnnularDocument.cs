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
    public class AnnularDocument
    {

        public XmlDocument annularDte(String token, String dataXml,String url)
        {
            //ENVIANDO DOCUMENTO
            var request = (HttpWebRequest)WebRequest.Create(url+Constants.URL_ANULAR_DOCUMENTO);
            var postData = dataXml;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(postData.ToString());
            var data = Encoding.UTF8.GetBytes(xmlDoc.InnerXml);
            request.Headers.Add("Authorization", "Bearer " + token.ToString().Trim());
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

    }
}