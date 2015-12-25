using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using UnitsNet;
using UnitsNet.Units;
using Humanizer;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class EditRifleViewModel : Screen
    {
        private readonly ConfigurationModel configurationModel;
        private readonly Rifle rifle;
        private readonly ScopesModel scopesModel;
        private readonly AmmoModel cartridgesModel;
        private bool isCanceling;

        public EditRifleViewModel(ConfigurationModel configurationModel,
                                AmmoModel cartridgesModel,
                                ScopesModel scopesModel,
                                Rifle rifle = null)
        {
            this.cartridgesModel = cartridgesModel;
            this.scopesModel = scopesModel;
            this.configurationModel = configurationModel;
            this.rifle = rifle ?? new Rifle
            {
                ZeroingWeather = WeatherCondition.Default
            };

            NotifyOfPropertyChange(() => Scope);
            NotifyOfPropertyChange(() => Cartridge);

            DisplayName = "Rifle Settings";
        }       

        public Rifle Rifle => rifle;
        public UnitsConfiguration Units => configurationModel.Units;

        public IEnumerable<Scope> Scopes => scopesModel.All();
        public IEnumerable<Cartridge> Cartridges => cartridgesModel.All();

        public bool HasErrors =>
            string.IsNullOrWhiteSpace(Name) || 
            rifle?.Scope == null || 
            rifle.Cartridge == null || 
            rifle.ZeroingWeather == null || 
            rifle.Cartridge.Name.Equals(string.Empty) || 
            rifle.Scope.Name.Equals(string.Empty);

        public string BarrelTwistUnits => 
            Units.BarrelTwist.Humanize().Pluralize().ToLower();

        public string Name
        {
            get
            {
                return rifle.Name;
            }
            set
            {
                rifle.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        
        public Scope Scope
        {
            get
            {
                return rifle.Scope;
            }
            set
            {
                rifle.Scope = value;
                NotifyOfPropertyChange(() => Scope);
            }
        }

        public Cartridge Cartridge
        {
            get
            {
                return rifle.Cartridge;
            }
            set
            {
                rifle.Cartridge = value;
                NotifyOfPropertyChange(() => Cartridge);
            }
        }

        public WeatherCondition ZeroingWeather
        {
            get
            {
                return rifle.ZeroingWeather;
            }
            set
            {
                rifle.ZeroingWeather = value;
                NotifyOfPropertyChange(() => ZeroingWeather);
            }
        }



        public double BarrelTwist
        {
            get
            {
                return rifle.BarrelTwist.As(Units.BarrelTwist);
            }
            set
            {
                rifle.BarrelTwist = Length.From(value, Units.BarrelTwist);
                NotifyOfPropertyChange(() => BarrelTwist);
            }
        }


        public override void CanClose(Action<bool> callback)
        {
            if (HasErrors && !isCanceling)
            {
                if(rifle != null && rifle.BarrelTwist.As(LengthUnit.Inch) <= 0)
                    MessageBox.Show("Please make sure that barrel twist is higher than zero before saving",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                    MessageBox.Show("Please fill-out all fields for the rifle before saving",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                callback?.Invoke(false);
            }
            else
                callback?.Invoke(true);
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Enter:
                    Save();
                    break;
                case Key.Escape:
                    Cancel();
                    break;
            }
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
