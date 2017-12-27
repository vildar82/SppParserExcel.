using System.Collections.Generic;

namespace SppParserExcel.Model.Data
{
    public class Section
    {
        public string Mark { get; set; }
        public string Orientation { get; set; }
        public int Floors { get; set; }
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
        public List<ApartmentInFloor> Apartments { get; set; }=new List<ApartmentInFloor>();
    }
}