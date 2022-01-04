using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HConnexum.Base.Vista
{
	public static class ControlsExtensions
	{
		#region Limpiar Controles
		
		private static readonly Dictionary<Type, Action<Control>> controldefaultsClear = new Dictionary<Type, Action<Control>>()
		{
			#region Controles .NET
			
			{ typeof(TextBox), c => ((TextBox)c).Text = string.Empty },
			{ typeof(DropDownList), c => ((DropDownList)c).SelectedIndex = 0 },
			{ typeof(CheckBox), c => ((CheckBox)c).Checked = false },
			{ typeof(RadioButton), c => ((RadioButton)c).Checked = false },
			
			#endregion
			
			#region Controles Telerik
			
			{ typeof(RadComboBox), c => ((RadComboBox)c).ClearSelection() },
			{ typeof(RadDatePicker), c => ((RadDatePicker)c).DbSelectedDate = null },
			
			#endregion
		};
		
		private static void FindAndInvokeClear(Type type, Control control)
		{
			if (controldefaultsClear.ContainsKey(type))
			{
				if (!ConfigurationManager.AppSettings[@"ClearControls"].Contains(string.Empty + control.ID))
					controldefaultsClear[type].Invoke(control);
			}
		}
		
		public static void ClearControls(this ControlCollection controls)
		{
			foreach (Control control in controls.FindAll())
			{
				FindAndInvokeClear(control.GetType(), control);
			}
		}
		
		public static void ClearControls<T>(this ControlCollection controls) where T : Control
		{
			if (!controldefaultsClear.ContainsKey(typeof(T)))
				return;
			
			foreach (Control control in controls.FindAll<T>())
			{
				FindAndInvokeClear(typeof(T), control);
			}
		}
		
		#endregion
		
		#region Bloquear Controles
		
		private static bool bloquearControles;
		
		private static readonly Dictionary<Type, Action<Control>> controldefaultsBlock = new Dictionary<Type, Action<Control>>()
		{
			#region Controles .NET
			
			{ typeof(TextBox), c => ((TextBox)c).ReadOnly = bloquearControles },
			{ typeof(DropDownList), c => ((DropDownList)c).Enabled = !bloquearControles },
			{ typeof(ListBox), c => ((ListBox)c).Enabled = !bloquearControles },
			{ typeof(CheckBox), c => ((CheckBox)c).Enabled = !bloquearControles },
			{ typeof(RadioButton), c => ((RadioButton)c).Enabled = !bloquearControles },
			
			#endregion
			
			#region Controles Telerik
			
			{ typeof(RadListBox), c => ((RadListBox)c).Enabled = !bloquearControles },
			{ typeof(RadComboBox), c => ((RadComboBox)c).Enabled = !bloquearControles },
			{ typeof(RadDatePicker), c => ((RadDatePicker)c).DatePopupButton.Enabled = ((RadDatePicker)c).EnableTyping = !bloquearControles },
			
			#endregion
		};
		
		private static void FindAndInvokeBlock(Type type, Control control)
		{
			if (controldefaultsBlock.ContainsKey(type))
				if (!ConfigurationManager.AppSettings[@"BlockControls"].Contains(string.Format("{0}{1}", string.Empty, control.ID)))
					controldefaultsBlock[type].Invoke(control);
		}
		
		public static void BlockControls(this ControlCollection controls, bool bloquear)
		{
			bloquearControles = bloquear;
			
			foreach (Control control in controls.FindAll())
			{
				FindAndInvokeBlock(control.GetType(), control);
			}
		}
		
		public static void BlockControls<T>(this ControlCollection controls, bool bloquear) where T : Control
		{
			bloquearControles = bloquear;
			
			if (!controldefaultsBlock.ContainsKey(typeof(T)))
				return;
			foreach (Control control in controls.FindAll<T>())
			{
				FindAndInvokeBlock(typeof(T), control);
			}
		}
		
		#endregion
		
		#region Buscar Controles
		
		public static IEnumerable<Control> FindAll(this ControlCollection collection)
		{
			foreach (Control item in collection)
			{
				if (item.GetType() != typeof(RadFilter))
				{
					yield return item;
					
					if (item.HasControls())
					{
						foreach (var subItem in item.Controls.FindAll())
						{
							yield return subItem;
						}
					}
				}
			}
		}
		
		public static IEnumerable<T> FindAll<T>(this ControlCollection collection) where T : Control
		{
			return collection.FindAll().OfType<T>();
		}
		
		#endregion
		
		#region "Validar Controles"
		
		public static void IsValidSpecialCharacterEmptySpace(this ControlCollection controls)
		{
			foreach (TextBox control in controls.FindAll<TextBox>())
			{
				control.Text = control.Text.Replace(@"<", string.Empty).Replace(@">", string.Empty).Trim();
			}
		}
		
		#endregion
	}
}