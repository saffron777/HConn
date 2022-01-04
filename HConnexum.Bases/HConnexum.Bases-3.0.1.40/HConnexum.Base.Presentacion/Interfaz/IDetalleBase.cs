using System;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Presentacion.Interfaz
{
	public interface IDetalleBase : IBase
	{
		int Id { get; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string FechaValidez { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
		AccionDetalle Accion { get; }
	}
}