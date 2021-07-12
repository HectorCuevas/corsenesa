using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FELFactura
{
    public interface IDocumentRegister
    {

        String getXML(string XMLInvoice, string XMLDetailInvoce, string path, string fac_num);
    }
}
