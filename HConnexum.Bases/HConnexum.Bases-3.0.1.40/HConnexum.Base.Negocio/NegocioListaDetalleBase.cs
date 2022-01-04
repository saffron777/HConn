using System.Collections.Generic;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Negocio
{
	public class NegocioListaDetalleBase<R, D> : NegocioDetalleBase<R, D>
	{
		private readonly NegocioListaBase<R> negocioListaBase;
		
		public NegocioListaDetalleBase(AuditoriaDto auditoriaDto) : base(auditoriaDto)
		{
			this.negocioListaBase = new NegocioListaBase<R>(auditoriaDto);
		}
		
		public bool VerificiacionEliminacion
		{
			get
			{
				return this.negocioListaBase.VerificiacionEliminacion;
			}
			set
			{
				this.negocioListaBase.VerificiacionEliminacion = value;
			}
		}
		
		public DatosMultiRegistroDto ObtenerFiltrado(string orden, int pagina, int registros, string parametrosFiltro)
		{
			return this.negocioListaBase.ObtenerFiltrado(orden, pagina, registros, parametrosFiltro);
		}
		
		public void Eliminar(IList<string> ids)
		{
			this.negocioListaBase.Eliminar(ids);
		}
		
		public void ActivarEliminado(IList<string> ids)
		{
			this.negocioListaBase.ActivarEliminado(ids);
		}
	}
}