using System;
using System.Linq;
using System.Reflection;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorMaestroDetalleBase<ND, NL, D, V> : PresentadorDetalleBase<ND, D, V> // y de PresentadorListaBase por composición
	{
		private readonly PresentadorListaBase<NL, V> presentadorListaBase;
		private readonly string campoEnlace;
		private readonly V vista;
		
		///<summary>Constructor de la clase.</summary>
		///<param name="auditoriaDto">Datos de auditoría.</param>
		public PresentadorMaestroDetalleBase(V vista, string campoEnlace) : base(vista)
		{
			this.vista = vista;
			this.campoEnlace = campoEnlace;
			this.presentadorListaBase = new PresentadorListaBase<NL, V>(vista);
		}
		
		public NL NegocioLista
		{
			get
			{
				return this.presentadorListaBase.Negocio;
			}
		}
		
		public new void MostrarVista()
		{
			PropertyInfo[] propertiesV = Reflection.GetPublicProperties(typeof(V));
			PropertyInfo propinfoV = propertiesV.Where(a => a.Name == @"Accion").First();
			
			if ((AccionDetalle)(propinfoV.GetValue(this.Vista, null)) != AccionDetalle.Agregar)
			{
				propinfoV = propertiesV.Where(a => a.Name == @"Id").First();
				string id = propinfoV.GetValue(this.vista, null).ToString();
				propinfoV = propertiesV.Where(a => a.Name == @"ParametrosFiltro").First();
				string parametrosFiltro = propinfoV.GetValue(this.vista, null).ToString();
				
				if (parametrosFiltro != @"")
				{
					parametrosFiltro += @" AND ";
				}
				parametrosFiltro += string.Format("{0}{1}{2}", this.campoEnlace, @"=", id);
				propinfoV.SetValue(this.presentadorListaBase.Vista, parametrosFiltro, null);
				this.presentadorListaBase.MostrarVista();
			}
		}
		
		public void Eliminar()
		{
			this.presentadorListaBase.Eliminar();
		}
		
		public void ActivarEliminado()
		{
			this.presentadorListaBase.ActivarEliminado();
		}
		
		public void MostrarVistaDetalle()
		{
			base.MostrarVista();
		}
		
		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>
		protected void PresentadorAVistaLista<T, U>(T dto, ref U vista)
		{
			this.presentadorListaBase.PresentadorAVista(dto, ref vista);
		}
	}
}