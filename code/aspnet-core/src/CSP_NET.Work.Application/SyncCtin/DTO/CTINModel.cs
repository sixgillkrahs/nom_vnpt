using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin.DTO
{

    public class PagingModel
    {

        public int? totalPage { get; set; }
        public long? count { get; set; }
        public string order { get; set; }
        public int page { get; set; }
        public int size { get; set; }

    }
    public class ResultModel<T>
    {
        public SerializableError error { get; set; }

        public T data { get; set; }

        public PagingModel paging { get; set; }
    }

    public class DataDbJson
    {
        public Guid _id { get; set; }  // must be public!;
        public virtual short status { get; set; } = (byte)Status_db.Nomal;
        public virtual DateTime createdDate { get; set; } = DateTime.Now;
        public virtual string createdBy { get; set; }
        public virtual DateTime? modifiedDate { get; set; }
        public virtual string modifiedBy { get; set; }
        public virtual DateTime? deletedDate { get; set; }
        public virtual string deletedBy { get; set; }

        public virtual int officeId { get; set; }
    }
    public enum Status_db
    {
        [Display(Name = "status_db.notactive")]
        NotActive,
        [Display(Name = "status_db.nomal")]
        Nomal,
        [Display(Name = "status_db.hiden")]
        Hide,
        [Display(Name = "status_db.delete")]
        Delete,
        [Display(Name = "status_db.deleted")]
        Deleted,
    }


    public class TenantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
