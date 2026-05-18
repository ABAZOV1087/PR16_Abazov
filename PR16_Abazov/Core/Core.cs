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
            EnemyTemplates.Add(new EnemyTemplates {TypeName = "ВВГ",BaseHP = 60,BaseAttack = 18,BaseDefense = 4,IsBoss = true,CritChance = 0.1});
            EnemyTemplates.Add(new EnemyTemplates {TypeName = "Архимаг С++",BaseHP = 54,BaseAttack = 24,BaseDefense = 2,IsBoss = true,FreezeChance = 0.1});
            EnemyTemplates.Add(new EnemyTemplates {TypeName = "Пестов С--",BaseHP = 39,BaseAttack = 27,BaseDefense = 1,IsBoss = true, FreezeChance = 0.15});


            Items.Add(new Items { Name = "Меч", Type = "Weapon", BonusDamage = 5 });
            Items.Add(new Items { Name = "Золотой Меч", Type = "Weapon", BonusDamage = 7 });
            Items.Add(new Items { Name = "Алмазный Меч", Type = "Weapon", BonusDamage = 9 });
            Items.Add(new Items { Name = "Кольчуга", Type = "Armor", BonusDefense = 4 });
            Items.Add(new Items { Name = "Золотая Броня", Type = "Armor", BonusDefense = 6 });
            Items.Add(new Items { Name = "Алмазная Броня", Type = "Armor", BonusDefense = 8 });
            Items.Add(new Items { Name = "Зелье", Type = "Potion", IsHeal = true });

            CurrentPlayer = new Players { Nickname = "Герой", MaxHP = 200, CurrentHP = 200, BaseAttack = 12, CurrentFloor = 1 };
        }
        public static void Save() { }
    }
}  