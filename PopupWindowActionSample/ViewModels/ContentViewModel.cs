using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupWindowActionSample.ViewModels
{
    public class ContentViewModel : BindableBase, IConfirmNavigationRequest, IRegionMemberLifetime
    {
        public bool KeepAlive => false;

        private IRegionNavigationService RegionNavigationService { get; set; }

        public DelegateCommand PopupWindowCommand { get; }
        public InteractionRequest<PopupNotification> PopupWindowRequest { get; } = new InteractionRequest<PopupNotification>();

        public ContentViewModel()
        {
            PopupWindowCommand = new DelegateCommand(() => PopupWindowRequest.Raise(new PopupNotification(), PopupWindowRequestCallback));
        }

        private void PopupWindowRequestCallback(PopupNotification n)
        {
            var list = n.PopupRegionManager.Regions.Select(r => r.Name).ToList();
            var ret = list.Aggregate(true, (b, name) => b && n.PopupRegionManager.Regions.Remove(name));
            if (ret == false)
                System.Windows.MessageBox.Show("IRegionのRemoveに失敗");
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
