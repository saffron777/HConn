using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase CasoDTO.</summary>
	public class CasoDTO
	{
		public int Id { get; set; }
		public bool IndEliminado { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public int? PrioridadAtencion { get; set; }
		public int IdSolicitud { get; set; }
		public int IdFlujoServicio { get; set; }
		public int IdServicio { get; set; }
		public int IdSuscriptor { get; set; }
		public string Ticket { get; set; }
		public string TipDocSolicitante { get; set; }
		public string NumDocSolicitante { get; set; }
        public string NumDocTit { get; set; }
		public string NombreSolicitante { get; set; }
		public string ApellidoSolicitante { get; set; }
		public string SupportIncident { get; set; }
		public string CodIdExternoInt { get; set; }
		public string MovilSolicitante { get; set; }
		public string EmailSolicitante { get; set; }
		public string ClaveSolicitante { get; set; }
		public string Actividad { get; set; }
		public string Cedula { get; set; }
		public string Asegurado { get; set; }
		public int Estatus { get; set; }
		public string NumPoliza { get; set; }
		public int? NumMvtoPoliza { get; set; }
		public int? NumCertificado { get; set; }
		public int? NumRiesgo { get; set; }
		public string TipDocAgt { get; set; }
		public string NumDocAgt { get; set; }
		/// <summary>Cadena de texto con la respuesta del caso en formato XML.</summary>
		public string XMLRespuesta { get; set; }
		public int CreadorPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public DateTime? FechaAnulacion { get; set; }
		public DateTime? FechaRechazo { get; set; }
		public DateTime? FechaEjecucion { get; set; }
		public DateTime? FechaReverso { get; set; }
        public DateTime? FechaEnProceso { get; set; }
		public int? IdMovimiento { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
		public int IdServiciosuscriptor { get; set; }
		public string NombreSuscriptor { get; set; }
		public string NombreServicioSuscriptor { get; set; }
		public string Movimiento { get; set; }
		public int IdCaso { get; set; }
		public int Caso { get; set; }
		public string NombreEstatusCaso { get; set; }
		public int Version { get; set; }
		public string TipoMovimiento { get; set; }
		public int EstatusCaso { get; set; }
		public int EstatusMovimiento { get; set; }
		public DateTime FechaCreacionBuzon { get; set; }
		public string Nombrepaso { get; set; }
		public string TipoP { get; set; }
		public string Status { get; set; }
		public DateTime? FechaAtencion { get; set; }
		public int? sLAEstimado { get; set; }
		public int SLAToleranciaFlujoServicio { get; set; }
		public string IdEncriptado { get; set; }
		public IEnumerable<FlujosServicioDTO> Combo { get; set; }
		public string NombreCompletoSolicitante { get; set; }
		public string NombreMovimiento { get; set; }
		public string NombreEstatusMovimiento { get; set; }
		public string NombreUsuario { get; set; }
		public int CargaLaboral { get; set; }
		public int IdUsuario { get; set; }
		public bool? EstatusUsuario { get; set; }
		public int AutonomiaSuscriptor { get; set; }
		public int CargosSuscriptor { get; set; }
		public int HabilidadSuscriptor { get; set; }
		public string NombreCargo { get; set; }
		public bool? UsuarioVigente { get; set; }
        public bool? indChat { get; set; }
        public string ImgChat { get; set; }
        public string IdSusIntermedario { get; set; }
        public string Idcasoexterno { get; set; }
        public string IdCasoexterno2 { get; set; }
		public string UltimoMovimiento { get; set; }
		public string NombreServicio{get; set;}//carta aval clave
        public string OrigenesDB { get; set; }
        public int Registros { get; set; }
        public string Intermediario { get; set; }
		public int IdPaso
		{
			get;
			set;
		}
        public int? total { get; set; }
		/// <summary>Lista de respuestas XML del caso.</summary>
		[Obsolete("No use este método.")]
		public List<CasoRespuestaXML> RespuestasXML { get; set; }
		/// <summary>Objeto que contiene la respuesta XML del caso.</summary>
		public CasoRespuestaXML RespuestaXMLObjeto { get; set; }
        public string Origen { get; set; }
		public string Diagnostico { get; set; }
	}

	/// <summary> Clase para el manejo del seguimiento de un caso.</summary>
	public sealed class CasoRespuestaXML
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Elemento XML para manejar los datos de la respuesta del caso.</summary>
		private XElement _caso;
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		/// <summary>Identificador único del caso.</summary>
		public string CasoId { get; set; }
		/// <summary>Identificador único del servicio asociado al caso.</summary>
		public string ServicioId { get; set; }
		[Obsolete("No use este método.")]
		public string XMLRespuesta { get; set; }
		/// <summary>Cadena de texto con la respuesta del caso en formato XML.</summary>
		public string RespuestaXML { get; set; }
		/// <summary>Lista de parámetros del caso.</summary>
		public List<CasoRespuestaXMLParametro> Parametros { get; set; }
		#endregion

		public CasoRespuestaXML(string respuestaXML = "")
		{
			try
			{
				this.XMLRespuesta = respuestaXML;   //TODO: obsolete. Delete me.
				this.RespuestaXML = respuestaXML;
				if(!string.IsNullOrWhiteSpace(this.RespuestaXML))
				{
					// SE CREA UN ELEMENTO XML DESDE LA CADENA DE TEXTO QUE CONTIENE EL SEGUIMIENTO DEL CASO.
					_caso = XElement.Parse(this.XMLRespuesta);  //TODO: obsolete. Delete me.
					_caso = XElement.Parse(this.RespuestaXML);
					// SE OBTIENEN LOS DATOS DEL CASO.
					XAttribute attCasoId = _caso.Attribute("IDCASO");
					XAttribute attServicioId = _caso.Attribute("IDSERVICIO");
					if(_caso.ExisteAtributo(attCasoId))
						this.CasoId = attCasoId.Value;
					if(_caso.ExisteAtributo(attServicioId))
						this.ServicioId = attServicioId.Value;
					// SE OBTIENEN LOS DATOS DE LOS PARÁMETROS DEL CASO.
					if(_caso.Descendants("PARAMETRO") != null)
					{
						this.Parametros = new List<CasoRespuestaXMLParametro>();
						var q = from p in _caso.Descendants("PARAMETRO")
								select p;
						foreach(XElement p in q)
						{
							XAttribute attNombre = p.Attribute("NOMBRE");
							XAttribute attValor = p.Attribute("VALOR");
							if(p.ExisteAtributo(attNombre) && p.ExisteAtributo(attValor))
								this.Parametros.Add(
									new CasoRespuestaXMLParametro()
									{
										Nombre = attNombre.Value,
										Valor = attValor.Value,
										CasoId = this.CasoId,
										ServicioId = this.ServicioId
									});
						}
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		#region R U T I N A S
		public string ObtenerParametroValor(string nombre)
		{
			try
			{
				if(ExisteParametro(nombre))
					return ObtenerParametro(nombre).Valor.Trim();
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return null;
		}
		/// <summary>Determina si existe en el caso un parámetro con el nombre especificado.</summary>
		/// <param name="nombre">Nombre del parámetro a consultar.</param>
		/// <returns>Verdadero si el parámetro existe; Falso en caso contrario.</returns>
		public bool ExisteParametro(string nombre)
		{
			try
			{
				return this.Parametros.Exists(p => p.Nombre.ToUpper() == nombre.ToUpper());
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return false;
		}
		/// <summary>Obtiene un parámetro según el nombre especificado.</summary>
		/// <param name="nombre">Nombre del parámetro a consultar.</param>
		/// <returns>Parámetro del caso.</returns>
		public CasoRespuestaXMLParametro ObtenerParametro(string nombre)
		{
			try
			{
				CasoRespuestaXMLParametro parametro = null;
				if(ExisteParametro(nombre))
					parametro = this.Parametros.First(p => p.Nombre.ToUpper() == nombre.ToUpper());
				return parametro;
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return null;
		}
		#endregion
	}
	/// <summary> Clase para el manejo de los parámetros del seguimiento de un caso.</summary>
	public sealed class CasoRespuestaXMLParametro
	{
		public string Nombre { get; set; }
		public string Valor { get; set; }
		public List<CasoRespuestaXMLParametroRespuesta> Respuestas { get; set; }
		/// <summary>Identificador único del caso en el que está involucrado el parámetro.</summary>
		public string CasoId { get; set; }
		/// <summary>Identificador único del servicio en el que está involucrado el parámetro.</summary>
		public string ServicioId { get; set; }

		/// <summary> Evalúa si existe un parámetro con el nombre y el valor suministrados.</summary>
		/// <param name="nombre">Nombre de parámetro a evaluar.</param>
		/// <param name="valor">valor de parámetro a evaluar.</param>
		/// <returns>Verdadero si existe un parámetro con argumentos consultados; Falso en caso contrario.</returns>
		public bool Existe(string nombre, string valor)
		{
			try
			{
				return (this.Nombre == nombre && this.Valor == valor);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return false;
		}
	}
	/// <summary> Clase para el manejo de la historia de los parámetros del seguimiento de un caso.</summary>
	public sealed class CasoRespuestaXMLParametroRespuesta
	{
		public string MovimientoId { get; set; }
		public string MovimientoEstatus { get; set; }
		public string Fecha { get; set; }
		public string PasoId { get; set; }
		public string Valor { get; set; }
	}
}
