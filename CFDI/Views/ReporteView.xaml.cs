using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace CFDI.Views
{
    /// <summary>
    /// Interaction logic for ReporteView.xaml
    /// </summary>
    public partial class ReporteView : Window
    {
        public ReporteView(string folio)
        {
            InitializeComponent();

            ReportDocument report = new ReportDocument();
            report.Load(Directory.GetCurrentDirectory() + @"\Reportes\PdfFactura.rpt");
            
            DataSet reportData = new DataSet();
            reportData.ReadXml(folio + ".xml");
            report.SetDataSource(reportData);

            report.SetParameterValue("CBBPath", folio + ".bmp");
            report.SetParameterValue("LogoPath", Environment.CurrentDirectory + @"\pt.jpg");
            report.SetParameterValue("CadenaOriginal",GeneradorCadenas(folio + ".xml"));

            CrystalReportsViewer1.ViewerCore.ReportSource = report;
        }
        public string GeneradorCadenas(string xmlpath)
        {
            //Cargar el XML
            StreamReader reader = new StreamReader(xmlpath);
            XPathDocument myXPathDoc = new XPathDocument(reader);

            //Cargando el XSLT
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(Directory.GetCurrentDirectory() + @"\cadenaoriginal_3_2.xslt");

            StringWriter str = new StringWriter();
            XmlTextWriter myWriter = new XmlTextWriter(str);

            //Aplicando transformacion
            myXslTrans.Transform(myXPathDoc, null, myWriter);

            //Resultado
            string result = str.ToString();

            return result;
            //Fin del programa.
        }
    }
}
