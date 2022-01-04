using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase PasoDTO.</summary>
	public class ReporteOpMedDTO
	{
		public int IdMovimiento { get; set; }

		public string Logos { get; set; }
		public string FechaActual { get; set; }
		public string IdExterno { get; set; }
		public string Titular { get; set; }
		public string Beneficiario { get; set; }
		public string CedulaBeneficiario { get; set; }
		public string CedulaTitular { get; set; }
		public string Diagnostico { get; set; }
		public string Tratamiento { get; set; }
		public string AsesorMedico { get; set; }
		public string Sexo { get; set; }
		public string Edad { get; set; }
		public string Parentesco { get; set; }
		public string Servicio { get; set; }
		public string MontoCubierto { get; set; }
		public string Antecedentes { get; set; }
		public string Cicatrices { get; set; }
		public string Peso { get; set; }
		public string Talla { get; set; }
		public string TensionArterial { get; set; }
		public string OpMed { get; set; }
		public string Observaciones { get; set; }
		public string Telefono { get; set; }
		public string Firma { get; set; }
		public string FechaOpMed { get; set; }
		public string SupportIncident { get; set; }
		public string Errores { get; set; }
	}
}
