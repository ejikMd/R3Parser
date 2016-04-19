using System.Collections.Generic;
using System.Data;


namespace XMLCleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = "SoldHouses.xml";

            DataSet DataSet = new DataSet();
            DataView DataView;


            DataSet.ReadXml(Path, XmlReadMode.ReadSchema);
            if (DataSet.Tables.Count > 0)
            {
                DataView = DataSet.Tables[0].DefaultView;
            }


            DataSet.Clear();
            DataSet.ReadXml(Path, XmlReadMode.ReadSchema);

            var result = new List<RealEstateSold>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (DataRow row in DataSet.Tables[0].Rows)
            {
                result.Add((RealEstateSold)row[1]);
            }

        }
    }
}
