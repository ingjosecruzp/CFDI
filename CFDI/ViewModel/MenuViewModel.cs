using CFDI.Model;
using CFDI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CFDI.ViewModel
{
    public class MenuViewModel: ObservableObject
    {
        public DelegateCommand HolaMundo { get; set; }
        public ObservableCollection<TabItem> Tabs { get; set; }
        private object selectedViewModel;

        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; RaisePropertyChangedEvent("SelectedViewModel"); }
        }

        public string UsuarioFirmado
        {
            get
            {
                return UsuarioModel.Nombre;
            }
            set {}
        }

        public MenuViewModel()
        {
            TokenModel.Nombre = "";
            HolaMundo = new DelegateCommand(OnDelete);
            Tabs = new ObservableCollection<TabItem>();
            if (TokenModel.Nombre == null || TokenModel.Nombre == "")
            {
                LoginView FrmLogin = new LoginView();
                LoginViewModel loginViewModel = new LoginViewModel();
                FrmLogin.DataContext = loginViewModel;
                FrmLogin.ShowDialog();
                if (TokenModel.Nombre == "050" || TokenModel.Nombre=="")
                        CerrarFormulario("FrmMain");
            }
        }
        private void OnDelete(object parameter)
        {
            if (parameter.ToString() == "Facturas")
                SelectedViewModel = new FacturasViewModel();
            else if (parameter.ToString() == "Clientes")
                SelectedViewModel = new ClientesGridViewModel();
            else if (parameter.ToString() == "Productos")
                SelectedViewModel = new ProductosGridViewModel();
            else if (parameter.ToString() == "ListaFactura")
                SelectedViewModel = new FacturasGridViewModel();
        }
    }
    public class TabItem
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
