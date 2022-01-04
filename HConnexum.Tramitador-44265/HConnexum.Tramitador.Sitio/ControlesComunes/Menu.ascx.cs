using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.ControlesComunes
{
	public partial class Menu : UserControl
	{
		#region Eventos Publicas
		/// <summary>
		/// Function Executed on Client Side to open a Window
		/// </summary>
		public string ItemClicking;

		/// <summary>
		/// Valor del Menu (ruta)
		/// </summary>
		public string MenuValueField { get; set; }

		/// <summary>
		/// Texto del Menu
		/// </summary>
		public string MenuTextField { get; set; }

		/// <summary>
		/// Nombre del Campo que contiene el ID del registro
		/// </summary>
		public string MenuFieldID { get; set; }

		/// <summary>
		/// Nombre del Campo que contiene el ID del padre
		/// </summary>
		public string MenuFieldParentID { get; set; }

		/// <summary>
		/// DataSource para llenar el menu
		/// </summary>
		public DataTable MenuDataSource { get; set; }
		#endregion

		#region Metodos Protected
		/// <summary>
		/// Carga de Propiedades del Control
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			menuWeb.OnClientItemClicking = ItemClicking;
		}

		/// <summary>
		/// Este método realiza el binding del data source al menú.
		/// </summary>
		public void MenuDataBind()
		{
			menuWeb.DataValueField = this.MenuValueField;
			menuWeb.DataTextField = this.MenuTextField;
			menuWeb.DataFieldID = this.MenuFieldID;
			menuWeb.DataFieldParentID = this.MenuFieldParentID;
			menuWeb.DataSource = MenuDataSource;
			menuWeb.DataBind();
		}
		#endregion

		/// <summary>
		/// Este método se dispara  después de que termine el databind, asegura que el primer ítem sea expandido hacia abajo 
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void menuWeb_DataBound(object sender, EventArgs e)
		{
			if(menuWeb.Items.Count > 0)
			{
				menuWeb.Items[0].GroupSettings.ExpandDirection = ExpandDirection.Down;
				menuWeb.Items[0].GroupSettings.OffsetY = 3;
				recursiva(menuWeb.Items[0]);
			}
		}

		/// <summary>
		/// Este método  se asegura que todos los itemns hijos del ítem actual se expandan hacia la derecha.
		/// </summary>
		/// <param name="item">Referencia al Item actual.</param>
		private void recursiva(RadMenuItem item)
		{
			for(int i = 0; i < item.Items.Count; i++)
			{
				item.Items[i].GroupSettings.ExpandDirection = ExpandDirection.Right;
				item.Items[i].GroupSettings.OffsetX = 6;
				if(item.Items[i].Items.Count > 0)
					recursiva(item.Items[i]);
			}
		}
	}
}