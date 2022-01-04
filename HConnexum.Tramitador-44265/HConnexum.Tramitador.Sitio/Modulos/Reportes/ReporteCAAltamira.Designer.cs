namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
    partial class ReporteCAAltamira
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ReporteCAAltamira ) );
			Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
			Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
			this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
			this.currentTimeTextBox = new Telerik.Reporting.TextBox();
			this.pictureBox1 = new Telerik.Reporting.PictureBox();
			this.textBox7 = new Telerik.Reporting.TextBox();
			this.textBox12 = new Telerik.Reporting.TextBox();
			this.textBox6 = new Telerik.Reporting.TextBox();
			this.barcode1 = new Telerik.Reporting.Barcode();
			this.detail = new Telerik.Reporting.DetailSection();
			this.htmlTextBox3 = new Telerik.Reporting.HtmlTextBox();
			this.textBox25 = new Telerik.Reporting.TextBox();
			this.textBox18 = new Telerik.Reporting.TextBox();
			this.textBox14 = new Telerik.Reporting.TextBox();
			this.textBox3 = new Telerik.Reporting.TextBox();
			this.textBox5 = new Telerik.Reporting.TextBox();
			this.textBox36 = new Telerik.Reporting.TextBox();
			this.textBox26 = new Telerik.Reporting.TextBox();
			this.textBox11 = new Telerik.Reporting.TextBox();
			this.textBox10 = new Telerik.Reporting.TextBox();
			this.textBox9 = new Telerik.Reporting.TextBox();
			this.textBox8 = new Telerik.Reporting.TextBox();
			this.textBox2 = new Telerik.Reporting.TextBox();
			this.textBox1 = new Telerik.Reporting.TextBox();
			this.textBox4 = new Telerik.Reporting.TextBox();
			this.Certificado = new Telerik.Reporting.TextBox();
			this.htmlTextBox1 = new Telerik.Reporting.HtmlTextBox();
			this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
			this.textBox46 = new Telerik.Reporting.TextBox();
			this.textBox45 = new Telerik.Reporting.TextBox();
			this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// pageHeaderSection1
			// 
			this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm( 2.6000001430511475D );
			this.pageHeaderSection1.Items.AddRange( new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pictureBox1,
            this.textBox7,
            this.textBox12,
            this.textBox6,
            this.barcode1} );
			this.pageHeaderSection1.Name = "pageHeaderSection1";
			// 
			// currentTimeTextBox
			// 
			this.currentTimeTextBox.Format = "{0:d}";
			this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 13.000000953674316D ), Telerik.Reporting.Drawing.Unit.Cm( 0.99980068206787109D ) );
			this.currentTimeTextBox.Name = "currentTimeTextBox";
			this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 1.9999990463256836D ), Telerik.Reporting.Drawing.Unit.Cm( 0.49999964237213135D ) );
			this.currentTimeTextBox.StyleName = "PageInfo";
			this.currentTimeTextBox.Value = "= Now()";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.86583292484283447D ), Telerik.Reporting.Drawing.Unit.Cm( 0D ) );
			this.pictureBox1.MimeType = "";
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 2.6000001430511475D ), Telerik.Reporting.Drawing.Unit.Cm( 2.6000001430511475D ) );
			this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
			this.pictureBox1.Value = "=Fields.logo.LogoSuscriptor";
			// 
			// textBox7
			// 
			this.textBox7.CanGrow = true;
			this.textBox7.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 9.5015554428100586D ), Telerik.Reporting.Drawing.Unit.Cm( 0.99980068206787109D ) );
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.4982447624206543D ), Telerik.Reporting.Drawing.Unit.Cm( 0.49999955296516418D ) );
			this.textBox7.Style.Font.Bold = true;
			this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox7.StyleName = "Caption";
			this.textBox7.Value = "Fecha de emisión:";
			// 
			// textBox12
			// 
			this.textBox12.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 12D ), Telerik.Reporting.Drawing.Unit.Cm( 1.5D ) );
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.0000016689300537D ), Telerik.Reporting.Drawing.Unit.Cm( 0.60000008344650269D ) );
			this.textBox12.Style.Font.Bold = true;
			this.textBox12.StyleName = "Caption";
			this.textBox12.Value = "= Fields.Expediente + \"/\" + Fields.Reclamo";
			// 
			// textBox6
			// 
			this.textBox6.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 9.0997991561889648D ), Telerik.Reporting.Drawing.Unit.Cm( 1.5D ) );
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 2.9000012874603271D ), Telerik.Reporting.Drawing.Unit.Cm( 0.60000008344650269D ) );
			this.textBox6.Style.Font.Bold = true;
			this.textBox6.Value = "Carta Aval Nro: ";
			// 
			// barcode1
			// 
			this.barcode1.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 11.499900817871094D ), Telerik.Reporting.Drawing.Unit.Cm( 0.00010002215276472271D ) );
			this.barcode1.Name = "barcode1";
			this.barcode1.ShowText = false;
			this.barcode1.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.50010085105896D ), Telerik.Reporting.Drawing.Unit.Cm( 0.999500572681427D ) );
			this.barcode1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.barcode1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
			this.barcode1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
			this.barcode1.Value = "= Fields.Reclamo";
			// 
			// detail
			// 
			this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm( 16.399999618530273D );
			this.detail.Items.AddRange( new Telerik.Reporting.ReportItemBase[] {
            this.htmlTextBox3,
            this.textBox25,
            this.textBox18,
            this.textBox14,
            this.textBox3,
            this.textBox5,
            this.textBox36,
            this.textBox26,
            this.textBox11,
            this.textBox10,
            this.textBox9,
            this.textBox8,
            this.textBox2,
            this.textBox1,
            this.textBox4,
            this.Certificado,
            this.htmlTextBox1} );
			this.detail.Name = "detail";
			// 
			// htmlTextBox3
			// 
			this.htmlTextBox3.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 4.8999996185302734D ) );
			this.htmlTextBox3.Name = "htmlTextBox3";
			this.htmlTextBox3.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 14.199999809265137D ), Telerik.Reporting.Drawing.Unit.Cm( 2.5923166275024414D ) );
			this.htmlTextBox3.Value = resources.GetString( "htmlTextBox3.Value" );
			// 
			// textBox25
			// 
			this.textBox25.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 6.0999999046325684D ), Telerik.Reporting.Drawing.Unit.Cm( 7.899998664855957D ) );
			this.textBox25.Name = "textBox25";
			this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 8.90000057220459D ), Telerik.Reporting.Drawing.Unit.Cm( 0.44146770238876343D ) );
			this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox25.Value = "=Fields.Diagnostico + \"/\" + Fields.Tratamiento";
			// 
			// textBox18
			// 
			this.textBox18.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 4.4716668128967285D ), Telerik.Reporting.Drawing.Unit.Cm( 4.0795378684997559D ) );
			this.textBox18.Name = "textBox18";
			this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 10.528332710266113D ), Telerik.Reporting.Drawing.Unit.Cm( 0.44979164004325867D ) );
			this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox18.Value = "=Fields.Asegurado";
			// 
			// textBox14
			// 
			this.textBox14.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 4.475733757019043D ), Telerik.Reporting.Drawing.Unit.Cm( 3.6297457218170166D ) );
			this.textBox14.Name = "textBox14";
			this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 10.524267196655273D ), Telerik.Reporting.Drawing.Unit.Cm( 0.44979164004325867D ) );
			this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox14.Value = "=Fields.CiTitular";
			// 
			// textBox3
			// 
			this.textBox3.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 4.475733757019043D ), Telerik.Reporting.Drawing.Unit.Cm( 3.1799542903900146D ) );
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 10.524266242980957D ), Telerik.Reporting.Drawing.Unit.Cm( 0.44979164004325867D ) );
			this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox3.Value = "=Fields.Titular";
			// 
			// textBox5
			// 
			this.textBox5.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 4.4716668128967285D ), Telerik.Reporting.Drawing.Unit.Cm( 2.7301626205444336D ) );
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 10.528332710266113D ), Telerik.Reporting.Drawing.Unit.Cm( 0.42333331704139709D ) );
			this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox5.Value = "=Fields.Contratante";
			// 
			// textBox36
			// 
			this.textBox36.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 0.60833251476287842D ) );
			this.textBox36.Name = "textBox36";
			this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 14.199999809265137D ), Telerik.Reporting.Drawing.Unit.Cm( 0.5027083158493042D ) );
			this.textBox36.Style.Font.Bold = true;
			this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox36.Value = "=Fields.Clinica";
			// 
			// textBox26
			// 
			this.textBox26.CanGrow = true;
			this.textBox26.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 7.899998664855957D ) );
			this.textBox26.Name = "textBox26";
			this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 4.9031252861022949D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox26.Style.Font.Bold = true;
			this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox26.StyleName = "Caption";
			this.textBox26.Value = "Diagnóstico/Procedimiento:";
			// 
			// textBox11
			// 
			this.textBox11.CanGrow = true;
			this.textBox11.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 3.6500072479248047D ) );
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.2716670036315918D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox11.Style.Font.Bold = true;
			this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox11.StyleName = "Caption";
			this.textBox11.Value = "C.I.:";
			// 
			// textBox10
			// 
			this.textBox10.CanGrow = true;
			this.textBox10.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 4.0997986793518066D ) );
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.2716667652130127D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox10.Style.Font.Bold = true;
			this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox10.StyleName = "Caption";
			this.textBox10.Value = "Beneficiario:";
			// 
			// textBox9
			// 
			this.textBox9.CanGrow = true;
			this.textBox9.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 3.2002155780792236D ) );
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.2716672420501709D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox9.Style.Font.Bold = true;
			this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox9.StyleName = "Caption";
			this.textBox9.Value = "Titular:";
			// 
			// textBox8
			// 
			this.textBox8.CanGrow = true;
			this.textBox8.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 2.7504239082336426D ) );
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.2716667652130127D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox8.Style.Font.Bold = true;
			this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox8.StyleName = "Caption";
			this.textBox8.Value = "Contratante:";
			// 
			// textBox2
			// 
			this.textBox2.CanGrow = true;
			this.textBox2.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 1.2962491512298584D ) );
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 2.2999989986419678D ), Telerik.Reporting.Drawing.Unit.Cm( 0.49999955296516418D ) );
			this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox2.StyleName = "Caption";
			this.textBox2.Value = "Presente.-";
			// 
			// textBox1
			// 
			this.textBox1.CanGrow = true;
			this.textBox1.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 0.10562445223331451D ) );
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 2.2999999523162842D ), Telerik.Reporting.Drawing.Unit.Cm( 0.48333308100700378D ) );
			this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox1.StyleName = "Caption";
			this.textBox1.Value = "Señores:";
			// 
			// textBox4
			// 
			this.textBox4.CanGrow = true;
			this.textBox4.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 2.3087568283081055D ) );
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 3.2716667652130127D ), Telerik.Reporting.Drawing.Unit.Cm( 0.4414675235748291D ) );
			this.textBox4.Style.Font.Bold = true;
			this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
			this.textBox4.StyleName = "Caption";
			this.textBox4.Value = "Póliza-Certificado:";
			// 
			// Certificado
			// 
			this.Certificado.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 4.475733757019043D ), Telerik.Reporting.Drawing.Unit.Cm( 2.2884955406188965D ) );
			this.Certificado.Name = "Certificado";
			this.Certificado.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 10.524267196655273D ), Telerik.Reporting.Drawing.Unit.Cm( 0.42333331704139709D ) );
			this.Certificado.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 10D );
			this.Certificado.Value = "= Fields.Poliza + \" - \" + Fields.Certificado";
			// 
			// htmlTextBox1
			// 
			this.htmlTextBox1.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 8.69999885559082D ) );
			this.htmlTextBox1.Name = "htmlTextBox1";
			this.htmlTextBox1.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 14.199999809265137D ), Telerik.Reporting.Drawing.Unit.Cm( 7.500002384185791D ) );
			this.htmlTextBox1.Value = resources.GetString( "htmlTextBox1.Value" );
			// 
			// pageFooterSection1
			// 
			this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm( 1.512710452079773D );
			this.pageFooterSection1.Items.AddRange( new Telerik.Reporting.ReportItemBase[] {
            this.textBox46,
            this.textBox45} );
			this.pageFooterSection1.Name = "pageFooterSection1";
			// 
			// textBox46
			// 
			this.textBox46.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 0.800000011920929D ), Telerik.Reporting.Drawing.Unit.Cm( 0.20396526157855988D ) );
			this.textBox46.Name = "textBox46";
			this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 2.06583309173584D ), Telerik.Reporting.Drawing.Unit.Cm( 0.43270725011825562D ) );
			this.textBox46.StyleName = "PageInfo";
			this.textBox46.Value = "Conforme:";
			// 
			// textBox45
			// 
			this.textBox45.CanGrow = true;
			this.textBox45.Location = new Telerik.Reporting.Drawing.PointU( Telerik.Reporting.Drawing.Unit.Cm( 8.9389591217041016D ), Telerik.Reporting.Drawing.Unit.Cm( 0.20396526157855988D ) );
			this.textBox45.Name = "textBox45";
			this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU( Telerik.Reporting.Drawing.Unit.Cm( 6.0610413551330566D ), Telerik.Reporting.Drawing.Unit.Cm( 1.0525015592575073D ) );
			this.textBox45.Style.Font.Italic = true;
			this.textBox45.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point( 8D );
			this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
			this.textBox45.StyleName = "Caption";
			this.textBox45.Value = "NIYUVE TORCAT\r\nGERENCIA DE RECLAMOS";
			// 
			// objectDataSource1
			// 
			this.objectDataSource1.DataMember = "LlenarReporteDetalleSolicitud";
			this.objectDataSource1.DataSource = typeof( HConnexum.Tramitador.Sitio.Modulos.Reportes.ReporteCartaAvalAltamira );
			this.objectDataSource1.Name = "objectDataSource1";
			this.objectDataSource1.Parameters.AddRange( new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("idCarta", typeof(int), "=Parameters.idCarta.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("idSuscriptor", typeof(int), "=Parameters.idSuscriptor.Value")} );
			// 
			// ReporteCAAltamira
			// 
			this.DataSource = this.objectDataSource1;
			this.Items.AddRange( new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1} );
			this.Name = "ReporteCAAltamira";
			this.PageSettings.Landscape = false;
			this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm( 25.399999618530273D );
			this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm( 25.399999618530273D );
			this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm( 25.399999618530273D );
			this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm( 25.399999618530273D );
			this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
			reportParameter1.Name = "idCarta";
			reportParameter1.Text = "idCarta";
			reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
			reportParameter2.Name = "idSuscriptor";
			reportParameter2.Text = "idSuscriptor";
			reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
			this.ReportParameters.Add( reportParameter1 );
			this.ReportParameters.Add( reportParameter2 );
			this.Style.BackgroundColor = System.Drawing.Color.White;
			this.Width = Telerik.Reporting.Drawing.Unit.Cm( 15.5D );
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
		private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.TextBox currentTimeTextBox;
		private Telerik.Reporting.HtmlTextBox htmlTextBox3;
		private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
		private Telerik.Reporting.TextBox textBox36;
		private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox2;
		private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox45;
		private Telerik.Reporting.PictureBox pictureBox1;
		private Telerik.Reporting.TextBox textBox4;
		private Telerik.Reporting.TextBox Certificado;
		private Telerik.Reporting.TextBox textBox7;
		private Telerik.Reporting.TextBox textBox12;
		private Telerik.Reporting.TextBox textBox6;
		private Telerik.Reporting.HtmlTextBox htmlTextBox1;
		private Telerik.Reporting.Barcode barcode1;
    }
}