using System.Collections.Generic;

namespace SppData
{
    /// <summary>
    /// Данные по секции
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Шифр секции
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ориентация - Широтная/Меридиональная/Угловая
        /// </summary>
        public string Orientation { get; set; }
        /// <summary>
        /// S общая кв.
        /// </summary>
        public double AreaCommon { get; set; }
        /// <summary>
        /// S кв. 1 эт жилой
        /// </summary>
        public double Area1Live { get; set; }
        /// <summary>
        /// S кв. 1 эт БКФН
        /// </summary>
        public double Area1Bkfn { get; set; }
        /// <summary>
        /// Этажность
        /// </summary>
        public int Floors { get; set; }
        /// <summary>
        /// Квартиры типового этажа
        /// </summary> 
        public List<ApartmentInFloor> ApartmentsInFloor { get; set; } = new List<ApartmentInFloor>();
    }
}