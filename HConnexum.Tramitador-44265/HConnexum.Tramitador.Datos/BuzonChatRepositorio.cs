using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Datos
{
	public sealed class BuzonChatRepositorio : RepositorioBase<BuzonChat>
	{
		///<summary>Constructor.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public BuzonChatRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}

		///<summary>Consulta los mensajes de chat ocurridos en el seguimiento de un caso determinado.</summary>
		///<param name="pCasoId">Identificador del caso a consultar.</param>
		///<returns>Lista de objetos DTO.</returns>
		public IEnumerable<BuzonChatDTO> ObtenerDTO(int pCasoId)
		{
			var tBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
			var tSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
            var tSuscriptorRecibe = udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from ch in tBuzonChat
							join s in tSuscriptor on ch.IdSuscriptorEnvio equals s.Id
                            join sr in tSuscriptorRecibe on ch.IdSuscriptorRecibe equals sr.Id   into tabRec
                            from tab in tabRec.DefaultIfEmpty()
							where ch.IdCaso == pCasoId
							orderby ch.FechaCreacion descending
							select new BuzonChatDTO
							{
								Id = ch.Id
								, Mensaje = ch.Mensaje
								, IndLeido = ch.IndLeido
								, IdCaso = ch.IdCaso
								, IdMovimiento = ch.IdMovimiento
								, IdSuscriptorEnvio = ch.IdSuscriptorEnvio
								, NombreSuscriptorEnvio = s.Nombre
								, Remitente = ch.Remitente
								, IdSuscriptorRecibe = ch.IdSuscriptorRecibe
                                ,NombreSuscriptorRecibe = tab.Nombre
								, LeidoPor = ch.LeidoPor
								, CreadoPor = ch.CreadoPor
								, FechaCreacion = ch.FechaCreacion
								, ModificadoPor = ch.ModificadoPor
								, FechaModificacion = ch.FechaModificacion
								, FechaValidacion = ch.FechaValidacion
								, IndValido = ch.IndValido
								, IndEliminado = ch.IndEliminado
							};
			return coleccion;
		}

        public int ObtenerMensajesNoLeidoDTO(int pCasoId, int IdSuscriptor)
        {
            var tBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
            var tSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
            int coleccion = (from ch in tBuzonChat
                             where ch.IdCaso == pCasoId && ch.IndLeido == false && ch.IdSuscriptorEnvio != IdSuscriptor
                            select new 
                            {
                                Id = ch.Id
                                
                            }).Count();
            return coleccion;
        }
        public int ObtenerMNoLeidoporMovDTO(int pMovimientoId, int pCasoId, int IdSuscriptor)
        {
            var tBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
            var tSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
            int coleccion = (from ch in tBuzonChat
                             where ch.IdCaso == pCasoId && ch.IndLeido == false && ch.IdSuscriptorEnvio != IdSuscriptor
                             && ch.IdMovimiento == pMovimientoId
                             select new
                             {
                                 Id = ch.Id

                             }).Count();
            return coleccion;
        }

        public IEnumerable<BuzonChatDTO> ObtenerMensajesPendientesNoLeidosDTO(int pCasoId)
        {
            var tBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
            var tSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
            var coleccion = from ch in tBuzonChat
                            where ch.IdCaso == pCasoId
                            select new BuzonChatDTO
                            {
                                Id = ch.Id
                                ,
                                IndLeido = ch.IndLeido
                                ,
                                IdCaso = ch.IdCaso
                              
                            };
            return coleccion;
        }
        public bool ActualizoChatPorIdCaso(int idCaso)
        {
            var tBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
           
            var coleccion = (from ch in tBuzonChat
                            where ch.IdCaso == idCaso
                           orderby ch.FechaCreacion descending 
                            select new BuzonChatDTO
                            {
                                Id = ch.Id
                                ,
                                IndLeido = ch.IndLeido
                                ,
                                IdCaso = ch.IdCaso

                            }).FirstOrDefault();
            if (coleccion != null)
            {
                try
                {
                    udt.IniciarTransaccion();
                    BuzonChat bc = ObtenerPorId(coleccion.Id);
                    if (!bc.IndLeido)
                    {
                        bc.IndLeido = true;

                        udt.MarcarModificado(bc);
                        udt.Commit();
                    }
                 return true;
                }
                catch (Exception)
                {

                    return false;
                }
               

            }
            else return false;
            
        }
	}
}