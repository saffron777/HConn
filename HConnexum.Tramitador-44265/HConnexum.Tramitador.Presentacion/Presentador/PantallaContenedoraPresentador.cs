using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Reflection;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PantallaContenedoraPresentador : PresentadorDetalleBase<PasosBloque>
	{
		///<summary>Variable vista de la interfaz IPasosBloqueDetalle.</summary>
		readonly IPantallaContenedora vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PantallaContenedoraPresentador(IPantallaContenedora vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		public enum JustificarA
		{
			left,
			rigth,
			Center,
			Justied
		}

		#region Metodos de Pagina Contenedora

		public void ValidarEstatusMovimiento(int idMovimiento)
		{
			udt.IniciarTransaccion();
			ListasValorRepositorio repositorioListaValor = new ListasValorRepositorio(udt);
			MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(udt);
			Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMovimiento);
			if(movimiento.Estatus == repositorioListaValor.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusPendiente"]).Id)
			{
				movimiento.FechaModificacion = DateTime.Now;
                movimiento.FechaEnProceso = System.DateTime.Now;
				movimiento.ModificadoPor = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
				movimiento.UsuarioAsignado = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
				movimiento.IdSuscriptor = vista.UsuarioActual.SuscriptorSeleccionado.Id;
				movimiento.Estatus = repositorioListaValor.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusEnProceso"]).Id;
			}
			udt.MarcarModificado(movimiento);
			udt.Commit();
		}

		public void ObtenerMovimientoHijo(int idMovimiento)
		{
			udt.IniciarTransaccion();
			ListasValorRepositorio repositorioListaValor = new ListasValorRepositorio(udt);
			MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(udt);
			Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMovimiento);
			if(movimiento.Estatus == repositorioListaValor.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusPendiente"]).Id)
			{
				movimiento.FechaModificacion = DateTime.Now;
                movimiento.FechaEnProceso = System.DateTime.Now;
				movimiento.ModificadoPor = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
				movimiento.UsuarioAsignado = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
				movimiento.IdSuscriptor = vista.UsuarioActual.SuscriptorSeleccionado.Id;
				movimiento.Estatus = repositorioListaValor.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusEnProceso"]).Id;
			}
			udt.MarcarModificado(movimiento);
			udt.Commit();
		}

		/// <summary>
		/// Metodo que valida si el paso actual es encadenado para permitir al usuario retroceder
		/// </summary>
		/// <param name="idMovimiento"></param>
		public void ValidaPadreHijoNavegacion(int idMovimiento)
		{
			// Validacion si el paso es un hijo valido 
			ListasValorRepositorio repositorioListaValor = new ListasValorRepositorio(udt);
			PasoRepositorio repositorioPasos = new PasoRepositorio(udt);
			PasoDTO Paso = new PasoDTO();
			Paso = repositorioPasos.PasoActualMovimiento(idMovimiento);
			this.vista.IdMovimientoPadre = Paso.idMovimientoPadre.ToString();
		}

		/// <summary>
		/// Creación dinamica de los controles en la Pagina Contenedora
		/// </summary>
		/// <param name="IdMovimiento">parámetro de entrada</param>
		/// <param name="masterPage">pagina masterpage para navegar los controles de la Pagina Contenedora</param>
		public void CrearControles(int idMovimiento, MasterPage masterPage, string NombreServicio)
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(udt);
				Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMovimiento);
				vista.NombrePagina = movimiento.Nombre;
				Dictionary<string, string> parametrosEntrada = servicio.ObtenerParametrosEntradaDefault(movimiento.IdCaso, movimiento.IdPaso);
				PasosBloqueRepositorio pasosBloqueRepositorio = new PasosBloqueRepositorio(udt);
				IEnumerable<PasosBloqueDTO> pasosBloque = pasosBloqueRepositorio.ObtenerDTO(movimiento.IdPaso);
				#region Creacion de Controles
				int countPanel = 0;
				ContentPlaceHolder myContent = (ContentPlaceHolder)masterPage.FindControl(@"cphBody");
				UpdatePanel pContenedor = myContent.FindControl(@"pContenedor") as UpdatePanel;
				Button btnChat = myContent.FindControl(@"cmdChat") as Button;
				vista.IdCaso = movimiento.IdCaso;
				CasoRepositorio casorep = new CasoRepositorio(udt);
				CasoDTO caso = casorep.ObtnerIndChat(movimiento.Id);
				if(caso != null)
					if(caso.indChat != null)
						btnChat.Visible = (bool)caso.indChat;
					else
						btnChat.Visible = false;

				/*..::[ HEADER ] ::.. */

				PlaceHolder PanelHeader = new PlaceHolder();

				PanelHeader.Controls.Add(new LiteralControl("<fieldset id='FieldSet_Header' class='coolfieldset'><legend><b> Datos del Caso </b></legend>"));
				Table _table = new Table();

				TableRow _tableRowSuscriptor = new TableRow();
				_tableRowSuscriptor.Controls.Add(CrearCelda("<fieldset><span style='text-align:center;'><b>" + vista.UsuarioActual.SuscriptorSeleccionado.Nombre.ToUpper() + "</b></span></fieldset>", null, "100%", JustificarA.Center, 3));
				_table.Rows.Add(_tableRowSuscriptor);

				TableRow _tableRowHeader = new TableRow();
				_tableRowHeader.Controls.Add(CrearCelda("<b>Nro. de Caso</b>", null, "80px", JustificarA.left, 0));
				_tableRowHeader.Controls.Add(CrearCelda("<b>Servicio</b>", null, "300px", JustificarA.left, 0));
				_tableRowHeader.Controls.Add(CrearCelda("<b>Descripción</b>", null, "70%", JustificarA.left, 0));
				_table.Rows.Add(_tableRowHeader);
				_tableRowHeader = new TableRow();

				TextBox _textBoxIdCaso = new TextBox();
				_textBoxIdCaso.ID = "txt_IdCaso";
				_textBoxIdCaso.Enabled = false;
				_textBoxIdCaso.Text = vista.IdCaso.ToString();
				_tableRowHeader.Controls.Add(CrearCelda("", _textBoxIdCaso, "70px", JustificarA.left, 0));

				TextBox _textBoxServicio = new TextBox();
				_textBoxServicio.ID = "txt_NombreServicio";
				_textBoxServicio.Enabled = false;
				_textBoxServicio.Text = NombreServicio;
				_tableRowHeader.Controls.Add(CrearCelda("", _textBoxServicio, "96%", JustificarA.left, 0));

				TextBox _textBoxObservacion = new TextBox();
				_textBoxObservacion.ID = "txt_MovPasoObservacion";
				_textBoxObservacion.Enabled = false;
				_textBoxObservacion.Text = movimiento.Paso.Observacion;
				_tableRowHeader.Controls.Add(CrearCelda("", _textBoxObservacion, "99%", JustificarA.left, 0));

				_table.Rows.Add(_tableRowHeader);

				PanelHeader.Controls.Add(_table);
				PanelHeader.Controls.Add(new LiteralControl("</fieldset><br>"));
				pContenedor.ContentTemplateContainer.Controls.Add(PanelHeader);

				/*..::[ FIN - HEADER ] ::.. */

				foreach(PasosBloqueDTO item in pasosBloque)
				{
					countPanel++;
					PlaceHolder panel;
					if(!string.IsNullOrEmpty(item.TituloBloque))
					{
						panel = new PlaceHolder();
						panel.ID = @"p" + countPanel.ToString();
						LiteralControl fieldset = new LiteralControl("<fieldset id=\"fieldset" + panel.ID + "\" class=\"coolfieldset\">");
						LiteralControl legend = new LiteralControl("<legend><b>" + item.TituloBloque + "</b></legend>");
						panel.Controls.Add(fieldset);
						panel.Controls.Add(legend);
					}
					else
						panel = pContenedor.FindControl(@"p" + (countPanel - 1).ToString()) as PlaceHolder;
					if(!string.IsNullOrEmpty(item.NombrePrograma))
					{
						UserControl controlUsuario = (UserControl)myContent.Page.LoadControl(WebConfigurationManager.AppSettings[@"RutaControlUsuarios"].ToString() + item.NombrePrograma);
						panel.Controls.Add(new LiteralControl(@"<div>"));
						panel.Controls.Add(controlUsuario);
						panel.Controls.Add(new LiteralControl(@"</div>"));
						LiteralControl closeFieldset = new LiteralControl(@"</fieldset>");
						panel.Controls.Add(closeFieldset);
						string sProperty = @"collapsed:" + item.IndColapsado.ToString().ToLower();
						StringBuilder sb = new StringBuilder();
						sb.AppendLine("<script type=\"text/javascript\">");
						sb.AppendLine("	$('#fieldset" + panel.ID + "').coolfieldset({" + sProperty + "});");
						sb.AppendLine(@"</script>");
						panel.Controls.Add(new LiteralControl(sb.ToString()));
						pContenedor.ContentTemplateContainer.Controls.Add(panel);
						panel.Controls.Add(new LiteralControl(@"<br/>"));
						Type controlType = controlUsuario.GetType();
						PropertyInfo controlProperty = controlType.GetProperty(@"ParametrosEntrada");
						if(controlProperty != null)
							controlProperty.SetValue(controlUsuario, parametrosEntrada, null);
						bool actualizable = false;
						foreach(Control control in controlUsuario.Controls)
						{
							actualizable = item.IndActualizable;

							if(control is TextBox)
							{
								TextBox textBox = control as TextBox;
								if(controlUsuario.ToString() == "ASP.modulos_bloques_anulacionom_ascx" &&
									textBox.ID == "ObservacionAnulacion")
									textBox.Enabled = false;
								else
									textBox.Enabled = actualizable;
							}
							if(control is CheckBox)
							{
								CheckBox checkBox = control as CheckBox;
								checkBox.Enabled = actualizable;
							}
							else if(control is RadComboBox)
							{
								RadComboBox radComboBox = control as RadComboBox;
								radComboBox.Enabled = actualizable;
							}
							else if(control is RadDatePicker)
							{
								RadDatePicker radDatePicker = control as RadDatePicker;
								radDatePicker.Enabled = actualizable;
							}
							else if(control is UserControl)
							{
								if(control.GetType().Name == @"controlescomunes_telefono_ascx")
								{
									TextBox txtPais = control.Controls.FindAll().Where(c => c.ID == @"txtCodPais").SingleOrDefault() as TextBox;
									TextBox txtArea = control.Controls.FindAll().Where(c => c.ID == @"txtCodArea").SingleOrDefault() as TextBox;
									TextBox txtNumero = control.Controls.FindAll().Where(c => c.ID == @"txtNumero").SingleOrDefault() as TextBox;
									txtPais.Enabled = txtArea.Enabled = txtNumero.Enabled = actualizable;
								}
								else if(control.GetType().Name == @"controlescomunes_multilinecounter_ascx")
								{
									TextBox textBox = control.Controls[1] as TextBox;
									textBox.Enabled = actualizable;
								}
								else if(control.GetType().Name == @"controlescomunes_fileupload_ascx")
								{
									RadAsyncUpload uploader = control.Controls[2] as RadAsyncUpload;
									uploader.Enabled = actualizable;
								}
							}
						}
					}
					else
					{
						string valor = string.Empty;
						if(parametrosEntrada.Keys.Contains(item.KeyCampoXML.ToUpper()))
							valor = parametrosEntrada[item.KeyCampoXML.ToUpper()];
						Table table = new Table();
						TableRow tableRow = new TableRow();
						tableRow.Controls.Add(CrearCell(item, valor));
						table.Rows.Add(tableRow);
						panel.Controls.Add(table);
						pContenedor.ContentTemplateContainer.Controls.Add(panel);
					}
				}
				#endregion
				if(movimiento.Paso.IndRequiereRespuesta)
				{
					ContentPlaceHolder content = (ContentPlaceHolder)masterPage.FindControl(@"cphBody");
					Button cmdGuardar = (Button)content.Controls.FindAll().Where(c => c.ID == "cmdGuardar").ToList()[0];
					cmdGuardar.Style["display"] = "none";
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
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public TableCell CrearCelda(string valor, Control ctrl, string ancho, JustificarA Justificar, int ColSpan)
		{
			TableCell _tableCell = new TableCell();
			if(ctrl == null)
			{
				_tableCell.Style.Add(HtmlTextWriterStyle.Width, ancho);
				_tableCell.Style.Add(HtmlTextWriterStyle.TextAlign, Justificar.ToString());
				if(ColSpan != 0)
					_tableCell.ColumnSpan = ColSpan;
				_tableCell.Controls.Add(new LiteralControl(valor));
			}
			else
			{
				((TextBox)ctrl).Style.Add(HtmlTextWriterStyle.Width, ancho);
				_tableCell.Controls.Add(ctrl);
			}
			return _tableCell;
		}

		public void CargarDatosControles(int idMovimiento, MasterPage master)
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(udt);
				Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMovimiento);
				vista.NombrePagina = movimiento.Nombre;
				Dictionary<string, string> parametrosEntrada = servicio.ObtenerParametrosEntradaDefault(movimiento.IdCaso, movimiento.IdPaso);
				IEnumerable<Control> controlesEntrada = master.Controls.FindAll().Where(c => c.ID != null && parametrosEntrada.Keys.ToArray().Contains(c.ID.ToUpper()));
				foreach(Control control in controlesEntrada)
				{
					if(control is HiddenField)
					{
						HiddenField hiddenField = control as HiddenField;
						hiddenField.Value = parametrosEntrada[hiddenField.ID.ToUpper()];
					}
					else if(control is TextBox)
					{
						TextBox textBox = control as TextBox;
						textBox.Text = parametrosEntrada[textBox.ID.ToUpper()];
					}
					else if(control is CheckBox)
					{
						CheckBox checkBox = control as CheckBox;
						if(!string.IsNullOrEmpty(parametrosEntrada[checkBox.ID.ToUpper()]))
							checkBox.Checked = bool.Parse(parametrosEntrada[checkBox.ID.ToUpper()]);
					}
					else if(control is RadComboBox)
					{
						RadComboBox radComboBox = control as RadComboBox;
						if(!string.IsNullOrEmpty(parametrosEntrada[radComboBox.ID.ToUpper()]))
							radComboBox.Items.FindItemByText(parametrosEntrada[radComboBox.ID.ToUpper()]).Selected = true;
					}
					else if(control is RadDatePicker)
					{
						RadDatePicker radDatePicker = control as RadDatePicker;
						radDatePicker.DbSelectedDate = ExtensionesString.ConvertirFecha(parametrosEntrada[radDatePicker.ID.ToUpper()]);
					}
					else if(control is UserControl)
					{
						if(control.GetType().Name == @"controlescomunes_telefono_ascx")
						{
							TextBox txtPais = control.Controls.FindAll().Where(c => c.ID == @"txtCodPais").SingleOrDefault() as TextBox;
							TextBox txtArea = control.Controls.FindAll().Where(c => c.ID == @"txtCodArea").SingleOrDefault() as TextBox;
							TextBox txtNumero = control.Controls.FindAll().Where(c => c.ID == @"txtNumero").SingleOrDefault() as TextBox;
							string[] numeroTlf = parametrosEntrada[control.ID.ToUpper()].Split('-');
							if(numeroTlf.Length == 3)
							{
								txtPais.Text = numeroTlf[0];
								txtArea.Text = numeroTlf[1];
								txtNumero.Text = numeroTlf[2];
							}
						}
						else if(control.GetType().Name == @"controlescomunes_multilinecounter_ascx")
						{
							TextBox textBox = control.Controls[1] as TextBox;
							textBox.Text = parametrosEntrada[control.ID.ToUpper()];
							Label label = control.Controls[3] as Label;
							label.Text = (textBox.MaxLength - textBox.Text.Length).ToString();
						}
					}
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
		private TableCell CrearCell(PasosBloqueDTO item, string keyValor)
		{
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(ControlesDinamicos(item.IdTipoControl, item, item.IdListaValor, keyValor));
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
		private Control ControlesDinamicos(int? idTipoControl, PasosBloqueDTO item, int? idLista, string keyValor)
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
					if(!String.IsNullOrEmpty(keyValor))
						radComboBox.Items.FindItemByValue(keyValor).Selected = true;
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
					if(!String.IsNullOrEmpty(keyValor))
						radioButtonList.Items.FindByValue(keyValor).Selected = true;
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
					if(!String.IsNullOrEmpty(keyValor))
					{
						string[] spliValor = keyValor.Split(';');
						foreach(string valor in spliValor)
							if(!String.IsNullOrEmpty(valor))
								checkBoxList.Items.FindByValue(valor).Selected = true;
					}
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
		public int GuardarCambios(int idMovimiento, MasterPage masterPage, bool continuar, bool? bInterventor)
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
				Movimiento movimiento = repositorio.ObtenerPorId(idMovimiento);
				if(movimiento.Estatus == 154 || movimiento.Estatus == 155 || bInterventor == true)
				{
					Dictionary<string, string> parametrosSalida = servicio.ObtenerParametrosSalidaDefault(movimiento.IdCaso, movimiento.IdPaso);
					IEnumerable<Control> controles = masterPage.FindControl(@"cphBody").FindControl(@"pContenedor").Controls.FindAll().Where(c => c.ID != null && parametrosSalida.Keys.ToArray().Contains(c.ID.ToUpper()));
					foreach(Control control in controles)
					{
						#region CargaDatosEnControles
						if(control is HiddenField)
						{
							HiddenField hiddenfield = control as HiddenField;
							parametrosSalida[hiddenfield.ID.ToUpper()] = hiddenfield.Value;
						}
						else if(control is TextBox)
						{
							TextBox textBox = control as TextBox;
							parametrosSalida[textBox.ID.ToUpper()] = textBox.Text;
						}
						else if(control is CheckBox)
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
						#endregion
					}
					servicio.EscribirXml(movimiento.IdCaso, movimiento.Caso1.IdServicio, movimiento.Id, movimiento.Nombre, movimiento.IdPaso, parametrosSalida, vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
					int nidMovimiento = movimiento.Id;
					if(bInterventor == true)
						nidMovimiento = servicio.InterventorMovimiento(idMovimiento, vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
					if(continuar)
						if((bInterventor == true || bInterventor == null))
							return servicio.ProcesarMovimiento(nidMovimiento, movimiento.IdPaso, parametrosSalida, vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
				}
				else
				{
					this.vista.swNotPendingStatus = true;
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
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return 0;
		}

		public int BuscaMensajesPendienteChat(int idCaso)
		{
			try
			{
				BuzonChatRepositorio buzon = new BuzonChatRepositorio(udt);
				int mensajes = buzon.ObtenerMensajesNoLeidoDTO(idCaso, vista.UsuarioActual.SuscriptorSeleccionado.Id);
				return mensajes;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				return 0;
			}
		}

		/// <summary>
		/// enumerado TipoControl para la creación de ControlesDinamicos
		/// </summary>
		enum TipoControl
		{
			ComboBox = 119,
			RadioButtonList = 137,
			CheckBoxList = 138,
		}
		#endregion
	}
}
