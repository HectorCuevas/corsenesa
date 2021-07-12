using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma;

namespace FirmaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            FirmaDocumentoAnulacion();
            Console.ReadKey();
        }

        public static void FirmaDocumentof()
        {
            /*
             C:\Users\lflorian\Documents\ONIX\Documentos\FACT.xml
             C:\Users\lflorian\Documents\ONIX\Documentos\FESP.xml
             C:\Users\lflorian\Documents\ONIX\Documentos\NCRE.xml
             */
            var documento = FirmaDocumento.FirmarDocumento("C:\\temp\\Firma\\50510231.p12", "Prueba123", @"C:\Users\lflorian\Documents\ONIX\Documentos\FESP.xml", @"C:\Users\lflorian\Documents\ONIX\Documentos");
            if (documento != null)
            {
                Console.WriteLine("Documento firmado correctamente");
            }
            else
            {
                Console.WriteLine("No se pudo firmar el documento");
            }
        }

        public static void FirmaDocumentoAnulacion()
        {
            var documento = FirmaDocumento.FirmarDocumentoAnulacion("C:\\temp\\Firma\\50510231.p12", "Prueba123", @"C:\Users\lflorian\Documents\ONIX\Documentos\FESP-Anulacion.xml", @"C:\Users\lflorian\Documents\ONIX\Documentos");
            if (documento != null)
            {
                Console.WriteLine("Documento firmado correctamente");
            }
            else
            {
                Console.WriteLine("No se pudo firmar el documento");
            }
        }
    }
}
