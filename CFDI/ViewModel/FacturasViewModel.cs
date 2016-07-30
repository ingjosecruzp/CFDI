using CFDI.grid;
using CFDI.Model;
using SelectPdf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace CFDI.ViewModel
{
    public class FacturasViewModel : ObservableObject
    {
        //Inicia Atributos
        public DelegateCommand BusquedaProducto { get; set; }
        public DelegateCommand CalcularGrid { get; set; }
        public DelegateCommand BuscarP { get; set; }
        public DelegateCommand _GuardarFactura { get; set; }
        private ObservableCollection<ClientesModel> _Clientes;
        private ClientesModel _SelectCliente;
        private ObservableCollection<EstadosModel> _Estados;
        private ObservableCollection<MunicipiosModel> _Municipios;
        private ObservableCollection<PaisesModel> _Paises;
        private ObservableCollection<MetodosdePagosModel> _MetodosdePagos;
        private ObservableCollection<TipoPagosModel> _TipoPagos;
        private ObservableCollection<SeriesModel> _Series;
        private ObservableCollection<ProductosModel> _Productos;
        private ObservableCollection<GridProductos> _ProductosGrid;
        private ObservableCollection<UnidadesModel> _Unidades;
        private ObservableCollection<DetalleViewModel> _Detalles;
        private SeriesModel _SelectSeries;
        private EstadosModel _SelectEstado;
        private DetalleViewModel _SelectDetalleProductos;
        private FacturasModel _Factura;
        private ClientesModel _Cliente;
        private MunicipiosModel _SelectMunicipio;
        private PaisesModel _SelectPais;
        private MetodosdePagosModel _SelectMetodosdePagos;
        private TipoPagosModel _SelectTipoPagos;
        private decimal _Subtotal;
        private decimal _Iva;
        private decimal _Total;
        private int _Folio;
        private RespuestaTimbrado _Respueta;
        //Inicia Propiedas
        public ObservableCollection<ClientesModel> Clientes
        {
            get
            {
                return _Clientes;
            }
            set
            {
                _Clientes = value;
                RaisePropertyChangedEvent("Clientes");
            }
        }
        public EstadosModel SelectEstado
        {
            get
            {
                return _SelectEstado;
            }

            set
            {
                _SelectEstado = value;
                this.LoadMunicipios();
                RaisePropertyChangedEvent("SelectEstado");
            }
        }
        public MunicipiosModel SelectMunicipio
        {
            get
            {
                return _SelectMunicipio;
            }

            set
            {
                _SelectMunicipio = value;
                RaisePropertyChangedEvent("SelectMunicipio");
            }
        }
        public PaisesModel SelectPais
        {
            get
            {
                return _SelectPais;
            }

            set
            {
                _SelectPais = value;
                RaisePropertyChangedEvent("SelectPais");
            }
        }
        public MetodosdePagosModel SelectMetodosdePagos
        {
            get
            {
                return _SelectMetodosdePagos;
            }

            set
            {
                _SelectMetodosdePagos = value;
                RaisePropertyChangedEvent("SelectMetodosdePagos");
            }
        }
        public TipoPagosModel SelectTipoPagos
        {
            get
            {
                return _SelectTipoPagos;
            }

            set
            {
                _SelectTipoPagos = value;
                RaisePropertyChangedEvent("SelectTipoPagos");
            }
        }
        public SeriesModel SelectSeries
        {
            get
            {
                return _SelectSeries;
            }

            set
            {
                _SelectSeries = value;
                RaisePropertyChangedEvent("SelectSeries");
                RaisePropertyChangedEvent("Folio");
            }
        }
        public FacturasModel Factura
        {
            get
            {
                return _Factura;
            }

            set
            {
                _Factura = value;
                RaisePropertyChangedEvent("Factura");
            }
        }
        public DetalleViewModel SelectDetalleProductos
        {
            get
            {
                return _SelectDetalleProductos;
            }

            set
            {
                _SelectDetalleProductos = value;
                RaisePropertyChangedEvent("SelectDetalleProductos");
            }
        }
        public ObservableCollection<MunicipiosModel> Municipios
        {
            get
            {
                return _Municipios;
            }
            set
            {
                _Municipios = value;
                RaisePropertyChangedEvent("Municipios");
            }
        }
        public ObservableCollection<EstadosModel> Estados
        {
            get
            {
                return _Estados;
            }
            set
            {
                _Estados = value;
                RaisePropertyChangedEvent("Estados");
            }
        }
        public ObservableCollection<PaisesModel> Paises
        {
            get
            {
                return _Paises;
            }
            set
            {
                _Paises = value;
                RaisePropertyChangedEvent("Paises");
            }
        }
        public ObservableCollection<TipoPagosModel> TipoPagos
        {
            get
            {
                return _TipoPagos;
            }
            set
            {
                _TipoPagos = value;
                RaisePropertyChangedEvent("TipoPagos");
            }
        }
        public ObservableCollection<MetodosdePagosModel> MetodosdePagos
        {
            get
            {
                return _MetodosdePagos;
            }
            set
            {
                _MetodosdePagos = value;
                RaisePropertyChangedEvent("MetodosdePagos");
            }
        }
        public ObservableCollection<SeriesModel> Series
        {
            get
            {
                return _Series;
            }
            set
            {
                _Series = value;
                RaisePropertyChangedEvent("Series");
            }
        }
        public ObservableCollection<ProductosModel> Productos
        {
            get
            {
                return _Productos;
            }

            set
            {
                _Productos = value;
                RaisePropertyChangedEvent("Productos");
            }
        }
        public ObservableCollection<GridProductos> ProductosGrid
        {
            get
            {
                return _ProductosGrid;
            }

            set
            {
                _ProductosGrid = value;
                RaisePropertyChangedEvent("ProductosGrid");
            }
        }
        public ObservableCollection<UnidadesModel> Unidades
        {
            get
            {
                return _Unidades;
            }
            set
            {
                _Unidades = value;
                RaisePropertyChangedEvent("Unidades");
            }
        }
        public ObservableCollection<DetalleViewModel> Detalles
        {
            get
            {
                return _Detalles;
            }
            set
            {
                _Detalles = value;
                RaisePropertyChangedEvent("Detalles");
            }
        }
        public ClientesModel SelectCliente
        {
            get
            {
                return _SelectCliente;
            }

            set
            {
                _SelectCliente = value;
                Cliente = _SelectCliente;
                SelectEstado = Estados.Single(i => i.Id == Cliente.EstadoId);
                SelectPais = Paises.Single(i => i.Id == Cliente.PaisId);
                SelectMunicipio = Municipios.Single(i => i.Id == Cliente.MunicipioId);
                RaisePropertyChangedEvent("SelectCliente");
            }
        }
        public decimal Subtotal
        {
            get
            {
                _Subtotal = Detalles.Sum(a => a.Importe);
                Factura.Subtotal = _Subtotal;
                return _Subtotal;
            }
            set { }
        }
        public decimal Iva
        {
            get
            {
                _Iva = _Subtotal * 0.16m;
                Factura.Iva = _Iva;
                return _Iva;
            }
            set { }
        }
        public int Folio
        {
            get
            {
                if (SelectSeries != null)
                {
                    _Folio = LoadFolio();
                    Factura.Folio = _Folio;
                }
                return _Folio;
            }
            set { }
        }
        public decimal Total
        {
            get
            {
                _Total = Subtotal + Iva;
                Factura.Total = _Total;
                return _Total;
            }

            set { }
        }
        public RespuestaTimbrado Respueta
        {
            get
            {
                return _Respueta;
            }

            set
            {
                _Respueta = value;
            }
        }
        //Constructor
        public FacturasViewModel()
        {
            Productos = new ObservableCollection<ProductosModel>();
            Detalles = new ObservableCollection<DetalleViewModel>();
            BusquedaProducto = new DelegateCommand(BProducto);
            BuscarP = new DelegateCommand(WsBuscarProducto);
            _GuardarFactura = new DelegateCommand(PdfSharpConvert);
            CalcularGrid = new DelegateCommand(Calculos);
            Factura = new FacturasModel();
            LoadClientes();
            LoadPaises();
            LoadEstados();
            LoadMetodosdePagos();
            LoadTipoPagos();
            LoadSeries();
            LoadUnidades();
            LoadProductos();
        }
        public void Calculos(object parameter)
        {
            if (_SelectDetalleProductos.ProductoId != 0)
            {
                var item = _Detalles.FirstOrDefault(i => i.ProductoId == SelectDetalleProductos.ProductoId);
                var precio = _Productos.FirstOrDefault(i => i.Id == SelectDetalleProductos.ProductoId);
                if (item.PrecioUnitario != precio.PrecioUnitario || item.PrecioUnitario != 0)
                    item.PrecioUnitario = precio.PrecioUnitario;
            }
            RaisePropertyChangedEvent("Subtotal");
            RaisePropertyChangedEvent("Iva");
            RaisePropertyChangedEvent("Total");
        }
        public void BProducto(object parameter)
        {
            /*LoadProductosGrid();
            BusquedaView FrmBusqueda = new BusquedaView();
            FrmBusqueda.DataContext = this;
            FrmBusqueda.Show();*/
        }
        public void WsBuscarProducto(object parameter)
        {
            MessageBox.Show("Click");
        }
        public ClientesModel Cliente
        {
            get
            {
                return _Cliente;
            }

            set
            {
                if (_Cliente != value)
                {
                    _Cliente = value;
                    RaisePropertyChangedEvent("Cliente");
                }
            }
        }

        public void LoadProductosGrid()
        {
            ServicioWS WS = new ServicioWS("WsProductos.svc", "getProductosGrid", null, typeof(ObservableCollection<GridProductos>), null);
            ProductosGrid = (ObservableCollection<GridProductos>)WS.Peticion();
        }
        public void LoadProductos()
        {
            ServicioWS WS = new ServicioWS("WsProductos.svc", "getProductos", null, typeof(ObservableCollection<ProductosModel>), null);
            Productos = (ObservableCollection<ProductosModel>)WS.Peticion();
        }
        public void LoadUnidades()
        {
            ServicioWS WS = new ServicioWS("WsUnidades.svc", "getUnidades", null, typeof(ObservableCollection<UnidadesModel>), null);
            Unidades = (ObservableCollection<UnidadesModel>)WS.Peticion();
        }
        public void LoadPaises()
        {
            ObservableCollection<PaisesModel> paises = new ObservableCollection<PaisesModel>();
            paises.Add(new PaisesModel { Id = 35, Nombre = "Mexico" });
            Paises = paises;
        }
        public void LoadTipoPagos()
        {
            ObservableCollection<TipoPagosModel> tipopagos = new ObservableCollection<TipoPagosModel>();
            tipopagos.Add(new TipoPagosModel { Id = 1, Nombre = "Pago en una sola exhibición" });
            tipopagos.Add(new TipoPagosModel { Id = 2, Nombre = "En parcialidades" });
            TipoPagos = tipopagos;
        }
        public void LoadMunicipios()
        {
            ServicioWS WS = new ServicioWS("WsMunicipios.svc", "getMunicipios", _SelectEstado, typeof(ObservableCollection<MunicipiosModel>), "estado");
            Municipios = (ObservableCollection<MunicipiosModel>)WS.Peticion();
        }
        public void LoadEstados()
        {
            ServicioWS WS = new ServicioWS("WsEstados.svc", "getEstados", null, typeof(ObservableCollection<EstadosModel>), null);
            Estados = (ObservableCollection<EstadosModel>)WS.Peticion();
        }
        public void LoadClientes()
        {
            ServicioWS WS = new ServicioWS("WsClientes.svc", "getClientes", null, typeof(ObservableCollection<ClientesModel>), null);
            Clientes = (ObservableCollection<ClientesModel>)WS.Peticion();
        }
        public void LoadMetodosdePagos()
        {
            ServicioWS WS = new ServicioWS("WsMetodosdePago.svc", "getMetodosdePago", null, typeof(ObservableCollection<MetodosdePagosModel>), null);
            MetodosdePagos = (ObservableCollection<MetodosdePagosModel>)WS.Peticion();
        }
        public void LoadSeries()
        {
            ServicioWS WS = new ServicioWS("WsSeries.svc", "getSeries", null, typeof(ObservableCollection<SeriesModel>), null);
            Series = (ObservableCollection<SeriesModel>)WS.Peticion();
        }
        public void GuardarFactura(object parameter)
        {
            try
            {

                /*if (Factura.ClienteId != 0 && Factura.MetododePagosId != 0 && Factura.LugarExpedicion != "" && Factura.LugarExpedicion!=null && Factura.SerieId != 0)
                {
                    if(Detalles.ToList().Count>0)
                    { 
                        this.CargarDetalles();
                        ServicioWS WS = new ServicioWS("WsFacturasCFDI.svc", "addFactura", Factura, typeof(RespuestaTimbrado), "factura");
                        Respueta = (RespuestaTimbrado)WS.Peticion();
                        if (Respueta.status == "ok")
                        {
                            Base64Decode(Respueta.xml, @"C:\Facturas\", Respueta.folio + ".xml");
                            Base64Decode(Respueta.cbb, @"C:\Facturas\", Respueta.folio + ".bmp");
                            Base64Decode(Respueta.pdf, @"C:\Facturas\", Respueta.folio + ".htm");
                        }
                        else
                            throw new Exception(Respueta.msj);
                    }
                    else
                        throw new Exception("Al menos debe de ingresar un producto");
                }
                else
                    throw new Exception("Los campos con * son obligatorio para continuar");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void CargarDetalles()
        {
            foreach (DetalleViewModel Detalle in Detalles)
            {
                Factura.Detalles.Add(new DetalleFacturasModel
                {
                    ProductoId = Detalle.ProductoId,
                    UnidadId = Detalle.UnidadId,
                    PrecioUnitario = Detalle.PrecioUnitario,
                    Cantidad = Detalle.Cantidad,
                    Importe = Detalle.Importe,
                    Descuento = Detalle.Descuento
                }
                                     );
            }
        }
        public int LoadFolio()
        {
            ServicioWS WS = new ServicioWS("WsSeries.svc", "getUltimoFolio", _SelectSeries.Id, typeof(string), "serieid");
            return Convert.ToInt16((string)WS.Peticion());
        }
        private static void Base64Decode(string archivo_base64, string ruta, string nombre_archivo)
        {
            byte[] bytes = Convert.FromBase64String(archivo_base64);
            File.WriteAllBytes(ruta + nombre_archivo, bytes);
        }
        public void PdfSharpConvert(object parameter)
        {
            /*string htmlContent = File.ReadAllText(@"C:\Facturas\A12.html");
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfDocument pdf = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            File.WriteAllBytes(@"C:\Facturas\A12.pdf", res);*/
            // instantiate the html to pdf converter          
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize =PdfPageSize.Letter;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            /*converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;*/

            /*converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;*/

            // convert the url to pdf
            PdfDocument doc = converter.ConvertUrl(@"C:\Facturas\A12.html");

            // save pdf document
            doc.Save(@"C:\Facturas\A12.pdf");
            // close pdf document
            doc.Close();
        }
    }
}
