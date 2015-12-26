using Raven.Client;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sharp.Ballistics.Calculator.Models
{

    public class ConfigurationModel
    {
        private readonly IDocumentStore documentStore;
        private int isInitialized;
        public ConfigurationModel(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize()
        {
            //make sure it is initialized only once 
            if (Interlocked.CompareExchange(ref isInitialized, 1, 0) == 1)
                return;

            Load();
        }

        public void Load()
        {
            using (var session = documentStore.OpenSession())
            {
                var unitsConfig = session.Load<UnitSettings>(Constants.UnitsConfigurationId);
                if (unitsConfig != null && !IsValidUnitsConfig(unitsConfig))
                    unitsConfig = null;

                units = unitsConfig ?? UnitSettings.Metric;

                calculatorSettings = session.Load<CalculatorSettings>(Constants.CalculatorSettingsId) 
                                        ?? new CalculatorSettings();
            }
        }

        private static bool IsValidUnitsConfig(UnitSettings unitsConfig)
        {
            if (unitsConfig.Barometer == UnitsNet.Units.PressureUnit.Undefined ||
               unitsConfig.BulletOffsets == UnitsNet.Units.LengthUnit.Undefined ||
               unitsConfig.Distance == UnitsNet.Units.LengthUnit.Undefined ||
               unitsConfig.MuzzleSpeed == UnitsNet.Units.SpeedUnit.Undefined ||
               unitsConfig.ScopeHeight == UnitsNet.Units.LengthUnit.Undefined ||
               unitsConfig.Temperature == UnitsNet.Units.TemperatureUnit.Undefined ||
               unitsConfig.WindSpeed == UnitsNet.Units.SpeedUnit.Undefined)
                return false;

            return true;
        }

        public bool CanSave() => IsValidUnitsConfig(Units);

        public void Save()
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(Units, Constants.UnitsConfigurationId);
                session.Store(CalculatorSettings, Constants.CalculatorSettingsId);
                session.SaveChanges();
            }
        }

        private UnitSettings units;
        public UnitSettings Units
        {
            get
            {
                Initialize();
                return units;
            }
        }

        private CalculatorSettings calculatorSettings;
        public CalculatorSettings CalculatorSettings
        {
            get
            {
                Initialize();
                return calculatorSettings;                
            }
        }
    }
}
