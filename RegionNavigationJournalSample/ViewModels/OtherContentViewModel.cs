using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RegionSample.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSample.ViewModels
{
    public class OtherContentViewModel : BindableBase, IConfirmNavigationRequest, IJournalAware, IRegionMemberLifetime
    {
        public virtual bool KeepAlive => false;

        private IRegionNavigationService RegionNavigationService { get; set; }

        public DelegateCommand BackCommand { get; }
        public DelegateCommand ForwardCommand { get; }
        public DelegateCommand ContentCommand { get; }

        public OtherContentViewModel()
        {
            BackCommand = new DelegateCommand(() => RegionNavigationService.Journal.GoBack(), () => RegionNavigationService?.Journal?.CanGoBack ?? false);
            ForwardCommand = new DelegateCommand(() => RegionNavigationService.Journal.GoForward(), () => RegionNavigationService?.Journal?.CanGoForward ?? false);
            ContentCommand = new DelegateCommand(() => RegionNavigationService.RequestNavigate(nameof(ContentView)));
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            RegionNavigationService = navigationContext.NavigationService;
            BackCommand.RaiseCanExecuteChanged();
            ForwardCommand.RaiseCanExecuteChanged();
        }

        public virtual bool PersistInHistory() => false;
    }
}
