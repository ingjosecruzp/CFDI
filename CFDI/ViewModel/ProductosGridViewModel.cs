using CFDI.Model;
using CFDI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDI.grid;
using System.Windows;

namespace CFDI.ViewModel
{
    public class ProductosGridViewModel : ObservableObject
    {
        public DelegateCommand LlamarVentana { get; set; }
        public DelegateCommand ModificarProducto { get; set; }
        private ObservableCollection<GridProductos> _Productos;
        private GridProductos _SelectProducto;
        //Inician Propiedads
        public GridProductos SelectProducto
        {
            get
            {
                return _SelectProducto;
            }

            set
            {
                _SelectProducto = value;
                RaisePropertyChangedEvent("SelectProducto");
            }
        }

        public ObservableCollection<GridProductos> Productos
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

        public ProductosGridViewModel()
        {
            LlamarVentana = new DelegateCommand(Llamar);
            ModificarProducto = new DelegateCommand(Modificar);
            LoadProductos();
        }
        private void Modificar(object parameter)
        {
            ProductosViewModel productosViewModel = new ProductosViewModel();
            ProductosView FrmProductos = new ProductosView();
            FrmProductos.DataContext = productosViewModel;
            productosViewModel.CargarForm(SelectProducto.Id);
            FrmProductos.ShowDialog();
            LoadProductos();
        }
        private void Llamar(object parameter)
        {
            ProductosViewModel productosViewModel = new ProductosViewModel();
            ProductosView FrmProductos = new ProductosView();
            FrmProductos.DataContext = productosViewModel;
            FrmProductos.ShowDialog();
            LoadProductos();
        }
        public void LoadProductos()
        {
            ServicioWS WS = new ServicioWS("WsProductos.svc", "getProductosGrid", null, typeof(ObservableCollection<GridProductos>),null);
            Productos = (ObservableCollection<GridProductos>)WS.Peticion();
        }

    }
}
