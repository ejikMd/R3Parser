using System.Collections.Generic;
using System.Linq;
using R3.DataStorage.Tables;

namespace R3.DataStorage
{
    public class AccessLogsRepository
    {
        private readonly MainStorage db = new MainStorage();

        public List<AccessLog> GetAllAccessLogs()
        {
            return db.AccessLogs.ToList();
        }

    }
}