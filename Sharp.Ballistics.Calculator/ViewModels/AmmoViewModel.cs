using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class AmmoViewModel : FunctionScreen
    {
        private readonly RiflesModel riflesModel;
        private readonly IEventAggregator eventsAggregator;

        public override string IconFilename => "ammo.png";

        public override int Order => 4;

        private readonly AmmoModel ammoModel;
        private readonly IWindowManager windowManager;
        private readonly ConfigurationModel configurationModel;
        public AmmoViewModel(AmmoModel ammoModel, 
                             RiflesModel riflesModel,
                             ConfigurationModel configurationModel, 
                             IWindowManager windowManager,
                             IEventAggregator eventsAggregator)
            :base(eventsAggregator)
        {            
#pragma warning disable CC0021 // Use nameof
            DisplayName = "Cartridges";
#pragma warning restore CC0021 // Use nameof
            this.ammoModel = ammoModel;
            this.configurationModel = configurationModel;
            this.windowManager = windowManager;

            configurationModel.Initialize();
            this.eventsAggregator = eventsAggregator;
            this.riflesModel = riflesModel;
        }

        public UnitSettings Units => configurationModel.Units;

        public IEnumerable<Cartridge> Cartridges => ammoModel.All();

        public bool IsBusy { get; set; }

        public string BusyText { get; set; }      

        public void AddCartridge()
        {
            var newCartridgeViewModel = new EditCartridgeViewModel(configurationModel, ammoModel);
            if (windowManager.ShowDialog(newCartridgeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving New Cartridge...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    ammoModel.InsertOrUpdate(newCartridgeViewModel.Cartridge);
                    NotifyOfPropertyChange(() => Cartridges);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void EditCartridge(Cartridge cartridge)
        {
            var editCartridgeViewModel = new EditCartridgeViewModel(configurationModel,ammoModel,cartridge);
            if (windowManager.ShowDialog(editCartridgeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving Cartridge Changes...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    ammoModel.InsertOrUpdate(editCartridgeViewModel.Cartridge);
                    NotifyOfPropertyChange(() => Cartridges);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void RemoveCartridge(Cartridge cartridge)
        {
            var relevantRifles = riflesModel.RiflesByCartridgeName(cartridge.Name).ToList();
            if(relevantRifles.Any())
            {
                if (MessageBox.Show($"Deleting {cartridge.Name} will affect {relevantRifles.Count} stored rifle(s). Continue?","Query",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }           

            ammoModel.Delete(cartridge);
            NotifyOfPropertyChange(() => Cartridges);
            var appEvent = new AppEvent
            {
                Type = Constants.CartridgeRemovedMessage,
            };
            appEvent.Parameters.Add(Constants.ChangedItemName, cartridge.Name);

            eventAggregator.PublishOnBackgroundThread(appEvent);
        }

       
    }
}
