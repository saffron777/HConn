using System.Data;
///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface ConfiguracionPasos.</summary>
    public interface IConfiguracionPasos : InterfazBase
    {
        string Estructura { get; set; } 
        DataTable ComboTipo { set; } 
        string Errores { set; }
        DataTable ComboAmbito { set; }
        string ErroresCustomEditar { set; }
    }
}
