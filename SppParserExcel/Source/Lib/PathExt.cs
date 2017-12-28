using System.IO;

namespace SppParserExcel.Lib
{
    public static class PathExt
    {
        public static void TryDeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                //
            }
        }
    }
}
