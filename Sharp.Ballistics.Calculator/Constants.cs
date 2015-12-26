namespace Sharp.Ballistics.Calculator
{
    public static class Constants
    {
        public const string DatabaseName = "Ballistics.Calculator";

        public const string UnitsConfigurationId = "Configuration/Units";
        public const string CalculatorSettingsId = "Configuration/Calculator";

        //messages
        public const string ConfigurationChangedMessage = "Ballstic/Event/ConfigurationChanged";
        public const string RifleSettingsChangedMessage = "Ballstic/Event/RifleSettingsChanged";
        public const string RifleRemovedMessage = "Ballistic/Event/RifleRemoved";
        public const string CartridgeRemovedMessage = "Ballistic/Event/CartridgeRemoved";
        public const string ScopeRemovedMessage = "Ballistic/Event/ScopeRemoved";
        
        //params
        public const string ChangedItemName = "Parameters/ChangedItem";
    }
}
