using System;

namespace HConnexum.Tramitador.Sitio
{
    /// <summary>
    /// Clase que permite contener la información correspondiente a los eventos del ascx LoginControl.
    /// </summary>
    public class ControlUsuarioEventArgs : EventArgs
    {
        private String login;
        private String password;
        private String email;
        private String infoRecuperar;
        private int idUsuarioSuscriptorSeleccionado;
        /// <summary>
        /// Propiedad que obtiene o establace el nombre de Usuario.
        /// </summary>
        public String Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
        /// <summary>
        /// Propiedad que obiene o establace la Clave del Usuario.
        /// </summary>
        public String Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        /// <summary>
        /// Propiedad que obiene o establace el E-mail del Usuario.
        /// </summary>
        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        /// <summary>
        /// Propiedad que obtiene o establece la información a ser recuperada: "login" o "password".
        /// </summary>
        public String InfoRecuperar
        {
            get
            {
                return infoRecuperar;
            }
            set
            {
                infoRecuperar = value;
            }
        }

        /// <summary>
        /// Propiedad que obtiene o establece IdUsuarioSuscriptorSeleccionado
        /// </summary>
        public int IdUsuarioSuscriptorSeleccionado
        {
            get
            {
                return idUsuarioSuscriptorSeleccionado;
            }
            set
            {
                idUsuarioSuscriptorSeleccionado = value;
            }
        }
    }
}
