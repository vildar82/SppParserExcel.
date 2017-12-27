using System;
using System.Collections.Generic;

namespace SppData
{
    /// <summary>
    /// Данные по секциям
    /// </summary>
    public class SectionsData
    {
        public string ExcelFileName { get; set; }
        public DateTime ExcelFileLastWrite { get; set; }
        public DateTime ParseData { get; set; }

        public Dictionary<int, string> ApartmentTypes = new Dictionary<int, string>
        {
            { (int) ApartmentTypeEnum.Studio, "Студия"},
            { (int) ApartmentTypeEnum.One, "1-комнатная"},
            {  (int) ApartmentTypeEnum.Two,"2-комнатная"},
            {  (int) ApartmentTypeEnum.Three,"3-комнатная"},
            {  (int) ApartmentTypeEnum.Four,"4-комнатная"}
        };

        public Dictionary<int, string> ApartmentSizes = new Dictionary<int, string>
        {
            {  (int) ApartmentSizeEnum.S,"S"},
            {  (int) ApartmentSizeEnum.M,"M"},
            {  (int) ApartmentSizeEnum.L,"L"},
        };

        /// <summary>
        /// Квартиры
        /// </summary>
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        /// <summary>
        /// Секции
        /// </summary>
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
