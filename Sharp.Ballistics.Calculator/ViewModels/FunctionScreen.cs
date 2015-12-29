using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public abstract class FunctionScreen : Screen, IHandle<AppEvent>, IHandle<ExportImportEvent>
    {
        public abstract string IconFilename { get; }

        private ImageSource icon;
        public ImageSource Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = new BitmapImage(new Uri($"pack://application:,,,/Sharp.Ballistics.Calculator;component/Images/{IconFilename}"));
                    icon.Freeze();
                }
                return icon;
            }
        }

        public abstract int Order { get; }

        protected IEventAggregator Messenger { get; }

        protected FunctionScreen(IEventAggregator eventsAggregator)
        {
            this.Messenger = eventsAggregator;
            eventsAggregator.Subscribe(this);
        }

        public virtual void Handle(AppEvent message)
        {
            if (message.Type == Constants.ConfigurationChangedMessage)
            {
                Refresh();
            }            
        }

        public virtual void Handle(ExportImportEvent message)
        {
            Refresh();
        }
    }
}
