using System;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace SppData
{
    /// <summary>
    /// Квартира
    /// </summary>
    public class Apartment : IEquatable<Apartment>
    {
        public int ID { get; set; }
        /// <summary>
        /// Тип квартиры
        /// </summary>
        public ApartmentTypeEnum Type { get; set; }
        /// <summary>
        /// размер квартиры - S,M,L
        /// </summary>
        public ApartmentSizeEnum Size { get; set; }

        public bool Equals(Apartment other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Size == other.Size;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Apartment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Type * 397) ^ (int)Size;
            }
        }
    }
}