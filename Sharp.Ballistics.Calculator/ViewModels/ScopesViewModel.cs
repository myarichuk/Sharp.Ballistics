using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ScopesViewModel : FunctionScreen
    {
        private readonly RiflesModel riflesModel;
        private readonly ConfigurationModel configurationModel;
        private readonly IWindowManager windowManager;
        private readonly ScopesModel scopesModel;

        public override int Order => 3;

        public override string IconFilename => "scope.png";

        public bool IsBusy { get; set; }
        public string BusyText { get; set; }

        public ScopesViewModel(ScopesModel scopesModel,
            ConfigurationModel configurationModel,
            RiflesModel riflesModel,
            IWindowManager windowManager,
            IEventAggregator eventsAggregator) : base(eventsAggregator)
        {
            this.scopesModel = scopesModel;
            this.windowManager = windowManager;
            this.configurationModel = configurationModel;
            DisplayName = "Scopes";
            this.riflesModel = riflesModel;
        }

        public IEnumerable<Scope> Scopes => scopesModel.All();

        public void AddScope()
        {
            var newScopeViewModel = new EditScopeViewModel(configurationModel,scopesModel);
            if (windowManager.ShowDialog(newScopeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving New Scope...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    scopesModel.InsertOrUpdate(newScopeViewModel.Scope);
                    NotifyOfPropertyChange(() => Scopes);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void EditScope(Scope scope)
        {
            var editScopeViewModel = new EditScopeViewModel(configurationModel,scopesModel, scope);
            if (windowManager.ShowDialog(editScopeViewModel) ?? false)
            {
                Task.Run(() =>
                {
                    IsBusy = true;
                    BusyText = "Saving Scope Changes...";
                    NotifyOfPropertyChange(() => IsBusy);
                    NotifyOfPropertyChange(() => BusyText);

                    scopesModel.InsertOrUpdate(editScopeViewModel.Scope);
                    NotifyOfPropertyChange(() => Scopes);
                    Refresh();

                    IsBusy = false;
                    NotifyOfPropertyChange(() => IsBusy);
                });
            }
        }

        public void RemoveScope(Scope scope)
        {
            var relevantRifles = riflesModel.RiflesByScopeName(scope.Name).ToList();
            if (relevantRifles.Any())
            {
                if (MessageBox.Show($"Deleting {scope.Name} will affect {relevantRifles.Count} stored rifle(s). Continue?", "Query",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }

            scopesModel.Delete(scope);
            NotifyOfPropertyChange(() => Scopes);

            var appEvent = new AppEvent
            {
                Type = Constants.ScopeRemovedMessage,
            };
            appEvent.Parameters.Add(Constants.ChangedItemName, scope.Name);

            Messenger.PublishOnBackgroundThread(appEvent);
        }
    }
}
