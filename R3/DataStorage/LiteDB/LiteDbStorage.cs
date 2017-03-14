using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using R3.DataStorage.Tables;
using R3.Models;

namespace R3.DataStorage.LiteDB
{
    public static class LiteDbStorage
    {
        private static readonly string Path = AppDomain.CurrentDomain.BaseDirectory + "\\SoldHouses.db";

        public static bool Insert(RealEstateSold realEstateSold)
        {
            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var col = db.GetCollection<RealEstateSold>("soldHouses");

                    if (!col.Exists(x => x.Id.Equals(realEstateSold.Id)))
                        col.Insert(realEstateSold);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
                
        public static List<RealEstateSold> SelectAll()
        {
            using (var db = new LiteDatabase(Path))
            {
                var col = db.GetCollection<RealEstateSold>("soldHouses");

                return col.FindAll().OrderByDescending(x=>x.DateTaken).ToList();
            }
        }

        public static int GetPriceChangeFromArchive(RealEstateViewModel realEstate)
        {
            using (var db = new LiteDatabase(Path))
            {
                var collection = db.GetCollection<RealEstateSold>("soldHouses");

                var result = collection.Find(x => x.AddressText.Equals(realEstate.AddressText)).OrderByDescending(x => x.DateTaken).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        var oldPrice = int.Parse(result.Price.Replace("$", "").Replace(",", ""));
                        var newPrice = int.Parse(realEstate.Price.Replace("$", "").Replace(",", ""));

                        return newPrice - oldPrice;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }

                return 0;
            }
        }
    }
}