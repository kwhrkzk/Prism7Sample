using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RegionSample
{
    public class MyContentControlRegionAdapter : ContentControlRegionAdapter
    {
        private ILoggerFacade LoggerFacade { get; }

        public MyContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory, ILoggerFacade _loggerFacade)
            : base(regionBehaviorFactory)
        {
            LoggerFacade = _loggerFacade;
        }

        protected override void Adapt(IRegion region, ContentControl regionTarget)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.Adapt(region, regionTarget);
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegionDecorator(LoggerFacade);
        }
    }
}
