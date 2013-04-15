﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;

using Ext.Net;
using log4net;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReporteMovimientosInventarioDeCafeDeCooperativa : COCASJOL.LOGIC.Web.COCASJOLREPORT
    {
        private static ILog log = LogManager.GetLogger(typeof(ReporteMovimientosInventarioDeCafeDeCooperativa).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar reporte de movimientos de inventario de cafe de socios.", ex);
                throw;
            }
        }

        protected override void Report_Execution(object sender, DirectEventArgs e)
        {
            string formatoSalida = "";
            try
            {
                ReporteLogic reporteLogic = new ReporteLogic();

                List<reporte_movimientos_de_inventario_de_cafe_de_cooperativa> ReporteMovimientosInventarioDeCafeDeCooperativaLst = reporteLogic.GetMovimientosInventarioDeCafeDeCooperativa
                    (string.IsNullOrEmpty(this.f_CLASIFICACIONES_CAFE_ID.Text) ? 0 : Convert.ToInt32(this.f_CLASIFICACIONES_CAFE_ID.Text),
                    this.f_DESCRIPCION.Text,
                    this.f_FECHA.Text,
                    this.f_DATE_FROM.SelectedDate,
                    this.f_DATE_TO.SelectedDate,
                    this.f_CREADO_POR.Text,
                    this.f_FECHA_CREACION.SelectedDate);

                ReportDataSource datasourceMovimientoInventarioCafeCooperativa = new ReportDataSource("MovimientosInventarioDeCafeDeCooperativaDataSet", ReporteMovimientosInventarioDeCafeDeCooperativaLst);

                ReportParameterCollection reportParamCollection = new ReportParameterCollection();
                reportParamCollection.Add(new ReportParameter("parGroupByClasificacionCafe", this.g_CLASIFICACIONES_CAFE_ID.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByDescripcion", this.g_DESCRIPCION.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByFecha", this.g_FECHA.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByCreadoPor", this.g_CREADO_POR.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByFechaCreacion", this.g_FECHA_CREACION.Checked.ToString()));

                formatoSalida = this.f_SALIDA_FORMATO.Text;

                string rdlPath = "~/resources/rdlcs/ReporteMovimientosDeInventarioDeCafeDeCooperativa.rdlc";

                this.CreateFileOutput("ReporteMovimientosInventarioDeCafeDeSociosDeCooperativa", formatoSalida, rdlPath, datasourceMovimientoInventarioCafeCooperativa, reportParamCollection);
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Error fatal al generar reporte. Formato de salida: {0}", formatoSalida), ex);
                throw;
            }
        }

        //private void CreateFileOutput(string fileName, string format, string RDL_Path, ReportDataSource RptDatasource, ReportParameterCollection RptParams)
        //{
        //    try
        //    {
        //        // Variables
        //        Warning[] warnings;
        //        string[] streamIds;
        //        string mimeType = string.Empty;
        //        string encoding = string.Empty;
        //        string extension = string.Empty;


        //        // Setup the report viewer object and get the array of bytes
        //        ReportViewer viewer = new ReportViewer();
        //        viewer.ProcessingMode = ProcessingMode.Local;
        //        viewer.LocalReport.ReportPath = Server.MapPath(RDL_Path);
        //        viewer.LocalReport.SetParameters(RptParams);
        //        viewer.LocalReport.DataSources.Add(RptDatasource);

        //        byte[] bytes = viewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        //        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        //        Response.Buffer = true;
        //        Response.Clear();
        //        Response.ContentType = mimeType;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
        //        Response.BinaryWrite(bytes); // create the file
        //        Response.Flush(); // send it to the client to download
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal("Error fatal al obtener reporte.", ex);
        //        throw;
        //    }
        //}
    }
}