using System;
using System.Linq;
using System.Text;
using HConnexum.Infraestructura;
using System.Reflection;

namespace HConnexum.Base.Negocio
{
    public class UtilitariosNegocio
    {
        public static void LlenarEntidad<T, U>(T dto, ref U entidad, AccionDetalle accion)
        {
            int creadoPor = 0;
            PropertyInfo propinfoU = null;
            if (accion == AccionDetalle.Modificar)
            {
                propinfoU = typeof(U).GetProperty(@"CreadoPor");
                creadoPor = (int)propinfoU.GetValue(entidad, null);
            }
            PropertyInfo[] propertiesT = typeof(T).GetProperties();
            foreach (PropertyInfo property in propertiesT)
            {
                propinfoU = typeof(U).GetProperty(property.Name);
                if (propinfoU != null)
                    propinfoU.SetValue(entidad, property.GetValue(dto, null), null);
            }
            if (accion == AccionDetalle.Modificar)
            {
                propinfoU = typeof(U).GetProperty(@"CreadoPor");
                propinfoU.SetValue(entidad, creadoPor, null);
            }
        }

        public static void LlenarDto<T, U>(ref T dto, U entidad)
        {
            PropertyInfo[] propertiesT = typeof(T).GetProperties();
            foreach (PropertyInfo propinfoT in propertiesT)
            {
                PropertyInfo propinfoU = typeof(U).GetProperty(propinfoT.Name);
                propinfoT.SetValue(dto, propinfoU.GetValue(entidad, null), null);
            }
        }

        ///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
        /// <returns>Devuelve mensaje(s) con los datos validados.</returns>
        public static string ValidarDatos<T, U>(T datos, U metadata)
        {
            Type clase = typeof(U);
            StringBuilder errores = new StringBuilder();
            PropertyInfo[] propertiesT = typeof(T).GetProperties();
            foreach (PropertyInfo propinfoT in propertiesT)
            {
                if (propinfoT.Name != "Id" && propinfoT.Name != "CreadoPor" && propinfoT.Name != "FechaCreacion" && propinfoT.Name != "ModificadoPor" && propinfoT.Name != "FechaModificacion" && propinfoT.Name != "FechaValidez" && propinfoT.Name != "IndVigente" && propinfoT.Name != "IndEliminado")
                {
                    object[] parametros = new object[2];
                    parametros[0] = propinfoT.Name;
                    parametros[1] = propinfoT.GetValue(datos,null);
                    object result = (clase.GetMethod("ValidarPropiedad").Invoke(metadata, parametros));
                    if (result != null) //-->_
                        result.ToString(); //Para evitar el warning del VS o JustCode...
                }
            }
            return errores.ToString();
        }
    }
}
