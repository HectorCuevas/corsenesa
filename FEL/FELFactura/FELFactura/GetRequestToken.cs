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
    public class GetRequestToken
    {

       private  DataSet strreponsexml =  new DataSet();

        public XmlDocument getToken(String user, String apikey, String url)
        {

             url = url+Constants.URL_SOLICITAR_TOKEN;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var postData = getPostData(user, apikey);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(postData);
            var data = Encoding.ASCII.GetBytes(xmlDoc.InnerXml);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            XmlDocument xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(responseString);
           
            return xmlResponse;
        }
        private String getPostData(String user, String apikey)
        {
           String request = "<SolicitaTokenRequest>" +
                           "<usuario>" + user + "</usuario>" +
                           "<apikey>" + apikey + "</apikey>" +
                           "</SolicitaTokenRequest>";
            return request;
        }
    }
}