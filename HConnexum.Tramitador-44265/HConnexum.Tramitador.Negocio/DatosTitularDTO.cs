using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class DatosTitularDTO
	{
		public string Asegurado { get; set; }
		public string CedulaTitular { get; set; }
		public string Sexo { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public string Estado { get; set; }
	}
}
