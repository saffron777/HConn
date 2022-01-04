namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IPantallaContenedora : InterfazBase
	{
		int Id { get; set; }
		string NombrePagina { get; set; }
		string Errores { set; }
		int IdCaso { get; set; }
		bool swNotPendingStatus { get; set; }
		string IdMovimientoPadre { get; set; }
		string IdMovimientoHijo { get; set; }
	}
}