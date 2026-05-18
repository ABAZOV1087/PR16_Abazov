using PR16_Abazov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR16_Abazov.Core
{
    public static class CombatSystem
    {
        public static string PlayerAttack(Players player, EnemyTemplates enemy)
        {
            double damage = 10;
            if (player.CurrentWeapon != null) damage += player.CurrentWeapon.BonusDamage;

            enemy.BaseHP -= damage;
            if (enemy.BaseHP < 0) enemy.BaseHP = 0;
            return $"Вы нанесли {damage} урона!";
        }

        public static string EnemyAttack(EnemyTemplates enemy, Players player, bool isGuarding)
        {
            double damage = enemy.BaseAttack;
            double defense = 0;

            if (player.CurrentArmor != null)
                defense = player.CurrentArmor.BonusDefense;

            if (isGuarding) damage -= (defense * 2);
            else damage -= defense;

            if (damage < 0) damage = 0;
            player.CurrentHP -= damage;
            return $"{enemy.TypeName} нанес вам {damage} урона!";
        }
    }
}