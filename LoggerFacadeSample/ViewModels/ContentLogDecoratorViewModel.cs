using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFacadeSample.ViewModels
{
    public class ContentLogDecoratorViewModel : ContentViewModel
    {
        private ILoggerFacade LoggerFacade { get; }

        public ContentLogDecoratorViewModel(ILoggerFacade _loggerFacade)
            : base()
        {
            LoggerFacade = _loggerFacade;
        }

        protected override void execute()
        {
            LoggerFacade.Log(DateTime.Now.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name, Category.Debug, Priority.None);

            base.execute();
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            LoggerFacade.Log(DateTime.Now.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name, Category.Debug, Priority.None);

            return base.IsNavigationTarget(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            LoggerFacade.Log(DateTime.Now.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name, Category.Debug, Priority.None);

            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoggerFacade.Log(DateTime.Now.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name, Category.Debug, Priority.None);

            base.OnNavigatedTo(navigationContext);
        }
    }
}
