using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Item
    {
        public string BienOServicio = null;
        public string NumeroLinea = null;
        public string Cantidad = null;
        public string UnidadMedida = null;
        public String Descripcion = null;
        public string PrecioUnitario = null;
        public string Precio = null;
        public string Descuento = "0";
        public string Total = null;
        public List<Impuesto> impuestos;

    }
}
