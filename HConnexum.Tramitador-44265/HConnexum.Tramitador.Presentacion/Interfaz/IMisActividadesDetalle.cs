using System;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface MovimientoDetalle.</summary>
    public interface IMisActividadesDetalle : InterfazBase
    {
        int Id { get; set; }
        string Servicio { set; }
        string NroCaso { get; set; }
        string EstatusCaso { set; }
        string FechaSolicitud { set; }
        string Solicitante { set; }
        string MovilSolicitante { set; }
        string Estatus { set; }
        string NombrePaso { set; }
        string FechaCreacion { set; }
        string UsuarioCreacion { set; }
        string Descripcion { set; }
        string FechaModificacion { set; }
        string UsuarioModificacion { set; }
        string FechaProceso { set; }
        string Observaciones { set; }
        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }
        string EdadBeneficiario { set; }
        string SexoBeneficiario { set; }
        bool IndObligatorio { set; }
        string Intermediario { set; }
        string Contratante { set; }
        bool habilitoChat { set; }
        string imgChat { set; }
        bool atender { set; }
    }
}