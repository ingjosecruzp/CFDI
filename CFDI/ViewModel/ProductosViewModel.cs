using CFDI.Model;
using CFDI.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CFDI.ViewModel
{
    public class ProductosViewModel : ObservableObject
    {
        //Inician Atributos
        public DelegateCommand GuardarProducto { get; set; }
        private ObservableCollection<UnidadesModel> _Unidades;
        private UnidadesModel _SelectUnidad;
        private ProductosModel _ProductoNuevo;
        private string _Color;
        //Inician Propiedades
        public UnidadesModel SelectUnidad
        {
            get
            {
                return _SelectUnidad;
            }

            set
            {
                _SelectUnidad = value;
                RaisePropertyChangedEvent("SelectUnidad");
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
        public ProductosModel Producto
        {
            get
            {
                return _ProductoNuevo;
            }
            set
            {
                if (_ProductoNuevo != value)
                {
                    _ProductoNuevo = value;
                    RaisePropertyChangedEvent("Producto");
                }
            }
        }

        public string Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;
                RaisePropertyChangedEvent("Color");
            }
        }

        public ProductosViewModel()
        {
            _SelectUnidad = new UnidadesModel();
            _ProductoNuevo = new ProductosModel();
            GuardarProducto = new DelegateCommand(Guardar);
            Color = "Black";
            LoadUnidades();
        }
        public void CargarForm(int id)
        {
            try
            {
                ServicioWS WS = new ServicioWS("WsProductos.svc", "getProducto", id, typeof(ProductosModel), "id");
                Producto = (ProductosModel)WS.Peticion();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadUnidades()
        {
            try
            {
                ServicioWS WS = new ServicioWS("WsUnidades.svc", "getUnidades", null, typeof(ObservableCollection<UnidadesModel>), null);
                Unidades = (ObservableCollection<UnidadesModel>)WS.Peticion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Guardar(object parameter)
        {
          try
            {
                if (Producto.Codigo != "" && Producto.Descripcion != "" && SelectUnidad.Id != 0 && Producto.PrecioUnitario != 0)
                {
                    ServicioWS WS;
                    if (Producto.Id.ToString() == "0")
                        WS = new ServicioWS("WsProductos.svc", "addProducto", Producto, typeof(ProductosModel), "producto");
                    else
                        WS = new ServicioWS("WsProductos.svc", "updateProducto", Producto, typeof(ProductosModel), "producto");
                    Producto = (ProductosModel)WS.Peticion();
                    Color = "Black";
                    CerrarFormulario("FrmProductos");
                }
                else
                {
                    Color = "Red";
                    throw new Exception("Existen campos pendientes de capturar");
                }
            }   
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
