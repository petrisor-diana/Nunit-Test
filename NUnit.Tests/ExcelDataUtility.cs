using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace NUnit.Tests1
{
    class ExcelDataUtility
    {
        public static DataTable ExcelToDataTable(string fileName, int number)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader ExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx

            //Return as DataSet
            DataSet result = ExcelDataReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            //Get all the Tables
            DataTableCollection table = result.Tables;

            //Store it in DataTable
            DataTable resultTable = table["Sheet" + number];
            stream.Close();
            stream.Dispose();

            if (ExcelDataReader != null)
            {
                ExcelDataReader.Close();
                ExcelDataReader.Dispose();
            }

            //return 
            return resultTable;
        }

        public static List<Datacollection> dataCol = new List<Datacollection>();

        public static void PopulateInCollection(string fileName, int number)
        {
            DataTable table = ExcelToDataTable(fileName, number); 

            //Iterate through the rows and columns of the Table
            for (int row = 0; row < table.Rows.Count; row++)
            {
                Datacollection dtTable = new Datacollection()
                {
                    rowNumber = row,
                    colName = table.Rows[row][0].ToString(),
                    colValue = table.Rows[row][1].ToString()
                };
                //Add all the details for each row
                dataCol.Add(dtTable);

                //for (int col = 0; col < table.Columns.Count; col++)
                //{
                //    Datacollection dtTable = new Datacollection()
                //    {
                //        rowNumber = row,
                //        colName = table.Columns[col].ColumnName,
                //        colValue = table.Rows[row][col].ToString()
                //    };
                //    //Add all the details for each row
                //    dataCol.Add(dtTable);
                //}
            }    
        }


        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }
    }
}
