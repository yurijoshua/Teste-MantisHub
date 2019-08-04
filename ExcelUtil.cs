using ExcelDataReader;
using System.Data;
using System.Collections.Generic;
using System;
using System.IO;
using NUnit.Framework;
using System.Linq;

namespace TesteBase2
{
    public class ExcelUtil
    {
        public DataTable ExcelToDatable(string fileName)
        {
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result1 = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            DataSet result = excelReader.AsDataSet();
            DataTableCollection table = result.Tables;
            DataTable resultTable = table["shets"];
            return resultTable;

        }

        List<DataCollection> dataCol = new List<DataCollection>();
        public void PopulateInCollection(string fileName)
        {
            DataTable table = ExcelToDatable(fileName);
            int c = table.Rows.Count;
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    dataCol.Add(dtTable);  
                }
            }           
        }

       public string ReadData(int rowNumber, string columnName)
            {
                try
                {
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                return data.ToString();
                }
                catch (Exception e)
                {
                return null;
                }
            }

        internal class DataCollection
        {
            public int rowNumber { get; internal set; }
            public string colName { get; internal set; }
            public string colValue { get; internal set; }
        }
    }
}
