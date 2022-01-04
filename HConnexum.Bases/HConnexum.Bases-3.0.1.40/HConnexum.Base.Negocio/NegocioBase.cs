using System;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Negocio
{
	public class NegocioBase<R>
	{
		protected readonly AuditoriaDto auditoriaDto;
		protected readonly R repositorio;
		
		public NegocioBase(AuditoriaDto auditoriaDto)
		{
			this.auditoriaDto = auditoriaDto;
			object[] parametros = new object[1];
			parametros[0] = auditoriaDto;
			this.repositorio = (R)Activator.CreateInstance(typeof(R), parametros);
		}
	}
}