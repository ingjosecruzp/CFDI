using CFDI.Model;
using CFDI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CFDI.ViewModel
{
    public class MenuViewModel: ObservableObject
    {
        public DelegateCommand HolaMundo { get; set; }
        public ObservableCollection<TabItem> Tabs { get; set; }
        private FacturasViewModel FrmFactura { get; set; }
        private object selectedViewModel;
        private bool _Barra;
        BackgroundWorker worker = new BackgroundWorker();
        ProgressBarView BarraP = new ProgressBarView();

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

        public  bool Barra
        {
            get
            {
                return _Barra;
            }

            set
            {
                _Barra = value;
                RaisePropertyChangedEvent("Barra");
            }
        }
        public static void ActivarBarra()
        {
            
        }
        public MenuViewModel()
        {
            //TokenModel.Nombre = "";
            HolaMundo = new DelegateCommand(OnDelete);
            Tabs = new ObservableCollection<TabItem>();
        }
        private void OnDelete(object parameter)
        {
            if (parameter.ToString() == "Facturas")
            {
                if(FrmFactura==null)
                    FrmFactura = new FacturasViewModel();
                SelectedViewModel = FrmFactura;
                FrmFactura = (FacturasViewModel)SelectedViewModel;
            }
            else if (parameter.ToString() == "Clientes")
            { 
                SelectedViewModel = new ClientesGridViewModel();
            }
            else if (parameter.ToString() == "Productos")
            { 
                SelectedViewModel = new ProductosGridViewModel();
            }
            else if (parameter.ToString() == "ListaFactura")
            { 
                SelectedViewModel = new FacturasGridViewModel();
            }
        }
        public bool ChecarCaptura()
        {
            bool Captura = false;
            if (FrmFactura.Factura != null)
            {

                Captura = true;
            }
            return Captura;
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate ()
            {
                /*Barra = false;
                Console.WriteLine("worker_RunWorkerCompleted");*/
                BarraP.Close();
            });
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            { 
                if(GlobalsVariables.Peticion==true)
                { 
                   /* App.Current.Dispatcher.Invoke((Action)delegate ()
                    {
                        Barra = GlobalsVariables.Peticion;
                        Console.WriteLine("Barra=" + Barra);
                    });*/
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                              new Action(delegate {

                                  Barra = GlobalsVariables.Peticion;
                                  Console.WriteLine("Barra=" + Barra);
                              }));
                }
            }
        }
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //searchProgressBar.Value = e.ProgressPercentage;
        }
    }
    public class TabItem
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
