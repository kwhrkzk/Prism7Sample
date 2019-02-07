using PopupWindowActionSample.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupWindowActionSample.ViewModels
{
    public class SubbViewModel : BindableBase, IConfirmNavigationRequest, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        private IRegionNavigationService RegionNavigationService { get; set; }

        public DelegateCommand GoToSubACommand { get; }

        public SubbViewModel() => GoToSubACommand = new DelegateCommand(() => RegionNavigationService.RequestNavigate(nameof(SubaView)));

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public void OnNavigatedTo(NavigationContext navigationContext) => RegionNavigationService = navigationContext.NavigationService;

        public void OnNavigatedFrom(NavigationContext navigationContext) { }
    }
}
