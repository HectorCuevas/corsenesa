using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using Modelos;

namespace FELFactura
{
    public class LlenarEstructuras
    {


        
        public static void DatosReferenciasNota(DataSet dstcompanyxml, ReferenciasNota referenciasNota)
        {

            foreach (DataRow reader in dstcompanyxml.Tables[0].Rows)
            {
                var FechaEmisionDocumentoOrigen = reader["FechaEmisionDocumentoOrigen"];
                if (FechaEmisionDocumentoOrigen != null)
                {
                    referenciasNota.FechaEmisionDocumentoOrigen = FechaEmisionDocumentoOrigen.ToString();

                }
                var MotivoAjuste = reader["MotivoAjuste"];
                if (MotivoAjuste != null)
                {
                    referenciasNota.MotivoAjuste = MotivoAjuste.ToString();

                }
                var NumeroAutorizacionDocumentoOrigen = reader["NumeroAutorizacionDocumentoOrigen"];
                if (NumeroAutorizacionDocumentoOrigen != null)
                {
                    referenciasNota.NumeroAutorizacionDocumentoOrigen = NumeroAutorizacionDocumentoOrigen.ToString();

                }
                var SerieDocumentoOrigen = reader["SerieDocumentoOrigen"];
                if (SerieDocumentoOrigen != null)
                {
                    referenciasNota.SerieDocumentoOrigen = SerieDocumentoOrigen.ToString();

                }
                var numerodocumentoorigen = reader["numerodocumentoorigen"];
                if (numerodocumentoorigen != null)
                {
                    referenciasNota.numeroOrigen = numerodocumentoorigen.ToString();

                }
                var tipoFE  = reader["tipo_fe"];
                if (tipoFE != null)
                {
                    if(tipoFE.ToString() == "GFACE")
                    {
                        Constants.TIPOFE = true;
                    }

                }

            }

        }

       public static void DatosGenerales(DataSet dstcompanyxml, DatosGenerales datosGenerales, string tipo)
        {

            foreach (DataRow reader in dstcompanyxml.Tables[0].Rows)
            {


                var exenta = reader["exenta"];
                if (exenta != null)
                {
                    datosGenerales.exenta = exenta.ToString();

                }

                var CodigoMoneda = reader["codigomoneda"];
                if (CodigoMoneda != null)
                {
                    datosGenerales.CodigoMoneda = CodigoMoneda.ToString();

                }

                var NumeroAcceso = reader["numeroaccesso"];
                if (NumeroAcceso != null)
                {
                    datosGenerales.NumeroAcceso = NumeroAcceso.ToString();

                }
                var FechaHoraEmision = reader["FechaHoraEmision"];
                if (FechaHoraEmision != null)
                {
                    datosGenerales.FechaHoraEmision =FechaHoraEmision.ToString();

                }
                var expo = reader["exportacion"];
                if (expo != null)
                {
                    if (expo.Equals("si"))
                    {
                        Constants.EXPO = true;
                    }

                }


                datosGenerales.Tipo = tipo;




            }

        }

        public static void Totales(DataSet dstcompanyxml, Totales totales,List<Item>lst)
        {

            Double impuetos = 0d;
            foreach (DataRow reader in dstcompanyxml.Tables[0].Rows)
            {
                var GranTotal = reader["grantotal"];
                if (GranTotal != null)
                {
                    totales.GranTotal =  GranTotal.ToString();

                }

            }

            if (lst != null)
            {
                foreach (Item item in lst)
                {
                    if (item.impuestos != null)
                    {
                        foreach (Impuesto im in item.impuestos)
                        {
                            if (im.MontoImpuesto != null)
                            {
                                impuetos = impuetos + Double.Parse(im.MontoImpuesto);

                            }
                        }

                    }

                }

            }


            totales.TotalMontoImpuesto = impuetos.ToString();
            totales.NombreCorto = "IVA";

        }

