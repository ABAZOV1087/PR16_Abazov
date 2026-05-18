using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PR16_Abazov.Models;

namespace PR16_Abazov.Core
{
    public static class GameLogger
    {
        public static void Write(string message, System.Windows.Controls.ListBox logList)
        {
            logList.Items.Insert(0, $"[{System.DateTime.Now:HH:mm}] {message}");
        }
    }
}
