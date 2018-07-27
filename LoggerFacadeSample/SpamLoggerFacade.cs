using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerFacadeSample
{
    public class SpamLoggerFacade : ILoggerFacade
    {
        private List<ILoggerFacade> facades { get; }

        public SpamLoggerFacade(IEnumerable<ILoggerFacade> _facades)
            => facades = new List<ILoggerFacade>(_facades);

        public void Log(string message, Category category, Priority priority)
            => facades.ForEach(_facade => _facade.Log(message, category, priority));
    }
}
