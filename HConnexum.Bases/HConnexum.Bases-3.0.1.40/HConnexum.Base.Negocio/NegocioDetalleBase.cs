using System;
using System.Linq;
using System.Reflection;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Negocio
{
	public class NegocioDetalleBase<R, D> : NegocioBase<R>
	{
		public NegocioDetalleBase(AuditoriaDto auditoriaDto) : base(auditoriaDto)
		{
		}
		
		public D ObtenerPorId(int id, int idPaginaModulo, string idSesion, AccionDetalle accion)
		{
			try
			{
				Type clase = typeof(R);
				object[] parametros = new object[3];
				
				if (accion == AccionDetalle.Modificar)
				{
					parametros[0] = id;
					parametros[1] = idPaginaModulo;
					parametros[2] = idSesion;
					object result = (clase.GetMethod(@"BloquearRegistro").Invoke(this.repositorio, parametros));
					
					if (result != null)
						result.ToString(); //Para evitar el warning del VS o JustCode...
				}
				parametros = new object[1];
				parametros[0] = id;
				return (D)(clase.GetMethod(@"ObtenerPorId").Invoke(this.repositorio, parametros));
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		public void GuardarCambios(D datosVista, AccionDetalle accion)
		{
			try
			{
				Type clase = typeof(R);
				object[] parametros = new object[2];
				parametros[0] = datosVista;
				parametros[1] = accion;
				object result = (clase.GetMethod(@"GuardarCambios").Invoke(this.repositorio, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
				
				if (accion == AccionDetalle.Modificar)
				{
					PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(D));
					PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Id").First();
					parametros = new object[1];
					parametros[0] = propinfoV.GetValue(datosVista, null);
					result = (clase.GetMethod(@"LiberarTomado").Invoke(this.repositorio, parametros));
					
					if (result != null)
						result.ToString(); //Para evitar el warning del VS o JustCode...
				}
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
	}
}