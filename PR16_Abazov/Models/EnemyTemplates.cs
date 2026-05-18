using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR16_Abazov.Models
{
    public class EnemyTemplates
    {
        public int EnemyID { get; set; }
        public string TypeName { get; set; }
        public double BaseHP { get; set; }
        public double BaseAttack { get; set; }
        public double BaseDefense { get; set; }
        public bool IsBoss { get; set; }
    }
}