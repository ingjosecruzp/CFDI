using CFDI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CFDI.ViewModel
{
    public class Usr
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
    }
    public class LoginViewModel : ObservableObject
    {
        public DelegateCommand _Entrar { get; set; }

        public string Usuario
        {
            get
            {
                return UsuarioModel.Nombre;
            }

            set
            {
                UsuarioModel.Nombre = value;
            }
        }

        public string Password
        {
            get
            {
                return UsuarioModel.Password;
            }

            set
            {
                UsuarioModel.Password = value;
            }
        }

        public LoginViewModel()
        {
            UsuarioModel.Nombre = "";
            UsuarioModel.Password = "";
            _Entrar = new DelegateCommand(Entrar);
        }
        public void Entrar(object parameter)
        {
            try
            {
                var passwordBox = parameter as PasswordBox;
                Password = passwordBox.Password;
                if (UsuarioModel.Nombre != "" && UsuarioModel.Password != "")
                {
                    Usr usr = new Usr();
                    usr.Nombre = UsuarioModel.Nombre;
                    usr.Password = UsuarioModel.Password;
                    ServicioWS WS = new ServicioWS("WsLogin.svc", "Login",usr, typeof(string), "usuario");
                    TokenModel.Nombre = (string)WS.Peticion();
                    if (TokenModel.Nombre == "050")
                        throw new Exception("Usuario o contraseña invalidos");
                    else
                        CerrarFormulario("FrmLogin");
                }
                else
                { 
                    throw new Exception("Usuario o contraseña vacios");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
