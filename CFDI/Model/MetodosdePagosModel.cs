﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class MetodosdePagosModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int BanEliminar { get; set; }
        public string MetodoPago
        {
            get
            {
                return Clave + "-" + Nombre;
            }
        }
    }
}
