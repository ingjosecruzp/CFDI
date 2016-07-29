using CFDI.Model;
using CFDI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDI.grid;
using System.Windows;

namespace CFDI.ViewModel
{
    public class ClientesGridViewModel : ObservableObject
    {
        public DelegateCommand LlamarVentana { get; set; }
        public DelegateCommand ModificarCliente { get; set; }
        public ObservableCollection<GridClientes> Clientes { get; set; }
        private GridClientes _SelectCliente;
        //Inician Propiedads
        public GridClientes SelectCliente
        {
            get
            {
                return _SelectCliente;
            }

            set
            {
                _SelectCliente = value;
                RaisePropertyChangedEvent("SelectCliente");
            }
        }
        public ClientesGridViewModel()
        {
            LlamarVentana = new DelegateCommand(Llamar);
            ModificarCliente = new DelegateCommand(Modificar);
            LoadClientes();
        }
        private void Modificar(object parameter)
        {
            ClientesViewModel clientesViewModel = new ClientesViewModel();
            ClientesView FrmClientes = new ClientesView();
            FrmClientes.DataContext = clientesViewModel;
            clientesViewModel.CargarForm(SelectCliente.Id);
            FrmClientes.ShowDialog();
        }
        private void Llamar(object parameter)
        {
            ClientesViewModel clientesViewModel = new ClientesViewModel();
            ClientesView FrmClientes = new ClientesView();
            FrmClientes.DataContext = clientesViewModel;
            FrmClientes.ShowDialog();
        }
        public void LoadClientes()
        {
            ServicioWS WS = new ServicioWS("WsClientes.svc", "getClientesGrid", null, typeof(ObservableCollection<GridClientes>),null);
            Clientes = (ObservableCollection<GridClientes>)WS.Peticion();
        }

    }
}
