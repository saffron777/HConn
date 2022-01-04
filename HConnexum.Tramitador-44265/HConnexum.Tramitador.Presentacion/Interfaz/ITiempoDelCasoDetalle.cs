using System;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface CasoDetalle.</summary>
    public interface ITiempoDelCasoDetalle : InterfazBase
    {
        int Id { get; set; }
        string FechaCreacion { get; set; }
        string FechaAtencion { get; set; }
        string FechaCerrado { set; }
        string Calculo1 { set; }
        string Calculo2 { set; }
        string SLAestimado { set; }
        string Errores { set; }
    }
}