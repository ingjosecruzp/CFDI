using CFDI.grid;
using CFDI.Model;
using CFDI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CFDI.ViewModel
{
    public class LoginEmpresasViewModel: ObservableObject
    {
        private ObservableCollection<GridLoginEmpresas> _Empresas;
        public DelegateCommand _Entrar { get; set; }
        private GridLoginEmpresas _SelectEmpresa;
        public ObservableCollection<GridLoginEmpresas> Empresas
        {
            get
            {
                return _Empresas;
            }

            set
            {
                _Empresas = value;
                RaisePropertyChangedEvent("Empresas");
            }
        }

        public GridLoginEmpresas SelectEmpresa
        {
            get
            {
                return _SelectEmpresa;
            }

            set
            {
                _SelectEmpresa = value;
                RaisePropertyChangedEvent("SelectEmpresa");
            }
        }

        public LoginEmpresasViewModel()
        {
            _Entrar = new DelegateCommand(Entrar);
            LoadEmpresas();
        }
        public void Entrar(object parameter)
        {

            try
            {
                if (SelectEmpresa != null)
                {
                    MainWindow FrmMain = new MainWindow();
                    ServicioWS WS = new ServicioWS("WsEmpresas.svc", "tokenEmpresa", SelectEmpresa, typeof(string), "empresa");
                    TokenModel.Nombre = (string)WS.Peticion();
                    if (TokenModel.Nombre == null || TokenModel.Nombre == "")
                        throw new Exception("Error de autentificacion");
                    else
                    { 
                        FrmMain.Show();
                        CerrarFormulario("FrmEmpresas");
                    }
                    //var FrmMain = new MainWindow();

                }
                else
                {
                    throw new Exception("Seleccione una empresa");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadEmpresas()
        {
            ServicioWS WS = new ServicioWS("WsEmpresas.svc", "getEmpresas", null, typeof(ObservableCollection<GridLoginEmpresas>), null);
            Empresas = (ObservableCollection<GridLoginEmpresas>)WS.Peticion();
        }
    }
}
