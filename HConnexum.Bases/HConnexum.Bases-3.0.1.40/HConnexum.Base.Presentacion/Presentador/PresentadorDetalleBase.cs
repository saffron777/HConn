using System;
using System.Linq;
using System.Reflection;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorDetalleBase<N, D, V> : PresentadorBase<N, V>
	{
		///<summary>Constructor de la clase.</summary>
		///<param name="auditoriaDto">Datos de auditoría.</param>
		public PresentadorDetalleBase(V vista) : base((AuditoriaDto)Reflection.GetPublicProperties(typeof(V)).Where(a => a.Name == @"AuditoriaDto").First().GetValue(vista, null))
		{
			object[] parametros = new object[1];
			parametros[0] = Reflection.GetPublicProperties(typeof(V)).Where(a => a.Name == @"AuditoriaDto").First().GetValue(vista, null);
			this.Negocio = (N)Activator.CreateInstance(typeof(N), parametros);
			this.Vista = vista;
		}
		
		public void MostrarVista()
		{
			try
			{
				PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
				PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Accion").First();
				
				if ((AccionDetalle)(propinfoV.GetValue(this.Vista, null)) == AccionDetalle.Ver || (AccionDetalle)(propinfoV.GetValue(this.Vista, null)) == AccionDetalle.Modificar)
				{
					Type clase = typeof(N);
					object[] parametros = new object[4];
					parametros[3] = propinfoV.GetValue(this.Vista, null);
					propinfoV = propertiesV.Where(a => a.Name == @"Id").First();
					parametros[0] = propinfoV.GetValue(this.Vista, null);
					propinfoV = propertiesV.Where(a => a.Name == @"IdPaginaModulo").First();
					parametros[1] = propinfoV.GetValue(this.Vista, null);
					PropertyInfo propinfoA = propertiesV.Where(a => a.Name == @"AuditoriaDto").First();
					propinfoV = propinfoA.PropertyType.GetProperty(@"IdSesion");
					parametros[2] = propinfoV.GetValue(propinfoA.GetValue(this.Vista, null), null);
					D dto = (D)Activator.CreateInstance(typeof(D), null);
					dto = (D)(clase.GetMethod("ObtenerPorId").Invoke(this.Negocio, parametros));
					this.PresentadorAVista(dto, ref Vista);
				}
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		
		/// <summary>
		/// Método encargado de recibir los datos de la vista e invocar al método de guardado en la capa de Negocio
		/// </summary>
		public void GuardarCambios()
		{
			try
			{
				D dto = (D)Activator.CreateInstance(typeof(D), null);
				this.VistaAPresentador(ref dto, Vista);
				Type clase = typeof(N);
				PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
				PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Accion").First();
				object[] parametros = new object[2];
				parametros[0] = dto;
				parametros[1] = propinfoV.GetValue(this.Vista, null);
				object result = (clase.GetMethod(@"GuardarCambios").Invoke(this.Negocio, parametros));
				
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
		
		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>
		public new void PresentadorAVista<T, U>(T dto, ref U vista)
		{
			base.PresentadorAVista(dto, ref vista);
			this.CargarIdAuditoriaPublicacion(dto, ref vista);
		}
		
		///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>
		public void VistaAPresentador<T, U>(ref T dto, U vista)
		{
			base.VistaAPresentadorBase(ref dto, vista);
			this.AsignarIdAuditoriaPublicacion(ref dto, vista);
		}
		
		private void AsignarIdAuditoriaPublicacion<T, U>(ref T dto, U vista)
		{
			PropertyInfo[] propertiesU = Reflection.GetPublicProperties(typeof(U));
			PropertyInfo propinfoU = propertiesU.Where(a => a.Name == @"Id").First();
			PropertyInfo propinfoT = typeof(T).GetProperty(@"Id");
			propinfoT.SetValue(dto, propinfoU.GetValue(vista, null), null);
			propinfoU = propertiesU.Where(a => a.Name == @"FechaValidez").First();
			propinfoT = typeof(T).GetProperty(@"FechaValidez");
			
			if (string.IsNullOrEmpty((string)propinfoU.GetValue(vista, null)))
				propinfoT.SetValue(dto, null, null);
			else
				propinfoT.SetValue(dto, DateTime.Parse((string)propinfoU.GetValue(vista, null)), null);
			
			propinfoU = propertiesU.Where(a => a.Name == @"IndVigente").First();
			propinfoT = typeof(T).GetProperty(@"IndVigente");
			propinfoT.SetValue(dto, bool.Parse((string)propinfoU.GetValue(vista, null)), null);
			propinfoU = propertiesU.Where(a => a.Name == @"Accion").First();
			propinfoT = typeof(T).GetProperty(@"IndEliminado");
			propinfoT.SetValue(dto, false, null);
			PropertyInfo propinfoI = propertiesU.Where(a => a.Name == @"UsuarioActual").First();
			propinfoU = propinfoI.PropertyType.GetProperty(@"IdUsuarioSuscriptorSeleccionado");
			propinfoT = typeof(T).GetProperty(@"CreadoPor");
			propinfoT.SetValue(dto, propinfoU.GetValue(propinfoI.GetValue(vista, null), null), null);
			propinfoT = typeof(T).GetProperty(@"FechaCreacion");
			propinfoT.SetValue(dto, DateTime.Now, null);
			propinfoI = propertiesU.Where(a => a.Name == @"UsuarioActual").First();
			propinfoU = propinfoI.PropertyType.GetProperty(@"IdUsuarioSuscriptorSeleccionado");
			propinfoT = typeof(T).GetProperty(@"ModificadoPor");
			propinfoT.SetValue(dto, propinfoU.GetValue(propinfoI.GetValue(vista, null), null), null);
			propinfoT = typeof(T).GetProperty(@"FechaModificacion");
			propinfoT.SetValue(dto, DateTime.Now, null);
		}
		
		private void CargarIdAuditoriaPublicacion<T, U>(T dto, ref U vista)
		{
			PropertyInfo[] propertiesU = Reflection.GetPublicProperties(typeof(U));
			PropertyInfo propinfoU = propertiesU.Where(a => a.Name == @"CreadoPor").First();
			PropertyInfo propinfoT = typeof(T).GetProperty(@"CreadoPor");
			propinfoU.SetValue(vista, this.negocioBase.ObtenerNombreUsuario((int)propinfoT.GetValue(dto, null)), null);
			propinfoT = typeof(T).GetProperty(@"FechaCreacion");
			propinfoU = propertiesU.Where(a => a.Name == @"FechaCreacion").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null).ToString(), null);
			propinfoT = typeof(T).GetProperty(@"ModificadoPor");
			propinfoU = propertiesU.Where(a => a.Name == @"ModificadoPor").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null) != null ? this.negocioBase.ObtenerNombreUsuario((int)propinfoT.GetValue(dto, null)) : null, null);
			propinfoT = typeof(T).GetProperty(@"FechaModificacion");
			propinfoU = propertiesU.Where(a => a.Name == @"FechaModificacion").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null) != null ? propinfoT.GetValue(dto, null).ToString() : "", null);
			propinfoT = typeof(T).GetProperty(@"FechaValidez");
			propinfoU = propertiesU.Where(a => a.Name == @"FechaValidez").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null) != null ? propinfoT.GetValue(dto, null).ToString() : "", null);
			propinfoT = typeof(T).GetProperty(@"IndVigente");
			propinfoU = propertiesU.Where(a => a.Name == @"IndVigente").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null).ToString(), null);
			propinfoT = typeof(T).GetProperty(@"IndEliminado");
			propinfoU = propertiesU.Where(a => a.Name == @"IndEliminado").First();
			propinfoU.SetValue(vista, propinfoT.GetValue(dto, null).ToString(), null);
		}
	}
}