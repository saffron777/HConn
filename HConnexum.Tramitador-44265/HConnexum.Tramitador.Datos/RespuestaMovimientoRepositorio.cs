using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Datos
{
    public sealed class RespuestaMovimientoRepositorio:RepositorioBase<RespuestaMovimiento>
    {
        #region "ConstructoreS"
		///<summary>Constructor de la clase MovimientoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
        public RespuestaMovimientoRepositorio(IUnidadDeTrabajo udt)
            : base(udt)
        {
			
        }
		#endregion "Constructores"
        public RespuestaMovimientoDTO ObtenerDTO(int idMov)
        {
            var tabRespuesta = udt.Sesion.CreateObjectSet<RespuestaMovimiento>();
            var tabPasoRespuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            var coleccion = (from tab in tabRespuesta
                           join tpr in tabPasoRespuesta on tab.IdPasoRespuesta equals tpr.Id
                             where tab.IdMovimiento == idMov
                             select new RespuestaMovimientoDTO
                             {
                                 OMA = tpr.DescripcionRespuesta
                                 
                             }).SingleOrDefault();
            return coleccion;
        }

    }
}
