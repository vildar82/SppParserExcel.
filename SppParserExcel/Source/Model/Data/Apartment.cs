namespace SppParserExcel.Model.Data
{
    [Equals]
    public class Apartment
    {
        /// <summary>
        /// Название по числу комнат - Студии, 1 комн...
        /// </summary>
        public string NameByRooms { get; set; }
        /// <summary>
        /// Размер - S/M/L
        /// </summary>
        public string Size { get; set; }
    }
}