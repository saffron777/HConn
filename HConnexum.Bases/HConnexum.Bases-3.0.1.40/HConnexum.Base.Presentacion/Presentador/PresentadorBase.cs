using System;
using System.Linq;
using System.Reflection;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Base.Negocio;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorBase<N, V>
	{
		public N Negocio;
		public V Vista;
		protected AuditoriaDto auditoriaDto;
		protected GeneralesNegocio negocioBase;
		
		public PresentadorBase(AuditoriaDto auditoriaDto)
		{
			this.negocioBase = new GeneralesNegocio(auditoriaDto);
			this.auditoriaDto = auditoriaDto;
		}
		
		public void PresentadorAVista<T, U>(T dto, ref U vista)
		{
			PropertyInfo[] propertiesU = typeof(U).GetProperties();
			
			foreach (PropertyInfo propinfoU in propertiesU)
			{
				PropertyInfo propinfoT = typeof(T).GetProperty(propinfoU.Name);
				
				if (propinfoT != null)
					propinfoU.SetValue(vista, propinfoT.GetValue(dto, null), null);
			}
		}
		
		///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>
		public void VistaAPresentadorBase<T, U>(ref T dto, U vista)
		{
			PropertyInfo[] propertiesU = typeof(U).GetProperties();
			
			foreach (PropertyInfo propinfoU in propertiesU)
			{
				PropertyInfo propinfoT = typeof(T).GetProperty(propinfoU.Name);
				
				if (propinfoT != null)
					propinfoT.SetValue(dto, propinfoU.GetValue(vista, null), null);
			}
		}
	}
}