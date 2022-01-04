using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	public interface IActualizacionDatosMedico : InterfazBaseBloques
	{
		IEnumerable<ListasValorDTO> ComboNuevoContacto { set; }
		string Ptlfmed { get; set; }
		IEnumerable<ListasValorDTO> ComboSolicitudAnulacion { set; }
	}
}