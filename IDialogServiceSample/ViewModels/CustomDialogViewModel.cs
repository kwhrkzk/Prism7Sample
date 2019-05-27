using IDialogServiceSample.Views;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace IDialogServiceSample.ViewModels
{
    public class CustomDialogViewModel : IDialogAware, IDisposable, INotifyPropertyChanged, IConfirmNavigationRequest, IRegionMemberLifetime
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        private IRegionNavigationService RegionNavigationService { get; set; }
        private IRegionManager RegionManager { get; }

        public bool KeepAlive => false;

        private CompositeDisposable DisposeCollection = new CompositeDisposable();
        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeCollection.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
        #endregion

        public string IconSource { get; set; } = "";
        public string Title { get; set; } = "CustomDialog";

        public ReactivePropertySlim<double> Height { get; } = new ReactivePropertySlim<double>(Double.NaN);
        public ReactivePropertySlim<double> Width { get; } = new ReactivePropertySlim<double>(Double.NaN);

        public ReactivePropertySlim<string> Input { get; } = new ReactivePropertySlim<string>();
        public ReactiveCommand OKCommand { get; } = new ReactiveCommand();
        public ReactiveCommand RecursionCommand { get; } = new ReactiveCommand();

        public ReactivePropertySlim<IRegionManager> CustomDialogRegionManager { get; } = new ReactivePropertySlim<IRegionManager>();

        public event Action<IDialogResult> RequestClose;

        public CustomDialogViewModel(
            IDialogService _dialogService,
            IRegionManager _regionManager
            )
        {
            RegionManager = _regionManager;
            CustomDialogRegionManager.Value = RegionManager.CreateRegionManager();

            OKCommand.Subscribe(Close).AddTo(DisposeCollection);
            RecursionCommand.Subscribe(() =>
            {
                IDialogResult result = null;
                _dialogService.ShowDialog(nameof(CustomDialogView), new DialogParameters { { "Input", Input.Value } }, ret => result = ret);

                if (result != null)
                    Input.Value = result.Parameters.GetValue<string>("Input");

            }).AddTo(DisposeCollection);
        }

        private void Close()
        {
            var p = new DialogParameters { { "Input", Input.Value } };

            if (RegionNavigationService == null)
                RequestClose(new DialogResult(true, p));
            else
                RegionNavigationService?.RequestNavigate(nameof(ContentView), p);
        }

        public bool CanCloseDialog() => true;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RegionNavigationService = navigationContext.NavigationService;

            CustomDialogRegionManager.Value.RequestNavigate("CustomDialogRegion", nameof(SubaView));

            if (navigationContext.Parameters.ContainsKey("Input"))
                Input.Value = navigationContext.Parameters.GetValue<string>("Input");
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Width.Value = 200;
            Height.Value = 100;

            CustomDialogRegionManager.Value.RequestNavigate("CustomDialogRegion", nameof(SubaView));

            if (parameters?.ContainsKey("Input") ?? false)
                Input.Value = parameters.GetValue<string>("Input");
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) => Dispose();

        public void OnDialogClosed()
        {
            foreach (IRegion r in CustomDialogRegionManager.Value.Regions)
                r.RemoveAll();

            Dispose();
        }
   }
}