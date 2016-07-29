using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CFDI.ViewModel
{
    public class DelegateCommand: ICommand
    {
        Action<object> _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public DelegateCommand(Action<object> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }
        public DelegateCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        // Beware - should use weak references if command instance lifetime 
        // is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation public event 
        //EventHandler CanExecuteChanged = delegate { };
        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod.Invoke(parameter);
            }
        }
    }
}
