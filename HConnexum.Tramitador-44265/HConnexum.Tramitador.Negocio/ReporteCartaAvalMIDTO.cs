using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
    public class ReporteCartaAvalMIDTO
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

        public int NumPoliza { get; set; }
        public int NumCertificado { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaVencimiento { get; set; }

      public string Parentesco { get; set; }
      public int Expediente { get; set; }
      public int Reclamo { get; set; }

        public ReporteSuscriptor logo { get; set; }

    }
    public class ReporteSuscriptor 
    {
        public string suscriptor { get; set; }
        public string LogoSuscriptor { get; set; }
        public string Numdoc { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }   
    }

}
