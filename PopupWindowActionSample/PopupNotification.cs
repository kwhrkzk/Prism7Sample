using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupWindowActionSample
{
    public class PopupNotification : INotification
    {
        public IRegionManager PopupRegionManager { get; set; }
        public string Title { get; set; } = "PopupWindow";
        public object Content { get; set; }
    }
}
