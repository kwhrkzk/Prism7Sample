using IDialogServiceSample.ViewModels;
using IDialogServiceSample.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace IDialogServiceSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ContentView>();
            containerRegistry.RegisterForNavigation<CustomDialogView>();
            containerRegistry.RegisterForNavigation<SubaView>();
            containerRegistry.RegisterForNavigation<SubbView>();

            //containerRegistry.RegisterDialog<CustomDialogView>(nameof(CustomDialogView));
            containerRegistry.RegisterDialog<CustomDialogView, CustomDialogViewModel>();

            containerRegistry.RegisterDialogWindow<CustomDialogWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Container.Resolve<IRegionManager>().RequestNavigate("ContentRegion", nameof(ContentView));
        }
    }
}
