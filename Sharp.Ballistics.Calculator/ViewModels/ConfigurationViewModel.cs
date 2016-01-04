using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Windows;
using Humanizer;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ConfigurationViewModel : FunctionScreen
    {
        private readonly ConfigurationModel model;
        public ConfigurationViewModel(ConfigurationModel model, 
            IEventAggregator eventsAggregator)
            :base(eventsAggregator)
        {
            this.model = model;
            DisplayName = "Configuration";
            model.ImportExportStarted += ImportExportStarted;
            model.ImportExportEnded += ImportExportEnded;
        }

        private void ImportExportEnded()
        {
            eventAggregator.PublishOnBackgroundThread(new ExportImportEvent
            {
                IsInProgress = false,
                Message = String.Empty
            });
        }

        private void ImportExportStarted(string message)
        {
            eventAggregator.PublishOnBackgroundThread(new ExportImportEvent
            {
                IsInProgress = true,
                Message = message
            });
        }

        protected override void OnViewReady(object view)
        {
            model.Load();
            NotifyOfPropertyChange(() => Units);
        }

        public override int Order => int.MaxValue - 1;
        public override string IconFilename => "config.png";

        public UnitSettings Units => model.Units;      

        public void Import()
        {
            var importSelectFile = new OpenFileDialog
            {
                DefaultExt = ".bc.config",
                Filter = "Calculator Configuration (.bc.config)|*.bc.config",
                Title = "Import Sharp.Ballistics Calculator Configuration",
                ValidateNames = true,
                CheckPathExists = true
            };

            if (importSelectFile.ShowDialog() ?? false)
                Task.Run(() => model.Import(importSelectFile.FileName));
        }

        public void Export()
        {
            var exportSelectFile = new SaveFileDialog
            {
                DefaultExt = ".bc.config",
                Filter = "Calculator Configuration (.bc.config)|*.bc.config",
                Title = "Export Sharp.Ballistics Calculator Configuration",
                ValidateNames = true,
                CheckPathExists = true
            };
            
            if (exportSelectFile.ShowDialog() ?? false)
                Task.Run(() => model.Export(exportSelectFile.FileName));
        }

        public void Save()
        {
            try
            {                
                model.Save();

                //signal about changes to all who might be interested
                eventAggregator.PublishOnBackgroundThread(new AppEvent
                {
                    Type = Constants.ConfigurationChangedMessage
                });
            }
            catch(Exception e)
            {
                MessageBox.Show($"Failed to save configuration. Reason : {e}","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Saved configuration.","Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }       
    }
}
