using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Ballistics.Calculator.Models;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class MultipleCalculationResultsViewModel : Screen
    {
        private bool isUsingCoriolis;
        private bool isUsingDifferentWeather;
        private Cartridge selectedCartridge;
        private Rifle selectedRifle;
        private ShotInfo shotInfo;
        private ShotLocationInfo shotLocationInfo;

        public MultipleCalculationResultsViewModel(ShotInfo shotInfo,
            Rifle selectedRifle, 
            Cartridge selectedCartridge, 
            bool isUsingDifferentWeather, 
            bool isUsingCoriolis, 
            ShotLocationInfo shotLocationInfo)
        {
            this.shotInfo = shotInfo;
            this.selectedRifle = selectedRifle;
            this.selectedCartridge = selectedCartridge;
            this.isUsingDifferentWeather = isUsingDifferentWeather;
            this.isUsingCoriolis = isUsingCoriolis;
            this.shotLocationInfo = shotLocationInfo;
        }
    }
}
