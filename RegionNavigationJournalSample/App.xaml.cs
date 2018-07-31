using Prism.DryIoc;
using Prism.Ioc;
using Prism.Logging;
using Prism.Regions;
using RegionSample.ViewModels;
using RegionSample.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RegionSample
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Container.Resolve<IRegionManager>().RequestNavigate("ContentRegion", nameof(ContentView));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>(nameof(MainWindow));
            containerRegistry.RegisterForNavigation<ContentView, ContentLogDecoratorViewModel>(nameof(ContentView));
            containerRegistry.RegisterForNavigation<OtherContentView>(nameof(OtherContentView));
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            containerRegistry.Register<MyContentControlRegionAdapter>();
            containerRegistry.Register<IRegionNavigationJournal, RegionNavigationJournalDecorator>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), Container.Resolve<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), Container.Resolve<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), Container.Resolve<MyContentControlRegionAdapter>());
            }
        }
    }
}
