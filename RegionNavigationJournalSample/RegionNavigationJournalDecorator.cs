using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSample
{
    public class RegionNavigationJournalDecorator : IRegionNavigationJournal
    {
        private ILoggerFacade LoggerFacade { get; }
        private RegionNavigationJournal RegionNavigationJournal { get; }

        public RegionNavigationJournalDecorator(ILoggerFacade _loggerFacade)
            : base()
        {
            LoggerFacade = _loggerFacade;
            RegionNavigationJournal = new RegionNavigationJournal();
        }

        public bool CanGoBack
        {
            get
            {
                LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, RegionNavigationJournal.CanGoBack), Category.Debug, Priority.None);
                return RegionNavigationJournal.CanGoBack;
            }
        }

        public bool CanGoForward
        {
            get
            {
                LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, RegionNavigationJournal.CanGoForward), Category.Debug, Priority.None);
                return RegionNavigationJournal.CanGoForward;
            }
        }

        public IRegionNavigationJournalEntry CurrentEntry => RegionNavigationJournal.CurrentEntry;

        public INavigateAsync NavigationTarget
        {
            get => RegionNavigationJournal.NavigationTarget;
            set => RegionNavigationJournal.NavigationTarget = value;
        }

        public void GoBack()
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            RegionNavigationJournal.GoBack();
        }

        public void GoForward()
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            RegionNavigationJournal.GoForward();
        }

        public void RecordNavigation(IRegionNavigationJournalEntry entry, bool persistInHistory)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            RegionNavigationJournal.RecordNavigation(entry, persistInHistory);
        }

        public void Clear()
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            RegionNavigationJournal.Clear();
        }
    }
}
