using System;
///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface CasoDetalle.</summary>
    public interface ICasoDetalle : InterfazBase
	{
	    int Id { get; set; }
		string PrioridadAtencion { set; }
		string IdSolicitud { set; }
        string Estatus { get; set; }
		string CreadorPor { set; }
        string FechaCreacion2 { set; }
		string FechaSolicitud {  set; }
		string FechaAnulacion { set; }
		string FechaRechazo { set; }
        string caso { set; }
        string version { set; }
		string Errores { set; }
        string Suscriptor { set; }
        string Servicio { set; }
        string NumDoc { set; }  
        string TipoDoc { set; }
        string Modificado { set; }
        bool indChat { set; }
        
	}
}