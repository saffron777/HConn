using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IBuzonChat : InterfazBaseBloques
	{
		string Mensaje { get; set; }
		int CasoId { get; set; }
		int? MovimientoId { get; set; }
		int? EnvioSuscriptorId { get; set; }
		string Remitente { get; set; }
		int? CreacionUsuario { get; set; }
		///<summary>Lista de mensajes.</summary>
		IEnumerable<BuzonChatDTO> Mensajes { set; }
	}
}