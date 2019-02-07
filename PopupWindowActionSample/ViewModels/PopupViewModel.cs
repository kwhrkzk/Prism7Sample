using PopupWindowActionSample.Views;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupWindowActionSample.ViewModels
{
    public class PopupViewModel : BindableBase, IInteractionRequestAware
    {
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public ReactivePropertySlim<IRegionManager> PopupRegionManager { get; } = new ReactivePropertySlim<IRegionManager>();

        public DelegateCommand LoadedCommand { get; }

        public PopupViewModel(IRegionManager _regionManager)
        {
            PopupRegionManager.Value = _regionManager.CreateRegionManager();

            LoadedCommand = new DelegateCommand(Loaded);
        }

        private void Loaded()
        {
            if (Notification is PopupNotification n)
                n.PopupRegionManager = PopupRegionManager.Value;

            PopupRegionManager.Value.RequestNavigate("PopupRegion", nameof(SubbView));
        }
    }
}
