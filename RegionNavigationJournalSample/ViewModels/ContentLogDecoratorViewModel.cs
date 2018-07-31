using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSample.ViewModels
{
    public class ContentLogDecoratorViewModel : ContentViewModel
    {
        private ILoggerFacade LoggerFacade { get; }

        public ContentLogDecoratorViewModel(ILoggerFacade _loggerFacade)
            : base()
        {
            LoggerFacade = _loggerFacade;
        }

        public override bool KeepAlive
        {
            get
            {
                LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
                return base.KeepAlive;
            }
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.ConfirmNavigationRequest(navigationContext, continuationCallback);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            return base.IsNavigationTarget(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.OnNavigatedTo(navigationContext);
        }

        public override bool PersistInHistory()
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            return base.PersistInHistory();
        }
    }
}
