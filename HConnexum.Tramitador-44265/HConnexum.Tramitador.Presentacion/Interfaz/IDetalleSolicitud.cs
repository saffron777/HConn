﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IDetalleSolicitud : InterfazBase
	{
		string Errores { set; }
		string Usuario { set; get; }
		string ConexionString { get; }
	}
}
