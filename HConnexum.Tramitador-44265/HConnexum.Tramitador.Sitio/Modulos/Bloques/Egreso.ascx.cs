using System;
using System.Linq;
using System.Data;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
    public partial class Egreso : System.Web.UI.UserControl
    {
		public void Cargar_RadComboBoxProcedimiento()
		{
           RadComboBoxProcedimiento.Enabled = true;
		   RadComboBoxProcedimiento.EnableTextSelection = false;

		   var dt = new DataTable();
		   dt.Columns.Add("Procedimiento");
		   dt.Columns.Add("Valor");

		   DataRow row1 = dt.NewRow(); row1[0] = "APENDICECTOMIA A CIELO ABIERTO";
		   DataRow row2 = dt.NewRow(); row2[0] = "APENDICECTOMÍA POR LAPAROSCOPIA";
		   DataRow row3 = dt.NewRow(); row3[0] = "APENDICECTOMÍA POR LAPAROSCOPIA";
		   DataRow row4 = dt.NewRow(); row4[0] = "COLECISTECTOMIA LAPAROSCOPICA  ";
		   DataRow row5 = dt.NewRow(); row5[0] = "LAPARATOMIA A CIELO ABIERTO  ";
		   DataRow row6 = dt.NewRow(); row6[0] = "LAPARATOMIA A CIELO ABIERTO  ";
		   DataRow row7 = dt.NewRow(); row7[0] = "LAPAROTOMIA GINECOLOGICA";
		   DataRow row8 = dt.NewRow(); row8[0] = "LAPAROTOMIA GINECOLOGICA";
		   DataRow row9 = dt.NewRow(); row9[0] = "LAPAROTOMIA GINECOLOGICA A CIELO ABIERTO  ";
		   DataRow row10 = dt.NewRow(); row10[0] = "LAPAROTOMIA LAPAROSCOPICA  ";
		   DataRow row11 = dt.NewRow(); row11[0] = "LAPAROTOMIA LAPAROSCOPICA  ";

		   dt.Rows.Add(row1);
		   dt.Rows.Add(row2);
		   dt.Rows.Add(row3);
		   dt.Rows.Add(row4);
		   dt.Rows.Add(row5);
		   dt.Rows.Add(row6);
		   dt.Rows.Add(row7);
		   dt.Rows.Add(row8);
		   dt.Rows.Add(row9);
		   dt.Rows.Add(row10);
		   dt.Rows.Add(row11);
		 
		   dt.AcceptChanges();
		   RadComboBoxProcedimiento.DataSource = dt;
		   RadComboBoxProcedimiento.DataBind();
		   RadComboBoxProcedimiento.SelectedIndex = 1;
}
		public void RadComboBoxDiagnostico_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			Cargar_RadComboBoxProcedimiento();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
		//        RadComboBoxProcedimiento.Enabled = false;
		//RadComboBoxProcedimiento.EnableTextSelection = false;
				Cargar_RadComboBoxProcedimiento();
				var dt = new DataTable();
				dt.Columns.Add("Diagnostico");
				dt.Columns.Add("Valor");

				DataRow row1 = dt.NewRow(); row1[0] = "APENDICITIS";
				DataRow row2 = dt.NewRow(); row2[0] = "APENDICITIS AGUDA CON PERITONITIS GENERALIZADA ";
				DataRow row3 = dt.NewRow(); row3[0] = "APENDICITIS AGUDA CON ABSCESO PERITONEAL";
				DataRow row4 = dt.NewRow(); row4[0] = "APENDICITIS AGUDA";
				DataRow row5 = dt.NewRow(); row5[0] = "APENDICITIS";
				DataRow row6 = dt.NewRow(); row6[0] = "PAROTIDITIS, FIEBRE URLIANA,  PAPERAS";
				DataRow row7 = dt.NewRow(); row7[0] = "NEOPLASIA BENIGNA (TUMOR BENIGNO) DE INTESTINO GRUESO, COLON, APENDICE, CIEGO";
				DataRow row8 = dt.NewRow(); row8[0] = "ACALASIA, CARDIOESPASMO, MEGAESOFAGO, APERISTALSIS DE ESOFAGO";
				DataRow row9 = dt.NewRow(); row9[0] = "APENDICITIS AGUDA ";
				DataRow row10 = dt.NewRow(); row10[0] = "TOS FERINA DEBIDA A BORDETELLA PARAPERTUSSIS";
				DataRow row11 = dt.NewRow(); row11[0] = "APENDICITIS AGUDA";
				DataRow row12 = dt.NewRow(); row12[0] = "APENDICITIS AGUDA CON PERITONITIS GENERALIZADA";
				DataRow row13 = dt.NewRow(); row13[0] = "APENDICITIS AGUDA CON ABSCESO PERITONEAL";
				DataRow row14 = dt.NewRow(); row14[0] = "APENDICITIS AGUDA, NO ESPECIFICADA";
				DataRow row15 = dt.NewRow(); row15[0] = "OTROS TIPOS DE APENDICITIS";
				DataRow row16 = dt.NewRow(); row16[0] = "APENDICITIS, NO ESPECIFICADA";
				DataRow row17 = dt.NewRow(); row17[0] = "OTRAS ENFERMEDADES DEL APENDICE";
				DataRow row18 = dt.NewRow(); row18[0] = "CONCRECIONES APENDICULARES";
				DataRow row19 = dt.NewRow(); row19[0] = "ENVENENAMIENTO POR ANESTESICOS Y GASES TERAPEUTICOS";
				DataRow row20 = dt.NewRow(); row20[0] = "ENVENENAMIENTO POR DEPRESORES DEL APETITO";
				DataRow row21 = dt.NewRow(); row21[0] = "EFECTOS ADVERSOS DE GASES ANESTESICOS Y TERAPEUTICOS";
				
				dt.Rows.Add(row1);
				dt.Rows.Add(row2);
				dt.Rows.Add(row3);
				dt.Rows.Add(row4);
				dt.Rows.Add(row5);
				dt.Rows.Add(row6);
				dt.Rows.Add(row7);
				dt.Rows.Add(row8);
				dt.Rows.Add(row9);
				dt.Rows.Add(row10);
				dt.Rows.Add(row11);
				dt.Rows.Add(row12);
				dt.Rows.Add(row13);
				dt.Rows.Add(row14);
				dt.Rows.Add(row15);
				dt.Rows.Add(row16);
				dt.Rows.Add(row17);
				dt.Rows.Add(row18);
				dt.Rows.Add(row19);
				dt.Rows.Add(row20);
				dt.Rows.Add(row21);
				dt.AcceptChanges();
					RadComboBoxDiagnostico.DataSource = dt;
                    RadComboBoxDiagnostico.DataBind();
					RadComboBoxDiagnostico.SelectedIndex = 0;

					var dt2 = new DataTable();
					dt2.Columns.Add("Ingreso");
					dt2.Columns.Add("Valor");
					DataRow row30 = dt2.NewRow(); row30[0] = "Egreso";
					DataRow row31 = dt2.NewRow(); row31[0] = "Extensión";
					dt2.Rows.Add(row30);
					dt2.Rows.Add(row31);
				     dt2.AcceptChanges();

					 RadComboBoxDiagnosticoTipoMovimiento.DataSource = dt2;
					 RadComboBoxDiagnosticoTipoMovimiento.DataBind();
					 

					 var dt3 = new DataTable();
					 dt3.Columns.Add("Modo");
					 dt3.Columns.Add("Valor");
					 DataRow row40 = dt3.NewRow(); row40[0] = "Quirúrgico";
					 DataRow row41 = dt3.NewRow(); row41[0] = "No Quirúrgico";
					 dt3.Rows.Add(row40);
					 dt3.Rows.Add(row41);
					 dt3.AcceptChanges();

					 RadComboBoxDiagnosticoModo.DataSource = dt3;
					 RadComboBoxDiagnosticoModo.DataBind();
					 RadComboBoxDiagnosticoModo.SelectedIndex = 0;
					 TextBox15.Text="Dr. Alex Campos";
			}
        }

		
    }
}