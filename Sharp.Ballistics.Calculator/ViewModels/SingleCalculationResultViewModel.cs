using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using GNUBallistics = GNUBallisticsLibrary;
using Sharp.Ballistics.Calculator.Models;
using UnitsNet;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class SingleCalculationResultViewModel : Screen
    {
        private readonly ConfigurationModel configurationModel;
        private readonly bool isUsingCoriolis;
        private readonly bool isUsingDifferentWeather;
        private readonly GNUBallistics.Rifle rifleCalculator;
        private readonly Cartridge selectedCartridge;
        private readonly Rifle selectedRifle;
        private readonly ShotInfo shotInfo;
        private readonly ShotLocationInfo shotLocationInfo;
        private readonly WeatherCondition currentWeather;

        public SingleCalculationResultViewModel(GNUBallistics.Rifle rifleCalculator, 
            ShotInfo shotInfo, 
            Rifle selectedRifle, 
            Cartridge selectedCartridge, 
            bool isUsingDifferentWeather, 
            bool isUsingCoriolis, 
            ShotLocationInfo shotLocationInfo,
            WeatherCondition currentWeather,
            ConfigurationModel configurationModel)
        {
            this.rifleCalculator = rifleCalculator;
            this.shotInfo = shotInfo;
            this.selectedRifle = selectedRifle;
            this.selectedCartridge = selectedCartridge;
            this.isUsingDifferentWeather = isUsingDifferentWeather;
            this.isUsingCoriolis = isUsingCoriolis;
            this.shotLocationInfo = shotLocationInfo;
            this.currentWeather = currentWeather;
            this.configurationModel = configurationModel;
        }

        public UnitSettings Units => configurationModel.Units;

        public double MovingTargetOffsetMOA
        {
            get
            {
                var timeOfFlight = BallisticSolution.TimeToTargetSec;
                
                var targetTravelDistance = Length.FromMeters(shotInfo.TargetSpeed.MetersPerSecond * timeOfFlight);
                var InchesInMOAForRange = (shotInfo.Range.Yards / 100) * Constants.InchesInMOA;

                return targetTravelDistance.Inches / InchesInMOAForRange;
            }
        }

        public double MovingTargetOffsetClicks
        {
            get
            {
                return MovingTargetOffsetMOA * selectedRifle.Scope.WindageClicksPerMOA;
            }
        }

        public double MovingTargetOffsetMils => MovingTargetOffsetMOA / 0.290888;

        private BallisticSolution ballisticSolution;
        public BallisticSolution BallisticSolution
        {
            get
            {
                if (ballisticSolution == null)
                    ballisticSolution = rifleCalculator.Solve(shotInfo.Angle,
                            shotInfo.WindSpeed,
                            shotInfo.WindDirection,
                            shotInfo.Range,
                            isUsingDifferentWeather ? currentWeather : null,
                            isUsingCoriolis ? shotLocationInfo : null);
                return ballisticSolution;
            }
        }
    }
}
