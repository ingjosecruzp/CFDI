using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class FacturasModel
    {
        public int Id { get; set; }
        public int SerieId { get; set; }
        public int Folio { get; set; }
        public int ClienteId { get; set; }
        public int MetododePagosId { get; set; }
        public string CondicionesPago { get; set; }
        public string LugarExpedicion { get; set; }
        public string CuentaPago { get; set; }
        public int TipoPago { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string UUID { get; set; }
        public Nullable<System.DateTime> FechaTimbrado { get; set; }
        public bool Status { get; set; }
        public bool BanEliminar { get; set; }
        public ObservableCollection<DetalleFacturasModel> Detalles { get; set; }

        public FacturasModel()
        {
            this.Detalles = new ObservableCollection<DetalleFacturasModel>();
        }
    }
}
