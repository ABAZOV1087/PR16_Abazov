using PR16_Abazov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PR16_Abazov.Core
{
    public static class Core
    {
        public static List<EnemyTemplates> EnemyTemplates = new List<EnemyTemplates>();
        public static List<Items> Items = new List<Items>();
        public static Players CurrentPlayer; 

        public static void InitData()
        {
            EnemyTemplates.Add(new EnemyTemplates { TypeName = "Гоблин", BaseHP = 30, BaseAttack = 12, BaseDefense = 3, IsBoss = false });
            EnemyTemplates.Add(new EnemyTemplates { TypeName = "Скелет", BaseHP = 40, BaseAttack = 10, BaseDefense = 5, IsBoss = false });
            EnemyTemplates.Add(new EnemyTemplates { TypeName = "Маг", BaseHP = 25, BaseAttack = 15, BaseDefense = 2, IsBoss = false });
            EnemyTemplates.Add(new EnemyTemplates { TypeName = "Ковальский (Босс)", BaseHP = 100, BaseAttack = 13, BaseDefense = 7, IsBoss = true });

            Items.Add(new Items { Name = "Меч", Type = "Weapon", BonusDamage = 5 });
            Items.Add(new Items { Name = "Кольчуга", Type = "Armor", BonusDefense = 4 });
            Items.Add(new Items { Name = "Зелье", Type = "Potion", IsHeal = true });

            CurrentPlayer = new Players { Nickname = "Герой", MaxHP = 100, CurrentHP = 100, CurrentFloor = 1 };
        }
        public static void Save() { }
    }
}