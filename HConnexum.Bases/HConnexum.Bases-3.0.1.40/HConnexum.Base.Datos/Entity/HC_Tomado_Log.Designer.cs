//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace HConnexum.Base.Datos.Entity
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class BD_HC_Tomado_Log : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new BD_HC_Tomado_Log object using the connection string found in the 'BD_HC_Tomado_Log' section of the application configuration file.
        /// </summary>
        public BD_HC_Tomado_Log() : base("name=BD_HC_Tomado_Log", "BD_HC_Tomado_Log")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new BD_HC_Tomado_Log object.
        /// </summary>
        public BD_HC_Tomado_Log(string connectionString) : base(connectionString, "BD_HC_Tomado_Log")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new BD_HC_Tomado_Log object.
        /// </summary>
        public BD_HC_Tomado_Log(EntityConnection connection) : base(connection, "BD_HC_Tomado_Log")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TB_Log> TB_Log
        {
            get
            {
                if ((_TB_Log == null))
                {
                    _TB_Log = base.CreateObjectSet<TB_Log>("TB_Log");
                }
                return _TB_Log;
            }
        }
        private ObjectSet<TB_Log> _TB_Log;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TB_Tomado> TB_Tomado
        {
            get
            {
                if ((_TB_Tomado == null))
                {
                    _TB_Tomado = base.CreateObjectSet<TB_Tomado>("TB_Tomado");
                }
                return _TB_Tomado;
            }
        }
        private ObjectSet<TB_Tomado> _TB_Tomado;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TB_Log EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTB_Log(TB_Log tB_Log)
        {
            base.AddObject("TB_Log", tB_Log);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TB_Tomado EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTB_Tomado(TB_Tomado tB_Tomado)
        {
            base.AddObject("TB_Tomado", tB_Tomado);
        }

        #endregion

        #region Function Imports
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        /// <param name="nombreTabla">No Metadata Documentation available.</param>
        /// <param name="idRegistro">No Metadata Documentation available.</param>
        public ObjectResult<Nullable<global::System.Boolean>> VerificacionEliminacion(global::System.String nombreTabla, Nullable<global::System.Int32> idRegistro)
        {
            ObjectParameter nombreTablaParameter;
            if (nombreTabla != null)
            {
                nombreTablaParameter = new ObjectParameter("nombreTabla", nombreTabla);
            }
            else
            {
                nombreTablaParameter = new ObjectParameter("nombreTabla", typeof(global::System.String));
            }
    
            ObjectParameter idRegistroParameter;
            if (idRegistro.HasValue)
            {
                idRegistroParameter = new ObjectParameter("idRegistro", idRegistro);
            }
            else
            {
                idRegistroParameter = new ObjectParameter("idRegistro", typeof(global::System.Int32));
            }
    
            return base.ExecuteFunction<Nullable<global::System.Boolean>>("VerificacionEliminacion", nombreTablaParameter, idRegistroParameter);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="HConnexum.Base.Datos", Name="TB_Log")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TB_Log : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TB_Log object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="fechaLog">Initial value of the FechaLog property.</param>
        /// <param name="tabla">Initial value of the Tabla property.</param>
        /// <param name="transaccionExitosa">Initial value of the TransaccionExitosa property.</param>
        /// <param name="ipUsuario">Initial value of the IpUsuario property.</param>
        public static TB_Log CreateTB_Log(global::System.Int32 id, global::System.DateTime fechaLog, global::System.String tabla, global::System.Boolean transaccionExitosa, global::System.String ipUsuario)
        {
            TB_Log tB_Log = new TB_Log();
            tB_Log.Id = id;
            tB_Log.FechaLog = fechaLog;
            tB_Log.Tabla = tabla;
            tB_Log.TransaccionExitosa = transaccionExitosa;
            tB_Log.IpUsuario = ipUsuario;
            return tB_Log;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String IdSesion
        {
            get
            {
                return _IdSesion;
            }
            set
            {
                OnIdSesionChanging(value);
                ReportPropertyChanging("IdSesion");
                _IdSesion = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("IdSesion");
                OnIdSesionChanged();
            }
        }
        private global::System.String _IdSesion;
        partial void OnIdSesionChanging(global::System.String value);
        partial void OnIdSesionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime FechaLog
        {
            get
            {
                return _FechaLog;
            }
            set
            {
                OnFechaLogChanging(value);
                ReportPropertyChanging("FechaLog");
                _FechaLog = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FechaLog");
                OnFechaLogChanged();
            }
        }
        private global::System.DateTime _FechaLog;
        partial void OnFechaLogChanging(global::System.DateTime value);
        partial void OnFechaLogChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SpEjecutado
        {
            get
            {
                return _SpEjecutado;
            }
            set
            {
                OnSpEjecutadoChanging(value);
                ReportPropertyChanging("SpEjecutado");
                _SpEjecutado = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SpEjecutado");
                OnSpEjecutadoChanged();
            }
        }
        private global::System.String _SpEjecutado;
        partial void OnSpEjecutadoChanging(global::System.String value);
        partial void OnSpEjecutadoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Tabla
        {
            get
            {
                return _Tabla;
            }
            set
            {
                OnTablaChanging(value);
                ReportPropertyChanging("Tabla");
                _Tabla = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Tabla");
                OnTablaChanged();
            }
        }
        private global::System.String _Tabla;
        partial void OnTablaChanging(global::System.String value);
        partial void OnTablaChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Accion
        {
            get
            {
                return _Accion;
            }
            set
            {
                OnAccionChanging(value);
                ReportPropertyChanging("Accion");
                _Accion = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Accion");
                OnAccionChanged();
            }
        }
        private global::System.String _Accion;
        partial void OnAccionChanging(global::System.String value);
        partial void OnAccionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String IdRegistro
        {
            get
            {
                return _IdRegistro;
            }
            set
            {
                OnIdRegistroChanging(value);
                ReportPropertyChanging("IdRegistro");
                _IdRegistro = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("IdRegistro");
                OnIdRegistroChanged();
            }
        }
        private global::System.String _IdRegistro;
        partial void OnIdRegistroChanging(global::System.String value);
        partial void OnIdRegistroChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String RegistroXML
        {
            get
            {
                return _RegistroXML;
            }
            set
            {
                OnRegistroXMLChanging(value);
                ReportPropertyChanging("RegistroXML");
                _RegistroXML = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("RegistroXML");
                OnRegistroXMLChanged();
            }
        }
        private global::System.String _RegistroXML;
        partial void OnRegistroXMLChanging(global::System.String value);
        partial void OnRegistroXMLChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean TransaccionExitosa
        {
            get
            {
                return _TransaccionExitosa;
            }
            set
            {
                OnTransaccionExitosaChanging(value);
                ReportPropertyChanging("TransaccionExitosa");
                _TransaccionExitosa = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TransaccionExitosa");
                OnTransaccionExitosaChanged();
            }
        }
        private global::System.Boolean _TransaccionExitosa;
        partial void OnTransaccionExitosaChanging(global::System.Boolean value);
        partial void OnTransaccionExitosaChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Mensaje
        {
            get
            {
                return _Mensaje;
            }
            set
            {
                OnMensajeChanging(value);
                ReportPropertyChanging("Mensaje");
                _Mensaje = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Mensaje");
                OnMensajeChanged();
            }
        }
        private global::System.String _Mensaje;
        partial void OnMensajeChanging(global::System.String value);
        partial void OnMensajeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String IpUsuario
        {
            get
            {
                return _IpUsuario;
            }
            set
            {
                OnIpUsuarioChanging(value);
                ReportPropertyChanging("IpUsuario");
                _IpUsuario = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("IpUsuario");
                OnIpUsuarioChanged();
            }
        }
        private global::System.String _IpUsuario;
        partial void OnIpUsuarioChanging(global::System.String value);
        partial void OnIpUsuarioChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String HostName
        {
            get
            {
                return _HostName;
            }
            set
            {
                OnHostNameChanging(value);
                ReportPropertyChanging("HostName");
                _HostName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("HostName");
                OnHostNameChanged();
            }
        }
        private global::System.String _HostName;
        partial void OnHostNameChanging(global::System.String value);
        partial void OnHostNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String HostProcess
        {
            get
            {
                return _HostProcess;
            }
            set
            {
                OnHostProcessChanging(value);
                ReportPropertyChanging("HostProcess");
                _HostProcess = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("HostProcess");
                OnHostProcessChanged();
            }
        }
        private global::System.String _HostProcess;
        partial void OnHostProcessChanging(global::System.String value);
        partial void OnHostProcessChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="HConnexum.Base.Datos", Name="TB_Tomado")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TB_Tomado : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TB_Tomado object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="idPaginaModulo">Initial value of the IdPaginaModulo property.</param>
        /// <param name="tabla">Initial value of the Tabla property.</param>
        /// <param name="idRegistro">Initial value of the IdRegistro property.</param>
        /// <param name="idSesionUsuario">Initial value of the IdSesionUsuario property.</param>
        /// <param name="fechaTomado">Initial value of the FechaTomado property.</param>
        /// <param name="loginUsuario">Initial value of the LoginUsuario property.</param>
        public static TB_Tomado CreateTB_Tomado(global::System.Int32 id, global::System.Int32 idPaginaModulo, global::System.String tabla, global::System.Int32 idRegistro, global::System.String idSesionUsuario, global::System.DateTime fechaTomado, global::System.String loginUsuario)
        {
            TB_Tomado tB_Tomado = new TB_Tomado();
            tB_Tomado.Id = id;
            tB_Tomado.IdPaginaModulo = idPaginaModulo;
            tB_Tomado.Tabla = tabla;
            tB_Tomado.IdRegistro = idRegistro;
            tB_Tomado.IdSesionUsuario = idSesionUsuario;
            tB_Tomado.FechaTomado = fechaTomado;
            tB_Tomado.LoginUsuario = loginUsuario;
            return tB_Tomado;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 IdPaginaModulo
        {
            get
            {
                return _IdPaginaModulo;
            }
            set
            {
                OnIdPaginaModuloChanging(value);
                ReportPropertyChanging("IdPaginaModulo");
                _IdPaginaModulo = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IdPaginaModulo");
                OnIdPaginaModuloChanged();
            }
        }
        private global::System.Int32 _IdPaginaModulo;
        partial void OnIdPaginaModuloChanging(global::System.Int32 value);
        partial void OnIdPaginaModuloChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Tabla
        {
            get
            {
                return _Tabla;
            }
            set
            {
                OnTablaChanging(value);
                ReportPropertyChanging("Tabla");
                _Tabla = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Tabla");
                OnTablaChanged();
            }
        }
        private global::System.String _Tabla;
        partial void OnTablaChanging(global::System.String value);
        partial void OnTablaChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 IdRegistro
        {
            get
            {
                return _IdRegistro;
            }
            set
            {
                OnIdRegistroChanging(value);
                ReportPropertyChanging("IdRegistro");
                _IdRegistro = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IdRegistro");
                OnIdRegistroChanged();
            }
        }
        private global::System.Int32 _IdRegistro;
        partial void OnIdRegistroChanging(global::System.Int32 value);
        partial void OnIdRegistroChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String IdSesionUsuario
        {
            get
            {
                return _IdSesionUsuario;
            }
            set
            {
                OnIdSesionUsuarioChanging(value);
                ReportPropertyChanging("IdSesionUsuario");
                _IdSesionUsuario = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("IdSesionUsuario");
                OnIdSesionUsuarioChanged();
            }
        }
        private global::System.String _IdSesionUsuario;
        partial void OnIdSesionUsuarioChanging(global::System.String value);
        partial void OnIdSesionUsuarioChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime FechaTomado
        {
            get
            {
                return _FechaTomado;
            }
            set
            {
                OnFechaTomadoChanging(value);
                ReportPropertyChanging("FechaTomado");
                _FechaTomado = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FechaTomado");
                OnFechaTomadoChanged();
            }
        }
        private global::System.DateTime _FechaTomado;
        partial void OnFechaTomadoChanging(global::System.DateTime value);
        partial void OnFechaTomadoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LoginUsuario
        {
            get
            {
                return _LoginUsuario;
            }
            set
            {
                OnLoginUsuarioChanging(value);
                ReportPropertyChanging("LoginUsuario");
                _LoginUsuario = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LoginUsuario");
                OnLoginUsuarioChanged();
            }
        }
        private global::System.String _LoginUsuario;
        partial void OnLoginUsuarioChanging(global::System.String value);
        partial void OnLoginUsuarioChanged();

        #endregion

    
    }

    #endregion

    
}
