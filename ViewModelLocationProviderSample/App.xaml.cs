using Prism.DryIoc;
using Prism.Ioc;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLocationProviderSample.V;
using ViewModelLocationProviderSample.VM;

namespace ViewModelLocationProviderSample
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() => new Main();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Container.Resolve<IRegionManager>().RequestNavigate("ContentARegion", nameof(ContentA));
            Container.Resolve<IRegionManager>().RequestNavigate("ContentBRegion", "ContentB");
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".V.", ".VM.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = "VM";
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}{1}, {2}", viewName, suffix, viewAssemblyName);
                return Type.GetType(viewModelName);
            });

            ViewModelLocationProvider.Register<ContentA>(() => Container.Resolve<Content>());
            ViewModelLocationProvider.Register<ContentB, Content>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Content>();
            containerRegistry.RegisterForNavigation<ContentA>(nameof(ContentA));
            containerRegistry.RegisterForNavigation<ContentB>("ContentB");
        }
    }
}
