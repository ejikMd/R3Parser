using System;
using System.Collections.Generic;
using R3.DataStorage;
using R3.Models;

namespace R3.Service
{
    public class RealEstateService
    {
        readonly MainStorageRepository mainStorageRepository = new MainStorageRepository();

        public void AddAnalysisData(ref List<RealEstateViewModel> results)
        {
            var historyRecords = mainStorageRepository.GetNumberOfHistoryRecords(results);

            foreach (var result in results)
            {
                result.PriceCoefficient = 190* Convert.ToInt32("0" + result.YearBuild) - Convert.ToInt32(result.Price.Replace("$", "").Replace(",",""));
                result.IsNew = historyRecords.ContainsKey(result.MlsNumber);
            }
        }

        public bool SetStatus(string mlsId, string status)
        {
            var result = mainStorageRepository.SetStatus(mlsId, status);

            return result;
        }
    }
}