using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class DetalleFacturasModel
    {
        public int Id;
        public int FacturasId;
        public int ProductoId;
        public int UnidadId;
        public decimal PrecioUnitario;
        public decimal Cantidad;
        public decimal Importe;
        public decimal Descuento;
        public bool BanEliminar;
    }
}
