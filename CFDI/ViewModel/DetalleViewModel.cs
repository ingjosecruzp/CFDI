using CFDI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.ViewModel
{
    public class DetalleViewModel:ObservableObject
    {
        private DetalleFacturasModel _Detalle;

        //public DetalleViewModel(DetalleFacturasModel detalle)
        public DetalleViewModel()
        {
            //_Detalle = detalle;
            _Detalle = new DetalleFacturasModel();
        }
        //Propiedads Grid
        public int ProductoId
        {
            get
            {
                return _Detalle.ProductoId;
            }
            set
            {
                _Detalle.ProductoId = value;
                RaisePropertyChangedEvent("ProductoId");
            }
        }

        public int UnidadId
        {
            get
            {
                return _Detalle.UnidadId;
            }

            set
            {
                _Detalle.UnidadId = value;
            }
        }

        public decimal PrecioUnitario
        {
            get
            {
                return _Detalle.PrecioUnitario;
            }

            set
            {
                _Detalle.PrecioUnitario = value;
                RaisePropertyChangedEvent("PrecioUnitario");
                RaisePropertyChangedEvent("Importe");
            }
        }

        public decimal Cantidad
        {
            get
            {
                return _Detalle.Cantidad;
            }

            set
            {
                _Detalle.Cantidad = value;
                RaisePropertyChangedEvent("Cantidad");
                RaisePropertyChangedEvent("Importe");
            }
        }

        public decimal Importe
        {
            get
            {
                decimal descuento_decimal = 0.0m;
                decimal TotalAntesDescuento = _Detalle.Cantidad * _Detalle.PrecioUnitario;
                if (_Detalle.Descuento>0.0m)
                {
                    descuento_decimal = TotalAntesDescuento*(_Detalle.Descuento/100);
                }
                _Detalle.Importe = (TotalAntesDescuento)-descuento_decimal;
                return _Detalle.Importe;
            }
            set{}
        }

        public decimal Descuento
        {
            get
            {
                return _Detalle.Descuento;
            }

            set
            {
                _Detalle.Descuento = value;
                RaisePropertyChangedEvent("Importe");
            }
        }
        //Terminana Propiedades grid
    }
}
