using System;
using System.Collections.Generic;

namespace SppParserExcel.Model.Data
{
    /// <summary>
    /// Данные для записи в JSON - секций
    /// </summary>
    public class SectionsData
    {
        public string ExcelFileName { get; set; }
        public DateTime ExcelFileLastWrite { get; set; }
        public DateTime ParseData { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
