using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility;
using System.Numerics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;



namespace Utility
{
    public class ExcelFile
    {

        private string excelFilePath = string.Empty;
        private int columnNumber = 1; // define first row number to enter data in excel

        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;

        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
        }

        public int ColumnNumber
        {
            get { return columnNumber; }
            set { columnNumber = value; }
        }

        public void OpenExcel()
        {
            myExcelApplication = null;
            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts
            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value)); // open the existing excel file
            int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)
            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)
            int numberOfSheets = myExcelWorkbook.Worksheets.Count; // get number of worksheets (optional)
        }

        public void AddDataToExcel(string[] dataColumn)
        {
            int i = 0;
            foreach(var item in dataColumn)
            {
                //var columnName = GetColumnName();
                myExcelWorkSheet.Cells[i++, "A"] = item;
            }
            ColumnNumber++;
        }

        public void CloseExcel()
        {
            try
            {
                myExcelWorkbook.SaveAs(excelFilePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value); // Save data in excel
                myExcelWorkbook.Close(true, excelFilePath, System.Reflection.Missing.Value); // close the worksheet
            }
            finally
            {
                if (myExcelApplication != null)
                {
                    myExcelApplication.Quit(); // close the excel application
                }
            }
        }
    }
}
