using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ScopesViewModel : FunctionScreen
    {
        private readonly ConfigurationModel configurationModel;
        private readonly IWindowManager windowManager;
        private readonly ScopesModel scopesModel;

        public override int Order
        {
            get
            {
                return 3;
            }
        }

        public override string IconFilename => "scope.png";

        public bool IsBusy { get; set; }
        public string BusyText { get; set; }

        public ScopesViewModel(ScopesModel scopesModel,
            ConfigurationModel configurationModel,
            IWindowManager windowManager,
            IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.scopesModel = scopesModel;
            this.windowManager = windowManager;
            this.configurationModel = configurationModel;
            DisplayName = "Scopes";
        }

        public IEnumerable<Scope> Scopes => scopesModel.All();

        public void AddScope()
        {
            var newScopeViewModel = new EditScopeViewModel(configurationModel);
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
            var editScopeViewModel = new EditScopeViewModel(configurationModel, scope);
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
            scopesModel.Delete(scope);
            NotifyOfPropertyChange(() => Scopes);
        }
    }
}
