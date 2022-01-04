using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class BloquesPresentadorBase
	{
		public IUnityContainer container;
		public IUnidadDeTrabajo unidadDeTrabajo;

		public BloquesPresentadorBase()
		{
			this.unidadDeTrabajo = new UnidadDeTrabajo();
		}

		public FlujosServicio ObtenerFlujoServicio(int idFlujoServicio)
		{
			FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(this.unidadDeTrabajo);
			return repositorio.ObtenerPorId(idFlujoServicio);
		}

		public IEnumerable<ListasValorDTO> ObtenerListaValor(string nombreLista)
		{
			ListasValorRepositorio repositorio = new ListasValorRepositorio(this.unidadDeTrabajo);
			return repositorio.ObtenerDTOByNombreLista(nombreLista);
		}

		public ListasValorDTO ObtenerIdListaValor(string nombreLista, string nombreListaValor)
		{
			ListasValorRepositorio repositorio = new ListasValorRepositorio(this.unidadDeTrabajo);
			return repositorio.ObtenerListaValoresDTO(nombreLista, nombreListaValor);
		}

		public string ObtenerNombreUsuario(int? id)
		{
			UsuarioRepositorio repositorio = new UsuarioRepositorio(this.unidadDeTrabajo);
			if(id != null && id > 0)
			{
				UsuarioDTO usuario = repositorio.ObtenerUsuarioPorIdUsuarioSuscriptor(id.Value);
				if(usuario != null)
					return usuario.LoginUsuario;
			}
			return string.Empty;
		}

		public Movimiento ObtenerMovimiento(int id)
		{
			MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
			Movimiento movimiento = movimientoRepositorio.ObtenerPorId(id);
			return movimiento;
		}

		public Solicitud ObtenerSolicitud(int idMovimiento)
		{
			MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
			Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMovimiento);
			if(movimiento != null)
				return movimiento.Caso1.Solicitud;
			return null;
		}
	}
}
