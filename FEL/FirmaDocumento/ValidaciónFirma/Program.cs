using System;
using System.Collections.Generic;
using System.Deployment.Internal.CodeSigning;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Firma;

namespace ValidaciónFirma
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingresa la ruta del documento a firmar");
            //var ruta = Console.ReadLine();
            var ruta = @"C:\temp\Firma\Documento-Firmado.xml";
            XmlDocument xdoc = new XmlDocument();
            var doc = File.ReadAllText(ruta);
            bool resultado = FirmaDocumento.ValidarDocumento(doc, "C:\\temp\\Firma\\50510231.p12", "Prueba123");
            if (resultado)
            {
                Console.WriteLine("Documento valido");
            } else
            {
                Console.WriteLine("Documento invalido");
            }
            Console.ReadKey();
        }

        public static bool Verifica(XmlDocument doc, X509Certificate2 cert)
        {
            // *** Load the doc this time
            SignedXml sdoc = new SignedXml(doc);

            // *** Find the signature and load it into SignedXml
            XmlNodeList nodeList = doc.GetElementsByTagName("ds:Signature");

            sdoc.LoadXml((XmlElement)nodeList[0]);

            // *** Now read the actual signature and validate
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            bool result = sdoc.CheckSignature(cert, true);

            return result;
        }
    }
}
