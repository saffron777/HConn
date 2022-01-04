using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface MovimientoDetalle.</summary>
    public interface IConsultaMovPorSus : InterfazBase
	{
	    string Suscriptor { get; }
        string UsuarioAsignado { get;  }
        string Servicio{get;}
        IEnumerable<SuscriptorDTO> ComboSuscriptor { set; }
        DataTable ComboUsuarioAsignado { set; }
        DataTable ComboServicio { set; }
      
        int NumeroDeRegistros { get; set; }
        IEnumerable<MovimientoDTO> Datos { set; }
        string Errores { set; }
	}
}