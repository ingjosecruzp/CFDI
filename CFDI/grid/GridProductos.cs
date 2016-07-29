using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.grid
{
    public class GridProductos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio_Unitario { get; set; }
    }
}
