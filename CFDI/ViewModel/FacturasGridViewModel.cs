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
        public DelegateCommand _DescargarXml { get; set; }
        public DelegateCommand _CancelarFactura { get; set; }
        private ObservableCollection<GridFacturas> _Facturas;
        private GridFacturas _SelectFactura;
        private Nullable<DateTime> _FechaI = null;
        private Nullable<DateTime> _FechaF = null;
        private string _Rfc;
        private string _Folio;
        private string _Cliente;
        private string[] Filtro;
        private bool _selectAll;
        private bool _checkon;
        public ObservableCollection<GridFacturas> Facturas
        {
            get
            {
                return _Facturas;
            }

            set
            {
                _Facturas = value;
                RaisePropertyChangedEvent("Facturas");
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

        public bool SelectAll
        {
            get
            {
                return _selectAll;
            }

            set
            {
                _selectAll = value;
                Busqueda(null);
                RaisePropertyChangedEvent("SelectAll");
            }
        }
        public bool Checkon
        {
            get
            {
                return _checkon;
            }

            set
            {
                _checkon = value;
                if(_SelectFactura != null)
                    _SelectFactura.Check = value;
                //RaisePropertyChangedEvent("Checkon");
            }
        }

        public DateTime? FechaI
        {
            get
            {
                return _FechaI;
            }

            set
            {
                _FechaI = value;
                RaisePropertyChangedEvent("FechaI");
            }
        }

        public DateTime? FechaF
        {
            get
            {
                return _FechaF;
            }

            set
            {
                _FechaF = value;
            }
        }

        public FacturasGridViewModel()
        {
            _Busqueda = new DelegateCommand(Busqueda);
            _DescargarPfd = new DelegateCommand(DescargarPfd);
            _DescargarXml = new DelegateCommand(DescargarXml);
            _CancelarFactura = new DelegateCommand(CancelarFactura);
            Filtro = new string[5];
            this.CargarFacturas();
        }
        public void CancelarFactura(object parameter)
        {
            try
            {
                //Busco facturas seleccionadas y las agrego a una lista
                List<GridFacturas> CFacturas = new List<GridFacturas>();
                CFacturas = Facturas.Where(r => r.Check == true).ToList();

                ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "cancelarFacturas", CFacturas, typeof(bool), "facturas");
                bool respuesta = (bool)WS.Peticion();
                if (respuesta == false)
                    throw new Exception("Error en el cancelado de la factura");
                CargarFacturas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void DescargarXml(object parameter)
        {
            try
            {
                string folio = SelectFactura.Serie + SelectFactura.Folio;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.Filter = "Xml Document|*.xml";
                SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                SaveFileDialog1.FileName = folio;
                if (SaveFileDialog1.ShowDialog() == true)
                {
                    ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturaPdf", folio, typeof(RespuestaTimbrado), "folio");
                    RespuestaTimbrado respuesta = (RespuestaTimbrado)WS.Peticion();

                    if (respuesta.status == "ok")
                    {
                        string ruta = Path.GetDirectoryName(SaveFileDialog1.FileName);
                        Base64Decode(respuesta.xml, ruta + "\\", folio + ".xml");
                    }
                    else
                        throw new Exception(respuesta.msj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DescargarPfd(object parameter)
        {
            try
            {
                string folio = SelectFactura.Serie + SelectFactura.Folio;
                    ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturaPdf", folio , typeof(RespuestaTimbrado), "folio");
                    RespuestaTimbrado respuesta = (RespuestaTimbrado)WS.Peticion();

                    if (respuesta.status == "ok")
                    {
                        string ruta = Path.GetDirectoryName(Path.GetTempPath());
                        Base64Decode(respuesta.xml, ruta + "\\", folio + ".xml");
                        Base64Decode(respuesta.cbb, ruta + "\\", folio + ".bmp");
                        ReporteView reporte = new ReporteView(ruta + "\\" + folio);
                        reporte.ShowDialog();
                    }
                    else
                        throw new Exception(respuesta.msj);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarFacturas()
        {
            Checkon = _selectAll;
            ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturasGrid", null, typeof(ObservableCollection<GridFacturas>), null);
            Facturas = (ObservableCollection<GridFacturas>)WS.Peticion();
            StatusCheck(_selectAll);
        }
        public void ValidaFechas()
        {
            try
            {
                if ((FechaI != null && FechaF == null) || (FechaF != null && FechaI == null))
                    throw new Exception("Debes de carga las dos fechas para poder filtrar");
                if(FechaF < FechaI)
                    throw new Exception("La fecha final debes ser mayor a la fecha de inicio");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Busqueda(object parameter)
        {
            try
            {
                Filtro[0] = Rfc;
                Filtro[1] = Folio;
                Filtro[2] = Cliente;
                Filtro[3] = FechaI != null ? FechaI.Value.ToString("yyyy-MM-dd") : null;
                Filtro[4] = FechaF != null ? FechaF.Value.ToString("yyyy-MM-dd") : null;
                Checkon = _selectAll;
                ValidaFechas();
                ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "getFacturasGrid", Filtro, typeof(ObservableCollection<GridFacturas>), "filtro");
                Facturas = (ObservableCollection<GridFacturas>)WS.Peticion();
                StatusCheck(_selectAll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void StatusCheck(bool status)
        {
            foreach (GridFacturas Campo in this.Facturas)
                Campo.Check = status;
        }
        private static void Base64Decode(string archivo_base64, string ruta, string nombre_archivo)
        {
            byte[] bytes = Convert.FromBase64String(archivo_base64);
            File.WriteAllBytes(ruta + nombre_archivo, bytes);
        }
    }
}
