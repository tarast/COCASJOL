﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.LOGIC;

namespace COCASJOL.LOGIC.Utiles
{
    public class PlantillaLogic
    {
        public PlantillaLogic() { }

        #region SELECT

        public List<plantilla_notificacion> GetPlantillas()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.plantillas_notificaciones.ToList<plantilla_notificacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<plantilla_notificacion> GetPlantillas
            ( string PLANTILLAS_LLAVE, 
              string PLANTILLAS_NOMBRE, 
              string PLANTILLAS_ASUNTO, 
              string PLANTILLAS_MENSAJE, 
              string CREADO_POR, 
            DateTime FECHA_CREACION, 
              string MODIFICADO_POR, 
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from p in db.plantillas_notificaciones
                                where
                                (string.IsNullOrEmpty(PLANTILLAS_LLAVE) ? true : p.PLANTILLAS_LLAVE.Contains(PLANTILLAS_LLAVE)) &&
                                (string.IsNullOrEmpty(PLANTILLAS_NOMBRE) ? true : p.PLANTILLAS_NOMBRE.Contains(PLANTILLAS_NOMBRE)) &&
                                (string.IsNullOrEmpty(PLANTILLAS_ASUNTO) ? true : p.PLANTILLAS_ASUNTO.Contains(PLANTILLAS_ASUNTO)) &&
                                (string.IsNullOrEmpty(PLANTILLAS_MENSAJE) ? true : p.PLANTILLAS_MENSAJE.Contains(PLANTILLAS_MENSAJE)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : p.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : p.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : p.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : p.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select p;

                    return query.ToList<plantilla_notificacion>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region UPDATE

        public void ActualizarPlantilla
            ( string PLANTILLAS_LLAVE,
              string PLANTILLAS_NOMBRE,
              string PLANTILLAS_ASUNTO,
              string PLANTILLAS_MENSAJE,
              string CREADO_POR,
            DateTime FECHA_CREACION,
              string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", PLANTILLAS_LLAVE);
                    var pl = db.GetObjectByKey(k);
                    plantilla_notificacion plantilla = (plantilla_notificacion)pl;

                    plantilla.PLANTILLAS_NOMBRE = PLANTILLAS_NOMBRE;
                    plantilla.PLANTILLAS_ASUNTO = PLANTILLAS_ASUNTO;
                    plantilla.PLANTILLAS_MENSAJE = PLANTILLAS_MENSAJE;
                    plantilla.MODIFICADO_POR = MODIFICADO_POR;
                    plantilla.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
    }
}
