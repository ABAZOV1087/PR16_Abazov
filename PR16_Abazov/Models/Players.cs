namespace PR16_Abazov.Models
{
    public class Players
    {
        public string Nickname { get; set; }
        public double CurrentHP { get; set; }
        public double BaseAttack { get; set; } 
        public double MaxHP { get; set; }
        public int CurrentFloor { get; set; }


        public Items CurrentWeapon { get; set; }
        public Items CurrentArmor { get; set; }
    }
}