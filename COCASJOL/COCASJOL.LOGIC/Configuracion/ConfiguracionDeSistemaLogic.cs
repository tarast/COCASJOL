﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Xml;

using log4net;

namespace COCASJOL.LOGIC.Configuracion
{
    public class ConfiguracionDeSistemaLogic
    {
        #region Private Members

        private static ILog log = LogManager.GetLogger(typeof(ConfiguracionDeSistemaLogic).Name);

        private XmlDocument Configuracion;

        private bool ventana_Maximizar;
        private bool ventana_CargarDatos;

        private DateTime consolidado_InicioPeriodo;
        private DateTime consolidado_FinalPeriodo;

        private string correo_CorreoLocal;
        private bool correo_UsarPassword;
        private string correo_Password;
        private string correo_SMTP;
        private int correo_Puerto;
        private bool correo_UsarSSL;

        private string auditoria_username;
        private DateTime auditoria_date;

        #endregion

        #region Public Properties

        public bool VentanasMaximizar
        {
            get
            {
                return this.ventana_Maximizar;
            }

            set
            {
                this.ventana_Maximizar = value;
            }
        }

        public bool VentanasCargarDatos
        {
            get
            {
                return this.ventana_CargarDatos;
            }
            set
            {
                this.ventana_CargarDatos = value;
            }
        }


        public DateTime ConsolidadoInventarioInicioPeriodo
        {
            get
            {
                return this.consolidado_InicioPeriodo;
            }
            set
            {
                this.consolidado_InicioPeriodo = value;
            }
        }

        public DateTime ConsolidadoInventarioFinalPeriodo
        {
            get
            {
                return this.consolidado_FinalPeriodo;
            }
            set
            {
                this.consolidado_FinalPeriodo = value;
            }
        }

        
        public string CorreoCorreoLocal
        {
            get
            {
                return this.correo_CorreoLocal;
            }
            set
            {
                this.correo_CorreoLocal = value;
            }
        }

        public bool CorreoUsarPassword
        {
            get
            {
                return this.correo_UsarPassword;
            }
            set
            {
                this.correo_UsarPassword = value;
            }
        }

        public string CorreoPassword
        {
            get
            {
                return this.correo_Password;
            }
            set
            {
                this.correo_Password = value;
            }
        }

        public string CorreoSMTP
        {
            get
            {
                return this.correo_SMTP;
            }
            set
            {
                this.correo_SMTP = value;
            }
        }

        public int CorreoPuerto
        {
            get
            {
                return this.correo_Puerto;
            }
            set
            {
                this.correo_Puerto = value;
            }
        }

        public bool CorreoUsarSSL
        {
            get
            {
                return this.correo_UsarSSL;
            }
            set
            {
                this.correo_UsarSSL = value;
            }
        }


        public string AuditoriaUserName
        {
            get
            {
                return this.auditoria_username;
            }
            set
            {
                this.auditoria_username = value;
            }
        }

        public DateTime AuditoriaDate
        {
            get
            {
                return this.auditoria_date;
            }
            set
            {
                this.auditoria_date = value;
            }
        }

        #endregion


        #region Constructors

        public ConfiguracionDeSistemaLogic()
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings.Get("configuracionDeSistemaXML");
                Configuracion = new XmlDocument();
                Configuracion.Load(System.Web.HttpContext.Current.Server.MapPath(path));

