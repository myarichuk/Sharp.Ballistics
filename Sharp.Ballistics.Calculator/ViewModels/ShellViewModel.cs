using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharp.Ballistics.Calculator.ViewModels
{

    public class ShellViewModel : Conductor<FunctionScreen>
    {
        private readonly IWindowManager windowManager;
        private readonly FunctionScreen[] screens;
        public ShellViewModel(IWindowManager windowManager,FunctionScreen[] screens)
        {
            this.windowManager = windowManager;
            this.screens = screens.OrderBy(x => x.Order).ToArray();
            
        }

        //since this is root VM, this should happen only once
        protected override void OnActivate() 
        {
            ActiveItem = screens.FirstOrDefault();            
            DisplayName = ActiveItem.DisplayName;
        }
    }
}