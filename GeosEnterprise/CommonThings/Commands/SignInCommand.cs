using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeosEnterprise.Commands
{
    public class SignInCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> executedMethod;
        Func<object, bool> canExecuteMethod;


        public SignInCommand(Action<object> executedMethod, Func<object, bool> canExecuteMethod)
        {
            this.executedMethod = executedMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executedMethod(parameter);
        }
    }
}
