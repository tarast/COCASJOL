﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Inventario.Ingresos; 

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class NotasDePesoEnPesaje : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;//necesario actualizarlo siempre, para el tracking correcto
                //this.ValidarCredenciales(typeof(NotasDePesoEnPesaje).Name);//necesario validar en todo momento las credenciales
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void NotasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException = true, ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "AgregarNotasFormP")]
        public void AddNotaDePeso_Click(string Detalles)
        {
            try
            {
                Dictionary<string, string> variables = this.GetVariables();
                if (this.ValidarVariables(variables))
                    return;

                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                NotaDePesoEnPesajeLogic notadepesologic = new NotaDePesoEnPesajeLogic();


                string pDefecto = this.AddPorcentajeDefectoTxt.Text.Replace("%", "");
                string pHumedad = this.AddPorcentajeHumedadTxt.Text.Replace("%", "");

                notadepesologic.InsertarNotaDePeso
                    (Convert.ToInt32(this.AddEstadoNotaCmb.Text),
                    this.AddSociosIdTxt.Text,
                    Convert.ToInt32(this.AddClasificacionCafeCmb.Text),
                    this.AddFechaNotaTxt.SelectedDate,
                    this.AddCooperativaRadio.Value == null ? false : true,
                    Convert.ToDecimal(pDefecto),
                    Convert.ToDecimal(pHumedad),
                    Convert.ToDecimal(this.AddSumaPesoBrutoTxt.Text),
                    Convert.ToDecimal(this.AddTaraTxt.Text),
                    Convert.ToInt32(this.AddSacosRetenidosTxt.Text),
                    loggedUser,
                    detalles,
                    variables);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod(RethrowException = true, ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "EditarNotasFormP")]
        public void EditNotaDePeso_Click(string Detalles)
        {
            try
            {
                Dictionary<string, string> variables = this.GetVariables();
                if (!this.ValidarVariables(variables))
                    return;

                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                NotaDePesoEnPesajeLogic notadepesologic = new NotaDePesoEnPesajeLogic();


                string pDefecto = this.EditPorcentajeDefectoTxt.Text.Replace("%", "");
                string pHumedad = this.EditPorcentajeHumedadTxt.Text.Replace("%", "");

                notadepesologic.ActualizarNotaDePeso
                    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                    this.EditSociosIdTxt.Text,
                    Convert.ToInt32(this.EditClasificacionCafeCmb.Text),
                    this.EditFechaNotaTxt.SelectedDate,
                    this.EditCooperativaRadio.Value == null ? false : true,
                    Convert.ToDecimal(pDefecto),
                    Convert.ToDecimal(pHumedad),
                    Convert.ToDecimal(this.EditSumaPesoBrutoTxt.Text),
                    Convert.ToDecimal(this.EditTaraTxt.Text),
                    Convert.ToInt32(this.EditSacosRetenidosTxt.Text),
                    loggedUser,
                    detalles,
                    variables);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void EditNotaDetalleSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string notaId = this.EditNotaIdTxt.Text;

                if (string.IsNullOrEmpty(notaId))
                    return;

                NotaDePesoEnPesajeLogic notadepesologic = new NotaDePesoEnPesajeLogic();

                this.EditNotaDetalleSt.DataSource = notadepesologic.GetDetalleNotaDePeso(Convert.ToInt32(notaId));
                this.EditNotaDetalleSt.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void DeleteNotaDePeso_Click(string strNOTAS_ID)
        {
            try
            {
                if (string.IsNullOrEmpty(strNOTAS_ID) || string.IsNullOrWhiteSpace(strNOTAS_ID))
                    return;

                int NOTAS_ID = Convert.ToInt32(strNOTAS_ID);
                NotaDePesoEnPesajeLogic notadepesologic = new NotaDePesoEnPesajeLogic();
                notadepesologic.EliminarNotaDePeso(NOTAS_ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}