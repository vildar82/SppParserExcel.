using System.Xml.Serialization;

namespace SppData
{
    /// <summary>
    /// Квартира на этаж
    /// </summary>
    public class ApartmentInFloor
    {
        /// <summary>
        /// Квартира
        /// </summary>
        [XmlIgnore]
        public Apartment Apartment { get; set; }
        /// <summary>
        /// ID квартиры
        /// </summary>
        public int ApartmentID { get; set; }
        /// <summary>
        /// Количество квартир
        /// </summary>
        public int Count { get; set; }
    }
}