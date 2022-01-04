using System;
using System.Runtime.Serialization;

namespace HConnexum.Tramitador.Sitio
{
    /// <summary>
    /// Representa al control LoginControl proporcionando propiedades y eventos para su uso.
    /// </summary>
    public partial class LoginControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Obtiene o establece si será mostrado o no el teclado virtual del control.
        /// </summary>
        public bool TecladoVirtual { get; set; }
        /// <summary>
        /// Obtiene o establece si se usará o no el teclado real para el ingreso de los datos.
        /// </summary>
        public bool TecladoReal { get; set; }

        [DataContract]
        internal class DatosLogin
        {
            [DataMember]
            internal string login;

            [DataMember]
            internal string password;

            [DataMember]
            internal string email;

            [DataMember]
            internal string infoRecuperar;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Se produce cuando se presiona el botón de Iniciar Sesión.
        /// </summary>
        public event IniciarSesionEventHandler IniciarSesionClicked;

        public delegate void IniciarSesionEventHandler(object sender, ControlUsuarioEventArgs e);

        protected void OnIniciarSesionClick(EventArgs e, string login, string password)
        {
            if (IniciarSesionClicked != null)
            {
                ControlUsuarioEventArgs ee = new ControlUsuarioEventArgs();
                ee.Login = login;
                ee.Password = password;
                IniciarSesionClicked(this, ee);
            }
        }
        /// <summary>
        /// Se produce cuando se presiona el botón de Recuperar.
        /// </summary>
        public event RecuperarEventHandler RecuperarClicked;

        public delegate void RecuperarEventHandler(object sender, ControlUsuarioEventArgs e);

        protected void OnRecuperarClick(EventArgs e, string email, string infoRecuperar)
        {
            if (RecuperarClicked != null)
            {
                ControlUsuarioEventArgs ee = new ControlUsuarioEventArgs();
                ee.Email = email;
                ee.InfoRecuperar = infoRecuperar;
                RecuperarClicked(this, ee);
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            OnIniciarSesionClick(e, RadTextBoxLogin.Text, RadTextBoxPassword.Text);
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            OnRecuperarClick(e, TxtEmail.Text, (infoRecuperar_0.Checked ? "login" : "clave"));
        }
    }
}