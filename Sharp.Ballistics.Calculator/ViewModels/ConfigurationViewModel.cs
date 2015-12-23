using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Windows;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ConfigurationViewModel : FunctionScreen
    {
        private readonly ConfigurationModel model;
        private readonly IEventAggregator eventAggregator;
        public ConfigurationViewModel(ConfigurationModel model, IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
            this.model = model;
            this.eventAggregator = eventAggregator;
            DisplayName = "Units Configuration";
        }

        protected override void OnViewReady(object view)
        {
            model.Load();
            NotifyOfPropertyChange(() => Units);
        }

        public override int Order => int.MaxValue;
        public override string IconFilename => "config.png";

        public UnitsConfiguration Units => model.Units;

        public void Save()
        {
            try
            {                
                model.Save();

                //signal about changes to all who might be interested
                eventAggregator.PublishOnBackgroundThread(new AppEvent
                {
                    MessageType = Constants.ConfigurationChangedMessage
                });
            }
            catch(Exception e)
            {
                MessageBox.Show($"Failed to save configuration. Reason : {e}","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Saved configuration.","Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void FormValueChanged()
        {
        }

    }
}
