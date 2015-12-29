using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharp.Ballistics.Calculator.ViewModels
{

    public class ShellViewModel : Conductor<FunctionScreen>, IHandle<ExportImportEvent>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;        
        private readonly FunctionScreen[] screens;
        public ShellViewModel(IWindowManager windowManager, 
            IEventAggregator eventAggregator,
            FunctionScreen[] screens)
        {
            this.windowManager = windowManager;
            this.screens = screens.OrderBy(x => x.Order).ToArray();
            DisplayName = "Ballistics Calculator";
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        public IEnumerable<FunctionScreen> FunctionScreens => screens;       

        public bool IsInExportImport { get; set; }

        public string ExportImportMessage { get; set; }

        //since this is root VM, this should happen only once
        protected override void OnActivate() 
        {
            ActiveItem = screens.FirstOrDefault();
        }

        public void Handle(ExportImportEvent message)
        {
            IsInExportImport = message.IsInProgress;
            ExportImportMessage = message.Message;
            NotifyOfPropertyChange(() => IsInExportImport);
            NotifyOfPropertyChange(() => ExportImportMessage);
        }
    }
}