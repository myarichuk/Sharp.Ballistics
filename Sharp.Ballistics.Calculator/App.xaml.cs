using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Bootstrap;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sharp.Ballistics.Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AppBootstrapper bootstrapper;
        public App()
        {
            Exit += OnExit;
            DispatcherUnhandledException += UnhandledException;
            try
            {
                bootstrapper = new AppBootstrapper();
                bootstrapper.Initialize();
            }
            catch (Exception)
            {
                //precaution
                bootstrapper?.Dispose();
                throw;
            }

        }

        private void UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            Shutdown(-1);
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            bootstrapper?.Dispose();
        }
    }
}
