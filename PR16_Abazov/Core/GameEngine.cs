using PR16_Abazov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR16_Abazov.Core
{
    public class GameEngine
    {
        private Random _rnd = new Random();

        public object GenerateEvent(int floor)
        {
            if (floor % 10 == 0) // Каждый 10 этаж - Босс
                return Core.EnemyTemplates.Where(e => e.IsBoss).OrderBy(x => _rnd.Next()).First();

            if (_rnd.Next(0, 2) == 0) return "Chest";

            return Core.EnemyTemplates.Where(e => !e.IsBoss).OrderBy(x => _rnd.Next()).First();
        }

        public Items GenerateLoot()
        {
            return Core.Items[_rnd.Next(Core.Items.Count)];
        }
    }
}