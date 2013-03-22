﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario
{
    public class InventarioDeCafeLogic
    {
        public InventarioDeCafeLogic() { }
         
        #region Select

        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.reporte_total_inventario_de_cafe_por_socio.ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafe
            (string SOCIOS_ID,
            string SOCIOS_NOMBRE_COMPLETO,
            string CLASIFICACIONES_CAFE_NOMBRE,
            decimal INVENTARIO_ENTRADAS_CANTIDAD,
            decimal INVENTARIO_SALIDAS_SALDO,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from invCafeSocio in db.reporte_total_inventario_de_cafe_por_socio
                                where
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : invCafeSocio.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_NOMBRE_COMPLETO) ? true : invCafeSocio.SOCIOS_NOMBRE_COMPLETO.Contains(SOCIOS_NOMBRE_COMPLETO)) &&
                                (string.IsNullOrEmpty(CLASIFICACIONES_CAFE_NOMBRE) ? true : invCafeSocio.CLASIFICACIONES_CAFE_NOMBRE.Contains(CLASIFICACIONES_CAFE_NOMBRE)) &&
                                (INVENTARIO_ENTRADAS_CANTIDAD.Equals(-1) ? true : invCafeSocio.INVENTARIO_ENTRADAS_CANTIDAD.Equals(INVENTARIO_ENTRADAS_CANTIDAD)) &&
                                (INVENTARIO_SALIDAS_SALDO.Equals(-1) ? true : invCafeSocio.INVENTARIO_SALIDAS_SALDO.Equals(INVENTARIO_SALIDAS_SALDO)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : invCafeSocio.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : invCafeSocio.FECHA_CREACION == FECHA_CREACION)
                                select invCafeSocio;

                    return query.OrderByDescending(ic => ic.FECHA_CREACION).ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public reporte_total_inventario_de_cafe_por_socio GetReporteTotalInventarioDeCafe(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe_por_socio", entityKeyValues);

                    Object invCafSoc = null;

                    reporte_total_inventario_de_cafe_por_socio asocInventory = null;

                    if (db.TryGetObjectByKey(k, out invCafSoc))
                        asocInventory = (reporte_total_inventario_de_cafe_por_socio)invCafSoc;
                    else
                        asocInventory = new reporte_total_inventario_de_cafe_por_socio();

                    return asocInventory;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetInventarioDeCafe(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe_por_socio", entityKeyValues);

                    Object invCafSoc = null;

                    if (db.TryGetObjectByKey(k, out invCafSoc))
                    {
                        reporte_total_inventario_de_cafe_por_socio asocInventory = (reporte_total_inventario_de_cafe_por_socio)invCafSoc;
                        return asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                    }
                    else
                        return 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert

        //Notas de Peso
        public int InsertarTransaccionInventarioDeCafeDeSocio(nota_de_peso NotaDePeso, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafe(NotaDePeso.SOCIOS_ID, NotaDePeso.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafe = new inventario_cafe_de_socio();

                inventarioDeCafe.SOCIOS_ID = NotaDePeso.SOCIOS_ID;
                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = NotaDePeso.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = NotaDePeso.NOTAS_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "ENTRADA";
                
                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario + NotaDePeso.NOTAS_PESO_TOTAL_RECIBIDO;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario;

                inventarioDeCafe.CREADO_POR = NotaDePeso.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = Convert.ToDateTime(NotaDePeso.FECHA_MODIFICACION);

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafe);

                db.SaveChanges();

                return inventarioDeCafe.TRANSACCION_NUMERO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Hojas de Liquidacion
        public void InsertarTransaccionInventarioDeCafeDeSocio(liquidacion HojaDeLiquidacion, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafe(HojaDeLiquidacion.SOCIOS_ID, HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafe = new inventario_cafe_de_socio();

                inventarioDeCafe.SOCIOS_ID = HojaDeLiquidacion.SOCIOS_ID;
                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "SALIDA";

                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario - HojaDeLiquidacion.LIQUIDACIONES_TOTAL_LIBRAS;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario + HojaDeLiquidacion.LIQUIDACIONES_VALOR_TOTAL;

                inventarioDeCafe.CREADO_POR = HojaDeLiquidacion.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = HojaDeLiquidacion.FECHA_CREACION;

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafe);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Venta de Inventario de Cafe
        public void InsertarTransaccionInventarioDeCafeDeSocio(venta_inventario_cafe VentaDeInventarioDeCafe, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafe(VentaDeInventarioDeCafe.SOCIOS_ID, VentaDeInventarioDeCafe.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafe = new inventario_cafe_de_socio();

                inventarioDeCafe.SOCIOS_ID = VentaDeInventarioDeCafe.SOCIOS_ID;
                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = VentaDeInventarioDeCafe.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = VentaDeInventarioDeCafe.VENTAS_INV_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "VENTA";

                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario - VentaDeInventarioDeCafe.VENTAS_INV_CAFE_CANTIDAD_LIBRAS;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario + VentaDeInventarioDeCafe.VENTAS_INV_CAFE_SALDO_TOTAL;

                inventarioDeCafe.CREADO_POR = VentaDeInventarioDeCafe.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = VentaDeInventarioDeCafe.FECHA_CREACION;

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafe);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
