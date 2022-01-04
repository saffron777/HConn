using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{

        public class ReporteCartaAvalSegurosFederalDTO
        {
            public string Clinica { get; set; }
            public string Contratante { get; set; }
            public string Titular { get; set; }
            public string CiTitular { get; set; }
            public string Asegurado { get; set; }
            public string CiAsegurado { get; set; }
            public double? presupuesto { get; set; }
            public double? MontoCubierto { get; set; }
            public double? Deducible { get; set; }
            public string Diagnostico { get; set; }
            public string Tratamiento { get; set; }
            
            public DateTime? FechaSolicitud { get; set; }
            public DateTime? FechaVencimiento { get; set; }

            public string Parentesco { get; set; }
            public int? Expediente { get; set; }
            public int? Reclamo { get; set; }

            public ReporteSuscriptorSegurosFederal logo { get; set; }

            public string Sexo { get; set; }
            public int? Edad { get; set; }
            public DateTime? FechaNacimiento { get; set; }
            public int? NumPoliza { get; set; }
            public int? NumCertificado { get; set; }
            public string Cobertura { get; set; }
            public string DireccionClinica { get; set; }
            public string tlfClinica { get; set; }
            public double? Monto { get; set; }
            public string Observaciones { get; set; }
            public string Especialidad { get; set; }
            public string Subespecialidad { get; set; }
            public DateTime? VigenciaDesde { get; set; }
            public DateTime? VigenciaHasta { get; set; }
        }

        public class ReporteSuscriptorSegurosFederal
        {
            public string suscriptor { get; set; }
            public string LogoSuscriptor { get; set; }
            public string Numdoc { get; set; }
            public string Telefono { get; set; }
            public string Fax { get; set; }
        }
}
