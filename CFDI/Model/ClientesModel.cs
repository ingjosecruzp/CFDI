using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI.Model
{
    public class ClientesModel: IDataErrorInfo
    {
        public int Id { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public int PaisId { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public string Ciudad { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string Cp { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Referencia { get; set; }
        private string _Error;
        public Boolean ValidarErrores = false;
        public string Error
        {
            get {
                //throw new NotImplementedException(); 
                    return _Error.ToString();
                }
        }

        public string this[string columnName]
        {
            get
            {
                _Error = string.Empty;
                if (ValidarErrores==true)
                { 
                    if (columnName == "Rfc")
                    {
                        if (string.IsNullOrEmpty(Rfc) || Rfc.Length < 11)
                            _Error = "Rfc es un campo obligatorio y debe contener al menos 10 caracteres";
                    }
                    if (columnName == "RazonSocial")
                    {
                        if (string.IsNullOrEmpty(RazonSocial))
                            _Error = "Razón social es un campo obligatorio";
                    }
                    if (columnName == "PaisId")
                    {
                        if (string.IsNullOrEmpty(RazonSocial))
                            _Error = "Pais es un campo obligatorio";
                    }
                }
                return _Error;
            }
        }
    }
}

