using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Logger
    {
        string logPath;
        public Logger(string logPath)
        {
            this.logPath = logPath;
        }

        public async Task Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Data: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.\n");
            sb.Append($"Erro: {ex.Message}.\n");
            sb.Append($"Stacktrace: {ex.StackTrace}\n");
            sb.Append("_______________________________________________________________________________________________________________________________________________________________________________________________________________\n");

            using(StreamWriter sw = new StreamWriter(logPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }
        }


    }
}
