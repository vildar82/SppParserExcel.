using Microsoft.Win32;
using SppParserExcel.Model;
using SppParserExcel.MVVM;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SppParserExcel.Lib;

namespace SppParserExcel.ViewModel
{
    public class MainVM : BaseViewModel
    {
        public MainVM(Window mainWindow)
        {
            MainWindow = mainWindow;
            SelectExcel = new Commamd(SelectExcelExec);
        }

        public Window MainWindow { get; set; }
        public string ExcelFile { get; set; }
        public ICommand SelectExcel { get; set; }

        public ObservableCollection<Error> Errors { get; set; }

        private void SelectExcelExec()
        {
            try
            {
                Errors = new ObservableCollection<Error>();
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog(MainWindow) == true)
                {
                    ExcelFile = dlg.FileName;
                    ParseExcel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ParseExcel()
        {
            var parser = new Parser(e => Errors.Add(e));
            parser.Parse(ExcelFile);
            var jsonFile = Path.ChangeExtension(ExcelFile, "json");
            parser.Data.Serialize(jsonFile);
            MessageBox.Show($"Готово - {jsonFile}");
        }
    }
}
