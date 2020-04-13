using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynTextLibrary;
using System.Windows;
using System.Windows.Input;
using SynTextDesktopUI.Views;

namespace SynTextDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private AppViewModel _appVM = new AppViewModel();

        public ShellViewModel()
        {
            ActivateItem(_appVM);
        }

        private Window _mainWindow = App.Current.MainWindow;

        public Window MainWindow
        {
            get { return _mainWindow = App.Current.MainWindow; }
            set { _mainWindow = App.Current.MainWindow = value; }
            
        }

        public void Close()
        {
            MainWindow.Close();
        }

        public void Minimize()
        {
            MainWindow.WindowState = WindowState.Minimized;
        }
    }

    
}
