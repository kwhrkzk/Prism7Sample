using Prism.Logging;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSample
{
    public class SingleActiveRegionDecorator : SingleActiveRegion
    {
        private ILoggerFacade LoggerFacade { get; }

        public SingleActiveRegionDecorator(ILoggerFacade _loggerFacade)
            : base()
        {
            LoggerFacade = _loggerFacade;
        }

        public override void Activate(object view)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.Activate(view);
        }

        public override IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            return base.Add(view, viewName, createRegionManagerScope);
        }

        public override void Deactivate(object view)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.Deactivate(view);
        }

        public override object GetView(string viewName)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            return base.GetView(viewName);
        }

        public override void Remove(object view)
        {
            LoggerFacade.Log(String.Join(" ", DateTime.Now.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name), Category.Debug, Priority.None);
            base.Remove(view);
        }
    }
}