                this.LoadMembers();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir ConfiguracionDeSistemaLogic.", ex);
                throw;
            }
        }

        public ConfiguracionDeSistemaLogic(XmlDocument SysConfig)
        {
            try
            {
                this.Configuracion = SysConfig;
                this.LoadMembers();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir ConfiguracionDeSistemaLogic.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        private void LoadMembers()
        {
            try
            {
                string strMaximizar = Configuracion.SelectSingleNode("configuracion/ventanas/MaximizarAlCargar").InnerText;
                string strCargarDatos = Configuracion.SelectSingleNode("configuracion/ventanas/CargarDatosAlInicio").InnerText;

                string strConsolidadoInicio = Configuracion.SelectSingleNode("configuracion/consolidadoInventario/InicioPeriodo").InnerText;
                string strConsolidadoFinal = Configuracion.SelectSingleNode("configuracion/consolidadoInventario/FinalPeriodo").InnerText;

                string strCorreoLocal = Configuracion.SelectSingleNode("configuracion/correo/correoLocal").InnerText;
                string strCorreoUsarPassword = Configuracion.SelectSingleNode("configuracion/correo/usarPassword").InnerText;
                string strCorreoPassword = Configuracion.SelectSingleNode("configuracion/correo/password").InnerText;
                string strCorreoSMTP = Configuracion.SelectSingleNode("configuracion/correo/smtp").InnerText;
                string strCorreoPuerto = Configuracion.SelectSingleNode("configuracion/correo/puerto").InnerText;
                string strCorreoUsarSSL = Configuracion.SelectSingleNode("configuracion/correo/usarSSL").InnerText;

                string strAuditoriaUsername = Configuracion.SelectSingleNode("configuracion/auditoria/username").InnerText;
                string strAuditoriaDate = Configuracion.SelectSingleNode("configuracion/auditoria/date").InnerText;



                bool.TryParse(strMaximizar, out this.ventana_Maximizar);
                bool.TryParse(strCargarDatos, out this.ventana_CargarDatos);

                DateTime.TryParse(strConsolidadoInicio, out this.consolidado_InicioPeriodo);
                DateTime.TryParse(strConsolidadoFinal, out this.consolidado_FinalPeriodo);

                this.correo_CorreoLocal = strCorreoLocal;
                bool.TryParse(strCorreoUsarPassword, out this.correo_UsarPassword);
                this.correo_Password = strCorreoPassword;
                this.correo_SMTP = strCorreoSMTP;
                int.TryParse(strCorreoPuerto, out this.correo_Puerto);
                bool.TryParse(strCorreoUsarSSL, out this.correo_UsarSSL);

                this.auditoria_username = strAuditoriaUsername;
                DateTime.TryParse(strAuditoriaDate, out this.auditoria_date);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar propiedades", ex);
                throw;
            }
        }

        public void SaveMembers()
        {
            try
            {
                Configuracion.SelectSingleNode("configuracion/ventanas/MaximizarAlCargar").InnerText = this.ventana_Maximizar.ToString();
                Configuracion.SelectSingleNode("configuracion/ventanas/CargarDatosAlInicio").InnerText = this.ventana_CargarDatos.ToString();

                Configuracion.SelectSingleNode("configuracion/consolidadoInventario/InicioPeriodo").InnerText = this.consolidado_InicioPeriodo.ToShortDateString();
                Configuracion.SelectSingleNode("configuracion/consolidadoInventario/FinalPeriodo").InnerText = this.consolidado_FinalPeriodo.ToShortDateString();

                Configuracion.SelectSingleNode("configuracion/correo/correoLocal").InnerText = this.correo_CorreoLocal;
                Configuracion.SelectSingleNode("configuracion/correo/usarPassword").InnerText = this.correo_UsarPassword.ToString();
                Configuracion.SelectSingleNode("configuracion/correo/password").InnerText = this.correo_Password;
                Configuracion.SelectSingleNode("configuracion/correo/smtp").InnerText = this.correo_SMTP;
                Configuracion.SelectSingleNode("configuracion/correo/puerto").InnerText = this.correo_Puerto.ToString();
                Configuracion.SelectSingleNode("configuracion/correo/usarSSL").InnerText = this.correo_UsarSSL.ToString();

                Configuracion.SelectSingleNode("configuracion/auditoria/username").InnerText = this.auditoria_username;
                Configuracion.SelectSingleNode("configuracion/auditoria/date").InnerText = this.auditoria_date.ToShortDateString();

                string SavePath = System.Configuration.ConfigurationManager.AppSettings.Get("configuracionDeSistemaXML");

                Configuracion.Save(HttpContext.Current.Server.MapPath(SavePath));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar propiedades", ex);
                throw;
            }
        }

        #endregion
    }
}
