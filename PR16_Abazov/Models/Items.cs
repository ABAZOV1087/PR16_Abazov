using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace PR16_Abazov.Models
{
    public class Items
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public double BonusDamage { get; set; }
        public double BonusAttack { get; set; } 
        public double BonusDefense { get; set; }
        public bool IsHeal { get; set; }
    }
}