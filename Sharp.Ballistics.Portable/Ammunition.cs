namespace Sharp.Ballistics
{
    public class Ammunition
    {
        public string Id { get; private set; }

        public DragFunction DragFunction { get; set; }

        //175 gr .308 Match-King HPBT BC -> 0.505
        //note : BC factors in weight
        public double BC { get; set; }

        public string Name { get; set; }
        
        public double MuzzleVelocity { get; set; }    

        public double Caliber { get; set; }

        public double Length { get; set; } //1.240 inches -> Sierra MatchKing HPBT 175gr

        public double WeightGrains { get; set; }
    }
}
