namespace HConnexum.Tramitador.Sitio
{
	public struct ControlPagina
	{
		public string Orden, Filtro;
		public string Accion;
		public int NumeroPagina;
		public int TamañoPagina;

		public ControlPagina(int tamañoPagina)
		{
			Orden = "";
			Filtro = "IndEliminado = 0";
			Accion = "";
			NumeroPagina = 0;
			TamañoPagina = tamañoPagina;
		}
	}
}