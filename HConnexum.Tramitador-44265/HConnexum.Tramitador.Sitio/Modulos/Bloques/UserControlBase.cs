using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class UserControlBase : UserControl
	{
		/// <summary>Evento de inicialización del control de usuario.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_Init(object sender, EventArgs e)
		{
		}

		///<summary>Evento de carga del control de usuario.</summary>
		///<param name="sender">Instancia del control de usuario.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
		}

		/// <summary>Extra una propiedad del ViewState o de un QueryString.</summary>
		/// <param name="propiedad">String. Nombre de la propiedad que se desea extraer.</param>
		/// <returns>String. Valor de la propiedad extraida.</returns>
		protected string ExtraerDeViewStateOQueryString(string propiedad)
		{
			string valor = string.Empty;
			if(this.ViewState[propiedad] != null && this.ViewState[propiedad].ToString() != string.Empty)
				valor = this.ViewState[propiedad].ToString();
			else if(!string.IsNullOrEmpty(this.Request.QueryString[propiedad]))
			{
				valor = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString[propiedad])).Desencriptar();
				this.ViewState[propiedad] = valor;
			}
			return valor;
		}

		#region Propiedades
		Dictionary<string, string> parametrosEntrada;

		/// <summary>Propiedad que obtiene o asigna los Parametros de Entrada.</summary>
		public Dictionary<string, string> ParametrosEntrada
		{
			get
			{
				if(parametrosEntrada == null)
				{
					Object oParametrosEntrada = this.ExtraerDeViewStateOQueryString(@"parametrosEntrada");
					if(oParametrosEntrada != null)
						parametrosEntrada = oParametrosEntrada as Dictionary<string, string>;
				}
				return parametrosEntrada;
			}
			set
			{
				parametrosEntrada = value;
				this.ViewState["parametrosEntrada"] = parametrosEntrada;
			}
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int idmovimiento = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int IdMovimiento
		{
			get
			{
				if(this.idmovimiento == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"idmovimiento");
					if(!string.IsNullOrWhiteSpace(sId))
						this.idmovimiento = int.Parse(sId);
				}
				return this.idmovimiento;
			}
			set
			{
				this.idmovimiento = value;
				this.ViewState["idmovimiento"] = this.idmovimiento;
			}
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int idflujoservicio = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int IdFlujoServicio
		{
			get
			{
				if(this.idflujoservicio == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"idflujoservicio");
					if(sId != "")
						this.idflujoservicio = Convert.ToInt32(sId);
				}
				return this.idflujoservicio;
			}
			set
			{
				this.idflujoservicio = Convert.ToInt32(value);
				this.ViewState["idflujoservicio"] = this.idflujoservicio;
			}
		}

		public UsuarioActual UsuarioActual
		{
			get
			{
				if(this.Session[@"UsuarioActual"] != null)
					return (UsuarioActual)this.Session[@"UsuarioActual"];
				return null;
			}
		}

		string errores = string.Empty;
		string mensaje = string.Empty;

		public string Errores
		{
			get
			{
				return errores;
			}
			set
			{
				errores = value;
			}
		}

		public string Mensaje
		{
			get
			{
				return mensaje;
			}
			set
			{
				mensaje = value;
			}
		}

		public void MostrarMensaje(string pMensaje)
		{
			mensaje = pMensaje;
			if(!string.IsNullOrEmpty(mensaje))
			{
				RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(windowManagerTemp != null)
                    windowManagerTemp.RadAlert(mensaje, 380, 50, "Información", "");
			}
		}

		public void MostrarMensaje()
		{
			MostrarMensaje(mensaje);
		}
		#endregion Propiedades
	}
}