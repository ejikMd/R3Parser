using System;
using System.Collections.Generic;
using System.Data;
using R3.DataStorage.Tables;

// ReSharper disable once CheckNamespace
namespace R3.DataStorage
{
    public class XMLStorage
    {
        readonly string Path = AppDomain.CurrentDomain.BaseDirectory + "\\SoldHouses.xml";

        readonly DataSet DataSet = new DataSet();
        readonly DataView DataView;

        public XMLStorage()
        {
            DataSet.ReadXml(Path, XmlReadMode.ReadSchema);
            if (DataSet.Tables.Count > 0)
            {
                DataView = DataSet.Tables[0].DefaultView;
            }
            else
            {
                CreateXmlFile();
                DataSet.ReadXml(Path, XmlReadMode.ReadSchema);
                DataView = DataSet.Tables[0].DefaultView;
            }
        }

        public void Save()
        {
            DataSet.WriteXml(Path, XmlWriteMode.WriteSchema);
        }

        public void Insert(RealEstateSold realEstateSold)
        {
            DataRow dr = DataView.Table.NewRow();
            dr["id"] = realEstateSold.Id;
            dr["realEstateSold"] = realEstateSold;

            DataSet.Tables[0].Rows.Add(dr);
            Save();
        }

        private void CreateXmlFile()
        {
            RealEstateSold realEstateSold = new RealEstateSold{Id = 0};

            DataSet ds = new DataSet();

            DataTable dt = new DataTable("RealEstateSold");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("realEstateSold", typeof(RealEstateSold)));

            DataRow dr = dt.NewRow();
            dr["id"] = realEstateSold.Id;
            dr["realEstateSold"] = realEstateSold;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            ds.WriteXml(Path, XmlWriteMode.WriteSchema);           
        }

        ///// <summary>
        ///// Updates a record in the Category table.
        ///// </summary>
        //public static void Update(string categoryID, string CategoryName)
        //{
        //    DataRow dr = Select(categoryID);
        //    dr[1] = CategoryName;
        //    save();
        //}

        ///// <summary>
        ///// Deletes a record from the Category table by a composite primary key.
        ///// </summary>
        //public static void Delete(string categoryID)
        //{
        //    dv.RowFilter = "categoryID='" + categoryID + "'";
        //    dv.Sort = "categoryID";
        //    dv.Delete(0);
        //    dv.RowFilter = "";
        //    save();
        //}

        //public static DataRow Select(string categoryID)
        //{
        //    _dataView.RowFilter = "Id='" + categoryID + "'";
        //    _dataView.Sort = "Id";
        //    DataRow dr = null;
        //    if (_dataView.Count > 0)
        //    {
        //        dr = _dataView[0].Row;
        //    }
        //    _dataView.RowFilter = "";
        //    return dr;
        //}

        public List<RealEstateSold> SelectAll()
        {
            DataSet.Clear();
            DataSet.ReadXml(Path, XmlReadMode.ReadSchema);

            var result = new List<RealEstateSold>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (DataRow row in DataSet.Tables[0].Rows)
            {
                result.Add((RealEstateSold)row[1]);
            }

            return result;
        }


    }
}