using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggerFacadeSample.ViewModels
{
    public class ContentViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand DoCommand { get; }

        public ContentViewModel()
        {
            DoCommand = new DelegateCommand(execute);
        }

        protected virtual void execute()
        {

        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
