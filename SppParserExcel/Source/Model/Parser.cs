using JetBrains.Annotations;
using OfficeOpenXml;
using SppParserExcel.Model.Data;
using SppParserExcel.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using SppParserExcel.Lib;

namespace SppParserExcel.Model
{
    public class Parser
    {
        private readonly Action<Error> addError;
        private int rOrient;
        private const int cProp = 2;
        private int rMark;
        private int rStudio;
        private int r1R;
        private int r2R;
        private int r3R;
        private int r4R;
        private int rFloor;
        private int rAreaCommon;
        private int rArea1Live;
        private int rArea1Bkfn;
        private ExcelWorksheet ws;
        private const string studioName = "Студии";
        private const string apart1Name = "1 комн";
        private const string apart2Name = "2 комн";
        private const string apart3Name = "3 комн";
        private const string apart4Name = "4 комн";
        private readonly List<Apartment> apartmentsUnique = new List<Apartment>();

        public Parser(Action<Error> addError)
        {
            this.addError = addError;
        }

        public SectionsData Data { get; set; }

        public void Parse([NotNull] string excelFile)
        {
            var copyFile = Path.GetTempFileName();
            try
            {
                File.Copy(excelFile, copyFile, true);
                using (var excel = new ExcelPackage(new FileInfo(copyFile)))
                {
                    ws = excel.Workbook.Worksheets["Квартирография"] ??
                         throw new Exception("Не найден лист Квартирография");
                    Data = new SectionsData
                    {
                        ExcelFileName = excelFile,
                        ExcelFileLastWrite = File.GetLastWriteTime(excelFile),
                        ParseData = DateTime.Now,
                        Sections = GetSections()
                    };
                }
            }
            finally
            {
                PathExt.TryDeleteFile(copyFile);
            }
        }

        [NotNull]
        private List<Section> GetSections()
        {
            var sections = new List<Section>();
            rOrient = 3;
            if (GetCellText(rOrient, cProp) != "Ориентация") throw new Exception($"Ячейка [{rOrient},{cProp}] != Ориентация");
            rMark = FindRow(rOrient + 1, cProp, "Марка");
            
            rStudio = FindRow(rMark + 1, cProp, studioName);
            r1R = FindRow(rStudio + 1, cProp, apart1Name);
            r2R = FindRow(r1R + 1, cProp, apart2Name);
            r3R = FindRow(r2R + 1, cProp, apart3Name);
            r4R = FindRow(r3R + 1, cProp, apart4Name);
            rFloor = FindRow(r4R + 1, cProp, "Этажность");
            rAreaCommon = FindRow(rFloor + 1, cProp, "S общая кв.");
            rArea1Live = FindRow(rAreaCommon + 1, cProp, "S кв. 1 эт жилой");
            rArea1Bkfn = FindRow(rArea1Live + 1, cProp, "S кв. 1 эт БКФН");

            var cSec = cProp +2;
            while (true)
            {
                var mark = GetCellText(rMark, cSec);
                if(string.IsNullOrEmpty(mark)) break;
                var section = new Section
                {
                    Mark = mark,
                    Orientation = GetCellText(rOrient, cSec),
                    AreaCommon = GetCellArea(rAreaCommon, cSec),
                    Area1Live = GetCellArea(rArea1Live, cSec),
                    Area1Bkfn = GetCellArea(rArea1Bkfn, cSec),
                    Apartments = GetApartmentsInFloor(cSec, out var floors),
                    Floors = floors
                };
                sections.Add(section);
                cSec++;
            }
            return sections;
        }

        [NotNull]
        private List<ApartmentInFloor> GetApartmentsInFloor(int col, out int floors)
        {
            floors = GetCellInt(rFloor, col);
            var aparts = GetApartsType(rStudio, col, floors);
            aparts.AddRange(GetApartsType(r1R, col, floors));
            aparts.AddRange(GetApartsType(r2R, col, floors));
            aparts.AddRange(GetApartsType(r3R, col, floors));
            aparts.AddRange(GetApartsType(r4R, col, floors));
            return aparts;
        }

        [NotNull]
        private List<ApartmentInFloor> GetApartsType(int rtype, int col, int floors)
        {
            var apartsInFloor = new List<ApartmentInFloor>();
            var apart = GetApartInFloor(rtype + 1, col, floors, studioName, "S");
            if (apart != null) apartsInFloor.Add(apart);

            apart = GetApartInFloor(rtype + 2, col, floors, studioName, "M");
            if (apart != null) apartsInFloor.Add(apart);

            apart = GetApartInFloor(rtype + 3, col, floors, studioName, "L");
            if (apart != null) apartsInFloor.Add(apart);

            return apartsInFloor;
        }

        [CanBeNull]
        private ApartmentInFloor GetApartInFloor(int r, int col, int floors, string apartType, string size)
        {
            var count = GetCellInt(r, col);
            if (count == 0) return null;
            var apart = GetApartment(apartType, size);
            return new ApartmentInFloor
            {
                Apartment = apart,
                Count = count / floors
            };
        }

        [NotNull]
        private Apartment GetApartment(string apartType, string size)
        {
            var apartNew = new Apartment {NameByRooms = apartType, Size = size};
            var apart = apartmentsUnique.Find(a=>a.Equals(apartNew));
            if (apart == null)
            {
                apart = apartNew;
                apartmentsUnique.Add(apart);
            }
            return apart;
        }

        private double GetCellArea(int r, int c)
        {
            var value = ws.Cells[r, c].Value;
            return Convert.ToDouble(value);
        }

        private int GetCellInt(int r, int c)
        {
            var value = ws.Cells[r, c].Value;
            return Convert.ToInt32(value);
        }

        [NotNull]
        private string GetCellText(int r, int c)
        {
            return ws.Cells[r, c].Text.Trim();
        }

        private int FindRow(int row, int col, [NotNull] string reqValue, int maxIterate = 10)
        {
            for (var i = 0; i < maxIterate; i++)
            {
                if (GetCellText(row + i, col) == reqValue) return row + i;
            }
            throw new Exception($"Не найдена ячейка {reqValue}");
        }
    }
}
