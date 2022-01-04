using System;

namespace HConnexum.Base.Dtos.ObjetosUtilitariosDto
{
	public class DetalleBaseDto
	{
		/// <summary>
		/// Obtiene o establece el id grupo righ fax entrada.
		/// </summary>
		/// <value> id grupo righ fax entrada.</value>
		public int Id { get; set; }
		
		/// <summary>
		/// Obtiene o establece el creado por.
		/// </summary>
		/// <value> creado por.</value>
		public int CreadoPor { get; set; }
		
		/// <summary>
		/// Obtiene o establece el fecha creacion.
		/// </summary>
		/// <value> fecha creacion.</value>
		public DateTime FechaCreacion { get; set; }
		
		/// <summary>
		/// Obtiene o establece el modificado por.
		/// </summary>
		/// <value> modificado por.</value>
		public int? ModificadoPor { get; set; }
		
		/// <summary>
		/// Obtiene o establece el fecha modificacion.
		/// </summary>
		/// <value> fecha modificacion.</value>
		public DateTime? FechaModificacion { get; set; }
		
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
	}
}