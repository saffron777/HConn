using System;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio
{
	public class PaginaDetalleBase : PaginaBase
	{
		/// <summary>Evento de inicialización de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"DRSB"))
				this.ClientScript.RegisterClientScriptInclude(typeof(string), @"DRSB", this.ResolveClientUrl(@"~/Scripts/DetalleRadScriptBlock1.js"));
			base.Page_Init(sender, e);
		}

		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
		}

		#region "Propiedades"
		/// <summary>Variable que contiene la acción ejecutada sobre una pagina.</summary>
		AccionDetalle accion = AccionDetalle.NoEstablecida;

		/// <summary>Propiedad que obtiene o asigna la accion.</summary>
		protected AccionDetalle Accion
		{
			get
			{
				if (this.accion == AccionDetalle.NoEstablecida)
				{
					string sAccion = this.ExtraerDeViewStateOQueryString("accion");
					switch (sAccion.ToLower())
					{
						case "agregar":
							this.accion = AccionDetalle.Agregar;
							break;
						case "modificar":
							this.accion = AccionDetalle.Modificar;
							break;
						case "ver":
							this.accion = AccionDetalle.Ver;
							break;
						default:
							this.accion = AccionDetalle.NoEstablecida;
							break;
					}
				}
				return this.accion;
			}
			set
			{
				switch (value)
				{
					case AccionDetalle.Agregar:
						this.ViewState["accion"] = "agregar";
						break;
					case AccionDetalle.Modificar:
						this.ViewState["accion"] = "modificar";
						break;
					case AccionDetalle.Ver:
						this.ViewState["accion"] = "ver";
						break;
					default:
						this.ViewState["accion"] = "ver";
						break;
				}
				this.accion = value;
			}
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int id = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int Id
		{
			get
			{
				if (this.id == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString("id");
					if (sId != "")
						this.id = Convert.ToInt32(sId);
				}
				return this.id;
			}
			set
			{
				this.id = Convert.ToInt32(value);
				this.ViewState["id"] = this.id;
			}
		}
		#endregion "Propiedades"

		#region "Metodos comunes"
		/// <summary>Metodo que permite visualar los botones o ocultarlos dependiendo de la accion.</summary>
		/// <param name="guardar">Button. Boton Guardar.</param>
		/// <param name="agregarOtro">Button. Boton AgregarOtro.</param>
		/// <param name="limpiar">Button. Boton Limpiar. </param>
		/// <param name="accion">AccionDetalle. Accion ejecutada sobre la pagina.</param>
		protected void MostrarBotones(Button guardar, Button agregarOtro, Button limpiar, AccionDetalle accion)
		{
			switch (accion)
			{
				case AccionDetalle.Agregar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = agregarOtro.Visible == false ? false : true;
					limpiar.Visible = limpiar.Visible == false ? false : true;
					break;
				case AccionDetalle.Modificar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				case AccionDetalle.Ver:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				default:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
			}
		}
		/// <summary>Metodo que permite visualar los botones o ocultarlos dependiendo de la accion.</summary>
		/// <param name="guardar">Button. Boton Guardar.</param>
		/// <param name="agregarOtro">Button. Boton AgregarOtro.</param>
		/// <param name="limpiar">Button. Boton Limpiar. </param>
		/// <param name="accion">AccionDetalle. Accion ejecutada sobre la pagina.</param>
		protected void MostrarBotones(RadButton guardar, RadButton agregarOtro, RadButton limpiar, AccionDetalle accion)
		{
			switch (accion)
			{
				case AccionDetalle.Agregar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = agregarOtro.Visible == false ? false : true;
					limpiar.Visible = limpiar.Visible == false ? false : true;
					break;
				case AccionDetalle.Modificar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				case AccionDetalle.Ver:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				default:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
			}
		}
		#endregion "Metodos comunes"
	}
}