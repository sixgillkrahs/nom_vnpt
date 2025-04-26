using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin.DTO
{
    public class CTINEmployee
    {
        public EmployeeBasicInfo basic { get; set; }

        public EmployeeContact contact { get; set; }

        public EmployeeJob curentJob { get; set; }

        public long id { get; set; }

        public string? userName { get; set; }
    }

    public class EmployeeBasicInfo
    {
        public string code { get; set; }

        public string fullName { get; set; }
    }

    public class EmployeeContact
    {
        public string phone { get; set; }

        public string email { get; set; }
    }

    public class EmployeeJob
    {
        public Data company { get; set; }

        public Data department { get; set; }

        public Data type { get; set; }

        public Data position { get; set; }

        public Data status { get; set; }

        public Data title { get; set; }
    }

    public class Data
    {
        public int value { get; set; }

        public string text { get; set; }
    }
}
