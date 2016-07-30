using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class RespuestaTimbrado
    {
        public int Id { get; set; }
        public string status { get; set; }
        public string msj { get; set; }
        public string folio { get; set; }
        public string xml { get; set; }
        public string cbb { get; set; }
        public string pdf { get; set; }
    }
}
