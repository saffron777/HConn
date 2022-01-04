using System;
using System.Collections.Generic;
using System.Reflection;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Negocio
{
	public class NegocioListaBase<R> : NegocioBase<R>
	{
		public bool VerificiacionEliminacion;
		
		public NegocioListaBase(AuditoriaDto auditoriaDto) : base(auditoriaDto)
		{
			this.VerificiacionEliminacion = true;
		}
		
		public DatosMultiRegistroDto ObtenerFiltrado(string orden, int pagina, int registros, string parametrosFiltro)
		{
			try
			{
				Type clase = typeof(R);
				object[] parametros = new object[4];
				parametros[0] = registros;
				parametros[1] = pagina;
				parametros[2] = parametrosFiltro;
				parametros[3] = orden;
				DatosMultiRegistroDto result = (DatosMultiRegistroDto)(clase.GetMethod(@"ObtenerFiltradoSp").Invoke(this.repositorio, parametros));
				return result;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		public void Eliminar(IList<string> ids)
		{
			try
			{
				Type clase = typeof(R);
				object[] parametros = new object[2];
				parametros[0] = ids;
				parametros[1] = this.VerificiacionEliminacion;
				object result = (clase.GetMethod(@"EliminarLogicoLista").Invoke(this.repositorio, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		public void ActivarEliminado(IList<string> ids)
		{
			try
			{
				Type clase = typeof(R);
				object[] parametros = new object[1];
				parametros[0] = ids;
				object result = (clase.GetMethod(@"ActivarEliminado").Invoke(this.repositorio, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
	}
}
