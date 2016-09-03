using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.grid
{
    public class GridFacturas
    {
        public int Id { get; set; }
        public bool Check { get; set; }
        public string Folio { get; set; }
        public string UUID { get; set; }
        public string Rfc { get; set; }
        public string Cliente { get; set; }
        public string Serie { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public string Estado { get; set; }
    }
}
