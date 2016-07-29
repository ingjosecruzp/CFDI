using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class SeriesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int FolioInicial { get; set; }
        public int FolioFinal { get; set; }
        public int FolioInicio { get; set; }
    }
}
