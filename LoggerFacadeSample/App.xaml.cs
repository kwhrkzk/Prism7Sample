using LoggerFacadeSample.ViewModels;
using LoggerFacadeSample.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LoggerFacadeSample
{
    /// <summary>
    /// Interaction logic for App.xaml
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
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            containerRegistry.RegisterInstance<ILoggerFacade>(
                new SpamLoggerFacade(
                    new List<ILoggerFacade> {
                        new TraceLogger(),
                        new TextLogger(),
                        new Log4NetLogFacadeAdapter(),
                        new MessageBoxShowLoggerFacade()
                    }
                )
            );
        }
    }
}
