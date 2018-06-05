using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml
{
    class XMLtoEXCEL
    {
        private string path;
        public XMLtoEXCEL(string a)
        {
            this.path = a;
            Convert(path);
        }
        public void Convert(string path)
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            Dt.Load(ds.CreateDataReader());
            if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                string XlFile = fi.DirectoryName + "\\" + fi.Name.Replace(fi.Extension, ".xlsx");
                System.Data.DataTable dt = Dt;
                ExportDataTableToExcel(dt, XlFile);
                Console.WriteLine("Экспортировано!");
            }
        }
        public static void ExportDataTableToExcel(System.Data.DataTable table, string Xlfile)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook book = excel.Application.Workbooks.Add(Type.Missing);
            excel.Visible = false;
            excel.DisplayAlerts = false;
            Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)book.ActiveSheet;
            excelWorkSheet.Name = table.TableName;
            //Заголовки
            for (int i = 1; i < table.Columns.Count + 1; i++) 
            {
                excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            //Содержимое
            for (int j = 0; j < table.Rows.Count; j++)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                }
            }
            book.SaveAs(Xlfile);
            book.Close(true);
            excel.Quit();
        }
    }
}
