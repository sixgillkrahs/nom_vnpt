using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin.DTO
{
    public class CTINUser
    {
        public long employeeId { get; set; }

        public string employeeCode { get; set; }

        public string userName { get; set; }

        //public string[] roles { get; set; }
    }

    public class ResultModel2<T>
    {
        public SerializableError error { get; set; }

        public T data { get; set; }

        public PagingModel2 page { get; set; }
    }
    public class PagingModel2
    {

        public int? total { get; set; }

        public int number { get; set; }

        public int size { get; set; }

    }
}
