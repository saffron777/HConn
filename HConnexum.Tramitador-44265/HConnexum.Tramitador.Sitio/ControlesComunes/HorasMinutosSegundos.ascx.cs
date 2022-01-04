using System;
using System.Linq;
using System.ComponentModel;

namespace HConnexum.Tramitador.Sitio.ControlesComunes
{
    public partial class HorasMinutosSegundos : System.Web.UI.UserControl
    {
        public string Horas
        {
            get
            {
                return this.txtHoras.Text;
            }
            set
            {
                this.txtHoras.Text = value.Trim();
            }
        }

        public string Minutos
        {
            get
            {
                return this.txtMinutos.Text;
            }
            set
            {
                this.txtMinutos.Text = value.Trim();
            }
        }

        public string Segundos
        {
            get
            {
                return this.txtSegundos.Text;
            }
            set
            {
                this.txtSegundos.Text = value.Trim();
            }
        }

        public string CantidadTotalEnSegundos
        {
            get
            {
                int horas = 0;
                int.TryParse(txtHoras.Text, out horas);
                int minutos = 0;
                int.TryParse(txtMinutos.Text, out minutos);
                int segundos = 0;
                int.TryParse(txtSegundos.Text, out segundos);
                int total = total = (horas * 3600) + (minutos * 60) + segundos;
                return total.ToString();
            }
            set
            {
                int total = int.Parse(value);
                txtHoras.Text = string.Format("{0:N0}", System.Math.DivRem(total, 3600, out total));
                txtMinutos.Text = string.Format("{0:N0}", System.Math.DivRem(total, 60, out total));
                txtSegundos.Text = string.Format("{0:N0}", total);
            }
        }

        private int _MaxLengthHoras = 0;
        private bool _Enabled = true;
        private bool _IsRequired = false;
        private string _ErrorMessage = @"";
        private string _ValidationGroup = @"";

        [DefaultValue(4)]
        public int MaxLengthHoras
        {
            get
            {
                return this._MaxLengthHoras;
            }
            set
            {
                this._MaxLengthHoras = value;
            }
        }

        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return this._Enabled;
            }
            set
            {
                this._Enabled = value;
            }
        }

        [DefaultValue(false)]
        public bool IsRequired
        {
            get
            {
                return this._IsRequired;
            }
            set
            {
                this._IsRequired = value;
            }
        }

        [DefaultValue(@"")]
        public string ErrorMessage
        {
            get
            {
                return this._ErrorMessage;
            }
            set
            {
                this._ErrorMessage = value;
            }
        }

        [DefaultValue(@"")]
        public string ValidationGroup
        {
            get
            {
                return this._ValidationGroup;
            }
            set
            {
                this._ValidationGroup = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.rimHorasMinutosSegundos.InputSettings[0].Validation.ValidationGroup = this.ValidationGroup;
            this.rimHorasMinutosSegundos.InputSettings[0].Validation.IsRequired = this.IsRequired;
            this.rimHorasMinutosSegundos.InputSettings[0].ErrorMessage = this.ErrorMessage;
            this.rimHorasMinutosSegundos.InputSettings[1].Validation.ValidationGroup = this.ValidationGroup;
            this.rimHorasMinutosSegundos.InputSettings[1].Validation.IsRequired = this.IsRequired;
            this.rimHorasMinutosSegundos.InputSettings[1].ErrorMessage = this.ErrorMessage;
            this.rimHorasMinutosSegundos.InputSettings[2].Validation.ValidationGroup = this.ValidationGroup;
            this.rimHorasMinutosSegundos.InputSettings[2].Validation.IsRequired = this.IsRequired;
            this.rimHorasMinutosSegundos.InputSettings[2].ErrorMessage = this.ErrorMessage;
            MaxLengthControles();
            this.HabilitarControles();
            this.txtHoras.Attributes.Add(@"onkeypress", @"return SoloNumeros(event)");
            this.txtMinutos.Attributes.Add(@"onkeypress", @"return SoloNumeros(event)");
            this.txtSegundos.Attributes.Add(@"onkeypress", @"return SoloNumeros(event)");
        }

        private void MaxLengthControles()
        {
            this.txtHoras.MaxLength = this.MaxLengthHoras;
        }

        private void HabilitarControles()
        {
            this.txtHoras.Enabled = this.Enabled;
            this.txtMinutos.Enabled = this.Enabled;
            this.txtSegundos.Enabled = this.Enabled;
        }
    }
}