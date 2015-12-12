using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class Ammunition : IHaveId
    {
        public string Id { get; private set; }

        public DragFunction DragFunction { get; set; }

        //175 gr .308 Match-King HPBT BC -> 0.505
        //note : BC factors in weight
        public double BC { get; set; }

        public string Name { get; set; }
        
        public Speed MuzzleVelocity { get; set; }    

        public Length Caliber { get; set; }

        public Length Length { get; set; } //1.240 inches -> Sierra MatchKing HPBT 175gr

        public double WeightGrains { get; set; }
    }
}
