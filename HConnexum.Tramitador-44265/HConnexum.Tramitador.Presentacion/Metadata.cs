using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Presentacion
{
	//Para un tipo obtiene su metadata a partir de la clase "Buddy"
	public class Metadata<T>
	{
		#region "Variables Locales"
		System.Type tipoMetadata = null;
		System.Type tipoEntidad = typeof(T);
		#endregion "Variables Locales"

		#region "Constructor"
		public Metadata()
		{
			System.Type tipoEntidad = typeof(T);
			IEnumerable<MetadataTypeAttribute> tiposMetadata = tipoEntidad.GetCustomAttributes(typeof(MetadataTypeAttribute), false).OfType<MetadataTypeAttribute>();

			if(tiposMetadata.Count() > 0)
			{
				tipoMetadata = tiposMetadata.First().MetadataClassType;
			}
		}
		#endregion "Constructor"

		#region "Propiedades"
		public bool HayMetadata
		{
			get
			{
				return (tipoMetadata != null);
			}
		}
		#endregion "Propiedades"

		#region "Metodos Publicos"
		//public List<MetadataColumna> ColumnasConMetadata()
		//{
		//    List<MetadataColumna> Columnas = new List<MetadataColumna>();
		//    PropertyInfo[] propertyInfos = tipoEntidad.GetProperties();

		//    foreach (PropertyInfo propEntidad in propertyInfos)
		//    {
		//        MetadataColumna metadata = new MetadataColumna();
		//        PropertyInfo propMetadata = tipoMetadata.GetProperty(propEntidad.Name);
		//        if (propMetadata != null)
		//        {
		//            metadata.CampoDatos = propEntidad.Name;
		//            metadata.Encabezado = ObtenerDisplayName(propMetadata);
		//            metadata.Formato = ObtenerDisplayFormat(propMetadata);
		//            metadata.Ancho = ObtenerAncho(propMetadata);
		//            metadata.Ordenable = ObtenerOrdenable(propMetadata);
		//            metadata.Alineacion = ObtenerAlineacion(propMetadata);

		//            Columnas.Add(metadata);
		//        }
		//    }
		//    return Columnas;
		//}

		public string ValidarPropiedad(string nombrePropiedad, object valor)
		{
			StringBuilder Errores = new StringBuilder();
			PropertyInfo propMetadata = tipoMetadata.GetProperty(nombrePropiedad);
			foreach(ValidationAttribute attr in propMetadata.GetCustomAttributes(typeof(ValidationAttribute), true).Cast<ValidationAttribute>())
			{
				if(!attr.IsValid(valor))
				{
					string displayName = ObtenerDisplayName(propMetadata);
					Errores.AppendWithBreak(attr.FormatErrorMessage(displayName));
				}
			}
			return Errores.ToString();
		}
		#endregion "Metodos Publicos"

		#region "Metodos para obtener valores de Atributos"
		private string ObtenerDisplayName(PropertyInfo propertyInfo)
		{
			DisplayNameAttribute attrDNA = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).OfType<DisplayNameAttribute>().FirstOrDefault();
			string displayName = attrDNA == null ? propertyInfo.Name : attrDNA.DisplayName;
			return displayName;
		}
		//private string ObtenerDisplayFormat(PropertyInfo propertyInfo)
		//{
		//    DisplayFormatAttribute attrDNA = propertyInfo.GetCustomAttributes(typeof(DisplayFormatAttribute), true).OfType<DisplayFormatAttribute>().FirstOrDefault();
		//    string displayName = attrDNA == null ? "" : attrDNA.DataFormatString;
		//    return displayName;
		//}
		//private int ObtenerAncho(PropertyInfo propertyInfo)
		//{
		//    Ancho attrDNA = propertyInfo.GetCustomAttributes(typeof(Ancho), true).OfType<Ancho>().FirstOrDefault();
		//    int ancho = attrDNA == null ? 0 : attrDNA.AnchoColumna;
		//    return ancho;
		//}
		//private bool ObtenerOrdenable(PropertyInfo propertyInfo)
		//{
		//    Ordenable attrDNA = propertyInfo.GetCustomAttributes(typeof(Ordenable), true).OfType<Ordenable>().FirstOrDefault();
		//    bool ordenable = attrDNA == null ? false : attrDNA.ColumnaOrdenable;
		//    return ordenable;
		//}
		//private TipoAlineacion ObtenerAlineacion(PropertyInfo propertyInfo)
		//{
		//    Alineacion attrDNA = propertyInfo.GetCustomAttributes(typeof(Alineacion), true).OfType<Alineacion>().FirstOrDefault();
		//    TipoAlineacion alineacion = attrDNA == null ? TipoAlineacion.Izquierda : attrDNA.AlineacionColumna;
		//    return alineacion;
		//}
		#endregion "Metodos para obtener valores de Atributos"
	}
	/// <summary>Tipos de mensaje de la aplicación.</summary>
	public enum TiposMensaje
	{
		PorDefecto = 0
		,Validacion_CampoRequerido
		,NoExisteEstacion
		,Error_Generico
		,PolizaVencida
		,PolizaInactiva
		,AseguradoSinCobertura
		,AseguradoInactivo
		,Informacion_Servicio_Clave_Emergencia_Verificacion_Parentesco_Recaudos
		,Informacion_Servicio_Clave_Emergencia_Verificacion_SolicitudAbierta
		,Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaAfiliadoRequerida
		,Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaGrupoFamiliarRequerida
		,Informacion_Servicio_Clave_Emergencia_Verificacion_RegistroSolicitudRequerido
		,Informacion_Servicio_Clave_Emergencia_Verificacion_AfiliadoNoExiste
		,Informacion_Servicio_Clave_Emergencia_Verificacion_EdadIgualFechaNacimiento
		,Informacion_Servicio_Carta_Aval_Verificacion_Parentesco_Recaudos
		,Informacion_Servicio_Carta_Aval_Verificacion_SolicitudAbierta
		,Informacion_Servicio_Carta_Aval_Verificacion_ConsultaAfiliadoRequerida
		,Informacion_Servicio_Carta_Aval_Verificacion_ConsultaGrupoFamiliarRequerida
		,Informacion_Servicio_Carta_Aval_Verificacion_RegistroSolicitudRequerido
		,Informacion_Servicio_Carta_Aval_Verificacion_AfiliadoNoExiste
		,Informacion_Servicio_Carta_Aval_Verificacion_TitularNoMayorDeEdad
		,Informacion_Servicio_Carta_Aval_Verificacion_EdadIgualFechaNacimiento
	}
}