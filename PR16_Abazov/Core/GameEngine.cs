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
            if (floor % 10 == 0) 
                return Core.EnemyTemplates.Where(e => e.IsBoss).OrderBy(x => _rnd.Next()).First();

            if (_rnd.Next(0, 2) == 0) return "Chest";

            return Core.EnemyTemplates.Where(e => !e.IsBoss).OrderBy(x => _rnd.Next()).First();
        }

        public Items GenerateLoot()
        {
            return Core.Items[_rnd.Next(Core.Items.Count)];
        }

        // Добавь это в GameEngine.cs
        public List<EnemyTemplates> GetEnemyTemplates()
        {
            return new List<EnemyTemplates>
    {
        new EnemyTemplates { TypeName = "Гоблин", BaseHP = 30, BaseAttack = 10, BaseDefense = 2 },
        new EnemyTemplates { TypeName = "Скелет", BaseHP = 40, BaseAttack = 12, BaseDefense = 5 },
        new EnemyTemplates { TypeName = "Маг", BaseHP = 25, BaseAttack = 15, BaseDefense = 1 }
    };
        }

        // Теперь этот метод будет работать
        public EnemyTemplates GenerateEnemy(int floor)
        {
            Random rng = new Random();
            var templates = GetEnemyTemplates();
            return templates[rng.Next(templates.Count)];
        }
    }
}