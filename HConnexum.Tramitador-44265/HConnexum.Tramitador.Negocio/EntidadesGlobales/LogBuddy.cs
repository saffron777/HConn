using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: LogMetadata.</summary>
	internal interface ILogMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[Integer]
		int IdSesion { get; set; }

		[Required]
		[Date]
		DateTime FechaLog { get; set; }

		[StringLength(255)]
		string SpEjecutado { get; set; }

		[Required]
		[StringLength(255)]
		string Tabla { get; set; }

		[StringLength(40)]
		string Accion { get; set; }

		[StringLength(10)]
		string IdRegistro { get; set; }

		[StringLength(4000)]
		string RegistroXML { get; set; }

		[Required]
		bool TransaccionExitosa { get; set; }

		[StringLength(4000)]
		string Mensaje { get; set; }

		[Required]
		[StringLength(30)]
		string IpUsuario { get; set; }

		[StringLength(128)]
		string HostName { get; set; }

		[StringLength(10)]
		string HostProcess { get; set; }
	}

	[MetadataType(typeof(ILogMetadata))]
	public partial class Log : ILogMetadata
	{
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}