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
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IDialogServiceSample.ViewModels
{
    public class ContentViewModel : IConfirmNavigationRequest, IRegionMemberLifetime, IDisposable, INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        private IRegionNavigationService RegionNavigationService { get; set; }

        public bool KeepAlive => false;

        private IDialogService DialogService { get; }

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

        public ReactivePropertySlim<string> Input { get; } = new ReactivePropertySlim<string>("Default Value");

        public ReactiveCommand ShowDialogCommand { get; } = new ReactiveCommand();
        public AsyncReactiveCommand ShowCommand { get; } = new AsyncReactiveCommand();

        public ReactiveCommand RequestNavigateCommand { get; } = new ReactiveCommand();

        public ContentViewModel(IDialogService _dialogService)
        {
            DialogService = _dialogService;

            RequestNavigateCommand
                .Subscribe(() => RegionNavigationService.RequestNavigate(nameof(CustomDialogView), new NavigationParameters { { "Input", Input.Value } }))
                .AddTo(DisposeCollection);

            ShowDialogCommand
                .Subscribe(() =>
                {
                    IDialogResult result = null;
                    DialogService.ShowDialog(nameof(CustomDialogView), new DialogParameters { { "Input", Input.Value } }, ret => result = ret);

                    if (result != null)
                        Input.Value = result.Parameters.GetValue<string>("Input");

                }).AddTo(DisposeCollection);

            ShowCommand
                .Subscribe(async () =>
                {
                    var trigger = new Subject<IDialogResult>();
                    var result = trigger.ToReadOnlyReactivePropertySlim();

                    DialogService.Show(nameof(CustomDialogView), new DialogParameters { { "Input", Input.Value } }, trigger.OnNext);

                    await result;

                    Input.Value = result.Value.Parameters.GetValue<string>("Input");

                }).AddTo(DisposeCollection);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RegionNavigationService = navigationContext.NavigationService;

            if (navigationContext.Parameters.ContainsKey("Input"))
                Input.Value = navigationContext.Parameters.GetValue<string>("Input");
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) => Dispose();
    }
}
