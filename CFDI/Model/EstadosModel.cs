using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class EstadosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PaisesId { get; set; }
        public virtual PaisesModel Paises { get; set; }
    }
}
