using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Anular
    {

        public string NumeroDocumentoAAnular = null;
        public string NITEmisor = null;
        public string IDReceptor = null;
        public string FechaEmisionDocumentoAnular = null;
        public string FechaHoraAnulacion = null;
        public string MotivoAnulacion = null;
        public string uuid = null;

        

        public Anular(string NumeroDocumentoAAnular, string NITEmisor, 
            string IDReceptor, string FechaEmisionDocumentoAnular, 
            string FechaHoraAnulacion, string MotivoAnulacion,string uuid)
        {
            this.NITEmisor = NITEmisor;
            this.NumeroDocumentoAAnular = NumeroDocumentoAAnular;
            this.IDReceptor = IDReceptor;
            this.FechaEmisionDocumentoAnular = FechaEmisionDocumentoAnular;
            this.FechaHoraAnulacion = FechaHoraAnulacion;
            this.MotivoAnulacion = MotivoAnulacion;
            this.uuid = uuid;
         }



        public Anular()
        {
            this.NITEmisor = "";
            this.NumeroDocumentoAAnular = "";
            this.IDReceptor = "";
            this.FechaEmisionDocumentoAnular = "";
            this.FechaHoraAnulacion = "";
            this.MotivoAnulacion = "";
            this.uuid = "";
        }

    }
}
