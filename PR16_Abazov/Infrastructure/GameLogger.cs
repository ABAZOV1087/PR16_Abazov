using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PR16_Abazov.Models;

namespace PR16_Abazov.Infrastructure
{
    public static class GameLogger
    {
        public static void LogEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}

