using System;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface PasosRepuestaDetalle.</summary>
    public interface IPasosRepuestaDetalle : InterfazBase
	{
	    int Id { get; set; }
        string ValorRespuesta { get; set; }
        string DescripcionRespuesta { get; set; }
		int Secuencia { get; set; }
		string IndCierre { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
        string ErroresCustomEditar { set; }
		string Errores { set; }
	}
}