using System;
using PR16_Abazov.Models;

namespace PR16_Abazov.Core
{
    public static class CombatSystem
    {
        public static string EnemyAttack(EnemyTemplates enemy, Players player, bool isGuarding)
        {
            Random rng = new Random();

            if (isGuarding && rng.NextDouble() < 0.40)
            {
                return $"{enemy.TypeName} атаковал, но вы полностью УКЛОНИЛИСЬ!";
            }

            double damage = enemy.BaseAttack;
            string effectMessage = "";
            string type = enemy.TypeName.ToLower();

            if (type.Contains("гоблин") && rng.NextDouble() < 0.20)
            {
                damage *= 2;
                effectMessage = " КРИТИЧЕСКИЙ УДАР!";
            }

            double defenseValue = player.CurrentArmor?.BonusDefense ?? 0;

            if (type.Contains("скелет")) 
            {
                defenseValue = 0;
                effectMessage += " (Игнор брони!)";
            }
            else if (isGuarding) 
            {
                double blockMultiplier = rng.Next(70, 101) / 100.0;
                defenseValue *= blockMultiplier;
                effectMessage += $" (Блок: -{Math.Round(defenseValue, 1)} урона)";
            }

            double finalDamage = damage - defenseValue;
            if (finalDamage < 0) finalDamage = 0;
            player.CurrentHP -= finalDamage;

            if (type.Contains("маг") && rng.NextDouble() < 0.15)
            {
                effectMessage += " ВЫ ЗАМОРОЖЕНЫ!";
            }

            return $"{enemy.TypeName} нанес {Math.Round(finalDamage, 1)} урона.{effectMessage}";
        }

        public static string PlayerAttack(Players player, EnemyTemplates target)
        {
            double weaponBonus = player.CurrentWeapon != null
                ? (player.CurrentWeapon.BonusAttack + player.CurrentWeapon.BonusDamage)
                : 0;

            double totalDamage = player.BaseAttack + weaponBonus;
            double finalDamage = totalDamage - target.BaseDefense;

            if (finalDamage < 1) finalDamage = 1;

            target.BaseHP -= finalDamage;
            return $"{player.Nickname} ударил {target.TypeName} на {Math.Round(finalDamage, 1)}";
        }
    }
} 