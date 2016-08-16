using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class ProductosModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int UnidadId { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int BanElminar { get; set; }
    }

}
