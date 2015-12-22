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
            try
            {
                bootstrapper = new AppBootstrapper();
                bootstrapper.Initialize();

                ConventionManager.ApplyValidation = (binding, viewModelType, property) =>
                {
                    binding.ValidatesOnExceptions = true;
                    binding.ValidatesOnDataErrors = true;
                    binding.ValidatesOnNotifyDataErrors = true;
                };
            }
            catch (Exception)
            {
                //precaution
                bootstrapper?.Dispose();
                throw;
            }

        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            bootstrapper?.Dispose();
        }
    }
}
