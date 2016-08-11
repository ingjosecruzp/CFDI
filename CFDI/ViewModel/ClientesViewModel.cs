using CFDI.Model;
using CFDI.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CFDI.ViewModel
{
    public class ClientesViewModel : ObservableObject
    {
        //Inician Atributos
        public DelegateCommand GuardarCliente { get; set; }
        public DelegateCommand _CerrarVentana { get; set; }
        private ObservableCollection<EstadosModel> _Estados;
        private ObservableCollection<MunicipiosModel> _Municipios;
        private ObservableCollection<PaisesModel> _Paises;
        private EstadosModel _SelectEstado;
        private MunicipiosModel _SelectMunicipio;
        private PaisesModel _SelectPais;
        private ClientesModel _ClienteNuevo;
        //Inician Propiedades
        public EstadosModel SelectEstado
        {
            get
            {
                return _SelectEstado;
            }

            set
            {
                _SelectEstado = value;
                this.LoadMunicipios();
                RaisePropertyChangedEvent("SelectEstado");
            }
        }
        public MunicipiosModel SelectMunicipio
        {
            get
            {
                return _SelectMunicipio;
            }

            set
            {
                _SelectMunicipio = value;
                RaisePropertyChangedEvent("SelectMunicipio");
            }
        }
        public PaisesModel SelectPais
        {
            get
            {
                return _SelectPais;
            }

            set
            {
                _SelectPais = value;
                RaisePropertyChangedEvent("SelectPais");
            }
        }
        public ObservableCollection<MunicipiosModel> Municipios
        {
            get
            {
                return _Municipios;
            }
            set
            {
                _Municipios = value;
                RaisePropertyChangedEvent("Municipios");
            }
        }
        public ObservableCollection<EstadosModel> Estados
        {
            get
            {
                return _Estados;
            }
            set
            {
                _Estados = value;
                RaisePropertyChangedEvent("Estados");
            }
        }
        public ObservableCollection<PaisesModel> Paises
        {
            get
            {
                return _Paises;
            }
            set
            {
                _Paises = value;
                RaisePropertyChangedEvent("Paises");
            }
        }
        public ClientesModel Cliente
        {
            get
            {
                return _ClienteNuevo;
            }

            set
            {
                if (_ClienteNuevo != value)
                {
                    _ClienteNuevo = value;
                    RaisePropertyChangedEvent("Cliente");
                }
            }
        }
        public ClientesViewModel()
        {
            _SelectEstado = new EstadosModel();
            _SelectMunicipio = new MunicipiosModel();
            _SelectPais = new PaisesModel();
            _ClienteNuevo = new ClientesModel();
            GuardarCliente = new DelegateCommand(Guardar);
            _CerrarVentana = new DelegateCommand(CerrarVentana);
            LoadPaises();
            LoadEstados();
        }
        public void CargarForm(int id)
        {
            ServicioWS WS = new ServicioWS("WsClientes.svc", "getCliente", id, typeof(ClientesModel), "id");
            Cliente = (ClientesModel)WS.Peticion();
            //SelectEstado = Estados.Single(i => i.Id == Cliente.EstadoId);
            //SelectPais = Paises.Single(i => i.Id == Cliente.PaisId);
            //SelectMunicipio = Municipios.Single(i => i.Id == Cliente.MunicipioId);
        }
        public void LoadPaises()
        {
            ObservableCollection<PaisesModel> paises = new ObservableCollection<PaisesModel>();
            paises.Add(new PaisesModel{ Id = 35, Nombre = "Mexico" });
            Paises = paises;
        }
        public void LoadMunicipios()
        {
            ServicioWS WS = new ServicioWS("WsMunicipios.svc", "getMunicipios", _SelectEstado, typeof(ObservableCollection<MunicipiosModel>), "estado");
            Municipios = (ObservableCollection<MunicipiosModel>)WS.Peticion();
        }
        public void LoadEstados()
        {
            ServicioWS WS = new ServicioWS("WsEstados.svc", "getEstados", null, typeof(ObservableCollection<EstadosModel>), null);
            Estados = (ObservableCollection<EstadosModel>)WS.Peticion();
        }
        public void CerrarVentana(object parameter)
        {
            //Application.Current.MainWindow.Close();
        }
        private void Guardar(object parameter)
        {
            Cliente.ValidarErrores = true;
            if (Cliente.Rfc != "" && Cliente.RazonSocial != "" && SelectPais.Id != 0)
            {
                ServicioWS WS;
                if (Cliente.Id.ToString() == "0")
                    WS = new ServicioWS("WsClientes.svc", "addCliente", Cliente, typeof(ClientesModel), "cliente");
                else
                    WS = new ServicioWS("WsClientes.svc", "updateCliente", Cliente, typeof(ClientesModel), "cliente");
                Cliente = (ClientesModel)WS.Peticion();
            }
            else
                MessageBox.Show("Antes de continuar, debe de corregir los erros señalados","Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }
}
