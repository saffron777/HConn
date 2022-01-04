using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
	public class ListaMovimientosDTO
	{
		public string Movimiento { get; set; }
		public string Cobertura { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public DateTime? HoraSolicitud { get; set; }
		public string Estatus { get; set; }
		public string Resultado { get; set; }
		public string NombreOperador { get; set; }
	}
}
