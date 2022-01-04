using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;
using System;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	/// <summary>Interfaz del control Web de usuario 'SeleccionAsegurado'.</summary>
	public interface ISeleccionAsegurado : InterfazBaseBloques
	{
		#region M I E M B R O S   P Ú B L I C O S
		/// <summary>Código del servicio a procesar.</summary>
		string ServicioCodigo { get; set; }
		///<summary>Identificador del suscriptor-proveedor.</summary>
		int SuscriptorProveedorId { get; set; }
		///<summary>Identificador del proveedor en la aplicación 'Gestor'.</summary>
		int ProveedorGestorId { get; set; }
		/// <summary>Código de la estación del proveedor.</summary>
		string ProveedorEstacionCodigo { get; set; }
		/// <summary>Código del proveedor simulado.</summary>
		int SuscriptorProveedor { get; set; }
		#region INTERMEDIARIO
		///<summary>Identificador del intermediario (suscriptor) en la aplicación 'HConnexum'.</summary>
		int SuscriptorIntermediarioId { get; set; }
		///<summary>Identificador del intermediario en la aplicación 'Gestor'.</summary>
		int IntermediarioGestorId { get; set; }
		/// <summary>Nombre del intermediario.</summary>
		string IntermediarioNombre { get; set; }
		/// <summary>Número del fax del intermediario.</summary>
		string IntermediarioFaxNumero { get; set; }
		///<summary>Indica si un intermediario permite o no la condición de que se pueda ingresar los datos de un afiliado 
		/// en caso de que no exista. (Asegurado no encontrado).</summary>
		bool IntermediarioPermiteAseguradoNoEncontrado { get; set; }
		#endregion
		/// <summary>Número de cédula del afiliado.</summary>
		string AfiliadoCedulaNumero { get; set; }
		#region TITULAR
		///<summary>Indica si existe o no el titular en al menos una póliza.</summary>
		bool TitularExiste { get; set; }
		/// <summary>Identificador del titular.</summary>
		string TitularId { set; }
		/// <summary>Nacionalidad del titular.</summary>
		string TitularNacionalidad { set; }
		/// <summary>Número de cédula del titular.</summary>
		string TitularCedulaNumero { set; }
		/// <summary>Número de cédula del titular no existente.</summary>
		string TitularNoExistenteCedulaNumero { get; set; }
		/// <summary>Nombre completo del titular.</summary>
		string TitularNombreCompleto { set; }
		/// <summary>Sexo del titular.</summary>
		string TitularSexo { set; }
		/// <summary>Fecha de nacimiento del titular.</summary>
		DateTime? TitularNacimientoFecha { set; }
		/// <summary>Valor máximo para una fecha de nacimiento de titular no encontrado.</summary>
		DateTime TitularNoEncontradoNacimientoFechaValorMaximo { set; }
		/// <summary>Contratante de la póliza.</summary>
		string TitularContratanteNombre { set; }
		/// <summary>Contratante de la póliza de un titular no existente.</summary>
		string TitularNoExistenteContratanteNombre { get; set; }
		#endregion
		#region BENEFICIARIO
		///<summary>Indica si existe o no el beneficiario en al menos una póliza.</summary>
		bool BeneficiarioExiste { get; set; }
		/// <summary>Identificador del beneficiario en la aplicación del cliente.</summary>
		string BeneficiarioId { get; set; }
		/// <summary>Nacionalidad del beneficiario.</summary>
		string BeneficiarioNacionalidad { get; set; }
		/// <summary>Número de cédula del beneficiario.</summary>
		string BeneficiarioCedulaNumero { set; }
		/// <summary>Número de cédula del beneficiario no existente.</summary>
		string BeneficiarioNoExistenteCedulaNumero { get; set; }
		/// <summary>Nombre completo del beneficiario.</summary>
		string BeneficiarioNombreCompleto { get; set; }
		/// <summary>Sexo del beneficiario.</summary>
		string BeneficiarioSexo { get; set; }
		/// <summary>Parentesco del beneficiario.</summary>
		string BeneficiarioParentesco { get; set; }
		/// <summary>Fecha de nacimiento del beneficiario.</summary>
		DateTime? BeneficiarioNacimientoFecha { get; set; }
		/// <summary>Fecha de nacimiento del beneficiario Hijo.</summary>
		DateTime? BeneficiarioHijoNacimientoFecha { get; set; }
		/// <summary>Edad del beneficiario Hijo.</summary>
		string BeneficiarioHijoEdad { get; set; }
		/// <summary>Valor máximo para una fecha de nacimiento de beneficiario no encontrado.</summary>
		DateTime BeneficiarioNoEncontradoNacimientoFechaValorMaximo { set; }
		/// <summary>Ramo del beneficiario.</summary>
		string BeneficiarioRamo { set; }
		#endregion
		///<summary>Establece el origen de datos para la lista de pólizas asociadas al afiliado.</summary>
		DataTable AfiliadoPolizasDatos { set; }
		///<summary>Obtiene o establece la cantidad de registros de la lista de pólizas asociadas al afiliado.</summary>
		int AfiliadoPolizasRegistrosCantidad { get; set; }
		///<summary>Establece el origen de datos para la lista de integrantes de un grupo familiar asociados a una póliza.</summary>
		DataTable PolizaGrupoFamiliarDatos { set; }
		///<summary>Obtiene o establece la cantidad de registros de la lista de integrantes de un grupo familiar asociados a una póliza.</summary>
		int PolizaGrupoFamiliarRegistrosCantidad { get; set; }
		///<summary>Fecha de inicio de la póliza.</summary>
		string PolizaVigenciaDesde { set; }
		///<summary>Fecha de vencimiento de la póliza.</summary>
		string PolizaVigenciaHasta { set; }
		///<summary>Fecha de la solicitud.</summary>
		string SolicitudFecha { set; }
		///<summary>Establece el valor máximo para la fecha de ocurrencia de una solicitud.</summary>
		DateTime SolicitudOcurrenciaFechaValorMinimo { set; }
		///<summary>Establece el valor máximo para la fecha de ocurrencia de una solicitud.</summary>
		DateTime SolicitudOcurrenciaFechaValorMaximo { set; }
		///<summary>Lista de tipos de documento.</summary>
		IEnumerable<ListasValorDTO> ListaDocumentoTipos { set; }
		///<summary>Lista de tipos de sexo.</summary>
		IEnumerable<ListasValorDTO> ListaSexoTipos { set; }
		///<summary>Lista de tipos de parentesco.</summary>
		IEnumerable<ListasValorDTO> ListaParentescoTipos { set; }
		///<summary>Indica si la sección 'Pólizas asociadas al afiliado' está visible.</summary>
		bool PanelAfiliadoPolizaVisible { get; set; }
		///<summary>Indica si la sección 'Grupo famliar asociado a la póliza' está visible.</summary>
		bool PanelPolizaGrupoFamiliarVisible { get; set; }
		///<summary>Indica si la sección 'Datos de la solicitud' está visible.</summary>
		bool PanelSolicitudDatosVisible { get; set; }
		#endregion
	}
}