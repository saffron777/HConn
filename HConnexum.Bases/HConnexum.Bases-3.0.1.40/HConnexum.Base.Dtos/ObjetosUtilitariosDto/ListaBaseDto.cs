using System;

namespace HConnexum.Base.Dtos.ObjetosUtilitariosDto
{
	public class ListaBaseDto
	{
		/// <summary>
		/// Obtiene o establece el Id.
		/// </summary>
		/// <value> Id.</value>
		public int Id { get; set; }
		
		/// <summary>
		/// Obtiene o establece el fecha validez.
		/// </summary>
		/// <value> fecha validez.</value>
		public DateTime? FechaValidez { get; set; }
		
		/// <summary>
		/// Obtiene o establece un valor que indica si [ind vigente].
		/// </summary>
		/// <value><c>true</c>si [ind vigente]; de otra manera, <c>false</c>.</value>
		public bool IndVigente { get; set; }
		
		/// <summary>
		/// Obtiene o establece un valor que indica si [ind eliminado].
		/// </summary>
		/// <value><c>true</c>si [ind eliminado]; de otra manera, <c>false</c>.</value>
		public bool IndEliminado { get; set; }
		
		/// <summary>
		/// Obtiene o establece el tomado.
		/// </summary>
		/// <value> tomado.</value>
		public string Tomado { get; set; }
		
		/// <summary>
		/// Obtiene o establece el usuario tomado.
		/// </summary>
		/// <value> usuario tomado.</value>
		public string UsuarioTomado { get; set; }
	}
}