        public static void DatosEmisor(DataSet dstinvoicexml, Emisor emisor)
        {
            foreach (DataRow reader in dstinvoicexml.Tables[0].Rows)
            {
                //este dato hay que preguntarlo
                var AfiliacionIVA = reader["afiliacioniva"];
                if (AfiliacionIVA != null)
                {
                    emisor.AfiliacionIVA = AfiliacionIVA.ToString();

                }
                var CodigoEstablecimiento = reader["codigoestablecimiento"];
                if (CodigoEstablecimiento != null)
                {
                    emisor.CodigoEstablecimiento = CodigoEstablecimiento.ToString();

                }
                var CorreoEmisor = reader["correoemisor"];
                if (CorreoEmisor != null)
                {
                    emisor.CorreoEmisor = CorreoEmisor.ToString();

                }

                var NITEmisor = reader["nitemisor"];
                if (NITEmisor != null)
                {
                    emisor.NITEmisor = NITEmisor.ToString();

                }
                var NombreComercial = reader["nombrecomercial"];
                if (NombreComercial != null)
                {
                    emisor.NombreComercial = NombreComercial.ToString();

                }
                var NombreEmisor = reader["nombreemisor"];
                if (NombreEmisor != null)
                {
                    emisor.NombreEmisor = NombreEmisor.ToString();

                }
                var Direccion = reader["direccionemisor"];
                if (Direccion != null)
                {
                    emisor.Direccion = Direccion.ToString();

                }
                var CodigoPostal = reader["codigoPostalemisor"];
                if (CodigoPostal != null)
                {
                    emisor.CodigoPostal = CodigoPostal.ToString();

                }
                var Municipio = reader["municipioemisor"];
                if (Municipio != null)
                {
                    emisor.Municipio = Municipio.ToString();

                }
                var Departamento = reader["departamentoemisor"];
                if (Departamento != null)
                {
                    emisor.Departamento = Departamento.ToString();


                }
                var Pais = reader["paisemisor"];
                if (Pais != null)
                {
                    emisor.Pais = Pais.ToString();


                }


            }

        }

        public static void DatosReceptor(DataSet dstinvoicexml, Receptor receptor,DatosGenerales datosGenerales)
        {
            foreach (DataRow reader in dstinvoicexml.Tables[0].Rows)
            {

               
                var CorreoReceptor = reader["correoreceptor"];
                if (CorreoReceptor != null)
                {
                    receptor.CorreoReceptor = CorreoReceptor.ToString();

                }
                var IDReceptor = reader["idreceptor"];
                if (IDReceptor != null)
                {
                    receptor.IDReceptor = IDReceptor.ToString();

                }
                var NombreReceptor = reader["nombrereceptor"];
                if (NombreReceptor != null)
                {
                    receptor.NombreReceptor = NombreReceptor.ToString();

                }

                var Direccion = reader["direccionReceptor"];
                if (Direccion != null)
                {
                    receptor.Direccion = Direccion.ToString();

                }
                var CodigoPostal = reader["codigoPostalReceptor"];
                if (CodigoPostal != null)
                {
                    receptor.CodigoPostal = CodigoPostal.ToString();

                }
                var Municipio = reader["municipioReceptor"];
                if (Municipio != null)
                {
                    receptor.Municipio = Municipio.ToString();

                }
                var Departamento = reader["departamentoReceptor"];
                if (Departamento != null)
                {
                    receptor.Departamento = Departamento.ToString();

                }
                var Pais = reader["paisReceptor"];
                if (Pais != null)
                {
                    receptor.Pais = Pais.ToString();

                }

            }

        }

