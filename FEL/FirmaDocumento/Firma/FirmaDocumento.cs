using System.IO;
using FirmaXadesNet;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Signature;
using FirmaXadesNet.Signature.Parameters;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Text;
using FirmaXadesNet.Validation;
using System;
namespace Firma
{
    public static class FirmaDocumento
    {
        //Invocación de la firma de documento, retorno  y almacenamiento de este
        public static XmlDocument FirmarDocumento(string rutaCertificado, string contraseñaCertificado, string rutaDocumento, string nombreDocumento, string ubicacionDestino)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

                SignatureParameters parametros = ParametrosdeFirma("DatosEmision");
                var nombre = nombreDocumento;
                System.DateTime fecha = System.DateTime.Now;
                //se demora la firma 1 minuto
                parametros.SigningDate = fecha.AddSeconds(-120);
                using (parametros.Signer = new Signer(cert))
                {
                    var documento = FirmaXades(parametros, rutaDocumento + nombreDocumento);
                    System.IO.File.Delete(rutaDocumento + nombre);
                    AlmacenamientoDocumento(documento, ubicacionDestino, nombre);
                    return documento.Document;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string FirmarDocumento2(string rutaCertificado, string contraseñaCertificado, string rutaDocumento, string nombreDocumento, string ubicacionDestino)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

                SignatureParameters parametros = ParametrosdeFirma("DatosEmision");
                var nombre = nombreDocumento;
                System.DateTime fecha = System.DateTime.Now;
                //se demora la firma 1 minuto
                parametros.SigningDate = fecha.AddSeconds(-120);
                using (parametros.Signer = new Signer(cert))
                {
                    var documento = FirmaXades(parametros, rutaDocumento + nombreDocumento);
                    System.IO.File.Delete(rutaDocumento + nombre);
                    AlmacenamientoDocumento(documento, ubicacionDestino, nombre);
                    return documento.Document.InnerXml;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public static XmlDocument FirmarAnulacion(string rutaCertificado, string contraseñaCertificado, string rutaDocumento, string ubicacionDestino)
        {
            X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

            SignatureParameters parametros = ParametrosdeFirma("DatosAnulacion");
            var nombredocumento = Path.GetFileNameWithoutExtension(rutaDocumento);

            using (parametros.Signer = new Signer(cert))
            {
                var documento = FirmaXades(parametros, rutaDocumento);
                AlmacenamientoDocumento(documento, ubicacionDestino, nombredocumento);
                return documento.Document;
            }
        }
        public static XmlDocument FirmarAnulacion(string rutaCertificado, string contraseñaCertificado, string rutaDocumento)
        {
            X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

            SignatureParameters parametros = ParametrosdeFirma("DatosAnulacion");
            using (parametros.Signer = new Signer(cert))
            {
                return FirmaXades(parametros, rutaDocumento).Document;
            }
        }


        //Invocación de la firma de documento y retorno de este
        public static XmlDocument FirmarDocumento(string rutaCertificado, string contraseñaCertificado, string rutaDocumento)
        {
            X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

            SignatureParameters parametros = ParametrosdeFirma("DatosEmision");
            using (parametros.Signer = new Signer(cert))
            {
                return FirmaXades(parametros, rutaDocumento).Document;
            }
        }

        //Firma del documento
        private static SignatureDocument FirmaXades(SignatureParameters sp, string ruta)
        {
            try
            {
                XadesService xadesService = new XadesService();
                using (FileStream fs = new FileStream(ruta, FileMode.Open))
                {
                    var documento = xadesService.Sign(fs, sp);
                    MoverNodoFirma(documento);
                    return documento;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        //Almacenamiento e ruta especifica
        private static void AlmacenamientoDocumento(SignatureDocument sd, string ruta, string nombre)
        {
            ruta = string.Format(@"{0}\{1}.xml", ruta, nombre);

            if (!File.Exists(ruta))
            {
                sd.Save(ruta);
            }
            else
            {
                System.IO.File.Delete(ruta);
                sd.Save(ruta);
            }
        }

        //Parametros para la firma del documento
        private static SignatureParameters ParametrosdeFirma(string ElementoAFirmar)
        {
            SignatureParameters parametros = new SignatureParameters
            {
                SignaturePackaging = SignaturePackaging.INTERNALLY_DETACHED,
                InputMimeType = "text/xml",
                ElementIdToSign = ElementoAFirmar,
                SignatureMethod = SignatureMethod.RSAwithSHA256,
                DigestMethod = DigestMethod.SHA256
            };

            return parametros;
        }

        //Cambio de posicion del nodo de la firma en el nodo padre del documento
        private static void MoverNodoFirma(SignatureDocument sd)
        {
            var documento = sd.Document;
            var NodoFirma = documento.GetElementsByTagName("ds:Signature")[0];
            NodoFirma.ParentNode.RemoveChild(NodoFirma);
            documento.DocumentElement.AppendChild(NodoFirma);
        }


        /* Anulaciones */

        //Invocación de la firma de documento, retorno  y almacenamiento de este
        public static XmlDocument FirmarDocumentoAnulacion(string rutaCertificado, string contraseñaCertificado, string rutaDocumento, string ubicacionDestino)
        {
            X509Certificate2 cert = new X509Certificate2(rutaCertificado, contraseñaCertificado, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);

            SignatureParameters parametros = ParametrosdeFirma("DatosAnulacion");
            var nombredocumento = Path.GetFileNameWithoutExtension(rutaDocumento);

            using (parametros.Signer = new Signer(cert))
            {
                var documento = FirmaXades(parametros, rutaDocumento);
                AlmacenamientoDocumento(documento, ubicacionDestino, nombredocumento);
                return documento.Document;
            }
        }

        //Invocación de la firma de documento y retorno de este
        public static XmlDocument FirmarDocumentoAnulacion(string rutaCertificado, string contraseñaCertificado, string rutaDocumento, string nombreDocumento, string ubicacionDestino)
        {
            

            try
            {
                X509Certificate2 cert= new X509Certificate2(rutaCertificado, contraseñaCertificado,
                               X509KeyStorageFlags.MachineKeySet
                             | X509KeyStorageFlags.PersistKeySet
                             | X509KeyStorageFlags.Exportable);

                SignatureParameters parametros = ParametrosdeFirma("DatosAnulacion");
                var nombredocumento = nombreDocumento;
                System.DateTime fecha = System.DateTime.Now;
                //se demora la firma 1 minuto
                parametros.SigningDate = fecha.AddSeconds(-120);
                //  using (parametros.Signer = new Signer(cert))
                // {
                //   return FirmaXades(parametros, rutaDocumento + nombreDocumento).Document;
                //}

                using (parametros.Signer = new Signer(cert))
                {
                    var documento = FirmaXades(parametros, rutaDocumento + nombreDocumento);
                    System.IO.File.Delete(rutaDocumento + nombredocumento);
                    AlmacenamientoDocumento(documento, ubicacionDestino, nombredocumento);
                    return documento.Document;
                }
            }
            catch (Exception ex)
            {
                String path = "C:\\xml\\Anulados\\errores-an.txt";
                File.WriteAllText(path, ex.ToString());
                return null;
            }
        }

    }
}
