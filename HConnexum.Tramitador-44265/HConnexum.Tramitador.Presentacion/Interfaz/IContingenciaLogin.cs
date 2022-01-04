using System.Collections.Generic;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IContingenciaLogin : InterfazBase
	{
		string Errores { set; }
	}
}