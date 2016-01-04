using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Models;
using Sharp.Ballistics.Calculator.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class RiflesViewModel : FunctionScreen
    {
        private readonly IWindowManager windowManager;
        private readonly ScopesModel scopesModel;
        private readonly AmmoModel cartridgesModel;
        private readonly ConfigurationModel configurationModel;
        private readonly RiflesModel riflesModel;

        public override int Order => 2;

        public override string IconFilename => "rifle.png";

        public bool IsBusy { get; set; }

        public string BusyText { get; set; }

        public RiflesViewModel(RiflesModel riflesModel,
                               ConfigurationModel configurationModel,
                               AmmoModel cartridgesModel,
                               ScopesModel scopesModel,
                               IWindowManager windowManager,
                               IEventAggregator eventsAggregator) : base(eventsAggregator)
        {
            DisplayName = "Rifles";
            this.riflesModel = riflesModel;
            this.configurationModel = configurationModel;
            this.cartridgesModel = cartridgesModel;
            this.scopesModel = scopesModel;
            this.windowManager = windowManager;
        }

        public UnitSettings Units => configurationModel.Units;

        public IEnumerable<Models.Rifle> Rifles => riflesModel.All();

        public void AddRifle()
        {
            var newRifleViewModel = new EditRifleViewModel(
                                            configurationModel,
                                            cartridgesModel,
                                            scopesModel);

            if (windowManager.ShowDialog(newRifleViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving new rifle...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    riflesModel.InsertOrUpdate(newRifleViewModel.Rifle);
                    NotifyOfPropertyChange(() => Rifles);

                    Refresh();
                    eventAggregator.PublishOnBackgroundThread(new AppEvent
                    {
                        Type = Constants.ConfigurationChangedMessage
                    });
                    eventAggregator.PublishOnBackgroundThread(new RifleChangedEvent
                    {
                        ChangedRifle = newRifleViewModel.Rifle
                    });

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void EditRifle(Models.Rifle rifle)
        {
            var editRifleViewModel = new EditRifleViewModel(
                                           configurationModel,
                                           cartridgesModel,
                                           scopesModel,
                                           rifle);

            if (windowManager.ShowDialog(editRifleViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving rifle changes...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    editRifleViewModel.Rifle.IsUsingNonListedAmmo = false;
                    editRifleViewModel.Rifle.IsUsingNonListedScope = false;
                    riflesModel.InsertOrUpdate(editRifleViewModel.Rifle);
                    NotifyOfPropertyChange(() => Rifles);
                    Refresh();

                    eventAggregator.PublishOnBackgroundThread(new AppEvent
                    {
                        Type = Constants.ConfigurationChangedMessage
                    });

                    eventAggregator.PublishOnBackgroundThread(new RifleChangedEvent
                    {
                        ChangedRifle = editRifleViewModel.Rifle
                    });

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public override void Handle(AppEvent message)
        {
            base.Handle(message);
            
            if(message.Type == Constants.ScopeRemovedMessage)
            {
                var removedScopeName = (string)message.Parameters[Constants.ChangedItemName];
                var relevantRifles = riflesModel.RiflesByScopeName(removedScopeName).ToArray();

                foreach (var rifle in relevantRifles)
                    rifle.IsUsingNonListedScope = true;

                riflesModel.InsertOrUpdate(relevantRifles);
            }

            if(message.Type == Constants.CartridgeRemovedMessage)
            {
                var removedCartridgeName = (string)message.Parameters[Constants.ChangedItemName];
                var relevantRifles = riflesModel.RiflesByCartridgeName(removedCartridgeName).ToArray();

                foreach (var rifle in relevantRifles)
                    rifle.IsUsingNonListedAmmo = true;

                riflesModel.InsertOrUpdate(relevantRifles);
            }
        }

        public void RemoveRifle(Models.Rifle rifle)
        {
            riflesModel.Delete(rifle);
            NotifyOfPropertyChange(() => Rifles);
            eventAggregator.PublishOnBackgroundThread(new AppEvent
            {
                Type = Constants.RifleRemovedMessage
            });
        }        
    }
}