        public static void DatosItems(DataSet dstdetailinvoicexml, List<Item> items)
        {
            foreach (DataRow reader in dstdetailinvoicexml.Tables[0].Rows)
            {
                Item item = new Item();
                item.impuestos = new List<Impuesto>();
                Impuesto impuesto = new Impuesto();
                //impuesto
                var impuestonombrecorto = reader["impuestonombrecorto"];
                if (impuestonombrecorto != null)
                {
                    impuesto.NombreCorto = impuestonombrecorto.ToString();

                }
                var codigounidadgravable = reader["codigounidadgravable"];
                if (codigounidadgravable != null)
                {
                    impuesto.CodigoUnidadGravable = codigounidadgravable.ToString();

                }
           
                var montoimpuesto = reader["montoimpuesto"];
                if (montoimpuesto != null)
                {


                    impuesto.MontoImpuesto = montoimpuesto.ToString();
                    
                }
                var montogravable = reader["montogravable"];
                if (montogravable != null)
                {
                    impuesto.MontoGravable =  montogravable.ToString();

                }
                //item en general
                var bienoservicio = reader["bienoservicio"];
                if (bienoservicio != null)
                {
                    item.BienOServicio = bienoservicio.ToString();

                }
                var descripcion = reader["descripcion"];
                if (descripcion != null)
                {
                    item.Descripcion = descripcion.ToString();

                }
                var numerolinea = reader["numerolinea"];
                if (numerolinea != null)
                {
                    item.NumeroLinea = numerolinea.ToString();

                }
                var cantidad = reader["cantidad"];
                if (cantidad != null)
                {
                    item.Cantidad = cantidad.ToString();

                }
                var unidadMedida = reader["unidadMedida"];
                if (unidadMedida != null)
                {
                    item.UnidadMedida = unidadMedida.ToString();

                }
                var precio = reader["precio"];
                if (precio != null)
                {
                    item.Precio =  precio.ToString();

                }
                var preciounitario = reader["preciounitario"];
                if (preciounitario != null)
                {
                    item.PrecioUnitario =  preciounitario.ToString();

                }

                var total = reader["total"];
                if (total != null)
                {
                    item.Total =  total.ToString();

                }

                var descuento = reader["descuento"];
                if (descuento != null )
                {
                    if (Double.Parse(descuento.ToString())>0d)
                    {
                        item.Descuento = descuento.ToString();

                    }

                }
                items.Add(item);
                item.impuestos.Add(impuesto);
            }
        }

        public static void DatosFactExportacion(DataSet dstinvoicexml, Complementos c)
        {
            foreach (DataRow reader in dstinvoicexml.Tables[0].Rows)
            {

                var INCOTERM = reader["transporte"];
                if (INCOTERM != null)
                {
                    c.transporte = INCOTERM.ToString();
                }
            }
        }

            public static void DatosComplementos(DataSet dstinvoicexml, Complementos c, Totales t, List<Item> items)
        {

            Double retencionIVA = 0d;
            
            foreach (DataRow reader in dstinvoicexml.Tables[0].Rows)
            {
                var retencion = reader["totalretenciones"];
                if (retencion != null)
                {
                    c.RetencionISR = retencion.ToString();
                }

                var totalsinisr = reader["totalsinretenciones"];
                if (totalsinisr != null)
                {
                    c.TotalMenosRetenciones = totalsinisr.ToString();
                }

            }

           if (items !=null)
            {
                foreach (Item  i in items)
                {
                    foreach (Impuesto im in i.impuestos)
                    {
                     if (im.MontoImpuesto != null)
                        {
                            retencionIVA = retencionIVA + Double.Parse(im.MontoImpuesto);

                        }

                    }

                }


            }

            
        

            c.RetencionIVA = retencionIVA.ToString();
            

        }



        public static void DatosAnular(DataSet dstcancelxml, Anular anular)
        {

            foreach (DataRow reader in dstcancelxml.Tables[0].Rows)
            {
                var NumeroDocumentoAAnular = reader["numerodocumentoanular"];
                var NITEmisor = reader["nitemisor"];
                var IDReceptor = reader["idreceptor"];
                var FechaEmisionDocumentoAnular = reader["fechaemisiondocumentoanular"];
                var FechaHoraAnulacion = reader["fechahoraanulacion"];
                var MotivoAnulacion = reader["motivoanulacion"];
                var uuid = reader["uuid"];

                anular.NumeroDocumentoAAnular = NumeroDocumentoAAnular.ToString();
                anular.NITEmisor = NITEmisor.ToString();
                anular.IDReceptor = IDReceptor.ToString();
                anular.FechaEmisionDocumentoAnular = FechaEmisionDocumentoAnular.ToString();
                anular.FechaHoraAnulacion = FechaHoraAnulacion.ToString();
                anular.MotivoAnulacion = MotivoAnulacion.ToString();
                anular.uuid = uuid.ToString();

            }

        }


    }
}