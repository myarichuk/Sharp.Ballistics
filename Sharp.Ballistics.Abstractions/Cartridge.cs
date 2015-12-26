using System;
using UnitsNet;

namespace Sharp.Ballistics.Abstractions
{
    public class Cartridge : IHaveId, IEquatable<Cartridge>
    {
        public string Id { get; private set; }

        public DragFunction DragFunction { get; set; } = DragFunction.G1;

        //175 gr .308 Match-King HPBT BC -> 0.505
        //note : BC factors in weight
        public double BC { get; set; }

        public string Name { get; set; }
        
        public Speed MuzzleVelocity { get; set; }    

        public Length Caliber { get; set; }

        public Length Length { get; set; } //1.240 inches -> Sierra MatchKing HPBT 175gr

        public double WeightGrains { get; set; }

        public bool Equals(Cartridge other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DragFunction == other.DragFunction && 
                BC.Equals(other.BC) && 
                string.Equals(Name, other.Name, 
                    StringComparison.InvariantCultureIgnoreCase) && 
                MuzzleVelocity.Equals(other.MuzzleVelocity) && 
                Caliber.Equals(other.Caliber) && 
                Length.Equals(other.Length) && 
                WeightGrains.Equals(other.WeightGrains);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrWhiteSpace(Id))
                return base.GetHashCode();

            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cartridge) obj);
        }       

        public static bool operator ==(Cartridge left, Cartridge right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Cartridge left, Cartridge right)
        {
            return !Equals(left, right);
        }
    }
}
