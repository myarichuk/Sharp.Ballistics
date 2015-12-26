using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using UnitsNet;
using System.Linq;
using System.Windows.Input;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class EditScopeViewModel : Screen
    {
        private readonly ScopesModel scopesModel;
        private readonly Scope scope;
        private readonly ConfigurationModel configurationModel;
        private bool isCanceling;
        private readonly bool isNew;
        public EditScopeViewModel(ConfigurationModel configurationModel, ScopesModel scopesModel, Scope scopeToEdit = null)
        {
            this.configurationModel = configurationModel;
            scope = scopeToEdit ?? new Scope();
            isNew = scopeToEdit == null;
            DisplayName = "Edit Scope";
            this.scopesModel = scopesModel;
        }

        public Scope Scope => scope;
        public UnitSettings Units => configurationModel.Units;

        public string Name
        {
            get
            {
                return scope.Name;
            }
            set
            {
                scope.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public double ZeroDistance
        {
            get
            {
                return scope.ZeroDistance.As(Units.Distance);
            }
            set
            {
                scope.ZeroDistance = Length.From(value, Units.Distance);
                NotifyOfPropertyChange(() => ZeroDistance);
            }
        }

        public double ScopeHeight
        {
            get
            {
                return scope.Height.As(Units.ScopeHeight);
            }
            set
            {
                scope.Height = Length.From(value, Units.ScopeHeight);
                NotifyOfPropertyChange(() => ScopeHeight);
            }
        }

        public double ElevationClicksPerMOA
        {
            get
            {
                return scope.ElevationClicksPerMOA;
            }
            set
            {
                scope.ElevationClicksPerMOA = value;
                NotifyOfPropertyChange(() => ElevationClicksPerMOA);
            }
        }

        public double WindageClicksPerMOA
        {
            get
            {
                return scope.WindageClicksPerMOA;
            }
            set
            {
                scope.WindageClicksPerMOA = value;
                NotifyOfPropertyChange(() => WindageClicksPerMOA);
            }
        }

        public Dictionary<string, bool> ValidationErrors { get; set; } = new Dictionary<string, bool>();
        public bool HasErrors => String.IsNullOrWhiteSpace(Name) || ValidationErrors.Any(x => x.Value);

        public override void CanClose(Action<bool> callback)
        {
            var scopeWithTheSameName = scopesModel.ByName(scope.Name);
            if(scopeWithTheSameName != null && !isCanceling && isNew)
            {
                MessageBox.Show("Scope with the same name already exists.",
                   "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                callback?.Invoke(false);
            }
            else if (HasErrors && !isCanceling)
            {
                MessageBox.Show("Please fill-out all fields for the scope before saving",
                   "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                callback?.Invoke(false);
            }
            else
                callback?.Invoke(true);
        }

        public void KeyPressed(Key key)
        {
            if (key == Key.Enter)
                Save();
            else if (key == Key.Escape)
                Cancel();
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            isCanceling = true;
            TryClose(false);
        }
    }
}
