using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ServiceModel;
using Telerik.Web.UI;
using System.Configuration;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PantallaContenedora2Presentador : PresentadorDetalleBase<SolicitudBloque>
	{
		///<summary>Variable vista de la interfaz IPasosBloqueDetalle.</summary>
		readonly IPantallaContenedora2 vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PantallaContenedora2Presentador(IPantallaContenedora2 vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		#region Metodos de Pagina Contenedora
		public bool VerificarBloquePrincipal()
		{
			FlujosServicio flujosServicio = null;
			try
			{
				FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(udt);
				flujosServicio = flujosServicioRepositorio.ObtenerPorId(vista.IdFlujoServicio);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			if(flujosServicio != null)
				return flujosServicio.IndBloqueGenericoSolicitud;
			return false;
		}

		public void MostrarVista()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(udt);
				FlujosServicio flujosServicio = flujosServicioRepositorio.ObtenerPorId(vista.IdFlujoServicio);
				SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
				Suscriptor suscriptor = suscriptorRepositorio.ObtenerPorId(flujosServicio.IdSuscriptor);
				vista.Suscriptor = suscriptor.Nombre;
				DataSet ds = servicio.ObtenerServicioSuscriptorPorId(flujosServicio.IdServicioSuscriptor);
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					vista.Servicio = ds.Tables[0].Rows[0][1].ToString();
				vista.FechaSolicitud = DateTime.Now.ToString();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos()
		{
			try
			{
				ListasValorRepositorio repositorioListasValor = new ListasValorRepositorio(udt);
				vista.ComboTipDoc = repositorioListasValor.ObtenerListaValoresDTO(ConfigurationManager.AppSettings[@"ListaTipoDocumento"]);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public bool BuscarSolicitante()
		{
			SolicitanteRepositorio solicitanteRepositorio = new SolicitanteRepositorio(udt);
			Solicitante solicitante = solicitanteRepositorio.ObtenerPorDocumento(vista.TipDoc, vista.NumDoc);
			if(solicitante != null)
			{
				vista.Nombres = solicitante.Nombre;
				vista.Apellidos = solicitante.Apellido;
				vista.Email = solicitante.Email;
				vista.Telefono = solicitante.Movil.ToString();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creación dinamica de los controles en la Pagina Contenedora
		/// </summary>
		/// <param name="IdMovimiento">parámetro de entrada</param>
		/// <param name="masterPage">pagina masterpage para navegar los controles de la Pagina Contenedora</param>
		public void CrearControles(MasterPage masterPage)
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				SolicitudBloqueRepositorio solicitudBloqueRepositorio = new SolicitudBloqueRepositorio(udt);
				IEnumerable<SolicitudBloqueDTO> solicitudBloque = solicitudBloqueRepositorio.ObtenerDTO(vista.IdFlujoServicio);
				#region Creacion de Controles
				int countPanel = 0;
				ContentPlaceHolder myContent = (ContentPlaceHolder)masterPage.FindControl(@"cphBody");
				//Control divContenedor = myContent.FindControl(@"DivContenedor");
				UpdatePanel pContenedor = myContent.FindControl(@"pContenedor") as UpdatePanel;
				foreach(SolicitudBloqueDTO item in solicitudBloque)
				{
					countPanel++;
					Panel panel;
					if(!string.IsNullOrEmpty(item.TituloBloque))
					{
						panel = new Panel();
						panel.ID = @"p" + countPanel.ToString();
						panel.GroupingText = item.TituloBloque;
						LiteralControl literalControl = new LiteralControl();
						literalControl.Text = @"<br/>";
						panel.Controls.Add(literalControl);
					}
					else
						panel = pContenedor.FindControl(@"p" + (countPanel - 1).ToString()) as Panel;
						//panel = divContenedor.FindControl(@"p" + (countPanel - 1).ToString()) as Panel;
					if(!string.IsNullOrEmpty(item.NombrePrograma))
					{
						UserControl controlUsuario = (UserControl)myContent.Page.LoadControl(WebConfigurationManager.AppSettings[@"RutaControlUsuarios"].ToString() + item.NombrePrograma);
						panel.Controls.Add(controlUsuario);
						//divContenedor.Controls.Add(panel);
						pContenedor.ContentTemplateContainer.Controls.Add(panel);
					}
					else
					{
						Table table = new Table();
						TableRow tableRow = new TableRow();
						tableRow.Controls.Add(CrearCell(item));
						table.Rows.Add(tableRow);
						panel.Controls.Add(table);
						if(!String.IsNullOrEmpty(item.TituloBloque))
							pContenedor.Controls.Add(panel);
							//divContenedor.Controls.Add(panel);
					}
				}
				#endregion
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		/// <summary>
		/// Crea la celda de una tablaHtml donde contiene un 
		/// </summary>
		/// <param name="tRow"></param>
		/// <param name="item"></param>
		/// <param name="KeyValor"></param>
		/// <returns></returns>
		private TableCell CrearCell(SolicitudBloqueDTO item)
		{
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(ControlesDinamicos(item.IdTipoControl, item, item.IdListaValor));
			return tableCell;
		}

		/// <summary>
		/// ControlesDinamicos: crea los controles dinámicos(0.ComboBox|1.RadioButtonList|3.CheckBoxList)
		/// </summary>
		/// <param name="IdTipoControl">tipo de control a crear</param>
		/// <param name="item">parámetro de tipo PasosBloqueDTO</param>
		/// <param name="IdLista">parámetro de la lista valores</param>
		/// <param name="KeyValor">Valor del Key en el Dictionary</param>
		/// <returns>Control creado dinamicamente</returns>
		private Control ControlesDinamicos(int? idTipoControl, SolicitudBloqueDTO item, int? idLista)
		{
			Control control = new Control();
			try
			{
				ListasValorRepositorio repositorio = new ListasValorRepositorio(udt);
				IEnumerable<ListasValorDTO> listasValor = repositorio.ObtenerDTOByIdLista(idLista);
				switch((TipoControl)idTipoControl)
				{
					case TipoControl.ComboBox:
						RadComboBox radComboBox = new RadComboBox();
						radComboBox.ID = item.KeyCampoXML;
						radComboBox.EmptyMessage = @"Seleccione";
						radComboBox.Enabled = item.IndActualizable;
						radComboBox.DataSource = listasValor;
						radComboBox.DataTextField = @"NombreValor";
						radComboBox.DataValueField = @"NombreValorCorto";
						radComboBox.DataBind();
						control = radComboBox;
						break;
					case TipoControl.RadioButtonList:
						RadioButtonList radioButtonList = new RadioButtonList();
						radioButtonList.ID = item.KeyCampoXML;
						radioButtonList.Enabled = item.IndActualizable;
						radioButtonList.DataSource = listasValor;
						radioButtonList.DataTextField = @"NombreValor";
						radioButtonList.DataValueField = @"NombreValorCorto";
						radioButtonList.DataBind();
						control = radioButtonList;
						break;
					case TipoControl.CheckBoxList:
						CheckBoxList checkBoxList = new CheckBoxList();
						checkBoxList.ID = item.KeyCampoXML;
						checkBoxList.Enabled = item.IndActualizable;
						checkBoxList.DataSource = listasValor;
						checkBoxList.DataTextField = @"NombreValor";
						checkBoxList.DataValueField = @"NombreValorCorto";
						checkBoxList.DataBind();
						control = checkBoxList;
						break;
					default:
						break;
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return control;
		}

		/// <summary>
		/// Metodo donde se reescriben los valores en el Dictionary
		/// </summary>
		/// <param name="IdMovimiento">parámetro de entrada</param>
		/// <param name="masterPage">pagina masterpage para navegar los controles de la Pagina Contenedora</param>
		public bool GuardarCambios(MasterPage masterPage)
		{
			string[] resultado = new string[2];
			bool exitoso = false;
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				Dictionary<string, string> parametrosSalida = servicio.ObtenerParametrosSalidaSolicitud(vista.IdFlujoServicio);
				IEnumerable<Control> controles = masterPage.FindControl(@"cphBody").FindControl(@"pContenedor").Controls.FindAll().Where(c => c.ID != null && parametrosSalida.Keys.ToArray().Contains(c.ID.ToUpper()));
				foreach(Control control in controles)
				{
					if(control is TextBox)
					{
						TextBox textBox = control as TextBox;
						parametrosSalida[textBox.ID.ToUpper()] = textBox.Text;
					}
					if(control is CheckBox)
					{
						CheckBox checkBox = control as CheckBox;
						parametrosSalida[checkBox.ID.ToUpper()] = checkBox.Checked.ToString();
					}
					else if(control is RadComboBox)
					{
						RadComboBox radComboBox = control as RadComboBox;
						if(radComboBox.SelectedItem != null)
							parametrosSalida[radComboBox.ID.ToUpper()] = radComboBox.SelectedItem.Text;
					}
					else if(control is RadDatePicker)
					{
						if(control.GetType().Name == @"RadDatePicker")
						{
							RadDatePicker radDatePicker = control as RadDatePicker;
							if(radDatePicker.SelectedDate != null)
								parametrosSalida[radDatePicker.ID.ToUpper()] = radDatePicker.SelectedDate.Value.ToShortDateString();
						}
						else if(control.GetType().Name == @"RadTimePicker")
						{
							RadTimePicker radTimePicker = control as RadTimePicker;
							if(radTimePicker.SelectedDate != null)
								parametrosSalida[radTimePicker.ID.ToUpper()] = radTimePicker.SelectedDate.Value.ToShortTimeString();
						}
					}
					else if(control is UserControl)
					{
						if(control.GetType().Name == @"controlescomunes_telefono_ascx")
						{
							TextBox txtPais = control.Controls.FindAll().Where(c => c.ID == @"txtCodPais").SingleOrDefault() as TextBox;
							TextBox txtArea = control.Controls.FindAll().Where(c => c.ID == @"txtCodArea").SingleOrDefault() as TextBox;
							TextBox txtNumero = control.Controls.FindAll().Where(c => c.ID == @"txtNumero").SingleOrDefault() as TextBox;
							parametrosSalida[control.ID.ToUpper()] = txtPais.Text + @"-" + txtArea.Text + @"-" + txtNumero.Text;
						}
						else if(control.GetType().Name == @"controlescomunes_multilinecounter_ascx")
						{
							TextBox textBox = control.Controls[1] as TextBox;
							parametrosSalida[control.ID.ToUpper()] = textBox.Text;
						}
					}
					else if(control is RadioButtonList)
					{
						RadioButtonList radioButtonList = control as RadioButtonList;
						parametrosSalida[radioButtonList.ID.ToUpper()] = radioButtonList.SelectedValue;
					}
					else if(control is CheckBoxList)
					{
						CheckBoxList checkBoxList = control as CheckBoxList;
						if(parametrosSalida.Keys.Contains(checkBoxList.ID.ToUpper()))
							foreach(ListItem item in checkBoxList.Items)
								if(item.Selected)
									parametrosSalida[checkBoxList.ID.ToUpper()] += item.Value + @";";
					}
					else if(control is HiddenField)
					{
						HiddenField hiddenfield = control as HiddenField;
						parametrosSalida[hiddenfield.ID.ToUpper()] = hiddenfield.Value;
					}
				}
				VerificarDatosGenerales(parametrosSalida);
				//TODO: LLamado al servicio para procesamiento de la solicitud.
				FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(udt);
				FlujosServicioDTO fS = repositorio.ObtenerServicioySuscriptorPorFlujosservicio(vista.IdFlujoServicio);
				ServicioExternosClient servicioExterno = new ServicioExternosClient();
				resultado = servicioExterno.Crea_SolicitudConUsuario(ConfigurationManager.AppSettings[@"OrigenCrearSolicitud"], vista.Nombres, vista.Apellidos, vista.TipDoc, vista.NumDoc, vista.Email, vista.Telefono, fS.IdServicioSuscriptor, null, vista.CasoRelacionado, string.Empty, string.Empty, parametrosSalida, vista.UsuarioActual.Id, vista.UsuarioActual.DatosBase.Nombre1 + @" " + vista.UsuarioActual.DatosBase.Nombre2 + @" " + vista.UsuarioActual.DatosBase.Apellido1 + @" " + vista.UsuarioActual.DatosBase.Apellido2, vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
				vista.Mensaje = resultado[1];
				exitoso = bool.Parse(resultado[0]);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				exitoso = false;
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return exitoso;
		}

		private void VerificarDatosGenerales(Dictionary<string, string> parametrosSalida)
		{
			if(string.IsNullOrWhiteSpace(vista.Nombres))
			{
				
                if (!string.IsNullOrWhiteSpace(parametrosSalida[@"NOMCOMPLETOBENEF"]))
                {
                    vista.Nombres = parametrosSalida[@"NOMCOMPLETOBENEF"];//.Split(',')[1];
                }
                else
                { 
                    vista.Nombres=parametrosSalida[@"PRIMERNOMBREBENEF"] + @" " + parametrosSalida[@"SEGUNDONOMBREBENEF"];
                    parametrosSalida[@"NOMCOMPLETOBENEF"] = parametrosSalida[@"PRIMERNOMBREBENEF"] + " " + parametrosSalida[@"SEGUNDONOMBREBENEF"] + " " + parametrosSalida[@"PRIMERAPELLIDOBENEF"] + " " + parametrosSalida[@"SEGUNDOAPELLIDOBENEF"];
                }
			}
			if(string.IsNullOrWhiteSpace(vista.Apellidos))
			{
				if(string.IsNullOrWhiteSpace(parametrosSalida[@"NOMCOMPLETOBENEF"]))
				vista.Apellidos = parametrosSalida[@"PRIMERAPELLIDOBENEF"] + @" " + parametrosSalida[@"SEGUNDOAPELLIDOBENEF"];
			}
            if (string.IsNullOrWhiteSpace(parametrosSalida[@"NOMCOMPLETOTIT"]))
            {
                parametrosSalida[@"NOMCOMPLETOTIT"] = parametrosSalida[@"PRIMERNOMBRETIT"] + " " + parametrosSalida[@"SEGUNDONOMBRETIT"] + " " + parametrosSalida[@"PRIMERAPELLIDOTIT"] + " " + parametrosSalida[@"SEGUNDOAPELLIDOTIT"];
            }
			if(string.IsNullOrWhiteSpace(vista.TipDoc))
				vista.TipDoc = @"CIV";//parametrosSalida[@"TIPODOCBENEF"];
			if(string.IsNullOrWhiteSpace(vista.NumDoc))
				vista.NumDoc = parametrosSalida[@"NUMDOCBENEF"];
			//if(string.IsNullOrWhiteSpace(vista.Email))
			//    vista.Email = parametrosSalida[@""];
			if(string.IsNullOrWhiteSpace(vista.Telefono))
				vista.Telefono = parametrosSalida[@"TLFSOLICITANTE"];
			//if(string.IsNullOrWhiteSpace(vista.CasoRelacionado))
			//    vista.CasoRelacionado = parametrosSalida[@""];
		}

		/// <summary>
		/// enumerado TipoControl para la creación de ControlesDinamicos
		/// </summary>
		enum TipoControl
		{
			ComboBox = 0,
			RadioButtonList = 1,
			CheckBoxList = 2,
		}
		#endregion
	}
}
