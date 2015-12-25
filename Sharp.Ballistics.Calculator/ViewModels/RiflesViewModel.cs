using Caliburn.Micro;
using Sharp.Ballistics.Calculator.Models;
using System.Collections.Generic;
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
                               IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DisplayName = "Rifles";
            this.riflesModel = riflesModel;
            this.configurationModel = configurationModel;
            this.cartridgesModel = cartridgesModel;
            this.scopesModel = scopesModel;
            this.windowManager = windowManager;
        }

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

                    riflesModel.InsertOrUpdate(editRifleViewModel.Rifle);
                    NotifyOfPropertyChange(() => Rifles);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void RemoveRifle(Models.Rifle rifle)
        {
            riflesModel.Delete(rifle);
            NotifyOfPropertyChange(() => Rifles);
        }
    }
}
