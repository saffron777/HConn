using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase PolizasDTO.</summary>
	public class PolizasDTO
	{
		public string Contratante { get; set; }
		public int Certificado { get; set; }
		public string Parentesco { get; set; }
		public string Cobertura { get; set; }
		public string Diasgnostico { get; set; }
		public double MontoFacturado { get; set; }
		public double Deducible { get; set; }
		public double MontoCubierto { get; set; }
	}
}
