using CFDI.Model;
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
        public MenuViewModel()
        {
            HolaMundo = new DelegateCommand(OnDelete);
            Tabs = new ObservableCollection<TabItem>();
        }
        private void OnDelete(object parameter)
        {
            if(parameter.ToString()=="Facturas")
                SelectedViewModel = new FacturasViewModel();     
            else if (parameter.ToString() == "Clientes")
                SelectedViewModel = new ClientesGridViewModel();     
        }
    }
    public class TabItem
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
