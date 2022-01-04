using System;
using System.Linq;
using System.Reflection;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorListaBase<N, V> : PresentadorBase<N, V>
	{
		///<summary>Constructor de la clase.</summary>
		///<param name="auditoriaDto">Datos de auditoría.</param>
		public PresentadorListaBase(V vista) : base((AuditoriaDto)Reflection.GetPublicProperties(typeof(V)).Where(a => a.Name == @"AuditoriaDto").First().GetValue(vista, null))
		{
			Type clase = typeof(N);
			object[] parametros = new object[1];
			parametros[0] = Reflection.GetPublicProperties(typeof(V)).Where(a => a.Name == @"AuditoriaDto").First().GetValue(vista, null);
			this.Negocio = (N)Activator.CreateInstance(clase, parametros);
			this.Vista = vista;
		}
		
		public void MostrarVista()
		{
			try
			{
				PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
				Type clase = typeof(N);
				object[] parametros = new object[4];
				PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Orden").First();
				parametros[0] = propinfoV.GetValue(this.Vista, null);
				propinfoV = propertiesV.Where(a => a.Name == @"NumeroPagina").First();
				parametros[1] = propinfoV.GetValue(this.Vista, null);
				propinfoV = propertiesV.Where(a => a.Name == @"TamanoPagina").First();
				parametros[2] = propinfoV.GetValue(this.Vista, null);
				propinfoV = propertiesV.Where(a => a.Name == @"ParametrosFiltro").First();
				parametros[3] = propinfoV.GetValue(this.Vista, null);
				DatosMultiRegistroDto datos = (DatosMultiRegistroDto)(clase.GetMethod(@"ObtenerFiltrado").Invoke(this.Negocio, parametros));
				this.PresentadorAVista(datos, ref Vista);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		public void Eliminar()
		{
			try
			{
				PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
				Type clase = typeof(N);
				object[] parametros = new object[1];
				PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"IdsEliminar").First();
				parametros[0] = propinfoV.GetValue(this.Vista, null);
				object result = (clase.GetMethod(@"Eliminar").Invoke(this.Negocio, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException.GetType() == typeof(CustomException))
				{
					PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
					PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Mensaje").First();
					propinfoV.SetValue(this.Vista, ex.InnerException.Message, null);
				}
				else
					throw ex.InnerException;
			}
		}
		
		public void ActivarEliminado()
		{
			try
			{
				PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
				Type clase = typeof(N);
				object[] parametros = new object[1];
				PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"IdsEliminar").First();
				parametros[0] = propinfoV.GetValue(this.Vista, null);
				object result = (clase.GetMethod(@"ActivarEliminado").Invoke(this.Negocio, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>
		public new void PresentadorAVista<T, U>(T dto, ref U vista)
		{
			PropertyInfo[] propertiesU = Reflection.GetPublicProperties(typeof(U));
			PropertyInfo propinfoU = propertiesU.Where(a => a.Name == @"NumeroDeRegistros").First();
			PropertyInfo propinfoT = typeof(T).GetProperty(@"CantidadTotalRegistros");
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null), null);
			propinfoU = propertiesU.Where(a => a.Name == @"Datos").First();
			propinfoT = typeof(T).GetProperty(@"ColeccionRegistros");
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null), null);
		}
	}
}