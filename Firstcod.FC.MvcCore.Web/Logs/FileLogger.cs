using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Firstcod.FC.MvcCore.Web.Logs
{
    public class FileLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            string logMessage = $"{LogLevel.Information} {eventId.Id} {formatter(state, exception)}";

            WriteLog(logMessage).GetAwaiter();
        }

        public async Task WriteLog(string log)
        {
            // Revize
            string filename = DateTime.Now.ToShortDateString();
            string path = Path.Combine(@"wwwroot/logs/" + filename + ".txt");

            using (var stream = new StreamWriter(path, true))
            {
                stream.WriteLine(DateTime.Now + " " + log);
                stream.Close();
                await stream.DisposeAsync();
            }
        }
    }
}
