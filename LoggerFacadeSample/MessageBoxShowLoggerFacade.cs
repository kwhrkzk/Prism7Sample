using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFacadeSample
{
    public class MessageBoxShowLoggerFacade : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
            => System.Windows.MessageBox.Show($"[{category.ToString()}] {message}");
    }
}
