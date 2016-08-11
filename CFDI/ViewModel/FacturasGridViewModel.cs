using CFDI.grid;
using CFDI.Model;
using CFDI.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CFDI.ViewModel
{
    class FacturasGridViewModel:ObservableObject
    {
        public DelegateCommand _Busqueda { get; set; }
        public DelegateCommand _DescargarPfd { get; set; }
        private ObservableCollection<GridFacturas> _GridFacturas;
        private GridFacturas _SelectFactura;
        private string _Rfc;
        private string _Folio;
        private string _Cliente;
        private string[] Filtro;
        public ObservableCollection<GridFacturas> GridFacturas
        {
            get
            {
                return _GridFacturas;
            }

            set
            {
                _GridFacturas = value;
                RaisePropertyChangedEvent("GridFacturas");
            }
        }

        public string Rfc
        {
            get
            {
                return _Rfc;
            }

            set
            {
                _Rfc = value;
                RaisePropertyChangedEvent("Rfc");
            }
        }

        public string Folio
        {
            get
            {
                return _Folio;
            }

            set
            {
                _Folio = value;
                RaisePropertyChangedEvent("Folio");
            }
        }

        public string Cliente
        {
            get
            {
                return _Cliente;
            }

            set
            {
                _Cliente = value;
                RaisePropertyChangedEvent("Folio");
            }
        }

        public GridFacturas SelectFactura
        {
            get
            {
                return _SelectFactura;
            }

            set
            {
                _SelectFactura = value;
                RaisePropertyChangedEvent("SelectFactura");
            }
        }

        public FacturasGridViewModel()
        {
            _Busqueda = new DelegateCommand(Busqueda);
            _DescargarPfd = new DelegateCommand(DescargarPfd);
            Filtro = new string[3];
            this.CargarFacturas();
        }
        public void DescargarPfd(object parameter)
        {
            try
            { 
                string folio = SelectFactura.Serie + SelectFactura.Folio;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                SaveFileDialog1.FileName = folio;
                if (SaveFileDialog1.ShowDialog() == true)
                {
                    ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturaPdf", folio , typeof(RespuestaTimbrado), "folio");
                    RespuestaTimbrado respuesta = (RespuestaTimbrado)WS.Peticion();

                    if (respuesta.status == "ok")
                    {
                        string ruta = Path.GetDirectoryName(SaveFileDialog1.FileName);
                        Base64Decode(respuesta.xml, ruta + "\\", folio + ".xml");
                        Base64Decode(respuesta.cbb, ruta + "\\", folio + ".bmp");
                        ReporteView reporte = new ReporteView(ruta + "\\" + folio);
                        reporte.ShowDialog();
                    }
                    else
                        throw new Exception(respuesta.msj);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarFacturas()
        {
            ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturasGrid", null, typeof(ObservableCollection<GridFacturas>), null);
            GridFacturas = (ObservableCollection<GridFacturas>)WS.Peticion();
        }
        public void Busqueda(object parameter)
        {
            Filtro[0] = Rfc;
            Filtro[1] = Folio;
            Filtro[2] = Cliente;
            ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturasGrid", Filtro, typeof(ObservableCollection<GridFacturas>), "filtro");
            GridFacturas = (ObservableCollection<GridFacturas>)WS.Peticion();
        }
        private static void Base64Decode(string archivo_base64, string ruta, string nombre_archivo)
        {
            byte[] bytes = Convert.FromBase64String(archivo_base64);
            File.WriteAllBytes(ruta + nombre_archivo, bytes);
        }
    }
}
