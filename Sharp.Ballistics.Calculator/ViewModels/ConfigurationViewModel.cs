using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Windows;
using Humanizer;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ConfigurationViewModel : FunctionScreen
    {
        private readonly ConfigurationModel model;
        public ConfigurationViewModel(ConfigurationModel model, IEventAggregator eventsAggregator)
            :base(eventsAggregator)
        {
            this.model = model;
            DisplayName = "Configuration";
        }

        protected override void OnViewReady(object view)
        {
            model.Load();
            NotifyOfPropertyChange(() => Units);
        }

        public override int Order => int.MaxValue - 1;
        public override string IconFilename => "config.png";

        public UnitSettings Units => model.Units;      

        public void Save()
        {
            try
            {                
                model.Save();

                //signal about changes to all who might be interested
                Messenger.PublishOnBackgroundThread(new AppEvent
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
