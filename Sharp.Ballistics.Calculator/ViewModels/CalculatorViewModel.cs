using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharp.Ballistics.Calculator.Models;
using Sharp.Ballistics.Abstractions;
using GNUBallistics = GNUBallisticsLibrary;
using Sharp.Ballistics.Calculator.Util;
using System;
using UnitsNet;
using System.Windows;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class CalculatorViewModel : FunctionScreen, IHandle<RifleChangedEvent>
    {
        private readonly AmmoModel cartridgeModel;
        private readonly RiflesModel riflesModel;
        private readonly ConfigurationModel configurationModel;
        private Rifle selectedRifle;
        private Cartridge selectedCartridge;
        public override int Order => 1;

        public override string IconFilename => "calc.png";

        public CalculatorViewModel(IEventAggregator eventsAggregator,
                                   RiflesModel riflesModel,
                                   AmmoModel cartridgeModel,
                                   ConfigurationModel configurationModel) : base(eventsAggregator)
        {
            this.riflesModel = riflesModel;
            this.configurationModel = configurationModel;
            this.cartridgeModel = cartridgeModel;
            selectedRifle = null;
            DisplayName = "Ballistic Calculation";
            configurationModel.Load();
            selectedRifle = configurationModel.CalculatorSettings.CurrentRifle;

            var rifles = Rifles.ToList();
            if (string.IsNullOrWhiteSpace(selectedRifle?.Name) && rifles.Count > 0)
                SelectedRifle = rifles.OrderByDescending(x => x.Name).FirstOrDefault();

            if (selectedRifle != null)
            {
                selectedCartridge = configurationModel.CalculatorSettings.CurrentCartridge;
                if (string.IsNullOrWhiteSpace(selectedCartridge?.Name))
                    selectedCartridge = selectedRifle.Cartridge;
            }
        }

        protected GNUBallistics.Rifle RifleCalculator =>
            new GNUBallistics.Rifle(new RifleInfo
            {
                Name = selectedRifle.Name,
                BarrelTwist = selectedRifle.BarrelTwist,
                ZeroingConditions = selectedRifle.ZeroingWeather
            }, selectedRifle.Scope, selectedCartridge);

        public CalculatorSettings CalculatorSettings => configurationModel.CalculatorSettings;
        public UnitSettings Units => configurationModel.Units;

       
        private ShotInfo shotInfo;
        public ShotInfo ShotInfo
        {
            get
            {
                if (shotInfo == null)
                    shotInfo = new ShotInfo
                    {
                        WindSpeed = Speed.FromKilometersPerHour(0),
                        TargetSpeed = Speed.FromKilometersPerHour(0),
                        Range = Length.FromMeters(0)
                    };
                return shotInfo;
            }
            set
            {
                shotInfo = value;
                NotifyOfPropertyChange(() => ShotInfo);
            }
        }

        public IEnumerable<Rifle> Rifles => riflesModel.All();

        public IEnumerable<Cartridge> RelevantCartridges
        {
            get
            {
                if (!Rifles.Any())
                    return new Cartridge[0];

                var relevantCartridges = cartridgeModel.All().Where(x =>
                    x.Caliber.Equals(selectedRifle.Cartridge.Caliber)).ToList();

                if (!relevantCartridges.Any(x => x.Equals(selectedRifle.Cartridge)))
                    relevantCartridges.Add(selectedRifle.Cartridge);
                return relevantCartridges;
            }
        }

        public Cartridge SelectedCartridge
        {
            get
            {
                if (string.IsNullOrWhiteSpace(selectedCartridge?.Name))
                    selectedCartridge = SelectedRifle?.Cartridge;
                return selectedCartridge;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                    return;

                selectedCartridge = value;
                SaveConfigurationSetting(settings => settings.CurrentCartridge = value);
                NotifyOfPropertyChange(() => SelectedCartridge);
            }
        }

        private WeatherCondition currentWeather;
        public WeatherCondition CurrentWeather
        {
            get
            {
                if (currentWeather == null)
                    currentWeather = WeatherCondition.Default;
                return currentWeather;
            }

            set
            {
                currentWeather = value;
                NotifyOfPropertyChange(() => CurrentWeather);
            }
        }

        public void InitializeCurrentWeatherWithZeroingWeather()
        {
            CurrentWeather = new WeatherCondition
            {
                Altitude = SelectedRifle.ZeroingWeather.Altitude,
                Barometer = SelectedRifle.ZeroingWeather.Barometer,
                RelativeHumidity = SelectedRifle.ZeroingWeather.RelativeHumidity,
                Temperature = SelectedRifle.ZeroingWeather.Temperature,
            };
        }

        public void ClearCurrentWeather()
        {
            CurrentWeather = WeatherCondition.Default;
        }

        private ShotLocationInfo shotLocationInfo;
        public ShotLocationInfo ShotLocationInfo
        {
            get
            {
                if (shotLocationInfo == null)
                    shotLocationInfo = new ShotLocationInfo();
                return shotLocationInfo;
            }

            set
            {
                shotLocationInfo = value;
                NotifyOfPropertyChange(() => ShotLocationInfo);
            }
        }        

        private bool isUsingCoriolis;
        public bool IsUsingCoriolis
        {
            get
            {
                return isUsingCoriolis;
            }

            set
            {
                isUsingCoriolis = value;
                NotifyOfPropertyChange(() => IsUsingCoriolis);
            }
        }

        private bool isUsingDifferentWeather;
        public bool IsUsingDifferentWeather
        {
            get
            {
                return isUsingDifferentWeather;
            }

            set
            {
                isUsingDifferentWeather = value;
                NotifyOfPropertyChange(() => IsUsingDifferentWeather);
            }
        }

        private void SaveConfigurationSetting(Action<CalculatorSettings> settingsMutator)
        {
            settingsMutator?.Invoke(CalculatorSettings);
            Task.Run(() => configurationModel.Save());
        }

        public Rifle SelectedRifle
        {
            get
            {              
                return selectedRifle;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                    return;

                selectedRifle = value;
                SaveConfigurationSetting(settings => settings.CurrentRifle = value);
                NotifyOfPropertyChange(() => SelectedRifle);
            }
        }       

        public void ResetToDefaults()
        {
            if (SelectedRifle?.Cartridge == null)
            {
                MessageBox.Show("This will do nothing, since no rifle is configured.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            SelectedCartridge = SelectedRifle?.Cartridge;
        }

        public override void Handle(AppEvent message)
        {
            base.Handle(message);
            if(message.Type.Equals(Constants.RifleRemovedMessage))
            {
                SelectedRifle = Rifles.FirstOrDefault();
                SelectedCartridge = SelectedRifle.Cartridge;
            }
            
            if (message.Type.Equals(Constants.CartridgeRemovedMessage))
            {
                SelectedCartridge = SelectedRifle.Cartridge;
            }
        }

        public void Handle(RifleChangedEvent message)
        {
            SelectedRifle = message.ChangedRifle;
            SelectedCartridge = SelectedRifle.Cartridge;
            Refresh();
        }
    }
}
